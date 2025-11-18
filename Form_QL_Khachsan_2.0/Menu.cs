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
    public partial class Menu : Form
    {
        private OracleConnection conn;
        private KQ co;
        public Menu()
        {
            InitializeComponent();
            CenterToScreen();

            try
            {
                // Lấy connection duy nhất từ database.cs
                conn = database.Get_Connect();
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                // Tạo KQ dùng chung connection
                co = new KQ(conn);
                co.SetupSessionMonitor();

                // Hiển thị chào mừng
                lb_chaomung.Text = $"Chào mừng {database.User} !";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo Menu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuanLyTK_Click(object sender, EventArgs e)
        {
            if (conn != null)
            {
                QuanLyTaiKhoan f = new QuanLyTaiKhoan(conn); // truyền connection chuẩn
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có kết nối database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
            if (conn != null)
            {
                MainFormN mnn = new MainFormN(); // MainFormN đã tự lấy connection từ database.cs
                mnn.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có kết nối database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (database.DangXuatSession(database.User))
            {
                MessageBox.Show("Đăng xuất toàn bộ session thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Đăng xuất toàn bộ session thất bại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
