using Oracle.ManagedDataAccess.Client;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Form_QL_Khachsan_2._0
{
    public partial class Dangky : Form
    {
        public Dangky()
        {
            InitializeComponent();
            CenterToScreen();
        }

        // -----------------------------
        // CHECK VALIDATION
        // -----------------------------
        bool Check_Input(string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Bạn chưa nhập Username!", "Thiếu thông tin");
                txt_dku.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Bạn chưa nhập Password!", "Thiếu thông tin");
                txt_dkp.Focus();
                return false;
            }

            // RULE: Username phải là chữ + số (Oracle rule)
            if (!user.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("Username chỉ được dùng chữ và số (Oracle quy định)!", "Sai định dạng");
                return false;
            }

            if (user.Length > 30)
            {
                MessageBox.Show("Username không được quá 30 ký tự!", "Sai định dạng");
                return false;
            }

            return true;
        }

        // -----------------------------
        // ĐĂNG KÝ USER
        // -----------------------------
        private void btndangky_Click(object sender, EventArgs e)
        {
            string newUser = txt_dku.Text.Trim().ToUpper();
            string newPass = txt_dkp.Text.Trim();

            if (!Check_Input(newUser, newPass))
                return;

            // KIỂM TRA USER TRÙNG TRƯỚC KHI TẠO
            if (Check_User_Exists(newUser))
            {
                MessageBox.Show("Username đã tồn tại! Hãy chọn username khác.", "Trùng username");
                return;
            }

            // TIẾN HÀNH TẠO USER
            if (database.CreateUser(newUser, newPass))
            {
                MessageBox.Show("Tạo user mới thành công!", "Thành công");

                this.Hide();
                Login lg = new Login();
                lg.Show();
                return;
            }
            else
            {
                MessageBox.Show("Tạo user thất bại!", "Lỗi");
            }
        }

        // -----------------------------
        // CHECK TÀI KHOẢN ĐÃ TỒN TẠI
        // -----------------------------
        private bool Check_User_Exists(string username)
        {
            try
            {
                string qlksConn =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=QLKHACHSAN;Password=qlks;";

                using (OracleConnection conn = new OracleConnection(qlksConn))
                {
                    conn.Open();

                    string sql = "SELECT COUNT(*) FROM all_users WHERE username = :u";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":u", username);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra user tồn tại: " + ex.Message);
                return true;
            }
        }


        // -----------------------------
        // MỞ LẠI TRANG LOGIN
        // -----------------------------
        private void btnMologin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }
    }
}
