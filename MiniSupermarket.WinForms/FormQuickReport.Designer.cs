namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
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
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblFrom = new Label();
            dtpFromDate = new DateTimePicker();
            lblTo = new Label();
            dtpToDate = new DateTimePicker();
            btnFilter = new Button();
            tblCards = new TableLayoutPanel();
            cardRevenue = new Panel();
            lblRevenueTitle = new Label();
            lblTotalRevenue = new Label();
            cardOrders = new Panel();
            lblOrdersTitle = new Label();
            lblTotalOrders = new Label();
            cardProducts = new Panel();
            lblProductsTitle = new Label();
            lblTotalProductsSold = new Label();
            cardAvg = new Panel();
            lblAvgTitle = new Label();
            lblAvgOrderValue = new Label();
            grpTopProducts = new GroupBox();
            dgvTopProducts = new DataGridView();
            pnlHeader.SuspendLayout();
            tblCards.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardOrders.SuspendLayout();
            cardProducts.SuspendLayout();
            cardAvg.SuspendLayout();
            grpTopProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblFrom);
            pnlHeader.Controls.Add(dtpFromDate);
            pnlHeader.Controls.Add(lblTo);
            pnlHeader.Controls.Add(dtpToDate);
            pnlHeader.Controls.Add(btnFilter);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(950, 70);
            pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            
            // 
            // lblFrom
            // 
            lblFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI", 9.5F);
            lblFrom.Location = new Point(380, 26);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(68, 21);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            dtpFromDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Font = new Font("Segoe UI", 9.5F);
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.Location = new Point(445, 21);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(125, 29);
            dtpFromDate.TabIndex = 2;
            // 
            // lblTo
            // 
            lblTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 9.5F);
            lblTo.Location = new Point(585, 26);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(41, 21);
            lblTo.TabIndex = 3;
            lblTo.Text = "Đến:";
            // 
            // dtpToDate
            // 
            dtpToDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Font = new Font("Segoe UI", 9.5F);
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.Location = new Point(625, 21);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(125, 29);
            dtpToDate.TabIndex = 4;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFilter.BackColor = Color.FromArgb(37, 99, 235);
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(765, 18);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(160, 35);
            btnFilter.TabIndex = 5;
            btnFilter.Text = "Xem Báo Cáo";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // tblCards
            // 
            tblCards.ColumnCount = 4;
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.Controls.Add(cardRevenue, 0, 0);
            tblCards.Controls.Add(cardOrders, 1, 0);
            tblCards.Controls.Add(cardProducts, 2, 0);
            tblCards.Controls.Add(cardAvg, 3, 0);
            tblCards.Dock = DockStyle.Top;
            tblCards.Location = new Point(0, 70);
            tblCards.Name = "tblCards";
            tblCards.Padding = new Padding(15, 15, 15, 10);
            tblCards.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblCards.Size = new Size(950, 115);
            tblCards.TabIndex = 1;
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.FromArgb(220, 252, 231);
            cardRevenue.Controls.Add(lblRevenueTitle);
            cardRevenue.Controls.Add(lblTotalRevenue);
            cardRevenue.Dock = DockStyle.Fill;
            cardRevenue.Location = new Point(20, 20);
            cardRevenue.Margin = new Padding(5);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Size = new Size(220, 80);
            cardRevenue.TabIndex = 0;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.Dock = DockStyle.Top;
            lblRevenueTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.FromArgb(22, 101, 52);
            lblRevenueTitle.Location = new Point(0, 0);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Padding = new Padding(10, 10, 0, 0);
            lblRevenueTitle.Size = new Size(220, 23);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "TỔNG DOANH THU";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.FromArgb(21, 128, 61);
            lblTotalRevenue.Location = new Point(0, 0);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Padding = new Padding(10, 0, 0, 0);
            lblTotalRevenue.Size = new Size(220, 80);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "0 đ";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardOrders
            // 
            cardOrders.BackColor = Color.FromArgb(219, 234, 254);
            cardOrders.Controls.Add(lblOrdersTitle);
            cardOrders.Controls.Add(lblTotalOrders);
            cardOrders.Dock = DockStyle.Fill;
            cardOrders.Location = new Point(250, 20);
            cardOrders.Margin = new Padding(5);
            cardOrders.Name = "cardOrders";
            cardOrders.Size = new Size(220, 80);
            cardOrders.TabIndex = 1;
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.Dock = DockStyle.Top;
            lblOrdersTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.FromArgb(30, 58, 138);
            lblOrdersTitle.Location = new Point(0, 0);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Padding = new Padding(10, 10, 0, 0);
            lblOrdersTitle.Size = new Size(220, 23);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "TỔNG ĐƠN HÀNG";
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.FromArgb(29, 78, 216);
            lblTotalOrders.Location = new Point(0, 0);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Padding = new Padding(10, 0, 0, 0);
            lblTotalOrders.Size = new Size(220, 80);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "0";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardProducts
            // 
            cardProducts.BackColor = Color.FromArgb(254, 243, 199);
            cardProducts.Controls.Add(lblProductsTitle);
            cardProducts.Controls.Add(lblTotalProductsSold);
            cardProducts.Dock = DockStyle.Fill;
            cardProducts.Location = new Point(480, 20);
            cardProducts.Margin = new Padding(5);
            cardProducts.Name = "cardProducts";
            cardProducts.Size = new Size(220, 80);
            cardProducts.TabIndex = 2;
            // 
            // lblProductsTitle
            // 
            lblProductsTitle.Dock = DockStyle.Top;
            lblProductsTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductsTitle.ForeColor = Color.FromArgb(120, 53, 15);
            lblProductsTitle.Location = new Point(0, 0);
            lblProductsTitle.Name = "lblProductsTitle";
            lblProductsTitle.Padding = new Padding(10, 10, 0, 0);
            lblProductsTitle.Size = new Size(220, 23);
            lblProductsTitle.TabIndex = 0;
            lblProductsTitle.Text = "SẢN PHẨM ĐÃ BÁN";
            // 
            // lblTotalProductsSold
            // 
            lblTotalProductsSold.Dock = DockStyle.Fill;
            lblTotalProductsSold.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalProductsSold.ForeColor = Color.FromArgb(180, 83, 9);
            lblTotalProductsSold.Location = new Point(0, 0);
            lblTotalProductsSold.Name = "lblTotalProductsSold";
            lblTotalProductsSold.Padding = new Padding(10, 0, 0, 0);
            lblTotalProductsSold.Size = new Size(220, 80);
            lblTotalProductsSold.TabIndex = 1;
            lblTotalProductsSold.Text = "0";
            lblTotalProductsSold.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardAvg
            // 
            cardAvg.BackColor = Color.FromArgb(243, 232, 255);
            cardAvg.Controls.Add(lblAvgTitle);
            cardAvg.Controls.Add(lblAvgOrderValue);
            cardAvg.Dock = DockStyle.Fill;
            cardAvg.Location = new Point(710, 20);
            cardAvg.Margin = new Padding(5);
            cardAvg.Name = "cardAvg";
            cardAvg.Size = new Size(220, 80);
            cardAvg.TabIndex = 3;
            // 
            // lblAvgTitle
            // 
            lblAvgTitle.Dock = DockStyle.Top;
            lblAvgTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblAvgTitle.ForeColor = Color.FromArgb(88, 28, 135);
            lblAvgTitle.Location = new Point(0, 0);
            lblAvgTitle.Name = "lblAvgTitle";
            lblAvgTitle.Padding = new Padding(10, 10, 0, 0);
            lblAvgTitle.Size = new Size(220, 23);
            lblAvgTitle.TabIndex = 0;
            lblAvgTitle.Text = "TB / ĐƠN HÀNG";
            // 
            // lblAvgOrderValue
            // 
            lblAvgOrderValue.Dock = DockStyle.Fill;
            lblAvgOrderValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAvgOrderValue.ForeColor = Color.FromArgb(126, 34, 206);
            lblAvgOrderValue.Location = new Point(0, 0);
            lblAvgOrderValue.Name = "lblAvgOrderValue";
            lblAvgOrderValue.Padding = new Padding(10, 0, 0, 0);
            lblAvgOrderValue.Size = new Size(220, 80);
            lblAvgOrderValue.TabIndex = 1;
            lblAvgOrderValue.Text = "0 đ";
            lblAvgOrderValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpTopProducts
            // 
            grpTopProducts.Controls.Add(dgvTopProducts);
            grpTopProducts.Dock = DockStyle.Fill;
            grpTopProducts.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpTopProducts.Location = new Point(0, 185);
            grpTopProducts.Margin = new Padding(20);
            grpTopProducts.Name = "grpTopProducts";
            grpTopProducts.Padding = new Padding(15);
            grpTopProducts.Size = new Size(950, 365);
            grpTopProducts.TabIndex = 0;
            grpTopProducts.TabStop = false;
            grpTopProducts.Text = " Top 5 Sản Phẩm Bán Chạy Nhất ";
            // 
            // dgvTopProducts
            // 
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.AllowUserToDeleteRows = false;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.BackgroundColor = Color.White;
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.ColumnHeadersHeight = 35;
            dgvTopProducts.Dock = DockStyle.Fill;
            dgvTopProducts.Font = new Font("Segoe UI", 9.5F);
            dgvTopProducts.Location = new Point(15, 38);
            dgvTopProducts.Name = "dgvTopProducts";
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.RowHeadersWidth = 51;
            dgvTopProducts.RowTemplate.Height = 32;
            dgvTopProducts.Size = new Size(920, 312);
            dgvTopProducts.TabIndex = 0;
            dgvTopProducts.CellContentClick += dgvTopProducts_CellContentClick;
            // 
            // FormQuickReport
            // 
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(950, 550);
            Controls.Add(grpTopProducts);
            Controls.Add(tblCards);
            Controls.Add(pnlHeader);
            Name = "FormQuickReport";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Báo Cáo Doanh Thu Hệ Thống";
            Load += FormQuickReport_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tblCards.ResumeLayout(false);
            cardRevenue.ResumeLayout(false);
            cardOrders.ResumeLayout(false);
            cardProducts.ResumeLayout(false);
            cardAvg.ResumeLayout(false);
            grpTopProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnFilter;

        private System.Windows.Forms.TableLayoutPanel tblCards;
        private System.Windows.Forms.Panel cardRevenue;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblTotalRevenue;

        private System.Windows.Forms.Panel cardOrders;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Label lblTotalOrders;

        private System.Windows.Forms.Panel cardProducts;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.Label lblTotalProductsSold;

        private System.Windows.Forms.Panel cardAvg;
        private System.Windows.Forms.Label lblAvgTitle;
        private System.Windows.Forms.Label lblAvgOrderValue;

        private System.Windows.Forms.GroupBox grpTopProducts;
        private System.Windows.Forms.DataGridView dgvTopProducts;
    }
}