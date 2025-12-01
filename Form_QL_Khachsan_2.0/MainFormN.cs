using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Form_QL_Khachsan_2._0
{
    public partial class MainFormN : Form
    {
        // Ở Form chính hoặc sau khi login
       

        private OracleConnection conn;
        private KQ co;
        private DataTable originalTableData;  // dữ liệu gốc (chưa mã hóa)
        private bool isEncryptedView = true;

        public MainFormN()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (database.DangXuatSession("qlkhachsan"))
            {
                MessageBox.Show("Đăng xuất toàn bộ session thành công!");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Đăng xuất thất bại!");
            }
        }

        private void MainFormN_Load(object sender, EventArgs e)
        {
            try
            {
                // 🔥 Lấy connection từ database.cs (không tạo mới)
                conn = database.Get_Connect();

                if (conn == null)
                    throw new Exception("Không lấy được connection từ database.cs");

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                // 🔥 KQ dùng đúng connection
                co = new KQ(conn);
                co.SetupSessionMonitor();

                // Load danh sách table
                chontb.Items.Add("KHACHHANG");
                chontb.Items.Add("PHONG");
                chontb.Items.Add("DATPHONG");
                chontb.Items.Add("HOADON");

                LoadDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải MainForm: " + ex.Message);
            }

        }

        private void chontb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chontb.SelectedItem != null)
                LoadTableData(chontb.SelectedItem.ToString());
        }

 // đang xem bản mã hóa hay không


        private void LoadTableData(string tableName)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("pr_show_table_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("tenbang", OracleDbType.Varchar2).Value = tableName;

                OracleParameter p_out = new OracleParameter("kq", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(p_out);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                originalTableData = dt.Copy();

                // ====== 🔐 MÃ HÓA DES ======
                byte[] key = Encoding.UTF8.GetBytes("12345678");
                DataTable encrypted = dt.Copy();

                foreach (DataRow row in encrypted.Rows)
                {
                    foreach (string col in new[] { "CMND", "SDT", "EMAIL" })
                    {
                        if (encrypted.Columns.Contains(col) && row[col] != DBNull.Value)
                        {
                            string plain = row[col].ToString();
                            row[col] = Convert.ToBase64String(co.Encrypt(plain, key));
                        }
                    }
                }

                dgv_hienthi.DataSource = encrypted;
                isEncryptedView = true;
                btn_giaima.Text = "Giải mã dữ liệu";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu bảng: " + ex.Message);
            }

        }

        private bool isDecrypted = false;  // trạng thái nút
        private string originalText = "";  // lưu giá trị mã hóa gốc

        private void LoadDoanhThu()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("fn_tong_doanhthu", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter ret = new OracleParameter
                {
                    OracleDbType = OracleDbType.Decimal,
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(ret);

                cmd.ExecuteNonQuery();

                decimal dt = ((OracleDecimal)ret.Value).Value;

                string encrypted = co.MaHoaCaesar_Func(dt.ToString(), 22);

                txt_xemdoanhthu.Text = encrypted + " VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy doanh thu: " + ex.Message);
            }
        }



        private void btn_xemdoanhthu_Click(object sender, EventArgs e)
        {
            if (txt_xemdoanhthu.Text == "") return;

            if (!isDecrypted)
            {
                originalText = txt_xemdoanhthu.Text;

                string encrypted = originalText.Replace("VND", "").Trim();

                string plain = co.GiaiMaCaesar_Func(encrypted, 22);

                txt_xemdoanhthu.Text = plain + " VND";
                btn_xemdoanhthu.Text = "Ẩn doanh thu";
                isDecrypted = true;
            }
            else
            {
                txt_xemdoanhthu.Text = originalText;
                btn_xemdoanhthu.Text = "Xem doanh thu";
                isDecrypted = false;
            }
        }

        private void btn_hd_Click(object sender, EventArgs e)
        {
            ql_hoadon dh = new ql_hoadon();
            dh.Show();
        }

        private void btn_timMP_Click(object sender, EventArgs e)
        {
            TimMaPhong m = new TimMaPhong();
            m.Show();
        }

        private void btn_giaima_Click(object sender, EventArgs e)
        {
            if (originalTableData == null) return;

            if (isEncryptedView)
            {
                dgv_hienthi.DataSource = originalTableData;
                btn_giaima.Text = "Ẩn dữ liệu gốc (hiển mã hóa)";
                isEncryptedView = false;
            }
            else
            {
                LoadTableData(chontb.SelectedItem.ToString());
            }
        }

        private void btn_TaoKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                // SỬA LỖI 1: Dùng biến conn toàn cục của Form, KHÔNG dùng 'using' để tránh đóng kết nối
                if (this.conn == null) this.conn = database.Get_Connect();
                if (this.conn.State != ConnectionState.Open) this.conn.Open();

                // BƯỚC 1: KIỂM TRA XEM ĐÃ CÓ KHÓA CHƯA
                string sqlCheck = "SELECT GIATRI FROM THAMSO_HE_THONG WHERE MA_THAMSO = 'DIGITAL_SIGNATURE_KEY'";
                using (var cmdCheck = new OracleCommand(sqlCheck, this.conn))
                {
                    var result = cmdCheck.ExecuteScalar();
                    if (result != null && result != DBNull.Value && !string.IsNullOrEmpty(result.ToString()))
                    {
                        MessageBox.Show("⛔ HỆ THỐNG ĐÃ CÓ KHÓA BẢO MẬT!\nKhông được phép tạo lại để tránh mất mát dữ liệu hóa đơn cũ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                // BƯỚC 2: TẠO KHÓA (GEN KEY)
                using (RSA rsa = RSA.Create(2048))
                {
                    var request = new CertificateRequest("CN=KhachSanDemo, O=IT_Team, C=VN", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    var certificate = request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(10));
                    byte[] pfxBytes = certificate.Export(X509ContentType.Pfx, "123");
                    string pfxBase64 = Convert.ToBase64String(pfxBytes);

                    // BƯỚC 3: LƯU XUỐNG DB (SỬA LỖI 2: Xử lý cả trường hợp chưa có dòng nào)

                    // Thử Update trước
                    string sqlUpdate = "UPDATE THAMSO_HE_THONG SET GIATRI = :val WHERE MA_THAMSO = 'DIGITAL_SIGNATURE_KEY'";
                    using (var cmdUpdate = new OracleCommand(sqlUpdate, this.conn))
                    {
                        cmdUpdate.Parameters.Add(":val", OracleDbType.Clob).Value = pfxBase64;
                        int rowsAffected = cmdUpdate.ExecuteNonQuery();

                        // Nếu Update không được dòng nào (rowsAffected == 0) -> Nghĩa là chưa có dòng đó -> Thực hiện Insert
                        if (rowsAffected == 0)
                        {
                            string sqlInsert = "INSERT INTO THAMSO_HE_THONG (MA_THAMSO, GIATRI) VALUES ('DIGITAL_SIGNATURE_KEY', :val)";
                            using (var cmdInsert = new OracleCommand(sqlInsert, this.conn))
                            {
                                cmdInsert.Parameters.Add(":val", OracleDbType.Clob).Value = pfxBase64;
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("✅ Đã khởi tạo và lưu Khóa Ký Số thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo khóa: " + ex.Message);
            }
        }
    }
}
