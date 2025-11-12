namespace Form_QL_Khachsan_2._0
{
    partial class ql_hoadon
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
            this.btn_tim = new System.Windows.Forms.Button();
            this.txt_hd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hien_dl = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.hien_dl)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_mahoa
            // 
            this.btn_mahoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_mahoa.Location = new System.Drawing.Point(352, 107);
            this.btn_mahoa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_mahoa.Name = "btn_mahoa";
            this.btn_mahoa.Size = new System.Drawing.Size(56, 32);
            this.btn_mahoa.TabIndex = 14;
            this.btn_mahoa.UseVisualStyleBackColor = true;
            this.btn_mahoa.Click += new System.EventHandler(this.btn_mahoa_Click);
            // 
            // btn_tim
            // 
            this.btn_tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_tim.Location = new System.Drawing.Point(265, 107);
            this.btn_tim.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tim.Name = "btn_tim";
            this.btn_tim.Size = new System.Drawing.Size(56, 32);
            this.btn_tim.TabIndex = 13;
            this.btn_tim.Text = "Tim";
            this.btn_tim.UseVisualStyleBackColor = true;
            this.btn_tim.Click += new System.EventHandler(this.btn_tim_Click);
            // 
            // txt_hd
            // 
            this.txt_hd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_hd.Location = new System.Drawing.Point(76, 110);
            this.txt_hd.Margin = new System.Windows.Forms.Padding(2);
            this.txt_hd.Name = "txt_hd";
            this.txt_hd.Size = new System.Drawing.Size(178, 26);
            this.txt_hd.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(73, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nhập Mã Hóa Đơn ";
            // 
            // hien_dl
            // 
            this.hien_dl.AllowUserToOrderColumns = true;
            this.hien_dl.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.hien_dl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hien_dl.GridColor = System.Drawing.SystemColors.Menu;
            this.hien_dl.Location = new System.Drawing.Point(62, 168);
            this.hien_dl.Margin = new System.Windows.Forms.Padding(2);
            this.hien_dl.Name = "hien_dl";
            this.hien_dl.RowHeadersWidth = 51;
            this.hien_dl.RowTemplate.Height = 24;
            this.hien_dl.Size = new System.Drawing.Size(676, 232);
            this.hien_dl.TabIndex = 10;
            this.hien_dl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.hien_dl_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(319, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "Quản lí hóa đơn ";
            // 
            // ql_hoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_mahoa);
            this.Controls.Add(this.btn_tim);
            this.Controls.Add(this.txt_hd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hien_dl);
            this.Controls.Add(this.label1);
            this.Name = "ql_hoadon";
            this.Text = "ql_hoadon";
            ((System.ComponentModel.ISupportInitialize)(this.hien_dl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_mahoa;
        private System.Windows.Forms.Button btn_tim;
        private System.Windows.Forms.TextBox txt_hd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView hien_dl;
        private System.Windows.Forms.Label label1;
    }
}