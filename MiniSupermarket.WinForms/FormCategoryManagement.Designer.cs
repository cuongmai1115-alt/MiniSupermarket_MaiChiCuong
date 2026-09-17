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
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnSearch = new Button();
            btnLoad = new Button();
            txtKeyword = new TextBox();

            groupBox2 = new GroupBox();
            dgvCategories = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();

            groupBox3 = new GroupBox();
            txtDescription = new TextBox();
            txtCategoryName = new TextBox();
            txtId = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();

            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();

            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();

            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(btnLoad);
            groupBox1.Controls.Add(txtKeyword);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(520, 48);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm";

            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(338, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(430, 17);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(13, 18);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(306, 23);
            txtKeyword.TabIndex = 0;

            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvCategories);
            groupBox2.Location = new Point(12, 66);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(520, 320);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách nhóm hàng";

            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvCategories.Columns.AddRange(
                new DataGridViewColumn[]
                {
                    colId,
                    colCategoryName,
                    colDescription
                });

            dgvCategories.Location = new Point(7, 17);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.Size = new Size(507, 297);
            dgvCategories.TabIndex = 0;

            // Chỉ sử dụng CellClick
            dgvCategories.CellClick += dgvCategories_CellClick;

            // 
            // colId
            // 
            colId.DataPropertyName = "CategoryId";
            colId.HeaderText = "Mã ID";
            colId.Name = "CategoryId";
            colId.ReadOnly = true;

            // 
            // colCategoryName
            // 
            colCategoryName.DataPropertyName = "CategoryName";
            colCategoryName.HeaderText = "Tên Nhóm Hàng";
            colCategoryName.Name = "CategoryName";
            colCategoryName.ReadOnly = true;

            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.HeaderText = "Mô tả";
            colDescription.Name = "Description";
            colDescription.ReadOnly = true;

            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtDescription);
            groupBox3.Controls.Add(txtCategoryName);
            groupBox3.Controls.Add(txtId);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);

            groupBox3.Location = new Point(538, 66);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(253, 204);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin nhóm hàng";

            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(11, 134);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(236, 64);
            txtDescription.TabIndex = 8;

            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(11, 89);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(236, 23);
            txtCategoryName.TabIndex = 7;

            // 
            // txtId
            // 
            txtId.Location = new Point(11, 45);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(236, 23);
            txtId.TabIndex = 6;

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 116);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 2;
            label3.Text = "Mô tả";

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 71);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 1;
            label2.Text = "Tên nhóm hàng";

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 24);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "Mã ID";

            // Không có:
            // label1.Click += label1_Click;

            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(716, 276);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(632, 276);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập Nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(538, 276);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 424);

            Controls.Add(btnDelete);
            Controls.Add(groupBox3);
            Controls.Add(btnUpdate);
            Controls.Add(groupBox2);
            Controls.Add(btnAdd);
            Controls.Add(groupBox1);

            Name = "FormCategoryManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý nhóm hàng";

            Load += FormCategoryManagement_Load;

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            groupBox2.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();

            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;

        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;

        private Label label1;
        private Label label2;
        private Label label3;

        private TextBox txtId;
        private TextBox txtCategoryName;
        private TextBox txtDescription;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
