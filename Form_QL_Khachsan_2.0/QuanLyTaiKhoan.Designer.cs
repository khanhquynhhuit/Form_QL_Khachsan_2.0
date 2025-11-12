namespace Form_QL_Khachsan_2._0
{
    partial class QuanLyTaiKhoan
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
            this.dgv_thietbi = new System.Windows.Forms.DataGridView();
            this.lb_username = new System.Windows.Forms.Label();
            this.lb_sosession = new System.Windows.Forms.Label();
            this.btn_lammoi = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thietbi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_thietbi
            // 
            this.dgv_thietbi.AccessibleName = "";
            this.dgv_thietbi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_thietbi.Location = new System.Drawing.Point(95, 132);
            this.dgv_thietbi.Name = "dgv_thietbi";
            this.dgv_thietbi.RowHeadersWidth = 62;
            this.dgv_thietbi.Size = new System.Drawing.Size(611, 186);
            this.dgv_thietbi.TabIndex = 2;
            // 
            // lb_username
            // 
            this.lb_username.AutoSize = true;
            this.lb_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_username.Location = new System.Drawing.Point(91, 80);
            this.lb_username.Name = "lb_username";
            this.lb_username.Size = new System.Drawing.Size(117, 24);
            this.lb_username.TabIndex = 3;
            this.lb_username.Text = "Username: ";
            // 
            // lb_sosession
            // 
            this.lb_sosession.AutoSize = true;
            this.lb_sosession.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_sosession.Location = new System.Drawing.Point(91, 104);
            this.lb_sosession.Name = "lb_sosession";
            this.lb_sosession.Size = new System.Drawing.Size(121, 24);
            this.lb_sosession.TabIndex = 4;
            this.lb_sosession.Text = "Số Session:";
            // 
            // btn_lammoi
            // 
            this.btn_lammoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lammoi.Location = new System.Drawing.Point(175, 367);
            this.btn_lammoi.Name = "btn_lammoi";
            this.btn_lammoi.Size = new System.Drawing.Size(122, 35);
            this.btn_lammoi.TabIndex = 5;
            this.btn_lammoi.Text = "Làm mới";
            this.btn_lammoi.UseVisualStyleBackColor = true;
            this.btn_lammoi.Click += new System.EventHandler(this.btn_lammoi_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(409, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(248, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Đăng xuất khỏi thiết bị";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // QuanLyTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_lammoi);
            this.Controls.Add(this.lb_sosession);
            this.Controls.Add(this.lb_username);
            this.Controls.Add(this.dgv_thietbi);
            this.Name = "QuanLyTaiKhoan";
            this.Text = "QuanLyTaiKhoan";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thietbi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_thietbi;
        private System.Windows.Forms.Label lb_username;
        private System.Windows.Forms.Label lb_sosession;
        private System.Windows.Forms.Button btn_lammoi;
        private System.Windows.Forms.Button button1;
    }
}