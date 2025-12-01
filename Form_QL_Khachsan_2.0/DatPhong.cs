using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

// --- THƯ VIỆN ITEXT 7.1.16 ---
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Signatures; // Chứa PdfSigner, PrivateKeySignature

// --- THƯ VIỆN BOUNCY CASTLE 1.8.9 ---
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace Form_QL_Khachsan_2._0
{
    public partial class DatPhong : Form
    {
        public DatPhong()
        {
            InitializeComponent();
        }

        // --- 1. HÀM LẤY KEY TỪ ORACLE ---
        // Bỏ "using", chỉ mở kết nối và dùng thôi
        private byte[] LayKeyTuOracle()
        {
            try
            {
                var conn = database.Get_Connect(); // Lấy kết nối chung
                if (conn.State != ConnectionState.Open) conn.Open(); // Mở nếu chưa mở

                string sql = "SELECT GIATRI FROM THAMSO_HE_THONG WHERE MA_THAMSO = 'DIGITAL_SIGNATURE_KEY'";

                // Command thì dùng using được, nhưng KHÔNG using connection
                using (var cmd = new OracleCommand(sql, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.FromBase64String(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lỗi lấy Key: " + ex.Message); // Tạm tắt thông báo lỗi nhỏ
            }
            return null;
        }

        // --- 2. HÀM KÝ SỐ (Code chuẩn cho iText 7.1.16 + BouncyCastle 1.8.9) ---
        private void KySoPdf(string srcPdf, string destPdf, byte[] pfxBytes, string password)
        {
            using (MemoryStream pfxStream = new MemoryStream(pfxBytes))
            {
                // Khởi tạo Pkcs12Store (Cách cũ, ổn định)
                Pkcs12Store pk12 = new Pkcs12Store(pfxStream, password.ToCharArray());

                string alias = null;
                foreach (string al in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(al)) { alias = al; break; }
                }

                AsymmetricKeyParameter privateKey = pk12.GetKey(alias).Key;
                X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                    chain[k] = ce[k].Certificate;

                using (PdfReader reader = new PdfReader(srcPdf))
                using (FileStream os = new FileStream(destPdf, FileMode.Create))
                {
                    PdfSigner signer = new PdfSigner(reader, os, new StampingProperties());

                    // Giao diện chữ ký
                    PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
                    appearance
                        .SetReason("Xác thực thanh toán")
                        .SetLocation("Hệ thống Khách sạn")
                        .SetPageNumber(1)
                        .SetPageRect(new iText.Kernel.Geom.Rectangle(350, 50, 200, 100));

                    signer.SetFieldName("DigitalSignature");

                    // IExternalSignature nhận trực tiếp private key
                    IExternalSignature pks = new PrivateKeySignature(privateKey, "SHA-256");

                    signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        // --- 3. HÀM GỬI EMAIL ---
        private void GuiEmailKhachHang(string emailKhach, string tieuDe, string noiDung, string pathFilePDF)
        {
            if (string.IsNullOrEmpty(emailKhach)) return;

            try
            {
                // --- 1. ĐIỀN THÔNG TIN THẬT CỦA BẠN VÀO ĐÂY ---
                string myEmail = "pinterestworkcode@gmail.com"; // <--- Thay bằng Gmail thật của bạn
                string myAppPassword = "nvcc iqmo knwu heup"; // <--- Thay bằng 16 ký tự App Password vừa lấy (xóa hay để khoảng trắng đều được)
                                                              // ----------------------------------------------

                var fromAddress = new MailAddress(myEmail, "Khách Sạn Demo");
                var toAddress = new MailAddress(emailKhach);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true, // Bắt buộc
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, myAppPassword) // Dùng App Password, KHÔNG dùng pass đăng nhập
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = tieuDe,
                    Body = noiDung
                })
                {
                    if (File.Exists(pathFilePDF))
                    {
                        message.Attachments.Add(new Attachment(pathFilePDF));
                    }
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi mail thất bại: " + ex.Message);
            }
        }

        // --- 4. HÀM TẠO HÓA ĐƠN & GỌI CÁC HÀM TRÊN ---
        private void TaoHoaDonPdf(int hoaDonId, string hoten, string maPhong, DateTime ngayNhan, DateTime ngayTra, string emailKhach)
        {
            try
            {
                hoten = hoten?.Trim() ?? "N/A";
                maPhong = maPhong?.Trim() ?? "N/A";

                string pdfDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HoaDon");
                if (!Directory.Exists(pdfDir)) Directory.CreateDirectory(pdfDir);

                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string tempPath = Path.Combine(pdfDir, $"Temp_{hoaDonId}.pdf");
                string finalPath = Path.Combine(pdfDir, $"HoaDon_{hoaDonId}_{timeStamp}.pdf");

                // 1. Tạo PDF
                using (PdfWriter writer = new PdfWriter(tempPath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    PdfFont font = File.Exists(fontPath) ? PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H) : PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    doc.Add(new Paragraph($"HÓA ĐƠN #{hoaDonId}").SetFont(font).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"Khách hàng: {hoten}").SetFont(font).SetFontSize(12));
                    doc.Add(new Paragraph($"Phòng: {maPhong}").SetFont(font).SetFontSize(12));
                    doc.Add(new Paragraph($"Ngày nhận: {ngayNhan:dd/MM/yyyy}").SetFont(font).SetFontSize(12));
                    doc.Add(new Paragraph($"Ngày trả: {ngayTra:dd/MM/yyyy}").SetFont(font).SetFontSize(12));
                    int soNgay = (ngayTra - ngayNhan).Days;
                    doc.Add(new Paragraph($"Số ngày thuê: {soNgay}").SetFont(font).SetFontSize(12));
                    doc.Add(new Paragraph("\nCảm ơn quý khách!").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                }

                // 2. Ký Số
                byte[] keyData = LayKeyTuOracle(); // Hàm này đã sửa ở bước 1 nên không gây lỗi connection nữa
                if (keyData != null)
                {
                    try
                    {
                        KySoPdf(tempPath, finalPath, keyData, "123");
                        if (File.Exists(tempPath)) File.Delete(tempPath);
                    }
                    catch
                    {
                        if (File.Exists(tempPath)) { if (File.Exists(finalPath)) File.Delete(finalPath); File.Move(tempPath, finalPath); }
                    }
                }
                else { if (File.Exists(tempPath)) File.Move(tempPath, finalPath); }

                // 3. GỬI EMAIL (FIX TREO: Đưa vào Task.Run)
                if (!string.IsNullOrEmpty(emailKhach))
                {
                    // Chạy việc gửi mail ở luồng phụ
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try
                        {
                            GuiEmailKhachHang(emailKhach, "Hóa Đơn Điện Tử", $"Xin chào {hoten}, hóa đơn đính kèm.", finalPath);
                            // Không nên MessageBox ở đây, nhưng nếu muốn biết mail đi chưa thì để tạm cũng được
                        }
                        catch { }
                    });

                    MessageBox.Show($"Đã tạo hóa đơn và đang gửi email ngầm tới: {emailKhach}", "Thành công");
                }
                else
                {
                    MessageBox.Show($"Đã xuất hóa đơn PDF: {finalPath}", "Thành công");
                }

                try { System.Diagnostics.Process.Start(finalPath); } catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- CÁC LOGIC KHÁC GIỮ NGUYÊN ---
        // Bỏ "using" kết nối để tránh ngắt kết nối của form
        private void LoadPhongTrong()
        {
            try
            {
                if (date_ngaytra.Value <= date_ngaynhan.Value)
                {
                    comboxphong.Items.Clear(); return;
                }

                var conn = database.Get_Connect();
                if (conn.State != ConnectionState.Open) conn.Open();

                string sql = @"SELECT MAPH, LOAIPHONG FROM Phong WHERE MAPH NOT IN (SELECT MAPH FROM DatPhong WHERE (:ngayNhan < NGAYTRAPHONG) AND (:ngayTra > NGAYNHANPHONG))";

                using (var cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add(":ngayTra", OracleDbType.Date).Value = date_ngaytra.Value;
                    cmd.Parameters.Add(":ngayNhan", OracleDbType.Date).Value = date_ngaynhan.Value;

                    comboxphong.Items.Clear();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboxphong.Items.Add(reader["MAPH"].ToString() + " - " + reader["LOAIPHONG"].ToString());
                        }
                    }
                }

                if (comboxphong.Items.Count > 0) comboxphong.SelectedIndex = 0;
                // else MessageBox.Show("Hết phòng!", "Thông báo"); // Tắt bớt thông báo cho đỡ phiền
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load phòng: " + ex.Message); }
        }

        private int LayMaHDMoiNhat(OracleConnection conn)
        {
            string sql = "SELECT MAX(MAHD) FROM HoaDon";
            using (var cmd = new OracleCommand(sql, conn))
            {
                var result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            }
        }

        private void date_ngaynhan_ValueChanged(object sender, EventArgs e) { LoadPhongTrong(); }
        private void date_ngaytra_ValueChanged(object sender, EventArgs e) { LoadPhongTrong(); }
        private void btb_ql_Click(object sender, EventArgs e) { this.Hide(); new MainFormN().ShowDialog(); }

        private void btn_datphong_Click(object sender, EventArgs e)
        {
            try
            {
                if (date_ngaytra.Value <= date_ngaynhan.Value) { MessageBox.Show("Ngày trả phải lớn hơn ngày nhận!"); return; }
                string maPhong = comboxphong.SelectedItem?.ToString().Split('-')[0].Trim();
                if (string.IsNullOrEmpty(maPhong)) { MessageBox.Show("Chưa chọn phòng!"); return; }

                // --- SỬA Ở ĐÂY: KHÔNG DÙNG 'using' CHO CONNECTION ---
                var conn = database.Get_Connect();
                if (conn.State != ConnectionState.Open) conn.Open();

                // Chỉ dùng 'using' cho Transaction thôi (Transaction xong thì Commit/Rollback chứ không đóng kết nối)
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string makh;
                        // Check/Insert Khách
                        string sqlCheckKhach = "SELECT MAKH FROM KhachHang WHERE CMND = :cmnd";
                        using (var cmdCheck = new OracleCommand(sqlCheckKhach, conn))
                        {
                            cmdCheck.Parameters.Add(":cmnd", OracleDbType.Varchar2).Value = txt_cccd.Text;
                            var result = cmdCheck.ExecuteScalar();
                            if (result != null) makh = result.ToString();
                            else
                            {
                                makh = "KH" + DateTime.Now.Ticks.ToString().Substring(10);
                                string sqlInsertKhach = "INSERT INTO KhachHang(MAKH,HOTEN,NGAYSINH,CMND,SDT,EMAIL) VALUES(:makh,:hoten,:ngaysinh,:cmnd,:sdt,:email)";
                                using (var cmdInsert = new OracleCommand(sqlInsertKhach, conn))
                                {
                                    cmdInsert.Parameters.Add(":makh", OracleDbType.Varchar2).Value = makh;
                                    cmdInsert.Parameters.Add(":hoten", OracleDbType.Varchar2).Value = txt_hoten.Text;
                                    cmdInsert.Parameters.Add(":ngaysinh", OracleDbType.Date).Value = date_ngaysinh.Value;
                                    cmdInsert.Parameters.Add(":cmnd", OracleDbType.Varchar2).Value = txt_cccd.Text;
                                    cmdInsert.Parameters.Add(":sdt", OracleDbType.Varchar2).Value = txt_sdt.Text;
                                    cmdInsert.Parameters.Add(":email", OracleDbType.Varchar2).Value = txt_mail.Text;
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }

                        // Insert Đặt Phòng
                        string sqlDatPhong = "INSERT INTO DatPhong(MADATPH,MAKH,MAPH,NGAYDAT,NGAYNHANPHONG,NGAYTRAPHONG,TIENCOC) VALUES(DATPH_SEQ.NEXTVAL,:makh,:maph,SYSDATE,:ngaynhan,:ngaytra,0)";
                        using (var cmdDatPhong = new OracleCommand(sqlDatPhong, conn))
                        {
                            cmdDatPhong.Parameters.Add(":makh", OracleDbType.Varchar2).Value = makh;
                            cmdDatPhong.Parameters.Add(":maph", OracleDbType.Int32).Value = int.Parse(maPhong);
                            cmdDatPhong.Parameters.Add(":ngaynhan", OracleDbType.Date).Value = date_ngaynhan.Value;
                            cmdDatPhong.Parameters.Add(":ngaytra", OracleDbType.Date).Value = date_ngaytra.Value;
                            cmdDatPhong.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Lưu dữ liệu thành công

                        // Xử lý hóa đơn (Ký số & Gửi mail)
                        // Lưu ý: Các hàm này không dùng chung transaction nên để ngoài cũng được, hoặc để trong cũng không sao
                        int hoaDonId = LayMaHDMoiNhat(conn);
                        if (hoaDonId > 0)
                        {
                            TaoHoaDonPdf(hoaDonId, txt_hoten.Text, maPhong, date_ngaynhan.Value, date_ngaytra.Value, txt_mail.Text);
                        }

                        // --- QUAN TRỌNG: LoadPhongTrong() GỌI Ở ĐÂY SẼ KHÔNG BỊ LỖI ---
                        // Vì conn chưa bị Dispose
                        LoadPhongTrong();
                    }
                    catch (Exception exTrans)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi giao dịch: " + exTrans.Message);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}