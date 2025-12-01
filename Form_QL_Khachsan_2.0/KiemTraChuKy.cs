//using System;
//using System.IO;
//using System.Windows.Forms;
//using System.Data;
//using System.Collections.Generic;
//using System.Linq;

//// --- ORACLE ---
//using Oracle.ManagedDataAccess.Client;

//// --- ITEXT 7 ---
//using iText.Kernel.Pdf;
//using iText.Signatures;

//// --- BOUNCY CASTLE ---
//using Org.BouncyCastle.Pkcs;
//using Org.BouncyCastle.X509;

//namespace Form_QL_Khachsan_2._0
//{
//    public partial class KiemTraChuKy : Form
//    {
//        public KiemTraChuKy()
//        {
//            InitializeComponent();
//        }
//        private byte[] LayKeyTuOracle()
//        {
//            try
//            {
//                using (var conn = database.Get_Connect())
//                {
//                    if (conn.State != ConnectionState.Open) conn.Open();
//                    string sql = "SELECT GIATRI FROM THAMSO_HE_THONG WHERE MA_THAMSO = 'DIGITAL_SIGNATURE_KEY'";
//                    using (var cmd = new OracleCommand(sql, conn))
//                    {
//                        var result = cmd.ExecuteScalar();
//                        if (result != null && result != DBNull.Value)
//                        {
//                            return Convert.FromBase64String(result.ToString());
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Không lấy được khóa từ DB: " + ex.Message);
//            }
//            return null;
//        }
//        private Org.BouncyCastle.X509.X509Certificate LayChungChiGoc()
//        {
//            byte[] pfxBytes = LayKeyTuOracle();
//            if (pfxBytes == null) return null;

//            using (MemoryStream pfxStream = new MemoryStream(pfxBytes))
//            {
//                // Password bạn đang set cứng là "123"
//                Pkcs12Store pk12 = new Pkcs12Store(pfxStream, "123".ToCharArray());

//                string alias = null;
//                foreach (string al in pk12.Aliases)
//                {
//                    if (pk12.IsKeyEntry(al)) { alias = al; break; }
//                }

//                if (alias != null)
//                {
//                    return pk12.GetCertificate(alias).Certificate;
//                }
//            }
//            return null;
//        }

//        private void btnChonFile_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog dlg = new OpenFileDialog();
//            dlg.Filter = "PDF Files|*.pdf";
//            if (dlg.ShowDialog() == DialogResult.OK)
//            {
//                txtPath.Text = dlg.FileName;
//                lblKetQua.Text = "Đã chọn file. Nhấn 'Kiểm Tra Ngay' để xử lý.";
//                lblKetQua.ForeColor = System.Drawing.Color.Black;
//            }
//        }

//        private void btnKiemTra_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(txtPath.Text) || !File.Exists(txtPath.Text))
//            {
//                MessageBox.Show("Vui lòng chọn file PDF hợp lệ!");
//                return;
//            }

//            try
//            {
//                // Lấy chứng chỉ gốc của Shop từ DB để đối chiếu
//                var shopCertificate = LayChungChiGoc();
//                if (shopCertificate == null)
//                {
//                    MessageBox.Show("Hệ thống chưa có khóa bảo mật trong DB. Không thể đối chiếu!");
//                    return;
//                }

//                VerifyPdfSignature(txtPath.Text, shopCertificate);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi khi kiểm tra: " + ex.Message);
//            }
//        }
//        // --- 3. LOGIC KIỂM TRA CHỮ KÝ (CORE) ---
//        private void VerifyPdfSignature(string path, Org.BouncyCastle.X509.X509Certificate shopCert)
//        {
//            using (PdfReader reader = new PdfReader(path))
//            using (PdfDocument pdfDoc = new PdfDocument(reader))
//            {
//                SignatureUtil signUtil = new SignatureUtil(pdfDoc);
//                IList<string> names = signUtil.GetSignatureNames();

//                if (names.Count == 0)
//                {
//                    lblKetQua.Text = "❌ File PDF này KHÔNG CÓ chữ ký số nào.";
//                    lblKetQua.ForeColor = System.Drawing.Color.Red;
//                    return;
//                }

