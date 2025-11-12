using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace Form_QL_Khachsan_2._0
{
    public partial class Dangky : Form
    {
        private KQ co;
        public Dangky()
        {
            InitializeComponent();
            CenterToScreen();
        }
        bool Check_Textbox(string user, string pass, string role, string manv)
        {
            if (user == "")
            {
                MessageBox.Show("Chưa điền thông tin UserName");
                txt_dku.Focus();
                return false;
            }
            else if (pass == "")
            {
                MessageBox.Show("Chưa điền thông tin PassWord");
                txt_dkp.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btndangky_Click(object sender, EventArgs e)
        {
            co = new KQ();
            string newUser = txt_dku.Text.Trim();
            string newPass = txt_dkp.Text.Trim();
            if (!Check_Textbox(newUser, newPass, "", ""))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            if (database.CreateUser(newUser, newPass))
            {
                MessageBox.Show("Tạo user mới thành công!");
                this.Hide();
                Login lg = new Login();
                lg.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tạo user thất bại!");
            }
        }

        private void btnMologin_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
        }
    }

}
