namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            panelMenu = new FlowLayoutPanel();
            btnPOS = new Button();
            btnCategory = new Button();
            btnProduct = new Button();
            btnCustomer = new Button();
            btnReports = new Button();
            btnUserManage = new Button();
            lblLogo = new Label();
            panelUserFooter = new Panel();
            btnLogout = new Button();
            panelTopHeader = new Panel();
            lblTitle = new Label();
            lblUserInfo = new Label();
            panelMainContent = new Panel();
            panelSidebar.SuspendLayout();
            panelMenu.SuspendLayout();
            panelUserFooter.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            panelSidebar.Controls.Add(panelMenu);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(3, 4, 3, 4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(263, 960);
            panelSidebar.TabIndex = 2;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(24, 30, 48);
            panelMenu.Controls.Add(btnPOS);
            panelMenu.Controls.Add(btnCategory);
            panelMenu.Controls.Add(btnProduct);
            panelMenu.Controls.Add(btnCustomer);
            panelMenu.Controls.Add(btnReports);
            panelMenu.Controls.Add(btnUserManage);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.FlowDirection = FlowDirection.TopDown;
            panelMenu.Location = new Point(0, 93);
            panelMenu.Margin = new Padding(3, 4, 3, 4);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(263, 787);
            panelMenu.TabIndex = 0;
            panelMenu.WrapContents = false;
            // 
            // btnPOS
            // 
            btnPOS.BackColor = Color.FromArgb(24, 30, 48);
            btnPOS.Cursor = Cursors.Hand;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10F);
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(0, 0);
            btnPOS.Margin = new Padding(0);
            btnPOS.Name = "btnPOS";
            btnPOS.Padding = new Padding(17, 0, 0, 0);
            btnPOS.Size = new Size(263, 60);
            btnPOS.TabIndex = 0;
            btnPOS.Text = "\U0001f6d2 Bán hàng (POS)";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnPOS_Click;
            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.FromArgb(24, 30, 48);
            btnCategory.Cursor = Cursors.Hand;
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10F);
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(0, 60);
            btnCategory.Margin = new Padding(0);
            btnCategory.Name = "btnCategory";
            btnCategory.Padding = new Padding(17, 0, 0, 0);
            btnCategory.Size = new Size(263, 60);
            btnCategory.TabIndex = 1;
            btnCategory.Text = "📁 Quản lý Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;
            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.FromArgb(24, 30, 48);
            btnProduct.Cursor = Cursors.Hand;
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10F);
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(0, 120);
            btnProduct.Margin = new Padding(0);
            btnProduct.Name = "btnProduct";
            btnProduct.Padding = new Padding(17, 0, 0, 0);
            btnProduct.Size = new Size(263, 60);
            btnProduct.TabIndex = 2;
            btnProduct.Text = "📦 Quản lý Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;
            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.FromArgb(24, 30, 48);
            btnCustomer.Cursor = Cursors.Hand;
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10F);
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(0, 180);
            btnCustomer.Margin = new Padding(0);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Padding = new Padding(17, 0, 0, 0);
            btnCustomer.Size = new Size(263, 60);
            btnCustomer.TabIndex = 3;
            btnCustomer.Text = "👥 Quản lý Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(24, 30, 48);
            btnReports.Cursor = Cursors.Hand;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 10F);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 240);
            btnReports.Margin = new Padding(0);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(17, 0, 0, 0);
            btnReports.Size = new Size(263, 60);
            btnReports.TabIndex = 4;
            btnReports.Text = "📊 Báo cáo Doanh thu";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;
            // 
            // btnUserManage
            // 
            btnUserManage.BackColor = Color.FromArgb(24, 30, 48);
            btnUserManage.Cursor = Cursors.Hand;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("Segoe UI", 10F);
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Location = new Point(0, 300);
            btnUserManage.Margin = new Padding(0);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(17, 0, 0, 0);
            btnUserManage.Size = new Size(263, 60);
            btnUserManage.TabIndex = 5;
            btnUserManage.Text = "🛡 Quản trị Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = false;
            btnUserManage.Click += btnUserManage_Click;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(263, 93);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "\U0001f6d2 AlphaMini POS";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUserFooter
            // 
            panelUserFooter.BackColor = Color.FromArgb(24, 30, 48);
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 880);
            panelUserFooter.Margin = new Padding(3, 4, 3, 4);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(263, 80);
            panelUserFooter.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 0);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(263, 80);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪 Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panelTopHeader
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(263, 0);
            panelTopHeader.Margin = new Padding(3, 4, 3, 4);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1200, 80);
            panelTopHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(17, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(314, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.Location = new Point(731, 28);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(97, 23);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào: ...";
            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(263, 80);
            panelMainContent.Margin = new Padding(3, 4, 3, 4);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1200, 880);
            panelMainContent.TabIndex = 0;
            panelMainContent.Paint += panelMainContent_Paint;
            // 
            // FormMainShell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1463, 960);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini";
            Load += FormMainShell_Load;
            panelSidebar.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelUserFooter.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private FlowLayoutPanel panelMenu;
        private Label lblLogo;
        private Panel panelUserFooter;
        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReports;
        private Button btnUserManage;
        private Button btnLogout;
        private Panel panelTopHeader;
        private Label lblTitle;
        private Label lblUserInfo;
        private Panel panelMainContent;
    }
}