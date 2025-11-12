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
    public partial class QuanLyTaiKhoan : Form
    {
        private OracleConnection connection;
        string currentUser = database.User;

        public QuanLyTaiKhoan(OracleConnection conn)
        {
            InitializeComponent();
            connection = conn;
            LoadActiveSessions();
            LoadActiveSessions_THIETBI(currentUser);
            lb_username.Text = $"Tài khoản hiện tại: {currentUser}";
        }

        private void LoadActiveSessions_THIETBI(string username)
        {
            try
            {
                string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
                                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=QLKHACHSAN;Password=qlks;";

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SELECT fn_is_user_logged_in(:username) FROM dual", conn))
                    {
                        cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

                        object result = cmd.ExecuteScalar();
                        int activeCount = Convert.ToInt32(result);

                        // Hiển thị trong Label
                        lb_sosession.Text = $"Số thiết bị đăng nhập: {activeCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra số thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadActiveSessions()
        {
            try
            {
                string adminConnString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";

                using (OracleConnection conn = new OracleConnection(adminConnString))
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
                string adminConnString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";

                using (OracleConnection conn = new OracleConnection(adminConnString))
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

            if (MessageBox.Show($"Bạn có chắc muốn ngắt kết nối thiết bị nàykhông?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KillSession(sid, serial);
                LoadActiveSessions();
            }
        }
    }
}