//                foreach (string name in names)
//                {
//                    // Đọc dữ liệu chữ ký
//                    PdfPKCS7 pkcs7 = signUtil.ReadSignatureData(name);

//                    // 1. Kiểm tra tính toàn vẹn (File có bị sửa sau khi ký không?)
//                    bool isIntegrityValid = pkcs7.VerifySignatureIntegrityAndAuthenticity();

//                    // 2. Kiểm tra danh tính (Có phải Shop mình ký không?)
//                    // Lấy chứng chỉ từ file PDF
//                    var certFromPdf = pkcs7.GetSigningCertificate();

//                    // So sánh chứng chỉ trong PDF với chứng chỉ gốc của Shop (So sánh mảng byte)
//                    bool isMyShop = certFromPdf.GetEncoded().SequenceEqual(shopCert.GetEncoded());

//                    if (isIntegrityValid && isMyShop)
//                    {
//                        lblKetQua.Text = "✅ HỢP LỆ!\n\n" +
//                                         "- File chưa bị chỉnh sửa.\n" +
//                                         "- Chữ ký xác thực từ hệ thống Khách Sạn Demo.\n" +
//                                         $"- Người ký: {certFromPdf.SubjectDN}\n" +
//                                         $"- Thời gian ký: {pkcs7.GetSignDate().ToLocalTime()}";
//                        lblKetQua.ForeColor = System.Drawing.Color.Green;
//                        return; // Tìm thấy chữ ký đúng thì dừng
//                    }
//                    else if (!isIntegrityValid)
//                    {
//                        lblKetQua.Text = "⚠️ CẢNH BÁO: File ĐÃ BỊ CHỈNH SỬA sau khi ký!\nGiá trị pháp lý không còn.";
//                        lblKetQua.ForeColor = System.Drawing.Color.Red;
//                    }
//                    else if (!isMyShop)
//                    {
//                        lblKetQua.Text = "⚠️ CẢNH BÁO: Chữ ký hợp lệ nhưng KHÔNG PHẢI CỦA SHOP.\n" +
//                                         $"Người ký lạ: {certFromPdf.SubjectDN}";
//                        lblKetQua.ForeColor = System.Drawing.Color.OrangeRed;
//                    }
//                }
//            }
//        }
//    }
//}
using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;

// --- ORACLE ---
using Oracle.ManagedDataAccess.Client;

// --- ITEXT 7 ---
using iText.Kernel.Pdf;
using iText.Signatures;

