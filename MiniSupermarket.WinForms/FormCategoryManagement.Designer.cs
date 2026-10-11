namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Bố cục mới (toàn bộ dùng Dock / TableLayoutPanel nên tự co giãn theo FormMainShell):
        ///   tlpMain (2 cột x 2 hàng)
        ///     hàng 1: groupBox1 (thanh tìm kiếm) trải hết 2 cột
        ///     hàng 2: cột trái = groupBox2 (lưới + dòng trạng thái), cột phải (340px) = pnlRight (form nhập + nút)
        /// </summary>
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
            dgvCategories = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            lblStatus = new Label();
            pnlRight = new Panel();
            groupBox3 = new GroupBox();
            tlpInfo = new TableLayoutPanel();
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            txtCategoryName = new TextBox();
            label3 = new Label();
            txtDescription = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
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
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340F));
            tlpMain.Controls.Add(groupBox1, 0, 0);
            tlpMain.SetColumnSpan(groupBox1, 2);
            tlpMain.Controls.Add(groupBox2, 0, 1);
            tlpMain.Controls.Add(pnlRight, 1, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(16, 16);
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(948, 528);
            tlpMain.TabIndex = 0;
            //
            // groupBox1 (thanh tìm kiếm)
            //
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(tlpSearch);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Margin = new Padding(0, 0, 0, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(10, 2, 10, 8);
            groupBox1.Size = new Size(948, 64);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm";
            //
            // tlpSearch
            //
            tlpSearch.ColumnCount = 3;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpSearch.Controls.Add(txtKeyword, 0, 0);
            tlpSearch.Controls.Add(btnSearch, 1, 0);
            tlpSearch.Controls.Add(btnLoad, 2, 0);
            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.Name = "tlpSearch";
            tlpSearch.RowCount = 1;
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSearch.Size = new Size(928, 40);
            tlpSearch.TabIndex = 0;
            //
            // txtKeyword
            //
            txtKeyword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.BackColor = Color.FromArgb(249, 250, 251);
            txtKeyword.Font = new Font("Segoe UI", 10.5F);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên nhóm hàng cần tìm, nhấn Enter";
            txtKeyword.Size = new Size(706, 27);
            txtKeyword.TabIndex = 0;
            //
            // btnSearch
            //
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Margin = new Padding(8, 2, 0, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(102, 36);
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
            btnLoad.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLoad.ForeColor = Color.FromArgb(17, 24, 39);
            btnLoad.Margin = new Padding(8, 2, 0, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(92, 36);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            //
            // groupBox2 (danh sách)
            //
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(dgvCategories);
            groupBox2.Controls.Add(lblStatus);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Margin = new Padding(0, 0, 12, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(8, 4, 8, 6);
            groupBox2.Size = new Size(596, 444);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách nhóm hàng";
            //
            // dgvCategories (định dạng chi tiết nằm trong SetupGrid ở FormCategoryManagement.cs)
            //
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colId, colCategoryName, colDescription });
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(580, 380);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            //
            // colId
            //
            colId.DataPropertyName = "CategoryId";
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            //
            // colCategoryName
            //
            colCategoryName.DataPropertyName = "CategoryName";
            colCategoryName.HeaderText = "Tên Nhóm Hàng";
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
            //
            // colDescription
            //
            colDescription.DataPropertyName = "Description";
            colDescription.HeaderText = "Mô tả";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            //
            // lblStatus (dòng trạng thái: số lượng, kết quả tìm kiếm, lỗi nhập liệu...)
            //
            lblStatus.AutoEllipsis = true;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2, 0, 0, 0);
            lblStatus.Size = new Size(580, 30);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Sẵn sàng.";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlRight
            // Thứ tự Add: control add SAU sẽ nằm trên cùng => add nút trước, rồi mới add groupBox3
            //
            pnlRight.Controls.Add(tlpButtons);
            pnlRight.Controls.Add(groupBox3);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Margin = new Padding(0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(340, 444);
            pnlRight.TabIndex = 2;
            //
            // groupBox3 (thông tin nhóm hàng)
            //
            groupBox3.BackColor = Color.White;
            groupBox3.Controls.Add(tlpInfo);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(10, 4, 10, 8);
            groupBox3.Size = new Size(340, 280);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin nhóm hàng";
            //
            // tlpInfo
            //
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInfo.Controls.Add(label1, 0, 0);
            tlpInfo.Controls.Add(txtId, 0, 1);
            tlpInfo.Controls.Add(label2, 0, 2);
            tlpInfo.Controls.Add(txtCategoryName, 0, 3);
            tlpInfo.Controls.Add(label3, 0, 4);
            tlpInfo.Controls.Add(txtDescription, 0, 5);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 6;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInfo.Size = new Size(320, 248);
            tlpInfo.TabIndex = 0;
            //
            // label1
            //
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(75, 85, 99);
            label1.Name = "label1";
            label1.Text = "Mã ID (tự sinh)";
            label1.TextAlign = ContentAlignment.BottomLeft;
            //
            // txtId
            //
            txtId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtId.BackColor = Color.FromArgb(243, 244, 246);
            txtId.Font = new Font("Segoe UI", 10.5F);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.TabIndex = 0;
            txtId.TabStop = false;
            //
            // label2
            //
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(75, 85, 99);
            label2.Name = "label2";
            label2.Text = "Tên nhóm hàng (*)";
            label2.TextAlign = ContentAlignment.BottomLeft;
            //
            // txtCategoryName
            //
            txtCategoryName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCategoryName.BackColor = Color.FromArgb(249, 250, 251);
            txtCategoryName.Font = new Font("Segoe UI", 10.5F);
            txtCategoryName.MaxLength = 100;
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.PlaceholderText = "Tối đa 100 ký tự";
            txtCategoryName.TabIndex = 1;
            //
            // label3
            //
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(75, 85, 99);
            label3.Name = "label3";
            label3.Text = "Mô tả";
            label3.TextAlign = ContentAlignment.BottomLeft;
            //
            // txtDescription
            //
            txtDescription.BackColor = Color.FromArgb(249, 250, 251);
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("Segoe UI", 10.5F);
            txtDescription.MaxLength = 255;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Tối đa 255 ký tự";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.TabIndex = 2;
            //
            // tlpButtons (lưới nút 2x2)
            //
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.Controls.Add(btnAdd, 0, 0);
            tlpButtons.Controls.Add(btnUpdate, 1, 0);
            tlpButtons.Controls.Add(btnDelete, 0, 1);
            tlpButtons.Controls.Add(btnClear, 1, 1);
            tlpButtons.Dock = DockStyle.Top;
            tlpButtons.Name = "tlpButtons";
            tlpButtons.Padding = new Padding(0, 12, 0, 0);
            tlpButtons.RowCount = 2;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpButtons.Size = new Size(340, 110);
            tlpButtons.TabIndex = 0;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Margin = new Padding(0, 0, 5, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.TabIndex = 0;
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
            btnUpdate.Margin = new Padding(5, 0, 0, 8);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete (nút viền đỏ, ít "nặng" hơn để tránh bấm nhầm)
            //
            btnDelete.BackColor = Color.White;
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.FromArgb(220, 38, 38);
            btnDelete.Margin = new Padding(0, 0, 5, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.TabIndex = 2;
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
            btnClear.Margin = new Padding(5, 0, 0, 8);
            btnClear.Name = "btnClear";
            btnClear.TabIndex = 3;
            btnClear.Text = "Nhập mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            //
            // errorProvider1 (hiện dấu ! đỏ cạnh ô nhập sai)
            //
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            //
            // FormCategoryManagement
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(980, 560);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "FormCategoryManagement";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý nhóm hàng";
            Load += FormCategoryManagement_Load;
            tlpMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
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
        private Button btnSearch;
        private Button btnLoad;
        private GroupBox groupBox2;
        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;
        private Label lblStatus;
        private Panel pnlRight;
        private GroupBox groupBox3;
        private TableLayoutPanel tlpInfo;
        private Label label1;
        private TextBox txtId;
        private Label label2;
        private TextBox txtCategoryName;
        private Label label3;
        private TextBox txtDescription;
        private TableLayoutPanel tlpButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private ErrorProvider errorProvider1;
    }
}