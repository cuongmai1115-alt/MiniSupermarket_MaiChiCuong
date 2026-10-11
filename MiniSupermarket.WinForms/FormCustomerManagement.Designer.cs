namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            btnSearch = new Button();
            btnLoad = new Button();
            groupBox2 = new GroupBox();
            dgvCustomers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colPoints = new DataGridViewTextBoxColumn();
            colRank = new DataGridViewTextBoxColumn();
            lblStatus = new Label();
            pnlRight = new TableLayoutPanel();
            groupBox3 = new GroupBox();
            tlpInfo = new TableLayoutPanel();
            lblId = new Label();
            txtId = new TextBox();
            lblName = new Label();
            txtCustomerName = new TextBox();
            lblPhone = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPoints = new Label();
            numPoints = new NumericUpDown();
            lblRank = new Label();
            cbRank = new ComboBox();
            tlpButtons = new TableLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            errorProvider1 = new ErrorProvider(components);
            tlpMain.SuspendLayout();
            groupBox1.SuspendLayout();
            tlpSearch.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            pnlRight.SuspendLayout();
            groupBox3.SuspendLayout();
            tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).BeginInit();
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
            tlpSearch.ColumnCount = 3;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tlpSearch.Controls.Add(txtKeyword, 0, 0);
            tlpSearch.Controls.Add(btnSearch, 1, 0);
            tlpSearch.Controls.Add(btnLoad, 2, 0);
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
            txtKeyword.PlaceholderText = "Nhập tên hoặc số điện thoại khách hàng cần tìm, nhấn Enter";
            txtKeyword.Size = new Size(953, 31);
            txtKeyword.TabIndex = 0;
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
            btnSearch.TabIndex = 1;
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
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(dgvCustomers);
            groupBox2.Controls.Add(lblStatus);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 99);
            groupBox2.Margin = new Padding(0, 0, 14, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(9, 5, 9, 8);
            groupBox2.Size = new Size(818, 686);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách khách hàng";
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.ColumnHeadersHeight = 29;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colPhone, colAddress, colPoints, colRank });
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(9, 25);
            dgvCustomers.Margin = new Padding(3, 4, 3, 4);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(800, 613);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            dgvCustomers.CellFormatting += dgvCustomers_CellFormatting;
            // 
            // colId
            // 
            colId.DataPropertyName = "CustomerId";
            colId.HeaderText = "Mã KH";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 125;
            // 
            // colName
            // 
            colName.DataPropertyName = "CustomerName";
            colName.HeaderText = "Tên khách hàng";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Width = 125;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "PhoneNumber";
            colPhone.HeaderText = "Số điện thoại";
            colPhone.MinimumWidth = 6;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            colPhone.Width = 125;
            // 
            // colAddress
            // 
            colAddress.DataPropertyName = "Address";
            colAddress.HeaderText = "Địa chỉ";
            colAddress.MinimumWidth = 6;
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            colAddress.Width = 125;
            // 
            // colPoints
            // 
            colPoints.DataPropertyName = "RewardPoints";
            colPoints.HeaderText = "Điểm thưởng";
            colPoints.MinimumWidth = 6;
            colPoints.Name = "colPoints";
            colPoints.ReadOnly = true;
            colPoints.Width = 125;
            // 
            // colRank
            // 
            colRank.DataPropertyName = "MembershipRank";
            colRank.HeaderText = "Hạng thẻ";
            colRank.MinimumWidth = 6;
            colRank.Name = "colRank";
            colRank.ReadOnly = true;
            colRank.Width = 125;
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
            pnlRight.AutoScroll = true;
            pnlRight.AutoScrollMinSize = new Size(0, 540);
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
            pnlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 149F));
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
            groupBox3.Size = new Size(389, 537);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin khách hàng";
            // 
            // tlpInfo
            // 
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInfo.Controls.Add(lblId, 0, 0);
            tlpInfo.Controls.Add(txtId, 0, 1);
            tlpInfo.Controls.Add(lblName, 0, 2);
            tlpInfo.Controls.Add(txtCustomerName, 0, 3);
            tlpInfo.Controls.Add(lblPhone, 0, 4);
            tlpInfo.Controls.Add(txtPhoneNumber, 0, 5);
            tlpInfo.Controls.Add(lblAddress, 0, 6);
            tlpInfo.Controls.Add(txtAddress, 0, 7);
            tlpInfo.Controls.Add(lblPoints, 0, 8);
            tlpInfo.Controls.Add(numPoints, 0, 9);
            tlpInfo.Controls.Add(lblRank, 0, 10);
            tlpInfo.Controls.Add(cbRank, 0, 11);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Location = new Point(11, 25);
            tlpInfo.Margin = new Padding(3, 4, 3, 4);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 12;
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
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            tlpInfo.Size = new Size(367, 501);
            tlpInfo.TabIndex = 0;
            // 
            // lblId
            // 
            lblId.Dock = DockStyle.Fill;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(75, 85, 99);
            lblId.Location = new Point(3, 0);
            lblId.Name = "lblId";
            lblId.Size = new Size(361, 31);
            lblId.TabIndex = 0;
            lblId.Text = "Mã KH (tự sinh)";
            lblId.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtId
            // 
            txtId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtId.BackColor = Color.FromArgb(243, 244, 246);
            txtId.Font = new Font("Segoe UI", 10.5F);
            txtId.Location = new Point(3, 41);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(361, 31);
            txtId.TabIndex = 0;
            txtId.TabStop = false;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(75, 85, 99);
            lblName.Location = new Point(3, 82);
            lblName.Name = "lblName";
            lblName.Size = new Size(361, 31);
            lblName.TabIndex = 1;
            lblName.Text = "Tên khách hàng (*)";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerName.BackColor = Color.FromArgb(249, 250, 251);
            txtCustomerName.Font = new Font("Segoe UI", 10.5F);
            txtCustomerName.Location = new Point(3, 123);
            txtCustomerName.Margin = new Padding(3, 4, 3, 4);
            txtCustomerName.MaxLength = 100;
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.PlaceholderText = "Tối đa 100 ký tự";
            txtCustomerName.Size = new Size(361, 31);
            txtCustomerName.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.Dock = DockStyle.Fill;
            lblPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(75, 85, 99);
            lblPhone.Location = new Point(3, 164);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(361, 31);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "Số điện thoại (*)";
            lblPhone.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPhoneNumber.BackColor = Color.FromArgb(249, 250, 251);
            txtPhoneNumber.Font = new Font("Segoe UI", 10.5F);
            txtPhoneNumber.Location = new Point(3, 205);
            txtPhoneNumber.Margin = new Padding(3, 4, 3, 4);
            txtPhoneNumber.MaxLength = 15;
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Ví dụ: 0901234567";
            txtPhoneNumber.Size = new Size(361, 31);
            txtPhoneNumber.TabIndex = 2;
            // 
            // lblAddress
            // 
            lblAddress.Dock = DockStyle.Fill;
            lblAddress.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(75, 85, 99);
            lblAddress.Location = new Point(3, 246);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(361, 31);
            lblAddress.TabIndex = 3;
            lblAddress.Text = "Địa chỉ";
            lblAddress.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtAddress
            // 
            txtAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAddress.BackColor = Color.FromArgb(249, 250, 251);
            txtAddress.Font = new Font("Segoe UI", 10.5F);
            txtAddress.Location = new Point(3, 287);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.MaxLength = 200;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Tối đa 200 ký tự";
            txtAddress.Size = new Size(361, 31);
            txtAddress.TabIndex = 3;
            // 
            // lblPoints
            // 
            lblPoints.Dock = DockStyle.Fill;
            lblPoints.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPoints.ForeColor = Color.FromArgb(75, 85, 99);
            lblPoints.Location = new Point(3, 328);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(361, 31);
            lblPoints.TabIndex = 4;
            lblPoints.Text = "Điểm thưởng";
            lblPoints.TextAlign = ContentAlignment.BottomLeft;
            // 
            // numPoints
            // 
            numPoints.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numPoints.Font = new Font("Segoe UI", 10.5F);
            numPoints.Location = new Point(3, 369);
            numPoints.Margin = new Padding(3, 4, 3, 4);
            numPoints.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            numPoints.Name = "numPoints";
            numPoints.Size = new Size(361, 31);
            numPoints.TabIndex = 4;
            numPoints.ThousandsSeparator = true;
            // 
            // lblRank
            // 
            lblRank.Dock = DockStyle.Fill;
            lblRank.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRank.ForeColor = Color.FromArgb(75, 85, 99);
            lblRank.Location = new Point(3, 410);
            lblRank.Name = "lblRank";
            lblRank.Size = new Size(361, 31);
            lblRank.TabIndex = 5;
            lblRank.Text = "Hạng thẻ";
            lblRank.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cbRank
            // 
            cbRank.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbRank.BackColor = Color.FromArgb(249, 250, 251);
            cbRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRank.FlatStyle = FlatStyle.Flat;
            cbRank.Font = new Font("Segoe UI", 10.5F);
            cbRank.Items.AddRange(new object[] { "Chuẩn", "Bạc", "Vàng" });
            cbRank.Location = new Point(3, 454);
            cbRank.Margin = new Padding(3, 4, 3, 4);
            cbRank.Name = "cbRank";
            cbRank.Size = new Size(361, 31);
            cbRank.TabIndex = 5;
            // 
            // tlpButtons
            // 
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.Controls.Add(btnAdd, 0, 0);
            tlpButtons.Controls.Add(btnUpdate, 1, 0);
            tlpButtons.Controls.Add(btnDelete, 0, 1);
            tlpButtons.Controls.Add(btnClear, 1, 1);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Location = new Point(0, 537);
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.Padding = new Padding(0, 16, 0, 0);
            tlpButtons.RowCount = 2;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tlpButtons.Size = new Size(389, 149);
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
            btnAdd.Location = new Point(0, 16);
            btnAdd.Margin = new Padding(0, 0, 6, 11);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(188, 53);
            btnAdd.TabIndex = 6;
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
            btnUpdate.Location = new Point(200, 16);
            btnUpdate.Margin = new Padding(6, 0, 0, 11);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(189, 53);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.FromArgb(220, 38, 38);
            btnDelete.Location = new Point(0, 80);
            btnDelete.Margin = new Padding(0, 0, 6, 11);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(188, 58);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Dock = DockStyle.Fill;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(17, 24, 39);
            btnClear.Location = new Point(200, 80);
            btnClear.Margin = new Padding(6, 0, 0, 11);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(189, 58);
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
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1257, 827);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormCustomerManagement";
            Padding = new Padding(18, 21, 18, 21);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý khách hàng";
            Load += FormCustomerManagement_Load;
            tlpMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlRight.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tlpInfo.ResumeLayout(false);
            tlpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).EndInit();
            tlpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private GroupBox groupBox1;
        private TableLayoutPanel tlpSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private GroupBox groupBox2;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colAddress;
        private DataGridViewTextBoxColumn colPoints;
        private DataGridViewTextBoxColumn colRank;
        private Label lblStatus;
        private TableLayoutPanel pnlRight;
        private GroupBox groupBox3;
        private TableLayoutPanel tlpInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblName;
        private TextBox txtCustomerName;
        private Label lblPhone;
        private TextBox txtPhoneNumber;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPoints;
        private NumericUpDown numPoints;
        private Label lblRank;
        private ComboBox cbRank;
        private TableLayoutPanel tlpButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private ErrorProvider errorProvider1;
    }
}