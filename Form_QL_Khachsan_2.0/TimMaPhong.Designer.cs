namespace Form_QL_Khachsan_2._0
{
    partial class TimMaPhong
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
            this.btn_mahoa = new System.Windows.Forms.Button();
            this.txt_maP = new System.Windows.Forms.TextBox();
            this.dgv_hienthi = new System.Windows.Forms.DataGridView();
            this.btn_tim = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hienthi)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_mahoa
            // 
            this.btn_mahoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_mahoa.Location = new System.Drawing.Point(564, 83);
            this.btn_mahoa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_mahoa.Name = "btn_mahoa";
            this.btn_mahoa.Size = new System.Drawing.Size(142, 32);
            this.btn_mahoa.TabIndex = 14;
            this.btn_mahoa.UseVisualStyleBackColor = true;
            this.btn_mahoa.Click += new System.EventHandler(this.btn_mahoa_Click);
            // 
            // txt_maP
            // 
            this.txt_maP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maP.Location = new System.Drawing.Point(237, 88);
            this.txt_maP.Margin = new System.Windows.Forms.Padding(2);
            this.txt_maP.Name = "txt_maP";
            this.txt_maP.Size = new System.Drawing.Size(191, 24);
            this.txt_maP.TabIndex = 13;
            // 
            // dgv_hienthi
            // 
            this.dgv_hienthi.AccessibleName = "";
            this.dgv_hienthi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_hienthi.Location = new System.Drawing.Point(78, 162);
            this.dgv_hienthi.Name = "dgv_hienthi";
            this.dgv_hienthi.RowHeadersWidth = 62;
            this.dgv_hienthi.Size = new System.Drawing.Size(628, 207);
            this.dgv_hienthi.TabIndex = 12;
            this.dgv_hienthi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_hienthi_CellContentClick);
            // 
            // btn_tim
            // 
            this.btn_tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tim.Location = new System.Drawing.Point(454, 82);
            this.btn_tim.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tim.Name = "btn_tim";
            this.btn_tim.Size = new System.Drawing.Size(88, 51);
            this.btn_tim.TabIndex = 11;
            this.btn_tim.Text = "Tìm";
            this.btn_tim.UseVisualStyleBackColor = true;
            this.btn_tim.Click += new System.EventHandler(this.btn_tim_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nhập Mã Phòng";
            // 
            // TimMaPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_mahoa);
            this.Controls.Add(this.txt_maP);
            this.Controls.Add(this.dgv_hienthi);
            this.Controls.Add(this.btn_tim);
            this.Controls.Add(this.label1);
            this.Name = "TimMaPhong";
            this.Text = "TimMaPhong";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hienthi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_mahoa;
        private System.Windows.Forms.TextBox txt_maP;
        private System.Windows.Forms.DataGridView dgv_hienthi;
        private System.Windows.Forms.Button btn_tim;
        private System.Windows.Forms.Label label1;
    }
}