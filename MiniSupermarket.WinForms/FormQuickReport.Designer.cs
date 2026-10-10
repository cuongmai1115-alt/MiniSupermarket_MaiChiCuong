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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();

            this.tblCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardRevenue = new System.Windows.Forms.Panel();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();

            this.cardOrders = new System.Windows.Forms.Panel();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();

            this.cardProducts = new System.Windows.Forms.Panel();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.lblTotalProductsSold = new System.Windows.Forms.Label();

            this.cardAvg = new System.Windows.Forms.Panel();
            this.lblAvgTitle = new System.Windows.Forms.Label();
            this.lblAvgOrderValue = new System.Windows.Forms.Label();

            this.grpTopProducts = new System.Windows.Forms.GroupBox();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();

            this.pnlHeader.SuspendLayout();
            this.tblCards.SuspendLayout();
            this.cardRevenue.SuspendLayout();
            this.cardOrders.SuspendLayout();
            this.cardProducts.SuspendLayout();
            this.cardAvg.SuspendLayout();
            this.grpTopProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader (Thanh công cụ lọc phía trên)
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblFrom);
            this.pnlHeader.Controls.Add(this.dtpFromDate);
            this.pnlHeader.Controls.Add(this.lblTo);
            this.pnlHeader.Controls.Add(this.dtpToDate);
            this.pnlHeader.Controls.Add(this.btnFilter);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(950, 70);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Text = "BÁO CÁO DOANH THU";

            // Lọc ngày
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFrom.Location = new System.Drawing.Point(380, 26);
            this.lblFrom.Text = "Từ ngày:";

            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(445, 21);
            this.dtpFromDate.Size = new System.Drawing.Size(125, 29);

            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTo.Location = new System.Drawing.Point(585, 26);
            this.lblTo.Text = "Đến:";

            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(625, 21);
            this.dtpToDate.Size = new System.Drawing.Size(125, 29);

            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(765, 18);
            this.btnFilter.Size = new System.Drawing.Size(160, 35);
            this.btnFilter.Text = "Xem Báo Cáo";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            // 
            // tblCards (Bố cục 4 ô Thống kê tự chia đều)
            // 
            this.tblCards.ColumnCount = 4;
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblCards.Controls.Add(this.cardRevenue, 0, 0);
            this.tblCards.Controls.Add(this.cardOrders, 1, 0);
            this.tblCards.Controls.Add(this.cardProducts, 2, 0);
            this.tblCards.Controls.Add(this.cardAvg, 3, 0);
            this.tblCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblCards.Location = new System.Drawing.Point(0, 70);
            this.tblCards.Padding = new System.Windows.Forms.Padding(15, 15, 15, 10);
            this.tblCards.Size = new System.Drawing.Size(950, 115);

            // Card 1: Doanh thu
            this.cardRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.cardRevenue.Controls.Add(this.lblRevenueTitle);
            this.cardRevenue.Controls.Add(this.lblTotalRevenue);
            this.cardRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRevenue.Margin = new System.Windows.Forms.Padding(5);

            this.lblRevenueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblRevenueTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblRevenueTitle.Text = "TỔNG DOANH THU";

            this.lblTotalRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblTotalRevenue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTotalRevenue.Text = "0 đ";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Card 2: Đơn hàng
            this.cardOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.cardOrders.Controls.Add(this.lblOrdersTitle);
            this.cardOrders.Controls.Add(this.lblTotalOrders);
            this.cardOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOrders.Margin = new System.Windows.Forms.Padding(5);

            this.lblOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.lblOrdersTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblOrdersTitle.Text = "TỔNG ĐƠN HÀNG";

            this.lblTotalOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblTotalOrders.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTotalOrders.Text = "0";
            this.lblTotalOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Card 3: Sản phẩm
            this.cardProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.cardProducts.Controls.Add(this.lblProductsTitle);
            this.cardProducts.Controls.Add(this.lblTotalProductsSold);
            this.cardProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardProducts.Margin = new System.Windows.Forms.Padding(5);

            this.lblProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductsTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblProductsTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblProductsTitle.Text = "SẢN PHẨM ĐÃ BÁN";

            this.lblTotalProductsSold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalProductsSold.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalProductsSold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblTotalProductsSold.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTotalProductsSold.Text = "0";
            this.lblTotalProductsSold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Card 4: Trung bình đơn
            this.cardAvg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.cardAvg.Controls.Add(this.lblAvgTitle);
            this.cardAvg.Controls.Add(this.lblAvgOrderValue);
            this.cardAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAvg.Margin = new System.Windows.Forms.Padding(5);

            this.lblAvgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvgTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAvgTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(28)))), ((int)(((byte)(135)))));
            this.lblAvgTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblAvgTitle.Text = "TB / ĐƠN HÀNG";

            this.lblAvgOrderValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvgOrderValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAvgOrderValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(34)))), ((int)(((byte)(206)))));
            this.lblAvgOrderValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblAvgOrderValue.Text = "0 đ";
            this.lblAvgOrderValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // grpTopProducts (Bảng danh sách sản phẩm bán chạy)
            // 
            this.grpTopProducts.Controls.Add(this.dgvTopProducts);
            this.grpTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTopProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTopProducts.Location = new System.Drawing.Point(0, 185);
            this.grpTopProducts.Margin = new System.Windows.Forms.Padding(20);
            this.grpTopProducts.Padding = new System.Windows.Forms.Padding(15);
            this.grpTopProducts.Size = new System.Drawing.Size(950, 365);
            this.grpTopProducts.Text = " Top 5 Sản Phẩm Bán Chạy Nhất ";

            // dgvTopProducts
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopProducts.ColumnHeadersHeight = 35;
            this.dgvTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProducts.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.RowTemplate.Height = 32;

            // 
            // FormQuickReport
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.grpTopProducts);
            this.Controls.Add(this.tblCards);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FormQuickReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo Cáo Doanh Thu Hệ Thống";
            this.Load += new System.EventHandler(this.FormQuickReport_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tblCards.ResumeLayout(false);
            this.cardRevenue.ResumeLayout(false);
            this.cardOrders.ResumeLayout(false);
            this.cardProducts.ResumeLayout(false);
            this.cardAvg.ResumeLayout(false);
            this.grpTopProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.ResumeLayout(false);
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