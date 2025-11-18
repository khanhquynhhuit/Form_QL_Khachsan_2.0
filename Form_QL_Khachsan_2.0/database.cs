using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Form_QL_Khachsan_2._0
{
    public class database
    {
        public static OracleConnection Conn;

        public static string Host;
        public static string Port;
        public static string Sid;
        public static string User;
        public static string Password;

        // Lưu thông tin kết nối
        public static void Set_Database(string host, string port, string sid, string user, string pass)
        {
            Host = host;
            Port = port;
            Sid = sid;
            User = user;
            Password = pass;
        }

        // ---------------------------
        //  HÀM KẾT NỐI CHUẨN ORACLE
        // ---------------------------
        public static bool Connect()
        {
            try
            {
                string sysPrivilege = "";

                if (User.ToUpper().Equals("SYS"))
                    sysPrivilege = ";DBA Privilege=SYSDBA";

                string connString =
                    $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Host})(PORT={Port}))" +
                    $"(CONNECT_DATA=(SERVICE_NAME={Sid})));User Id={User};Password={Password}{sysPrivilege}";

                Conn = new OracleConnection(connString);
                Conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                Conn = null;
                MessageBox.Show("Không thể kết nối Oracle: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ---------------------------
        //  LẤY CONNECTION (DÙNG CHUNG)
        // ---------------------------
        public static OracleConnection Get_Connect()
        {
            if (Conn == null)
            {
                if (!Connect())
                    throw new Exception("Không thể tạo kết nối Oracle!");
            }
            else if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }

            return Conn;
        }

        // ---------------------------
        //  TẠO USER MỚI
        // ---------------------------
        public static bool CreateUser(string newUser, string newPass)
        {
            try
            {
                string qlksConn =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=QLKHACHSAN;Password=qlks;";

                using (OracleConnection conn = new OracleConnection(qlksConn))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("taousermoi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = newUser;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = newPass;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo user: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ---------------------------
        //  ĐĂNG XUẤT TOÀN BỘ SESSION
        // ---------------------------
        public static bool DangXuatSession(string user)
        {
            try
            {
                string adminConn =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";

                using (OracleConnection conn = new OracleConnection(adminConn))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("pr_logout_all_user", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = user;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng xuất user: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
