using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Form_QL_Khachsan_2._0
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CenterToScreen();
            RBtn_Binhthuong.Checked = true;
        }

        bool Check_Textbox(string host, string port, string sid, string user, string pass)
        {
            if (host == "")
            {
                MessageBox.Show("Chua dien thong tin Host");
                txt_host.Focus();
                return false;
            }
            else if (port == "")
            {
                MessageBox.Show("Chua dien thong tin Port");
                txt_port.Focus();
                return false;
            }
            else if (sid == "")
            {
                MessageBox.Show("Chua dien thong tin Sid");
                txt_sid.Focus();
                return false;
            }
            else if (user == "")
            {
                MessageBox.Show("Chua dien thong tin User");
                txt_user.Focus();
                return false;
            }
            else if (pass == "")
            {
                MessageBox.Show("Chua dien thong tin Password");
                txt_password.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            string user = txt_user.Text;
            string pass = txt_password.Text;

            if (RBtn_sessionduynhat.Checked)
            {
                if (database.DangXuatSession("qlkhachsan"))
                {
                    MessageBox.Show("Đăng xuất toàn bộ các thiết bị!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đăng xuất toàn bộ thiết bị thất bại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (Check_Textbox(host, port, sid, user, pass))
            {
                database.Set_Database(host, port, sid, user, pass);
                if (database.Connect())
                {
                    OracleConnection c = database.Get_Connect();
                    MessageBox.Show("Dang nhap thanh cong\nServerVersion: " + c.ServerVersion);
                   
                    Menu mn = new Menu();
                    mn.Show();
                }
                else
                {
                    MessageBox.Show("Dang nhap that bai");
                }
            }
        }

        

        private void btnDangky_Click(object sender, EventArgs e)
        {
            Dangky dk = new Dangky();
            dk.Show();
        }

    }
}
