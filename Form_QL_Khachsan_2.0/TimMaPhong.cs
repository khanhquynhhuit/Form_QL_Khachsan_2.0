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
    public partial class TimMaPhong : Form
    {
        private bool hienthiGiaiMa = false;

        public TimMaPhong()
        {
            InitializeComponent();
        }

        private void btn_tim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_maP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Phòng cần tìm!", "Thông báo");
                return;
            }

            try
            {
                OracleConnection conn = database.Get_Connect();
                {
                    string query = hienthiGiaiMa ? @"
                    SELECT 
                        kh.HOTEN AS ""Tên Khách Hàng"",
                        MAkh.CMND AS ""CCCD (Mã hóa)"",
                        kh.NGAYSINH AS ""Ngày Sinh"",
                        p.SONGUOITOIDA AS ""Số Lượng Tối Đa"",
                        p.TINHTRANG AS ""Tình Trạng Phòng""
                    FROM Phong p
                    JOIN DatPhong dp ON p.MAPH = dp.MAPH
                    JOIN KhachHang kh ON dp.MAKH = kh.MAKH
                     WHERE p.MAPH = :maphong"
                    :
                    @"SELECT
                        kh.HOTEN AS ""Tên Khách Hàng"",
                        MAHOA_NHAN(TO_NUMBER(SUBSTR(kh.CMND, 1, 6))) AS ""CCCD(Mã hóa)"",
                        kh.NGAYSINH AS ""Ngày Sinh"",
                    p.SONGUOITOIDA AS ""Số Lượng Tối Đa"",
                    p.TINHTRANG AS ""Tình Trạng Phòng""
                    FROM Phong p
                    JOIN DatPhong dp ON p.MAPH = dp.MAPH
                    JOIN KhachHang kh ON dp.MAKH = kh.MAKH
                     WHERE p.MAPH = :maphong";

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.Parameters.Add(new OracleParameter(":maphong", txt_maP.Text.Trim()));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgv_hienthi.DataSource = dt.Rows.Count > 0 ? dt : null;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Không tìm thấy thông tin cho Mã Phòng: " + txt_maP.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm phòng: " + ex.Message);
            }
        }

        private void btn_mahoa_Click(object sender, EventArgs e)
        {
            hienthiGiaiMa = !hienthiGiaiMa;
            btn_mahoa.Text = hienthiGiaiMa ? "Ẩn số tiền" : "Hiện số tiền thật";
        }

        private void dgv_hienthi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hienthiGiaiMa = false;
            btn_mahoa.Text = "Hien cccd";
        }
    }
}
