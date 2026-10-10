namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            grpAddUser = new GroupBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            btnCreateUser = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpAddUser.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Left;
            dgvUsers.Location = new Point(0, 0);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(420, 420);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // grpAddUser
            // 
            grpAddUser.Controls.Add(lblUsername);
            grpAddUser.Controls.Add(txtUsername);
            grpAddUser.Controls.Add(lblPassword);
            grpAddUser.Controls.Add(txtPassword);
            grpAddUser.Controls.Add(lblFullName);
            grpAddUser.Controls.Add(txtFullName);
            grpAddUser.Controls.Add(lblRole);
            grpAddUser.Controls.Add(cboRole);
            grpAddUser.Controls.Add(btnCreateUser);
            grpAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpAddUser.Location = new Point(435, 10);
            grpAddUser.Name = "grpAddUser";
            grpAddUser.Size = new Size(285, 400);
            grpAddUser.TabIndex = 1;
            grpAddUser.TabStop = false;
            grpAddUser.Text = "Tạo Tai Khoản Mới";
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(15, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(15, 55);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(250, 25);
            txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(15, 95);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(15, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(250, 25);
            txtPassword.TabIndex = 3;
            // 
            // lblFullName
            // 
            lblFullName.Location = new Point(15, 160);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(100, 23);
            lblFullName.TabIndex = 4;
            lblFullName.Text = "Họ và tên:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(15, 185);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 25);
            txtFullName.TabIndex = 5;
            // 
            // lblRole
            // 
            lblRole.Location = new Point(15, 225);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(100, 23);
            lblRole.TabIndex = 6;
            lblRole.Text = "Quyền hạn:";
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Location = new Point(15, 250);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(250, 25);
            cboRole.TabIndex = 7;
            // 
            // btnCreateUser
            // 
            btnCreateUser.BackColor = Color.MidnightBlue;
            btnCreateUser.ForeColor = Color.White;
            btnCreateUser.Location = new Point(15, 310);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(250, 40);
            btnCreateUser.TabIndex = 8;
            btnCreateUser.Text = "Tạo Người Dùng";
            btnCreateUser.UseVisualStyleBackColor = false;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // FormUserManagement
            // 
            ClientSize = new Size(730, 420);
            Controls.Add(grpAddUser);
            Controls.Add(dgvUsers);
            Name = "FormUserManagement";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Người Dùng";
            Load += FormUserManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpAddUser.ResumeLayout(false);
            grpAddUser.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.GroupBox grpAddUser;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Button btnCreateUser;
    }
}