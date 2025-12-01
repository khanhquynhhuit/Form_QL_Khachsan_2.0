namespace Form_QL_Khachsan_2._0
{
    partial class Menu
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
            this.btnQuanLyTK = new System.Windows.Forms.Button();
            this.btn_logout = new System.Windows.Forms.Button();
            this.btn_admin = new System.Windows.Forms.Button();
            this.lb_chaomung = new System.Windows.Forms.Label();
            this.btn_datphong = new System.Windows.Forms.Button();
            this.btnktks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnQuanLyTK
            // 
            this.btnQuanLyTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyTK.Location = new System.Drawing.Point(375, 377);
            this.btnQuanLyTK.Name = "btnQuanLyTK";
            this.btnQuanLyTK.Size = new System.Drawing.Size(226, 45);
            this.btnQuanLyTK.TabIndex = 31;
            this.btnQuanLyTK.Text = "Quản lý tài khoản";
            this.btnQuanLyTK.UseVisualStyleBackColor = true;
            this.btnQuanLyTK.Click += new System.EventHandler(this.btnQuanLyTK_Click);
            // 
            // btn_logout
            // 
            this.btn_logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_logout.Location = new System.Drawing.Point(620, 377);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(142, 45);
            this.btn_logout.TabIndex = 31;
            this.btn_logout.Text = "Đăng xuất";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // btn_admin
            // 
            this.btn_admin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_admin.Location = new System.Drawing.Point(133, 377);
            this.btn_admin.Name = "btn_admin";
            this.btn_admin.Size = new System.Drawing.Size(226, 45);
            this.btn_admin.TabIndex = 32;
            this.btn_admin.Text = "Admin";
            this.btn_admin.UseVisualStyleBackColor = true;
            this.btn_admin.Click += new System.EventHandler(this.btn_admin_Click);
            // 
            // lb_chaomung
            // 
            this.lb_chaomung.AutoSize = true;
            this.lb_chaomung.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_chaomung.Location = new System.Drawing.Point(12, 9);
            this.lb_chaomung.Name = "lb_chaomung";
            this.lb_chaomung.Size = new System.Drawing.Size(283, 55);
            this.lb_chaomung.TabIndex = 33;
            this.lb_chaomung.Text = "Chào mừng";
            // 
            // btn_datphong
            // 
            this.btn_datphong.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_datphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_datphong.Location = new System.Drawing.Point(375, 324);
            this.btn_datphong.Name = "btn_datphong";
            this.btn_datphong.Size = new System.Drawing.Size(134, 38);
            this.btn_datphong.TabIndex = 34;
            this.btn_datphong.Text = "Đặt Phòng ";
            this.btn_datphong.UseVisualStyleBackColor = false;
            this.btn_datphong.Click += new System.EventHandler(this.btn_datphong_Click);
            // 
            // btnktks
            // 
            this.btnktks.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnktks.Location = new System.Drawing.Point(143, 326);
            this.btnktks.Name = "btnktks";
            this.btnktks.Size = new System.Drawing.Size(226, 45);
            this.btnktks.TabIndex = 35;
            this.btnktks.Text = "Kiểm tra kí số";
            this.btnktks.UseVisualStyleBackColor = true;
            this.btnktks.Click += new System.EventHandler(this.btnktks_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnktks);
            this.Controls.Add(this.btn_datphong);
            this.Controls.Add(this.lb_chaomung);
            this.Controls.Add(this.btn_admin);
            this.Controls.Add(this.btn_logout);
            this.Controls.Add(this.btnQuanLyTK);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuanLyTK;
        private System.Windows.Forms.Button btn_logout;
        private System.Windows.Forms.Button btn_admin;
        private System.Windows.Forms.Label lb_chaomung;
        private System.Windows.Forms.Button btn_datphong;
        private System.Windows.Forms.Button btnktks;
    }
}