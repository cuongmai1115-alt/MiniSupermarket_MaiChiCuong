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
            lblKeyword.Location = new Point(18, 14);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(52, 15);
            lblKeyword.TabIndex = 0;
            lblKeyword.Text = "Từ khóa:";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(79, 10);
            txtKeyword.Margin = new Padding(3, 2, 3, 2);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(219, 23);
            txtKeyword.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(306, 9);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(96, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(411, 9);
            btnLoad.Margin = new Padding(3, 2, 3, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(96, 23);
            btnLoad.TabIndex = 3;
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
            dgvCustomers.Location = new Point(18, 41);
            dgvCustomers.Margin = new Padding(3, 2, 3, 2);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(735, 195);
            dgvCustomers.TabIndex = 4;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            // 
            // lblCustomerId
            // 
            lblCustomerId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(18, 254);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(46, 15);
            lblCustomerId.TabIndex = 5;
            lblCustomerId.Text = "Mã KH:";
            // 
            // txtCustomerId
            // 
            txtCustomerId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtCustomerId.Location = new Point(122, 250);
            txtCustomerId.Margin = new Padding(3, 2, 3, 2);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(219, 23);
            txtCustomerId.TabIndex = 6;
            // 
            // lblCustomerName
            // 
            lblCustomerName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(18, 284);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(93, 15);
            lblCustomerName.TabIndex = 7;
            lblCustomerName.Text = "Tên khách hàng:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtCustomerName.Location = new Point(122, 280);
            txtCustomerName.Margin = new Padding(3, 2, 3, 2);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(219, 23);
            txtCustomerName.TabIndex = 8;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(18, 314);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(79, 15);
            lblPhoneNumber.TabIndex = 9;
            lblPhoneNumber.Text = "Số điện thoại:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPhoneNumber.Location = new Point(122, 310);
            txtPhoneNumber.Margin = new Padding(3, 2, 3, 2);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(219, 23);
            txtPhoneNumber.TabIndex = 10;
            // 
            // lblAddress
            // 
            lblAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(376, 254);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(46, 15);
            lblAddress.TabIndex = 11;
            lblAddress.Text = "Địa chỉ:";
            // 
            // txtAddress
            // 
            txtAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtAddress.Location = new Point(481, 250);
            txtAddress.Margin = new Padding(3, 2, 3, 2);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(263, 23);
            txtAddress.TabIndex = 12;
            // 
            // lblRewardPoints
            // 
            lblRewardPoints.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(376, 284);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Size = new Size(80, 15);
            lblRewardPoints.TabIndex = 13;
            lblRewardPoints.Text = "Điểm thưởng:";
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtRewardPoints.Location = new Point(481, 280);
            txtRewardPoints.Margin = new Padding(3, 2, 3, 2);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(263, 23);
            txtRewardPoints.TabIndex = 14;
            // 
            // lblMembershipRank
            // 
            lblMembershipRank.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(376, 314);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Size = new Size(59, 15);
            lblMembershipRank.TabIndex = 15;
            lblMembershipRank.Text = "Hạng thẻ:";
            // 
            // txtMembershipRank
            // 
            txtMembershipRank.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtMembershipRank.Location = new Point(481, 310);
            txtMembershipRank.Margin = new Padding(3, 2, 3, 2);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(263, 23);
            txtMembershipRank.TabIndex = 16;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(122, 352);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 27);
            btnAdd.TabIndex = 17;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(245, 352);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(105, 27);
            btnUpdate.TabIndex = 18;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(368, 352);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 27);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 398);
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
            Margin = new Padding(3, 2, 3, 2);
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