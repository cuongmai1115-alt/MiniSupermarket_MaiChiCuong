namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            lblKeyword = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            dgvCustomers = new DataGridView();
            lblCustomerId = new Label();
            txtCustomerId = new TextBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblPhoneNumber = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblRewardPoints = new Label();
            txtRewardPoints = new TextBox();
            lblMembershipRank = new Label();
            txtMembershipRank = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(20, 18);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Text = "Từ khóa:";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(90, 14);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(250, 27);
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(350, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 31);
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(470, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(110, 31);
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(20, 55);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(840, 260);
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            // 
            // lblCustomerId / txtCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCustomerId.Location = new Point(20, 338);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Text = "Mã KH:";
            txtCustomerId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtCustomerId.Location = new Point(140, 334);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(250, 27);
            // 
            // lblCustomerName / txtCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCustomerName.Location = new Point(20, 378);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Text = "Tên khách hàng:";
            txtCustomerName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtCustomerName.Location = new Point(140, 374);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(250, 27);
            // 
            // lblPhoneNumber / txtPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPhoneNumber.Location = new Point(20, 418);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Text = "Số điện thoại:";
            txtPhoneNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPhoneNumber.Location = new Point(140, 414);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(250, 27);
            // 
            // lblAddress / txtAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAddress.Location = new Point(430, 338);
            lblAddress.Name = "lblAddress";
            lblAddress.Text = "Địa chỉ:";
            txtAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtAddress.Location = new Point(550, 334);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(300, 27);
            // 
            // lblRewardPoints / txtRewardPoints
            // 
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblRewardPoints.Location = new Point(430, 378);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Text = "Điểm thưởng:";
            txtRewardPoints.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtRewardPoints.Location = new Point(550, 374);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(300, 27);
            // 
            // lblMembershipRank / txtMembershipRank
            // 
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMembershipRank.Location = new Point(430, 418);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Text = "Hạng thẻ:";
            txtMembershipRank.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtMembershipRank.Location = new Point(550, 414);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(300, 27);
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(140, 470);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 36);
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(280, 470);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 36);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(420, 470);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 36);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 531);
            Controls.Add(lblKeyword);
            Controls.Add(txtKeyword);
            Controls.Add(btnSearch);
            Controls.Add(btnLoad);
            Controls.Add(dgvCustomers);
            Controls.Add(lblCustomerId);
            Controls.Add(txtCustomerId);
            Controls.Add(lblCustomerName);
            Controls.Add(txtCustomerName);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblRewardPoints);
            Controls.Add(txtRewardPoints);
            Controls.Add(lblMembershipRank);
            Controls.Add(txtMembershipRank);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý khách hàng";
            Load += FormCustomerManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private DataGridView dgvCustomers;
        private Label lblCustomerId;
        private TextBox txtCustomerId;
        private Label lblCustomerName;
        private TextBox txtCustomerName;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblRewardPoints;
        private TextBox txtRewardPoints;
        private Label lblMembershipRank;
        private TextBox txtMembershipRank;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}