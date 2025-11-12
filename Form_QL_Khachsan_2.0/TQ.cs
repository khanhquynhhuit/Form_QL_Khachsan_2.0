using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Types;

namespace Form_QL_Khachsan_2._0
{
    internal class TQ
    {
        private OracleConnection conn;

        public TQ(OracleConnection connection)
        {
            conn = connection;
        }

        public string EncryptCaesar_Func(string plainText)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("caesar_encrypt", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter resultParam = new OracleParameter();
                    resultParam.ParameterName = "RETURN_VALUE";
                    resultParam.Direction = ParameterDirection.ReturnValue;
                    resultParam.OracleDbType = OracleDbType.Varchar2;
                    resultParam.Size = 4000;
                    cmd.Parameters.Add(resultParam);

                    OracleParameter textParam = new OracleParameter();
                    textParam.ParameterName = "p_text";
                    textParam.OracleDbType = OracleDbType.Varchar2;
                    textParam.Direction = ParameterDirection.Input;
                    textParam.Value = plainText;
                    cmd.Parameters.Add(textParam);

                    cmd.ExecuteNonQuery();

                    string result = resultParam.Value?.ToString() ?? "";
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gọi hàm mã hóa Caesar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
