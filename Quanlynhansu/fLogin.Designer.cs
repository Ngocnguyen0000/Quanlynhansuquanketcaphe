namespace Quanlynhansuquancaphe
{
    partial class fLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLogin));
            panel1 = new Panel();
            buttonExit = new Button();
            buttonLogin = new Button();
            panel3 = new Panel();
            textPass = new TextBox();
            passWord = new Label();
            panel2 = new Panel();
            textUsername = new TextBox();
            userName = new Label();
            TITLENHANVIEN = new Label();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonExit);
            panel1.Controls.Add(buttonLogin);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(324, 185);
            panel1.Name = "panel1";
            panel1.Size = new Size(590, 275);
            panel1.TabIndex = 1;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(322, 221);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(77, 35);
            buttonExit.TabIndex = 4;
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click_1;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(228, 221);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(79, 35);
            buttonLogin.TabIndex = 3;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(textPass);
            panel3.Controls.Add(passWord);
            panel3.Location = new Point(37, 123);
            panel3.Name = "panel3";
            panel3.Size = new Size(529, 74);
            panel3.TabIndex = 2;
            // 
            // textPass
            // 
            textPass.Location = new Point(191, 17);
            textPass.Name = "textPass";
            textPass.Size = new Size(284, 23);
            textPass.TabIndex = 2;
            textPass.Text = "MK6900";
            textPass.UseSystemPasswordChar = true;
            // 
            // passWord
            // 
            passWord.AutoSize = true;
            passWord.FlatStyle = FlatStyle.System;
            passWord.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            passWord.Location = new Point(18, 19);
            passWord.Name = "passWord";
            passWord.Size = new Size(109, 21);
            passWord.TabIndex = 0;
            passWord.Text = "Mật khẩu (*):";
            // 
            // panel2
            // 
            panel2.Controls.Add(textUsername);
            panel2.Controls.Add(userName);
            panel2.Location = new Point(37, 38);
            panel2.Name = "panel2";
            panel2.Size = new Size(529, 79);
            panel2.TabIndex = 0;
            // 
            // textUsername
            // 
            textUsername.Location = new Point(191, 21);
            textUsername.Name = "textUsername";
            textUsername.Size = new Size(284, 23);
            textUsername.TabIndex = 1;
            textUsername.Text = "NV0001";
            // 
            // userName
            // 
            userName.AutoSize = true;
            userName.FlatStyle = FlatStyle.System;
            userName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            userName.Location = new Point(18, 19);
            userName.Name = "userName";
            userName.Size = new Size(150, 21);
            userName.TabIndex = 0;
            userName.Text = "Tên đăng nhập (*):";
            // 
            // TITLENHANVIEN
            // 
            TITLENHANVIEN.AutoSize = true;
            TITLENHANVIEN.BackColor = Color.Transparent;
            TITLENHANVIEN.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TITLENHANVIEN.ForeColor = Color.White;
            TITLENHANVIEN.Location = new Point(464, 91);
            TITLENHANVIEN.Name = "TITLENHANVIEN";
            TITLENHANVIEN.Size = new Size(314, 55);
            TITLENHANVIEN.TabIndex = 15;
            TITLENHANVIEN.Text = "ĐĂNG NHẬP";
            // 
            // fLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1180, 630);
            Controls.Add(TITLENHANVIEN);
            Controls.Add(panel1);
            Name = "fLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PHẦN MỀM QUẢN LÝ NHÂN SỰ QUÁN CÀ PHÊ";
            FormClosing += fLogin_FormClosing_1;
            Load += fLogin_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button buttonExit;
        private Button buttonLogin;
        private Panel panel3;
        private TextBox textPass;
        private Label passWord;
        private Panel panel2;
        private TextBox textUsername;
        private Label userName;
        private Label TITLENHANVIEN;
    }
}