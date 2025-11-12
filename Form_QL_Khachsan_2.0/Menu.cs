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
            co = new KQ();
            InitializeComponent();
            co.SetupSessionMonitor();
            lb_chaomung.Text = $"Chào mừng {database.User} !";
        }

        private void btnQuanLyTK_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan f = new QuanLyTaiKhoan(conn);
            f.ShowDialog();
        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
            MainFormN mnn = new MainFormN();
            mnn.ShowDialog();
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
                MessageBox.Show("Đăng xuất toàn bộ session thất bại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
