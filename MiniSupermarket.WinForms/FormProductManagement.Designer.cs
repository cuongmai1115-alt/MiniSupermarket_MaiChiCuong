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
            dgvProducts = new DataGridView();
            grpInfo = new GroupBox();
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
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Left;
            dgvProducts.Location = new Point(0, 0);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(580, 600);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(lblBarcode);
            grpInfo.Controls.Add(txtBarcode);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtProductName);
            grpInfo.Controls.Add(lblCategory);
            grpInfo.Controls.Add(cbCategory);
            grpInfo.Controls.Add(lblPrice);
            grpInfo.Controls.Add(txtPrice);
            grpInfo.Controls.Add(lblStock);
            grpInfo.Controls.Add(numStock);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnClear);
            grpInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpInfo.Location = new Point(590, 10);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(310, 580);
            grpInfo.TabIndex = 1;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông Tin Sản Phẩm";
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(15, 25);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(71, 19);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "Mã Vạch:";
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Segoe UI", 10F);
            txtBarcode.Location = new Point(15, 47);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(280, 25);
            txtBarcode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(15, 82);
            lblName.Name = "lblName";
            lblName.Size = new Size(106, 19);
            lblName.TabIndex = 2;
            lblName.Text = "Tên Sản Phẩm:";
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Segoe UI", 10F);
            txtProductName.Location = new Point(15, 104);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(280, 25);
            txtProductName.TabIndex = 3;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(15, 139);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(81, 19);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "Danh Mục:";
            // 
            // cbCategory
            // 
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.Font = new Font("Segoe UI", 10F);
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(15, 161);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(280, 25);
            cbCategory.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(15, 196);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(89, 19);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Đơn Giá (đ):";
            // 
            // txtPrice
            // 
            txtPrice.Font = new Font("Segoe UI", 10F);
            txtPrice.Location = new Point(15, 218);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(280, 25);
            txtPrice.TabIndex = 7;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(15, 253);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(106, 19);
            lblStock.TabIndex = 8;
            lblStock.Text = "Số Lượng Tồn:";
            // 
            // numStock
            // 
            numStock.Font = new Font("Segoe UI", 10F);
            numStock.Location = new Point(15, 275);
            numStock.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(280, 25);
            numStock.TabIndex = 9;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 330);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 40);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Thêm";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(165, 330);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(130, 40);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Sửa";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(15, 385);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 40);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(165, 385);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 40);
            btnClear.TabIndex = 13;
            btnClear.Text = "Làm Mới";
            btnClear.Click += btnClear_Click;
            // 
            // FormProductManagement
            // 
            ClientSize = new Size(910, 600);
            Controls.Add(grpInfo);
            Controls.Add(dgvProducts);
            Name = "FormProductManagement";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Sản Phẩm";
            Load += FormProductManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown numStock;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
    }
}