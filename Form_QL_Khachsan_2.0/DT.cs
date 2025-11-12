using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form_QL_Khachsan_2._0
{
    internal static class DT
    {
        public static string EncryptToHex(string text, string key, bool nhanCong)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] result = new byte[data.Length];
            var (mult, add) = GenerateKeyArrays(key);

            for (int i = 0; i < data.Length; i++)
            {
                int m = mult[i % mult.Length];
                int a = add[i % add.Length];
                int val = nhanCong ? (data[i] * m + a) % 256 : ((data[i] + a) * m) % 256;
                result[i] = (byte)val;
            }
            return BitConverter.ToString(result).Replace("-", "");
        }

        public static string DecryptFromHex(string hex, string key, bool nhanCong)
        {
            byte[] data = HexToBytes(hex);
            byte[] result = new byte[data.Length];
            var (mult, add) = GenerateKeyArrays(key);

            for (int i = 0; i < data.Length; i++)
            {
                int m = mult[i % mult.Length];
                int a = add[i % add.Length];
                int inv = ModInverse(m, 256);
                int val = nhanCong
                    ? (inv * (data[i] - a + 256)) % 256
                    : ((inv * data[i]) - a + 256) % 256;
                result[i] = (byte)val;
            }

            try { return Encoding.UTF8.GetString(result); }
            catch { return Convert.ToBase64String(result); }
        }

        private static (int[] mult, int[] add) GenerateKeyArrays(string key)
        {
            int[] mult = new int[key.Length];
            int[] add = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                int code = key[i];
                mult[i] = ((code * 3 + 1) % 255) | 1;
                add[i] = (code * 7 + i) % 256;
            }
            return (mult, add);
        }

        private static byte[] HexToBytes(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return new byte[0];
            hex = hex.Replace("-", "").Replace(" ", "");
            int len = hex.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return bytes;
        }

        private static int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1) return x;
            throw new Exception("Không tìm được nghịch đảo cho " + a);
        }
    }
}