// --- BOUNCY CASTLE ---
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace Form_QL_Khachsan_2._0
{
    public partial class KiemTraChuKy : Form
    {
        // BIẾN TOÀN CỤC: Lưu chứng chỉ để dùng đi dùng lại, không cần tải lại từ DB
        private Org.BouncyCastle.X509.X509Certificate _shopCert = null;

        public KiemTraChuKy()
        {
            InitializeComponent();

            // Tải chứng chỉ ngay khi mở form
            LoadShopCertificate();
        }

        // --- 1. HÀM TẢI CHỨNG CHỈ TỪ DB (CHẠY 1 LẦN DUY NHẤT) ---
        private void LoadShopCertificate()
        {
            try
            {
                // SỬA LỖI DISPOSE: Không dùng 'using' cho conn, vì conn là biến dùng chung
                var conn = database.Get_Connect();
                if (conn.State != ConnectionState.Open) conn.Open();

                string sql = "SELECT GIATRI FROM THAMSO_HE_THONG WHERE MA_THAMSO = 'DIGITAL_SIGNATURE_KEY'";

                byte[] pfxBytes = null;

                using (var cmd = new OracleCommand(sql, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pfxBytes = Convert.FromBase64String(result.ToString());
                    }
                }
                // Lưu ý: Không đóng conn ở đây để các form khác còn dùng được

                // Nếu lấy được byte, chuyển đổi sang Certificate và lưu vào biến _shopCert
                if (pfxBytes != null)
                {
                    using (MemoryStream pfxStream = new MemoryStream(pfxBytes))
                    {
                        // Password set cứng là "123" như form tạo
                        Pkcs12Store pk12 = new Pkcs12Store(pfxStream, "123".ToCharArray());

                        string alias = null;
                        foreach (string al in pk12.Aliases)
                        {
                            if (pk12.IsKeyEntry(al)) { alias = al; break; }
                        }

                        if (alias != null)
                        {
                            _shopCert = pk12.GetCertificate(alias).Certificate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Key từ DB: " + ex.Message);
            }
        }

        // --- 2. SỰ KIỆN CHỌN FILE ---
        private void btnChonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PDF Files|*.pdf";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
                lblKetQua.Text = "Đã chọn file. Nhấn 'Kiểm Tra Ngay' để xử lý.";
                lblKetQua.ForeColor = System.Drawing.Color.Black;
            }
        }

        // --- 3. SỰ KIỆN NÚT KIỂM TRA ---
        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text) || !File.Exists(txtPath.Text))
            {
                MessageBox.Show("Vui lòng chọn file PDF hợp lệ!");
                return;
            }

            // Kiểm tra xem đã có chứng chỉ gốc chưa (Check Cache)
            if (_shopCert == null)
            {
                // Nếu chưa có (do lúc mở form bị lỗi mạng), thử tải lại
                LoadShopCertificate();

                if (_shopCert == null)
                {
                    MessageBox.Show("Hệ thống chưa có khóa bảo mật trong DB hoặc mất kết nối. Không thể đối chiếu!");
                    return;
                }
            }

            try
            {
                VerifyPdfSignature(txtPath.Text, _shopCert);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra: " + ex.Message);
            }
        }

        // --- 4. LOGIC KIỂM TRA CHỮ KÝ (CORE) ---
        private void VerifyPdfSignature(string path, Org.BouncyCastle.X509.X509Certificate shopCert)
        {
            try
            {
                using (PdfReader reader = new PdfReader(path))
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    SignatureUtil signUtil = new SignatureUtil(pdfDoc);
                    IList<string> names = signUtil.GetSignatureNames();

                    if (names.Count == 0)
                    {
                        lblKetQua.Text = "❌ File PDF này KHÔNG CÓ chữ ký số nào.";
                        lblKetQua.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    foreach (string name in names)
                    {
                        // Đọc dữ liệu chữ ký
                        PdfPKCS7 pkcs7 = signUtil.ReadSignatureData(name);

                        // A. Kiểm tra tính toàn vẹn (File có bị sửa sau khi ký không?)
                        bool isIntegrityValid = pkcs7.VerifySignatureIntegrityAndAuthenticity();

                        // B. Kiểm tra danh tính (Có phải Shop mình ký không?)
                        var certFromPdf = pkcs7.GetSigningCertificate();
                        bool isMyShop = certFromPdf.GetEncoded().SequenceEqual(shopCert.GetEncoded());

                        if (isIntegrityValid && isMyShop)
                        {
                            lblKetQua.Text = "✅ HỢP LỆ!\n\n" +
                                             "- File nguyên vẹn.\n" +
                                             "- Chữ ký CHÍNH CHỦ từ hệ thống Khách Sạn.\n" +
                                             $"- Người ký: {certFromPdf.SubjectDN}\n" +
                                             $"- Thời gian: {pkcs7.GetSignDate().ToLocalTime()}";
                            lblKetQua.ForeColor = System.Drawing.Color.Green;
                            return; // Tìm thấy chữ ký đúng thì dừng
                        }
                        else if (!isIntegrityValid)
                        {
                            lblKetQua.Text = "⚠️ CẢNH BÁO NGUY HIỂM:\nFile ĐÃ BỊ CHỈNH SỬA nội dung sau khi ký!\nGiá trị pháp lý không còn.";
                            lblKetQua.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (!isMyShop)
                        {
                            lblKetQua.Text = "⚠️ CẢNH BÁO GIẢ MẠO:\nChữ ký hợp lệ nhưng KHÔNG PHẢI CỦA SHOP.\n" +
                                             $"Người ký lạ: {certFromPdf.SubjectDN}";
                            lblKetQua.ForeColor = System.Drawing.Color.OrangeRed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu file PDF bị hỏng format (do hack bằng notepad ẩu)
                lblKetQua.Text = "❌ Lỗi đọc file: File có thể đã bị hỏng cấu trúc.\n" + ex.Message;
                lblKetQua.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
