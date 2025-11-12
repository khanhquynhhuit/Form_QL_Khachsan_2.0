using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public MainFormN()
        {
            co = new KQ();
            InitializeComponent();
            co.SetupSessionMonitor();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (database.DangXuatSession("qlkhachsan"))
            {
                MessageBox.Show("Đăng xuất toàn bộ session thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Đăng xuất toàn bộ session thất bại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MainFormN_Load(object sender, EventArgs e)
        {
            conn = database.Get_Connect();

            if (conn.State != ConnectionState.Open)
                conn.Open();

            co = new KQ(conn);

            chontb.Items.Add("KHACHHANG");
            chontb.Items.Add("PHONG");
            chontb.Items.Add("DATPHONG");
            chontb.Items.Add("HOADON");

            LoadDoanhThu();

            
        }

        private void chontb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = chontb.SelectedItem.ToString();
            LoadTableData(selectedTable);
        }

        private DataTable originalTableData;  // dữ liệu gốc (chưa mã hóa)
        private bool isEncryptedView = true;  // đang xem bản mã hóa hay không


        private void LoadTableData(string tableName)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("pr_show_table_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("tenbang", OracleDbType.Varchar2).Value = tableName.ToUpper();

                OracleParameter p_out = new OracleParameter("kq", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(p_out);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Lưu lại dữ liệu gốc
                originalTableData = dt.Copy();

                // ====== 🔐 MÃ HÓA DES ======
                byte[] key = Encoding.UTF8.GetBytes("12345678"); // key 8 byte (DES)
                DataTable encryptedTable = dt.Copy();

                foreach (DataRow row in encryptedTable.Rows)
                {
                    if (encryptedTable.Columns.Contains("CMND") && row["CMND"] != DBNull.Value)
                    {
                        try
                        {
                            string plain = row["CMND"].ToString();
                            byte[] cipher = co.Encrypt(plain, key);
                            row["CMND"] = Convert.ToBase64String(cipher);
                        }
                        catch { }
                    }

                    if (encryptedTable.Columns.Contains("SDT") && row["SDT"] != DBNull.Value)
                    {
                        try
                        {
                            string plain = row["SDT"].ToString();
                            byte[] cipher = co.Encrypt(plain, key);
                            row["SDT"] = Convert.ToBase64String(cipher);
                        }
                        catch { }
                    }

                    if (encryptedTable.Columns.Contains("EMAIL") && row["EMAIL"] != DBNull.Value)
                    {
                        try
                        {
                            string plain = row["EMAIL"].ToString();
                            byte[] cipher = co.Encrypt(plain, key);
                            row["EMAIL"] = Convert.ToBase64String(cipher);
                        }
                        catch { }
                    }
                }

                // Hiển thị bản mã hóa
                dgv_hienthi.DataSource = encryptedTable;
                isEncryptedView = true;
                btn_giaima.Text = "Giải mã dữ liệu";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu bảng {tableName}:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadDoanhThu()
        {
            try
            {
                
                OracleConnection conn = database.Get_Connect();
                // đảm bảo conn hợp lệ
                if (conn == null)
                    throw new Exception("Không lấy được connection.");
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand cmd = new OracleCommand("fn_tong_doanhthu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter p_return = new OracleParameter();
                    p_return.Direction = ParameterDirection.ReturnValue;
                    p_return.OracleDbType = OracleDbType.Decimal;
                    cmd.Parameters.Add(p_return);

                    cmd.ExecuteNonQuery();

                    decimal doanhThu = ((Oracle.ManagedDataAccess.Types.OracleDecimal)p_return.Value).Value;

                    // DÙNG lại đối tượng co đã tạo ở MainFormN_Load (không tạo KQ mới)
                    if (co == null)
                        co = new KQ(conn);  // fallback nếu chưa có

                    string doanhThuMaHoa = co.MaHoaCaesar_Func(doanhThu.ToString(), 22);

                    txt_xemdoanhthu.Text = doanhThuMaHoa + " VND";
                }
                // Không dispose conn ở đây vì nó là connection toàn cục
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy doanh thu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isDecrypted = false;  // trạng thái nút
        private string originalText = "";  // lưu giá trị mã hóa gốc

        private void btn_xemdoanhthu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_xemdoanhthu.Text))
            {
                try
                {
                    if (!isDecrypted)
                    {
                        originalText = txt_xemdoanhthu.Text.Trim();
                        string encryptText = originalText;

                        //Bỏ phần "VND"
                        if (encryptText.EndsWith("VND", StringComparison.OrdinalIgnoreCase))
                        {
                            encryptText = encryptText.Replace("VND", "").Trim();
                        }

                        int key = 22;

                        //Giải mã Caesar
                        string rs = co.GiaiMaCaesar_Func(encryptText, key);

                        //Hiển thị kết quả ra textbox
                        txt_xemdoanhthu.Text = rs + " VND";

                        isDecrypted = true;
                        btn_xemdoanhthu.Text = "Ẩn doanh thu";
                    }
                    else
                    {
                        //Khôi phục lại giá trị ban đầu
                        txt_xemdoanhthu.Text = originalText;

                        //Đổi lại trạng thái
                        isDecrypted = false;
                        btn_xemdoanhthu.Text = "Xem doanh thu";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi giải mã doanh thu: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông điệp cần giải mã!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_xemdoanhthu.Focus();
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
            if (originalTableData == null || originalTableData.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để giải mã!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isEncryptedView)
            {
                // Hiển thị dữ liệu gốc
                dgv_hienthi.DataSource = originalTableData;
                btn_giaima.Text = "Ẩn dữ liệu gốc (hiển mã hóa)";
                isEncryptedView = false;
            }
            else
            {
                // Mã hóa lại khi muốn ẩn dữ liệu
                LoadTableData(chontb.SelectedItem.ToString());
            }
        }
    }
}
