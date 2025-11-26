namespace Form_QL_Khachsan_2._0
{
    partial class MainFormN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogout = new System.Windows.Forms.Button();
            this.dgv_hienthi = new System.Windows.Forms.DataGridView();
            this.chontb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_xemdoanhthu = new System.Windows.Forms.TextBox();
            this.btn_xemdoanhthu = new System.Windows.Forms.Button();
            this.btn_hd = new System.Windows.Forms.Button();
            this.btn_timMP = new System.Windows.Forms.Button();
            this.btn_giaima = new System.Windows.Forms.Button();
            this.btn_TaoKhoa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hienthi)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(954, 589);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(213, 69);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // dgv_hienthi
            // 
            this.dgv_hienthi.AccessibleName = "";
            this.dgv_hienthi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_hienthi.Location = new System.Drawing.Point(28, 268);
            this.dgv_hienthi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_hienthi.Name = "dgv_hienthi";
            this.dgv_hienthi.RowHeadersWidth = 62;
            this.dgv_hienthi.Size = new System.Drawing.Size(916, 286);
            this.dgv_hienthi.TabIndex = 1;
            // 
            // chontb
            // 
            this.chontb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chontb.FormattingEnabled = true;
            this.chontb.Location = new System.Drawing.Point(30, 222);
            this.chontb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chontb.Name = "chontb";
            this.chontb.Size = new System.Drawing.Size(232, 33);
            this.chontb.TabIndex = 2;
            this.chontb.SelectedIndexChanged += new System.EventHandler(this.chontb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Doanh thu:";
            // 
            // txt_xemdoanhthu
            // 
            this.txt_xemdoanhthu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_xemdoanhthu.Location = new System.Drawing.Point(180, 18);
            this.txt_xemdoanhthu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_xemdoanhthu.Name = "txt_xemdoanhthu";
            this.txt_xemdoanhthu.Size = new System.Drawing.Size(426, 35);
            this.txt_xemdoanhthu.TabIndex = 4;
            // 
            // btn_xemdoanhthu
            // 
            this.btn_xemdoanhthu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xemdoanhthu.Location = new System.Drawing.Point(616, 18);
            this.btn_xemdoanhthu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_xemdoanhthu.Name = "btn_xemdoanhthu";
            this.btn_xemdoanhthu.Size = new System.Drawing.Size(126, 40);
            this.btn_xemdoanhthu.TabIndex = 5;
            this.btn_xemdoanhthu.Text = "Xem";
            this.btn_xemdoanhthu.UseVisualStyleBackColor = true;
            this.btn_xemdoanhthu.Click += new System.EventHandler(this.btn_xemdoanhthu_Click);
            // 
            // btn_hd
            // 
            this.btn_hd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_hd.Location = new System.Drawing.Point(18, 68);
            this.btn_hd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_hd.Name = "btn_hd";
            this.btn_hd.Size = new System.Drawing.Size(260, 65);
            this.btn_hd.TabIndex = 22;
            this.btn_hd.Text = "Quản Lí Hóa Đơn";
            this.btn_hd.UseVisualStyleBackColor = true;
            this.btn_hd.Click += new System.EventHandler(this.btn_hd_Click);
            // 
            // btn_timMP
            // 
            this.btn_timMP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_timMP.Location = new System.Drawing.Point(286, 74);
            this.btn_timMP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_timMP.Name = "btn_timMP";
            this.btn_timMP.Size = new System.Drawing.Size(201, 58);
            this.btn_timMP.TabIndex = 28;
            this.btn_timMP.Text = "list(mã phòng)";
            this.btn_timMP.UseVisualStyleBackColor = true;
            this.btn_timMP.Click += new System.EventHandler(this.btn_timMP_Click);
            // 
            // btn_giaima
            // 
            this.btn_giaima.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_giaima.Location = new System.Drawing.Point(286, 222);
            this.btn_giaima.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_giaima.Name = "btn_giaima";
            this.btn_giaima.Size = new System.Drawing.Size(201, 37);
            this.btn_giaima.TabIndex = 29;
            this.btn_giaima.Text = "Hiển thị";
            this.btn_giaima.UseVisualStyleBackColor = true;
            this.btn_giaima.Click += new System.EventHandler(this.btn_giaima_Click);
            // 
            // btn_TaoKhoa
            // 
            this.btn_TaoKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TaoKhoa.Location = new System.Drawing.Point(954, 485);
            this.btn_TaoKhoa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_TaoKhoa.Name = "btn_TaoKhoa";
            this.btn_TaoKhoa.Size = new System.Drawing.Size(213, 69);
            this.btn_TaoKhoa.TabIndex = 30;
            this.btn_TaoKhoa.Text = "Tạo khóa";
            this.btn_TaoKhoa.UseVisualStyleBackColor = true;
            this.btn_TaoKhoa.Click += new System.EventHandler(this.btn_TaoKhoa_Click);
            // 
            // MainFormN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.btn_TaoKhoa);
            this.Controls.Add(this.btn_giaima);
            this.Controls.Add(this.btn_timMP);
            this.Controls.Add(this.btn_hd);
            this.Controls.Add(this.btn_xemdoanhthu);
            this.Controls.Add(this.txt_xemdoanhthu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chontb);
            this.Controls.Add(this.dgv_hienthi);
            this.Controls.Add(this.btnLogout);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainFormN";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainFormN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hienthi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridView dgv_hienthi;
        private System.Windows.Forms.ComboBox chontb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_xemdoanhthu;
        private System.Windows.Forms.Button btn_xemdoanhthu;
        private System.Windows.Forms.Button btn_hd;
        private System.Windows.Forms.Button btn_timMP;
        private System.Windows.Forms.Button btn_giaima;
        private System.Windows.Forms.Button btn_TaoKhoa;
    }
}