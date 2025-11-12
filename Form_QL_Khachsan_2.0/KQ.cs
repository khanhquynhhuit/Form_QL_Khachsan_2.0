using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_QL_Khachsan_2._0
{
    public class KQ
    {
        public KQ() { }
        char[] bochu = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{};:,.?~".ToCharArray();
        private int doso(char a)
        {
            for (int i = 0; i < bochu.Length; i++)
            {
                if (a == bochu[i])
                {
                    return i;
                }
            }
            return -1;
        }
        private char dochu(int a)
        {
            return bochu[a];
        }
        public string mahoacong(string a, int k, int n)
        {
            if (n > bochu.Length)
                throw new Exception("Giá trị n vượt quá độ dài bảng chữ (60)!");
            a = a.ToLower();
            char[] m = a.ToCharArray();
            int bochu1 = bochu.Length;
            int dai = a.Length;
            string smh = "";
            for (int i = 0; i < dai; i++)
            {
                int so = doso(m[i]);
                so = so + k;
                if (so >= n)
                {
                    so = so - n;
                }
                smh += dochu(so);
            }
            return smh;
        }

        public string giaimacong(string a, int k, int n)
        {
            if (n > bochu.Length)
                throw new Exception("Giá trị n vượt quá độ dài bảng chữ (60)!");
            a = a.ToLower();
            char[] m = a.ToCharArray();
            int dai = a.Length;
            string smh = "";
            for (int i = 0; i < dai; i++)
            {
                int so = doso(m[i]);
                so = so - k;
                if (so < 0)
                {
                    so = so + n;
                }
                smh += dochu(so);
            }
            return smh;
        }

        public static OracleConnection Conn;

        public KQ(OracleConnection conn)
        {
            Conn = conn;
        }

        public string MaHoaCaesar_Func(string PlainText, int key)
        {
            try
            {
                if (Conn == null)
                {
                    Conn = database.Get_Connect();
                }
                else
                {
                    // kiểm tra conn đã bị dispose hay không bằng try/catch
                    try
                    {
                        var st = Conn.State;
                    }
                    catch
                    {
                        Conn = database.Get_Connect();
                    }
                }
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();

                string Funtion = "encryptCaesarCipher";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Conn;
                cmd.CommandText = Funtion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 100;
                resultParam.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@str";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = PlainText;
                str.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(k);

                cmd.ExecuteNonQuery();

                string s = "null";
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    s = ret.ToString();
                }
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }

        public string GiaiMaCaesar_Func(string EncryptedText, int key)
        {
            try
            {
                if (Conn == null)
                    Conn = database.Get_Connect();
                else
                {
                    try { var st = Conn.State; }
                    catch { Conn = database.Get_Connect(); } // nếu bị disposed -> lấy lại
                }

                if (Conn == null)
                    throw new Exception("Không có connection để giải mã.");
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();

                string Funtion = "decryptCaesarCipher";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Conn;
                cmd.CommandText = Funtion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 100;
                resultParam.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@str";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = EncryptedText;
                str.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(k);

                cmd.ExecuteNonQuery();

                string s = "null";
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    s = ret.ToString();
                }
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }

        // Initialization Vector (IV) - dùng mặc định 8 byte (DES yêu cầu 64-bit IV)
        byte[] IV = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        // Hàm mã hóa DES
        public byte[] Encrypt(string plainText, byte[] key)
        {
            try
            {
                // Tạo một MemoryStream để chứa dữ liệu mã hóa
                using (MemoryStream mStream = new MemoryStream())
                {
                    // Tạo đối tượng DES
                    using (DES des = DES.Create())
                    {
                        // Tạo bộ mã hóa DES với key và IV
                        using (ICryptoTransform encryptor = des.CreateEncryptor(key, IV))
                        {
                            // Tạo CryptoStream để ghi dữ liệu đã mã hóa vào MemoryStream
                            using (CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                            {
                                // Chuyển chuỗi cần mã hóa thành mảng byte
                                byte[] toEncrypt = Encoding.UTF8.GetBytes(plainText);

                                // Ghi dữ liệu vào CryptoStream
                                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                                cStream.FlushFinalBlock();

                                // Trả về dữ liệu đã mã hóa dưới dạng mảng byte
                                return mStream.ToArray();
                            }
                        }
                    }
                }
            }
            catch (CryptographicException e)
            {
                MessageBox.Show("A Cryptographic error occurred: " + e.Message);
                throw;
            }
        }

        public string Decrypt(byte[] encrypted, byte[] key)
        {
            try
            {
                // Tạo bộ đệm để chứa dữ liệu sau khi giải mã
                byte[] decrypted = new byte[encrypted.Length];
                int offset = 0;

                // Dùng MemoryStream chứa dữ liệu mã hóa đầu vào
                using (MemoryStream mStream = new MemoryStream(encrypted))
                {
                    // Tạo đối tượng DES
                    using (DES des = DES.Create())
                    {
                        // Tạo bộ giải mã DES từ khóa và IV
                        using (ICryptoTransform decryptor = des.CreateDecryptor(key, IV))
                        {
                            // Tạo CryptoStream để đọc dữ liệu đã giải mã
                            using (CryptoStream cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
                            {
                                int read = 1;

                                // Đọc toàn bộ dữ liệu từ CryptoStream cho đến khi kết thúc
                                while (read > 0)
                                {
                                    read = cStream.Read(decrypted, offset, decrypted.Length - offset);
                                    offset += read;
                                }
                            }
                        }
                    }
                }

                // Chuyển mảng byte sau giải mã thành chuỗi và trả về
                return Encoding.UTF8.GetString(decrypted, 0, offset);
            }
            catch (CryptographicException e)
            {
                MessageBox.Show("A Cryptographic error occurred: " + e.Message);
                throw;
            }
        }

        public Timer sessionTimer;

        public void SetupSessionMonitor()
        {
            sessionTimer = new Timer();
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        public void SessionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Kết nối bằng QLKHACHSAN thay vì SYS
                string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=26.87.215.54)(PORT=1521))" +
                                    "(CONNECT_DATA=(SERVICE_NAME=orcl1)));User Id=QLKHACHSAN;Password=qlks;";

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("SELECT fn_is_user_logged_in(:p_username) FROM dual", conn))
                    {
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = database.User;

                        object result = cmd.ExecuteScalar();

                        int activeCount = Convert.ToInt32(result);

                        if (activeCount == 0)
                        {
                            sessionTimer.Stop();
                            MessageBox.Show("Tài khoản đã bị đăng xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Application.Exit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi do mất kết nối hoặc DB bị shutdown => không nên Exit ngay
                sessionTimer.Stop();
                MessageBox.Show("Lỗi khi kiểm tra session: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
