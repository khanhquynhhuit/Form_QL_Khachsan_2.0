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
    public partial class ql_hoadon : Form
    {
        private bool hienthiGiaiMa = false;

        public ql_hoadon()
        {
            InitializeComponent();
        }

        private void ql_hoadon_Load(object sender, EventArgs e)
        {
            hienthiGiaiMa = false;
            btn_mahoa.Text = "Hiện số tiền thật";
            load_hoadon();
        }

        private void load_hoadon()
        {
            try
            {
                OracleConnection conn = database.Get_Connect();
                OracleCommand cmd = new OracleCommand("GET_HOADON", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Tham số đầu vào (p_show_real)
                cmd.Parameters.Add("p_show_real", OracleDbType.Int32).Value = hienthiGiaiMa ? 1 : 0;

                // Tham số output (cursor)
                OracleParameter cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.Parameters.Add(cursor);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                hien_dl.DataSource = null;
                hien_dl.Columns.Clear();

                // Gán lại bảng dữ liệu
                hien_dl.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hóa đơn: " + ex.Message);
            }
        }

        private void hien_dl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = hien_dl.Rows[e.RowIndex];
                txt_hd.Text = row.Cells["MaHD"].Value.ToString();
                btn_tim_Click(sender, e);
            }
        }

        private void btn_tim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_hd.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Hóa Đơn cần tìm!", "Thông báo");
                return;
            }

            try
            {
                OracleConnection conn = database.Get_Connect();
                OracleCommand cmd = new OracleCommand("GET_HOADON_THEO_MAHD", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                // Tham số đầu vào
                cmd.Parameters.Add("p_mahd", OracleDbType.Varchar2).Value = txt_hd.Text.Trim();
                cmd.Parameters.Add("p_show_real", OracleDbType.Int32).Value = hienthiGiaiMa ? 1 : 0;

                // Tham số output (cursor)
                OracleParameter cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.Parameters.Add(cursor);

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                hien_dl.DataSource = null;
                hien_dl.Columns.Clear();

                hien_dl.DataSource = dt.Rows.Count > 0 ? dt : null;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy hóa đơn có mã: " + txt_hd.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm hóa đơn: " + ex.Message);
            }
        }

        private void btn_mahoa_Click(object sender, EventArgs e)
        {
            hienthiGiaiMa = !hienthiGiaiMa;
            btn_mahoa.Text = hienthiGiaiMa ? "Ẩn số tiền" : "Hiện số tiền thật";
            load_hoadon();
        }
    }
}
