using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_QL_Khachsan_2._0
{
    public partial class QuanLyTaiKhoan : Form
    {
        private string currentUser = database.User;

        public QuanLyTaiKhoan(OracleConnection conn)
        {
            InitializeComponent();
            lb_username.Text = $"Tài khoản hiện tại: {currentUser}";
            LoadActiveSessions();
            LoadActiveSessions_THIETBI();
        }

        private void LoadActiveSessions_THIETBI()
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("SELECT fn_is_user_logged_in(:username) FROM dual", database.Get_Connect()))
                {
                    cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = currentUser;
                    object result = cmd.ExecuteScalar();
                    int activeCount = Convert.ToInt32(result);
                    lb_sosession.Text = $"Số thiết bị đăng nhập: {activeCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra số thiết bị: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadActiveSessions()
        {
            try
            {
                string adminConn =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";
                using (OracleConnection conn = new OracleConnection(adminConn))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PROC_GET_ACTIVE_SESSIONS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = currentUser;
                        cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgv_thietbi.DataSource = dt;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thiết bị:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KillSession(int sid, int serial)
        {
            try
            {
                string adminConn =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";
                using (OracleConnection conn = new OracleConnection(adminConn))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PROC_KILL_SESSION", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        cmd.Parameters.Add("p_sid", OracleDbType.Int32).Value = sid;
                        cmd.Parameters.Add("p_serial", OracleDbType.Int32).Value = serial;

                        cmd.ExecuteNonQuery();
                    }
                }
                    

                MessageBox.Show("Đã ngắt kết nối thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadActiveSessions(); // Refresh danh sách sau khi ngắt
                LoadActiveSessions_THIETBI(); // Refresh số thiết bị
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ngắt kết nối: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadActiveSessions();
            LoadActiveSessions_THIETBI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_thietbi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để ngắt kết nối.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sid = Convert.ToInt32(dgv_thietbi.CurrentRow.Cells["SID"].Value);
            int serial = Convert.ToInt32(dgv_thietbi.CurrentRow.Cells["SERIAL#"].Value);

            if (MessageBox.Show("Bạn có chắc muốn ngắt kết nối thiết bị này không?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KillSession(sid, serial);
            }
        }
    }
}
