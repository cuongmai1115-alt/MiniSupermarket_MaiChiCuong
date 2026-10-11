namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            dgvProducts = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBarcode = new DataGridViewTextBoxColumn();
            colProductName = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            lblStatus = new Label();
            pnlRight = new TableLayoutPanel();
            groupBox3 = new GroupBox();
            tlpInfo = new TableLayoutPanel();
            lblId = new Label();
            txtId = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblName = new Label();
            txtProductName = new TextBox();
            lblCategory = new Label();
            cbCategory = new ComboBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblStock = new Label();
            numStock = new NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnlRight.SuspendLayout();
            groupBox3.SuspendLayout();
            tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
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
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            tlpSearch.TabIndex = 0;
            //
            // txtKeyword
            //
            txtKeyword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.BackColor = Color.FromArgb(249, 250, 251);
            txtKeyword.Font = new Font("Segoe UI", 10.5F);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập mã vạch hoặc tên sản phẩm cần tìm, nhấn Enter";
            txtKeyword.TabIndex = 0;
            //
            // btnSearch
            //
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Margin = new Padding(8, 2, 0, 2);
            btnSearch.Name = "btnSearch";
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
            btnLoad.Margin = new Padding(8, 2, 0, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // groupBox2 (danh sách)
            //
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(dgvProducts);
            groupBox2.Controls.Add(lblStatus);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Margin = new Padding(0, 0, 12, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(8, 4, 8, 6);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách sản phẩm";
            //
            // dgvProducts (định dạng chi tiết nằm trong SetupGrid ở FormProductManagement.cs)
            //
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colId, colBarcode, colProductName, colCategory, colPrice, colStock });
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;
            //
            // colId
            //
            colId.DataPropertyName = "ProductId";
            colId.HeaderText = "Mã SP";
            colId.Name = "colId";
            colId.ReadOnly = true;
            //
            // colBarcode
            //
            colBarcode.DataPropertyName = "Barcode";
            colBarcode.HeaderText = "Mã vạch";
            colBarcode.Name = "colBarcode";
            colBarcode.ReadOnly = true;
            //
            // colProductName
            //
            colProductName.DataPropertyName = "ProductName";
            colProductName.HeaderText = "Tên sản phẩm";
            colProductName.Name = "colProductName";
            colProductName.ReadOnly = true;
            //
            // colCategory
            //
            colCategory.DataPropertyName = "CategoryName";
            colCategory.HeaderText = "Danh mục";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            //
            // colPrice
            //
            colPrice.DataPropertyName = "Price";
            colPrice.HeaderText = "Đơn giá (đ)";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            //
            // colStock
            //
            colStock.DataPropertyName = "StockQuantity";
            colStock.HeaderText = "Tồn kho";
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            //
            // lblStatus (dòng trạng thái: số lượng, kết quả tìm kiếm, cảnh báo, lỗi...)
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
            // pnlRight: hàng 0 = thông tin (co giãn theo chiều cao), hàng 1 = lưới nút (cố định)
            //
            pnlRight.AutoScroll = true;
            pnlRight.AutoScrollMinSize = new Size(0, 540);
            pnlRight.ColumnCount = 1;
            pnlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlRight.Controls.Add(groupBox3, 0, 0);
            pnlRight.Controls.Add(tlpButtons, 0, 1);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Margin = new Padding(0);
            pnlRight.Name = "pnlRight";
            pnlRight.RowCount = 2;
            pnlRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            pnlRight.TabIndex = 2;
            //
            // groupBox3 (thông tin sản phẩm)
            //
            groupBox3.BackColor = Color.White;
            groupBox3.Controls.Add(tlpInfo);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Margin = new Padding(0);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(10, 4, 10, 8);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin sản phẩm";
            //
            // tlpInfo
            //
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInfo.Controls.Add(lblId, 0, 0);
            tlpInfo.Controls.Add(txtId, 0, 1);
            tlpInfo.Controls.Add(lblBarcode, 0, 2);
            tlpInfo.Controls.Add(txtBarcode, 0, 3);
            tlpInfo.Controls.Add(lblName, 0, 4);
            tlpInfo.Controls.Add(txtProductName, 0, 5);
            tlpInfo.Controls.Add(lblCategory, 0, 6);
            tlpInfo.Controls.Add(cbCategory, 0, 7);
            tlpInfo.Controls.Add(lblPrice, 0, 8);
            tlpInfo.Controls.Add(txtPrice, 0, 9);
            tlpInfo.Controls.Add(lblStock, 0, 10);
            tlpInfo.Controls.Add(numStock, 0, 11);
            tlpInfo.Dock = DockStyle.Fill;
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
            tlpInfo.TabIndex = 0;
            //
            // lblId
            //
            lblId.Dock = DockStyle.Fill;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(75, 85, 99);
            lblId.Name = "lblId";
            lblId.Text = "Mã SP (tự sinh)";
            lblId.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblBarcode
            //
            lblBarcode.Dock = DockStyle.Fill;
            lblBarcode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.FromArgb(75, 85, 99);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Text = "Mã vạch (*)";
            lblBarcode.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblName
            //
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(75, 85, 99);
            lblName.Name = "lblName";
            lblName.Text = "Tên sản phẩm (*)";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblCategory
            //
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(75, 85, 99);
            lblCategory.Name = "lblCategory";
            lblCategory.Text = "Danh mục (*)";
            lblCategory.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblPrice
            //
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblPrice.Name = "lblPrice";
            lblPrice.Text = "Đơn giá (đồng) (*)";
            lblPrice.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblStock
            //
            lblStock.Dock = DockStyle.Fill;
            lblStock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStock.ForeColor = Color.FromArgb(75, 85, 99);
            lblStock.Name = "lblStock";
            lblStock.Text = "Số lượng tồn";
            lblStock.TextAlign = ContentAlignment.BottomLeft;
            //
            // txtId
            //
            txtId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtId.BackColor = Color.FromArgb(243, 244, 246);
            txtId.Font = new Font("Segoe UI", 10.5F);
            txtId.Name = "txtId";
            txtId.TabIndex = 0;
            txtId.ReadOnly = true;
            txtId.TabStop = false;
            //
            // txtBarcode
            //
            txtBarcode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.BackColor = Color.FromArgb(249, 250, 251);
            txtBarcode.Font = new Font("Segoe UI", 10.5F);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.TabIndex = 1;
            txtBarcode.MaxLength = 50;
            txtBarcode.PlaceholderText = "Tối đa 50 ký tự";
            //
            // txtProductName
            //
            txtProductName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.BackColor = Color.FromArgb(249, 250, 251);
            txtProductName.Font = new Font("Segoe UI", 10.5F);
            txtProductName.Name = "txtProductName";
            txtProductName.TabIndex = 2;
            txtProductName.MaxLength = 150;
            txtProductName.PlaceholderText = "Tối đa 150 ký tự";
            //
            // cbCategory
            //
            cbCategory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbCategory.BackColor = Color.FromArgb(249, 250, 251);
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.FlatStyle = FlatStyle.Flat;
            cbCategory.Font = new Font("Segoe UI", 10.5F);
            cbCategory.Name = "cbCategory";
            cbCategory.TabIndex = 3;
            //
            // txtPrice
            //
            txtPrice.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPrice.BackColor = Color.FromArgb(249, 250, 251);
            txtPrice.Font = new Font("Segoe UI", 10.5F);
            txtPrice.Name = "txtPrice";
            txtPrice.TabIndex = 4;
            txtPrice.MaxLength = 15;
            txtPrice.PlaceholderText = "Ví dụ: 32000";
            //
            // numStock
            //
            numStock.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numStock.Font = new Font("Segoe UI", 10.5F);
            numStock.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.TabIndex = 5;
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
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.Padding = new Padding(0, 12, 0, 0);
            tlpButtons.RowCount = 2;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpButtons.TabIndex = 1;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Margin = new Padding(0, 0, 5, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            //
            // btnUpdate
            //
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Margin = new Padding(5, 0, 0, 8);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.White;
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.FromArgb(220, 38, 38);
            btnDelete.Margin = new Padding(0, 0, 5, 8);
            btnDelete.Name = "btnDelete";
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
            btnClear.Margin = new Padding(5, 0, 0, 8);
            btnClear.Name = "btnClear";
            btnClear.TabIndex = 9;
            btnClear.Text = "Nhập mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            //
            // errorProvider1 (hiện dấu ! đỏ cạnh ô nhập sai)
            //
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            //
            // FormProductManagement
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1100, 620);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "FormProductManagement";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sản phẩm";
            Load += FormProductManagement_Load;
            tlpMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            pnlRight.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tlpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
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
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBarcode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colStock;
        private Label lblStatus;
        private TableLayoutPanel pnlRight;
        private GroupBox groupBox3;
        private TableLayoutPanel tlpInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblName;
        private TextBox txtProductName;
        private Label lblCategory;
        private ComboBox cbCategory;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblStock;
        private NumericUpDown numStock;
        private TableLayoutPanel tlpButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private ErrorProvider errorProvider1;
    }
}