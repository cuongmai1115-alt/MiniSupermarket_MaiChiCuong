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
            components = new System.ComponentModel.Container();
            tlpMain = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tlpSearch = new TableLayoutPanel();
            txtKeyword = new TextBox();
            cbFilterRole = new ComboBox();
            btnSearch = new Button();
            btnLoad = new Button();
            groupBox2 = new GroupBox();
            dgvUsers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colFullName = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            lblStatus = new Label();
            pnlRight = new TableLayoutPanel();
            groupBox3 = new GroupBox();
            tlpInfo = new TableLayoutPanel();
            lblId = new Label();
            txtId = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            cbRole = new ComboBox();
            tlpButtons = new TableLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnToggleLock = new Button();
            btnResetPassword = new Button();
            btnClear = new Button();
            errorProvider1 = new ErrorProvider(components);
            tlpMain.SuspendLayout();
            groupBox1.SuspendLayout();
            tlpSearch.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            pnlRight.SuspendLayout();
            groupBox3.SuspendLayout();
            tlpInfo.SuspendLayout();
            tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 389F));
            tlpMain.Controls.Add(groupBox1, 0, 0);
            tlpMain.Controls.Add(groupBox2, 0, 1);
            tlpMain.Controls.Add(pnlRight, 1, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(18, 21);
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 99F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1221, 785);
            tlpMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            tlpMain.SetColumnSpan(groupBox1, 2);
            groupBox1.Controls.Add(tlpSearch);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(0, 0, 0, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(11, 3, 11, 11);
            groupBox1.Size = new Size(1221, 86);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm";
            // 
            // tlpSearch
            // 
            tlpSearch.ColumnCount = 4;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tlpSearch.Controls.Add(txtKeyword, 0, 0);
            tlpSearch.Controls.Add(cbFilterRole, 1, 0);
            tlpSearch.Controls.Add(btnSearch, 2, 0);
            tlpSearch.Controls.Add(btnLoad, 3, 0);
            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.Location = new Point(11, 23);
            tlpSearch.Margin = new Padding(3, 4, 3, 4);
            tlpSearch.Name = "tlpSearch";
            tlpSearch.RowCount = 1;
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSearch.Size = new Size(1199, 52);
            tlpSearch.TabIndex = 0;
            // 
            // txtKeyword
            // 
            txtKeyword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.BackColor = Color.FromArgb(249, 250, 251);
            txtKeyword.Font = new Font("Segoe UI", 10.5F);
            txtKeyword.Location = new Point(3, 10);
            txtKeyword.Margin = new Padding(3, 4, 3, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên đăng nhập hoặc họ tên cần tìm, nhấn Enter";
            txtKeyword.Size = new Size(782, 31);
            txtKeyword.TabIndex = 0;
            // 
            // cbFilterRole
            // 
            cbFilterRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbFilterRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilterRole.Font = new Font("Segoe UI", 10.5F);
            cbFilterRole.Location = new Point(797, 9);
            cbFilterRole.Margin = new Padding(9, 4, 0, 4);
            cbFilterRole.Name = "cbFilterRole";
            cbFilterRole.Size = new Size(162, 31);
            cbFilterRole.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(968, 3);
            btnSearch.Margin = new Padding(9, 3, 0, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(117, 46);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.White;
            btnLoad.Dock = DockStyle.Fill;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLoad.ForeColor = Color.FromArgb(17, 24, 39);
            btnLoad.Location = new Point(1094, 3);
            btnLoad.Margin = new Padding(9, 3, 0, 3);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(105, 46);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(dgvUsers);
            groupBox2.Controls.Add(lblStatus);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 99);
            groupBox2.Margin = new Padding(0, 0, 14, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(9, 5, 9, 8);
            groupBox2.Size = new Size(818, 686);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách tài khoản";
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.ColumnHeadersHeight = 29;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { colId, colUsername, colFullName, colRole, colStatus });
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(9, 25);
            dgvUsers.Margin = new Padding(3, 4, 3, 4);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(800, 613);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            dgvUsers.CellFormatting += dgvUsers_CellFormatting;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Mã";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 125;
            // 
            // colUsername
            // 
            colUsername.DataPropertyName = "Username";
            colUsername.HeaderText = "Tên đăng nhập";
            colUsername.MinimumWidth = 6;
            colUsername.Name = "colUsername";
            colUsername.ReadOnly = true;
            colUsername.Width = 125;
            // 
            // colFullName
            // 
            colFullName.DataPropertyName = "FullName";
            colFullName.HeaderText = "Họ và tên";
            colFullName.MinimumWidth = 6;
            colFullName.Name = "colFullName";
            colFullName.ReadOnly = true;
            colFullName.Width = 125;
            // 
            // colRole
            // 
            colRole.DataPropertyName = "Role";
            colRole.HeaderText = "Quyền hạn";
            colRole.MinimumWidth = 6;
            colRole.Name = "colRole";
            colRole.ReadOnly = true;
            colRole.Width = 125;
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "StatusText";
            colStatus.HeaderText = "Trạng thái";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 125;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Location = new Point(9, 638);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2, 0, 0, 0);
            lblStatus.Size = new Size(800, 40);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Sẵn sàng.";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlRight
            // 
            pnlRight.ColumnCount = 1;
            pnlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlRight.Controls.Add(groupBox3, 0, 0);
            pnlRight.Controls.Add(tlpButtons, 0, 1);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(832, 99);
            pnlRight.Margin = new Padding(0);
            pnlRight.Name = "pnlRight";
            pnlRight.RowCount = 2;
            pnlRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            pnlRight.Size = new Size(389, 686);
            pnlRight.TabIndex = 2;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.White;
            groupBox3.Controls.Add(tlpInfo);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Margin = new Padding(0);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(11, 5, 11, 11);
            groupBox3.Size = new Size(389, 486);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin tài khoản";
            // 
            // tlpInfo
            // 
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInfo.Controls.Add(lblId, 0, 0);
            tlpInfo.Controls.Add(txtId, 0, 1);
            tlpInfo.Controls.Add(lblUsername, 0, 2);
            tlpInfo.Controls.Add(txtUsername, 0, 3);
            tlpInfo.Controls.Add(lblPassword, 0, 4);
            tlpInfo.Controls.Add(txtPassword, 0, 5);
            tlpInfo.Controls.Add(lblFullName, 0, 6);
            tlpInfo.Controls.Add(txtFullName, 0, 7);
            tlpInfo.Controls.Add(lblRole, 0, 8);
            tlpInfo.Controls.Add(cbRole, 0, 9);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Location = new Point(11, 25);
            tlpInfo.Margin = new Padding(3, 4, 3, 4);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 10;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.Size = new Size(367, 450);
            tlpInfo.TabIndex = 0;
            // 
            // lblId
            // 
            lblId.Dock = DockStyle.Fill;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(75, 85, 99);
            lblId.Location = new Point(3, 0);
            lblId.Name = "lblId";
            lblId.Size = new Size(361, 34);
            lblId.TabIndex = 0;
            lblId.Text = "Mã tài khoản (tự sinh)";
            lblId.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtId
            // 
            txtId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtId.BackColor = Color.FromArgb(243, 244, 246);
            txtId.Font = new Font("Segoe UI", 10.5F);
            txtId.Location = new Point(3, 46);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(361, 31);
            txtId.TabIndex = 1;
            txtId.TabStop = false;
            // 
            // lblUsername
            // 
            lblUsername.Dock = DockStyle.Fill;
            lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(75, 85, 99);
            lblUsername.Location = new Point(3, 89);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(361, 34);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Tên đăng nhập (*)";
            lblUsername.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Font = new Font("Segoe UI", 10.5F);
            txtUsername.Location = new Point(3, 135);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(361, 31);
            txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(75, 85, 99);
            lblPassword.Location = new Point(3, 178);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(361, 34);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu (*, tối thiểu 6 ký tự)";
            lblPassword.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 10.5F);
            txtPassword.Location = new Point(3, 224);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(361, 31);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblFullName
            // 
            lblFullName.Dock = DockStyle.Fill;
            lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(75, 85, 99);
            lblFullName.Location = new Point(3, 267);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(361, 34);
            lblFullName.TabIndex = 4;
            lblFullName.Text = "Họ và tên (*)";
            lblFullName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtFullName
            // 
            txtFullName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFullName.Font = new Font("Segoe UI", 10.5F);
            txtFullName.Location = new Point(3, 313);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.MaxLength = 100;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(361, 31);
            txtFullName.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.Dock = DockStyle.Fill;
            lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(75, 85, 99);
            lblRole.Location = new Point(3, 356);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(361, 34);
            lblRole.TabIndex = 5;
            lblRole.Text = "Quyền hạn (*)";
            lblRole.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cbRole
            // 
            cbRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.Font = new Font("Segoe UI", 10.5F);
            cbRole.Location = new Point(3, 403);
            cbRole.Margin = new Padding(3, 4, 3, 4);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(361, 31);
            cbRole.TabIndex = 4;
            // 
            // tlpButtons
            // 
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.Controls.Add(btnAdd, 0, 0);
            tlpButtons.Controls.Add(btnUpdate, 1, 0);
            tlpButtons.Controls.Add(btnToggleLock, 0, 1);
            tlpButtons.Controls.Add(btnResetPassword, 1, 1);
            tlpButtons.Controls.Add(btnClear, 0, 2);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Location = new Point(0, 486);
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.Padding = new Padding(0, 11, 0, 0);
            tlpButtons.RowCount = 3;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpButtons.Size = new Size(389, 200);
            tlpButtons.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(0, 11);
            btnAdd.Margin = new Padding(0, 0, 6, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(188, 48);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(200, 11);
            btnUpdate.Margin = new Padding(6, 0, 0, 8);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(189, 48);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnToggleLock
            // 
            btnToggleLock.BackColor = Color.White;
            btnToggleLock.Dock = DockStyle.Fill;
            btnToggleLock.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnToggleLock.FlatStyle = FlatStyle.Flat;
            btnToggleLock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnToggleLock.ForeColor = Color.FromArgb(220, 38, 38);
            btnToggleLock.Location = new Point(0, 67);
            btnToggleLock.Margin = new Padding(0, 0, 6, 8);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(188, 48);
            btnToggleLock.TabIndex = 7;
            btnToggleLock.Text = "Khóa";
            btnToggleLock.UseVisualStyleBackColor = false;
            btnToggleLock.Click += btnToggleLock_Click;
            // 
            // btnResetPassword
            // 
            btnResetPassword.BackColor = Color.White;
            btnResetPassword.Dock = DockStyle.Fill;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnResetPassword.ForeColor = Color.FromArgb(17, 24, 39);
            btnResetPassword.Location = new Point(200, 67);
            btnResetPassword.Margin = new Padding(6, 0, 0, 8);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(189, 48);
            btnResetPassword.TabIndex = 8;
            btnResetPassword.Text = "Đặt lại MK";
            btnResetPassword.UseVisualStyleBackColor = false;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            tlpButtons.SetColumnSpan(btnClear, 2);
            btnClear.Dock = DockStyle.Fill;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(17, 24, 39);
            btnClear.Location = new Point(0, 123);
            btnClear.Margin = new Padding(0, 0, 0, 8);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(389, 69);
            btnClear.TabIndex = 9;
            btnClear.Text = "Nhập mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1257, 827);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormUserManagement";
            Padding = new Padding(18, 21, 18, 21);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài khoản";
            Load += FormUserManagement_Load;
            tlpMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            pnlRight.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tlpInfo.ResumeLayout(false);
            tlpInfo.PerformLayout();
            tlpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private TableLayoutPanel tlpMain;
        private GroupBox groupBox1;
        private TableLayoutPanel tlpSearch;
        private TextBox txtKeyword;
        private ComboBox cbFilterRole;
        private Button btnSearch;
        private Button btnLoad;
        private GroupBox groupBox2;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewTextBoxColumn colRole;
        private DataGridViewTextBoxColumn colStatus;
        private Label lblStatus;
        private TableLayoutPanel pnlRight;
        private GroupBox groupBox3;
        private TableLayoutPanel tlpInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cbRole;
        private TableLayoutPanel tlpButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnToggleLock;
        private Button btnResetPassword;
        private Button btnClear;
        private ErrorProvider errorProvider1;
    }
}