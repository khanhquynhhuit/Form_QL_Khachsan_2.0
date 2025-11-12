using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void Set_Database(string host, string port, string sid, string user, string pass)
        {
            database.Host = host;
            database.Port = port;
            database.Sid = sid;
            database.User = user;
            database.Password = pass;
        }

        public static bool Connect()
        {
            string connsys = "";
            try
            {
                if (User.ToUpper().Equals("SYS"))
                {
                    connsys = ";DBA Privilege=SYSDBA;";
                }
                string connString = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST=" + Host + ")(PORT = " + Port + "))(CONNECT_DATA = (SERVER = DEDICATE)(SERVICE_NAME = " + Sid + ")));User ID=" + User + " ; Password = " + Password + connsys;

                Conn = new OracleConnection();
                Conn.ConnectionString = connString;
                Conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool CreateUser(string newUser, string newPass)
        {
            try
            {
                // Kết nối bằng user QLKHACHSAN thay vì SYS
                string qlkhachsanConnString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));" +
                    "User Id=QLKHACHSAN;Password=qlks;";

                using (OracleConnection conn = new OracleConnection(qlkhachsanConnString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("taousermoi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = newUser;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = newPass;

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi Oracle khi tạo user: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo user: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DangXuatSession(string user)
        {
            try
            {
                string adminConnString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                                         "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=SYS;Password=sys;DBA Privilege=SYSDBA;";

                using (OracleConnection conn = new OracleConnection(adminConnString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("pr_logout_all_user", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        // Truyền tham số username
                        cmd.Parameters.Add("p_username", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2).Value = user;

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static OracleConnection Get_Connect()
        {
            if (Conn == null)
            {
                // Gọi lại hàm Connect() để khởi tạo connection mới
                if (!Connect())
                {
                    throw new Exception("Không thể tạo kết nối Oracle!");
                }
            }
            else if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }

            return Conn;
        }
    }
}
