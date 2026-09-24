namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;

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
            lblUser = new Label();
            lblPass = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(209, 85);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(60, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "Tài khoản:";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(209, 128);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(60, 15);
            lblPass.TabIndex = 1;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(275, 85);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(180, 23);
            txtUser.TabIndex = 0;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(275, 128);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(180, 23);
            txtPass.TabIndex = 1;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(275, 171);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(180, 35);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Đăng nhập hệ thống";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(287, 42);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 3;
            label1.Text = "ĐĂNG NHẬP HỆ THỐNG\r\n";
            label1.Click += label1_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 343);
            Controls.Add(label1);
            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Controls.Add(lblPass);
            Controls.Add(txtPass);
            Controls.Add(btnLogin);
            Name = "FormLogin";
            Text = "Đăng nhập hệ thống";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}