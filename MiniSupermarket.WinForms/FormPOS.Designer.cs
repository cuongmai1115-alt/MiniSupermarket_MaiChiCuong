namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
        /// Bố cục mới: pnlLeft (Fill) + pnlRight (Right, rộng cố định 380).
        /// Toàn bộ dùng Dock / TableLayoutPanel nên tự co giãn khi nhúng vào FormMainShell,
        /// không còn khoảng trống hay lệch vị trí như bản cũ (tọa độ cố định).
        /// </summary>
        private void InitializeComponent()
        {
            pnlLeft = new Panel();
            dgvCart = new DataGridView();
            pnlCartTools = new Panel();
            tlpCartTools = new TableLayoutPanel();
            lblItemCount = new Label();
            btnMinus = new Button();
            btnPlus = new Button();
            btnRemoveItem = new Button();
            lblHint = new Label();
            pnlGap = new Panel();
            pnlBarcode = new Panel();
            tlpBarcode = new TableLayoutPanel();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            pnlRight = new Panel();
            tlpRight = new TableLayoutPanel();
            grpCustomer = new GroupBox();
            tlpCustomer = new TableLayoutPanel();
            lblPhoneTitle = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerName = new Label();
            pnlTotalCard = new Panel();
            lblTotalAmount = new Label();
            lblTotalTitle = new Label();
            pnlCash = new Panel();
            txtCashReceived = new TextBox();
            lblCashTitle = new Label();
            flowQuickCash = new FlowLayoutPanel();
            btnExact = new Button();
            btn50k = new Button();
            btn100k = new Button();
            btn200k = new Button();
            btn500k = new Button();
            pnlChange = new Panel();
            lblChange = new Label();
            lblChangeTitle = new Label();
            btnCheckout = new Button();
            btnClearCart = new Button();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlCartTools.SuspendLayout();
            tlpCartTools.SuspendLayout();
            pnlBarcode.SuspendLayout();
            tlpBarcode.SuspendLayout();
            pnlRight.SuspendLayout();
            tlpRight.SuspendLayout();
            grpCustomer.SuspendLayout();
            tlpCustomer.SuspendLayout();
            pnlTotalCard.SuspendLayout();
            pnlCash.SuspendLayout();
            flowQuickCash.SuspendLayout();
            pnlChange.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(dgvCart);
            pnlLeft.Controls.Add(pnlCartTools);
            pnlLeft.Controls.Add(lblHint);
            pnlLeft.Controls.Add(pnlGap);
            pnlLeft.Controls.Add(pnlBarcode);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Margin = new Padding(3, 4, 3, 4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(18, 21, 9, 16);
            pnlLeft.Size = new Size(823, 867);
            pnlLeft.TabIndex = 0;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.ColumnHeadersHeight = 29;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(18, 122);
            dgvCart.Margin = new Padding(3, 4, 3, 4);
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersWidth = 51;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(796, 628);
            dgvCart.TabIndex = 1;
            dgvCart.CellContentClick += dgvCart_CellContentClick;
            // 
            // pnlCartTools
            // 
            pnlCartTools.Controls.Add(tlpCartTools);
            pnlCartTools.Dock = DockStyle.Bottom;
            pnlCartTools.Location = new Point(18, 750);
            pnlCartTools.Margin = new Padding(3, 4, 3, 4);
            pnlCartTools.Name = "pnlCartTools";
            pnlCartTools.Padding = new Padding(0, 13, 0, 0);
            pnlCartTools.Size = new Size(796, 69);
            pnlCartTools.TabIndex = 2;
            // 
            // tlpCartTools
            // 
            tlpCartTools.ColumnCount = 4;
            tlpCartTools.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCartTools.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 57F));
            tlpCartTools.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 57F));
            tlpCartTools.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tlpCartTools.Controls.Add(lblItemCount, 0, 0);
            tlpCartTools.Controls.Add(btnMinus, 1, 0);
            tlpCartTools.Controls.Add(btnPlus, 2, 0);
            tlpCartTools.Controls.Add(btnRemoveItem, 3, 0);
            tlpCartTools.Dock = DockStyle.Fill;
            tlpCartTools.Location = new Point(0, 13);
            tlpCartTools.Margin = new Padding(3, 4, 3, 4);
            tlpCartTools.Name = "tlpCartTools";
            tlpCartTools.RowCount = 1;
            tlpCartTools.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCartTools.Size = new Size(796, 56);
            tlpCartTools.TabIndex = 0;
            // 
            // lblItemCount
            // 
            lblItemCount.Dock = DockStyle.Fill;
            lblItemCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblItemCount.ForeColor = Color.FromArgb(75, 85, 99);
            lblItemCount.Location = new Point(3, 0);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(516, 56);
            lblItemCount.TabIndex = 0;
            lblItemCount.Text = "Giỏ hàng trống";
            lblItemCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnMinus
            // 
            btnMinus.BackColor = Color.White;
            btnMinus.Cursor = Cursors.Hand;
            btnMinus.Dock = DockStyle.Fill;
            btnMinus.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnMinus.FlatStyle = FlatStyle.Flat;
            btnMinus.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnMinus.ForeColor = Color.FromArgb(17, 24, 39);
            btnMinus.Location = new Point(529, 0);
            btnMinus.Margin = new Padding(7, 0, 0, 0);
            btnMinus.Name = "btnMinus";
            btnMinus.Size = new Size(50, 56);
            btnMinus.TabIndex = 1;
            btnMinus.TabStop = false;
            btnMinus.Text = "−";
            btnMinus.UseVisualStyleBackColor = false;
            btnMinus.Click += btnMinus_Click;
            // 
            // btnPlus
            // 
            btnPlus.BackColor = Color.White;
            btnPlus.Cursor = Cursors.Hand;
            btnPlus.Dock = DockStyle.Fill;
            btnPlus.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnPlus.FlatStyle = FlatStyle.Flat;
            btnPlus.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnPlus.ForeColor = Color.FromArgb(17, 24, 39);
            btnPlus.Location = new Point(586, 0);
            btnPlus.Margin = new Padding(7, 0, 0, 0);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(50, 56);
            btnPlus.TabIndex = 2;
            btnPlus.TabStop = false;
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = false;
            btnPlus.Click += btnPlus_Click;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.BackColor = Color.White;
            btnRemoveItem.Cursor = Cursors.Hand;
            btnRemoveItem.Dock = DockStyle.Fill;
            btnRemoveItem.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRemoveItem.ForeColor = Color.FromArgb(220, 38, 38);
            btnRemoveItem.Location = new Point(643, 0);
            btnRemoveItem.Margin = new Padding(7, 0, 0, 0);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(153, 56);
            btnRemoveItem.TabIndex = 3;
            btnRemoveItem.TabStop = false;
            btnRemoveItem.Text = "Xóa dòng (Del)";
            btnRemoveItem.UseVisualStyleBackColor = false;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // lblHint
            // 
            lblHint.Dock = DockStyle.Bottom;
            lblHint.Font = new Font("Segoe UI", 8.5F);
            lblHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblHint.Location = new Point(18, 819);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(796, 32);
            lblHint.TabIndex = 3;
            lblHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGap
            // 
            pnlGap.Dock = DockStyle.Top;
            pnlGap.Location = new Point(18, 106);
            pnlGap.Margin = new Padding(3, 4, 3, 4);
            pnlGap.Name = "pnlGap";
            pnlGap.Size = new Size(796, 16);
            pnlGap.TabIndex = 1;
            // 
            // pnlBarcode
            // 
            pnlBarcode.BackColor = Color.White;
            pnlBarcode.Controls.Add(tlpBarcode);
            pnlBarcode.Dock = DockStyle.Top;
            pnlBarcode.Location = new Point(18, 21);
            pnlBarcode.Margin = new Padding(3, 4, 3, 4);
            pnlBarcode.Name = "pnlBarcode";
            pnlBarcode.Padding = new Padding(18, 0, 18, 0);
            pnlBarcode.Size = new Size(796, 85);
            pnlBarcode.TabIndex = 0;
            // 
            // tlpBarcode
            // 
            tlpBarcode.ColumnCount = 2;
            tlpBarcode.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tlpBarcode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBarcode.Controls.Add(lblBarcode, 0, 0);
            tlpBarcode.Controls.Add(txtBarcode, 1, 0);
            tlpBarcode.Dock = DockStyle.Fill;
            tlpBarcode.Location = new Point(18, 0);
            tlpBarcode.Margin = new Padding(3, 4, 3, 4);
            tlpBarcode.Name = "tlpBarcode";
            tlpBarcode.RowCount = 1;
            tlpBarcode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBarcode.Size = new Size(760, 85);
            tlpBarcode.TabIndex = 0;
            // 
            // lblBarcode
            // 
            lblBarcode.Dock = DockStyle.Fill;
            lblBarcode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.FromArgb(17, 24, 39);
            lblBarcode.Location = new Point(3, 0);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(154, 85);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "Quét mã vạch:";
            lblBarcode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBarcode
            // 
            txtBarcode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.BackColor = Color.FromArgb(249, 250, 251);
            txtBarcode.Font = new Font("Segoe UI", 13F);
            txtBarcode.Location = new Point(163, 24);
            txtBarcode.Margin = new Padding(3, 4, 3, 4);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Nhập hoặc quét mã vạch, rồi nhấn Enter";
            txtBarcode.Size = new Size(594, 36);
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // pnlRight
            // 
            pnlRight.AutoScroll = true;
            pnlRight.AutoScrollMinSize = new Size(0, 520);
            pnlRight.Controls.Add(tlpRight);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(823, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(9, 21, 18, 16);
            pnlRight.Size = new Size(434, 867);
            pnlRight.TabIndex = 1;
            // 
            // tlpRight
            // 
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(grpCustomer, 0, 0);
            tlpRight.Controls.Add(pnlTotalCard, 0, 1);
            tlpRight.Controls.Add(pnlCash, 0, 2);
            tlpRight.Controls.Add(flowQuickCash, 0, 3);
            tlpRight.Controls.Add(pnlChange, 0, 4);
            tlpRight.Controls.Add(btnCheckout, 0, 6);
            tlpRight.Controls.Add(btnClearCart, 0, 7);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Location = new Point(9, 21);
            tlpRight.Margin = new Padding(3, 4, 3, 4);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 8;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 123F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 101F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 115F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tlpRight.Size = new Size(407, 830);
            tlpRight.TabIndex = 0;
            // 
            // grpCustomer
            // 
            grpCustomer.BackColor = Color.White;
            grpCustomer.Controls.Add(tlpCustomer);
            grpCustomer.Dock = DockStyle.Fill;
            grpCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpCustomer.ForeColor = Color.FromArgb(17, 24, 39);
            grpCustomer.Location = new Point(0, 0);
            grpCustomer.Margin = new Padding(0, 0, 0, 13);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Padding = new Padding(11, 5, 11, 8);
            grpCustomer.Size = new Size(407, 115);
            grpCustomer.TabIndex = 0;
            grpCustomer.TabStop = false;
            grpCustomer.Text = "Khách hàng";
            // 
            // tlpCustomer
            // 
            tlpCustomer.ColumnCount = 2;
            tlpCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tlpCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCustomer.Controls.Add(lblPhoneTitle, 0, 0);
            tlpCustomer.Controls.Add(txtCustomerPhone, 1, 0);
            tlpCustomer.Controls.Add(lblCustomerName, 0, 1);
            tlpCustomer.Dock = DockStyle.Fill;
            tlpCustomer.Location = new Point(11, 28);
            tlpCustomer.Margin = new Padding(3, 4, 3, 4);
            tlpCustomer.Name = "tlpCustomer";
            tlpCustomer.RowCount = 2;
            tlpCustomer.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpCustomer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCustomer.Size = new Size(385, 79);
            tlpCustomer.TabIndex = 0;
            // 
            // lblPhoneTitle
            // 
            lblPhoneTitle.Dock = DockStyle.Fill;
            lblPhoneTitle.Font = new Font("Segoe UI", 9.5F);
            lblPhoneTitle.Location = new Point(3, 0);
            lblPhoneTitle.Name = "lblPhoneTitle";
            lblPhoneTitle.Size = new Size(108, 48);
            lblPhoneTitle.TabIndex = 0;
            lblPhoneTitle.Text = "Số điện thoại:";
            lblPhoneTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerPhone.BackColor = Color.FromArgb(249, 250, 251);
            txtCustomerPhone.Font = new Font("Segoe UI", 10.5F);
            txtCustomerPhone.Location = new Point(117, 8);
            txtCustomerPhone.Margin = new Padding(3, 4, 3, 4);
            txtCustomerPhone.MaxLength = 15;
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT, nhấn Enter";
            txtCustomerPhone.Size = new Size(265, 31);
            txtCustomerPhone.TabIndex = 1;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;
            txtCustomerPhone.Leave += txtCustomerPhone_Leave;
            // 
            // lblCustomerName
            // 
            tlpCustomer.SetColumnSpan(lblCustomerName, 2);
            lblCustomerName.Dock = DockStyle.Fill;
            lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCustomerName.ForeColor = Color.FromArgb(107, 114, 128);
            lblCustomerName.Location = new Point(3, 48);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(379, 31);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTotalCard
            // 
            pnlTotalCard.BackColor = Color.White;
            pnlTotalCard.Controls.Add(lblTotalAmount);
            pnlTotalCard.Controls.Add(lblTotalTitle);
            pnlTotalCard.Dock = DockStyle.Fill;
            pnlTotalCard.Location = new Point(0, 128);
            pnlTotalCard.Margin = new Padding(0, 0, 0, 13);
            pnlTotalCard.Name = "pnlTotalCard";
            pnlTotalCard.Padding = new Padding(16, 11, 16, 11);
            pnlTotalCard.Size = new Size(407, 110);
            pnlTotalCard.TabIndex = 1;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Dock = DockStyle.Fill;
            lblTotalAmount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(22, 163, 74);
            lblTotalAmount.Location = new Point(16, 46);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(375, 53);
            lblTotalAmount.TabIndex = 1;
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.Dock = DockStyle.Top;
            lblTotalTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalTitle.ForeColor = Color.FromArgb(75, 85, 99);
            lblTotalTitle.Location = new Point(16, 11);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(375, 35);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "TỔNG TIỀN CẦN TRẢ";
            lblTotalTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlCash
            // 
            pnlCash.Controls.Add(txtCashReceived);
            pnlCash.Controls.Add(lblCashTitle);
            pnlCash.Dock = DockStyle.Fill;
            pnlCash.Location = new Point(0, 251);
            pnlCash.Margin = new Padding(0, 0, 0, 8);
            pnlCash.Name = "pnlCash";
            pnlCash.Size = new Size(407, 93);
            pnlCash.TabIndex = 2;
            // 
            // txtCashReceived
            // 
            txtCashReceived.BackColor = Color.White;
            txtCashReceived.Dock = DockStyle.Top;
            txtCashReceived.Font = new Font("Segoe UI", 16F);
            txtCashReceived.Location = new Point(0, 37);
            txtCashReceived.Margin = new Padding(3, 4, 3, 4);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.PlaceholderText = "0";
            txtCashReceived.Size = new Size(407, 43);
            txtCashReceived.TabIndex = 2;
            txtCashReceived.TextAlign = HorizontalAlignment.Right;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;
            // 
            // lblCashTitle
            // 
            lblCashTitle.Dock = DockStyle.Top;
            lblCashTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCashTitle.ForeColor = Color.FromArgb(75, 85, 99);
            lblCashTitle.Location = new Point(0, 0);
            lblCashTitle.Name = "lblCashTitle";
            lblCashTitle.Size = new Size(407, 37);
            lblCashTitle.TabIndex = 0;
            lblCashTitle.Text = "TIỀN KHÁCH ĐƯA";
            lblCashTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowQuickCash
            // 
            flowQuickCash.Controls.Add(btnExact);
            flowQuickCash.Controls.Add(btn50k);
            flowQuickCash.Controls.Add(btn100k);
            flowQuickCash.Controls.Add(btn200k);
            flowQuickCash.Controls.Add(btn500k);
            flowQuickCash.Dock = DockStyle.Fill;
            flowQuickCash.Location = new Point(0, 352);
            flowQuickCash.Margin = new Padding(0, 0, 0, 13);
            flowQuickCash.Name = "flowQuickCash";
            flowQuickCash.Size = new Size(407, 46);
            flowQuickCash.TabIndex = 3;
            flowQuickCash.WrapContents = false;
            // 
            // btnExact
            // 
            btnExact.BackColor = Color.FromArgb(239, 246, 255);
            btnExact.Cursor = Cursors.Hand;
            btnExact.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnExact.FlatStyle = FlatStyle.Flat;
            btnExact.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExact.ForeColor = Color.FromArgb(37, 99, 235);
            btnExact.Location = new Point(0, 0);
            btnExact.Margin = new Padding(0, 0, 5, 0);
            btnExact.Name = "btnExact";
            btnExact.Size = new Size(89, 48);
            btnExact.TabIndex = 0;
            btnExact.TabStop = false;
            btnExact.Tag = "EXACT";
            btnExact.Text = "Vừa đủ";
            btnExact.UseVisualStyleBackColor = false;
            btnExact.Click += btnQuickCash_Click;
            // 
            // btn50k
            // 
            btn50k.BackColor = Color.White;
            btn50k.Cursor = Cursors.Hand;
            btn50k.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btn50k.FlatStyle = FlatStyle.Flat;
            btn50k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn50k.ForeColor = Color.FromArgb(17, 24, 39);
            btn50k.Location = new Point(94, 0);
            btn50k.Margin = new Padding(0, 0, 5, 0);
            btn50k.Name = "btn50k";
            btn50k.Size = new Size(71, 48);
            btn50k.TabIndex = 1;
            btn50k.TabStop = false;
            btn50k.Tag = "50000";
            btn50k.Text = "50k";
            btn50k.UseVisualStyleBackColor = false;
            btn50k.Click += btnQuickCash_Click;
            // 
            // btn100k
            // 
            btn100k.BackColor = Color.White;
            btn100k.Cursor = Cursors.Hand;
            btn100k.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btn100k.FlatStyle = FlatStyle.Flat;
            btn100k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn100k.ForeColor = Color.FromArgb(17, 24, 39);
            btn100k.Location = new Point(170, 0);
            btn100k.Margin = new Padding(0, 0, 5, 0);
            btn100k.Name = "btn100k";
            btn100k.Size = new Size(71, 48);
            btn100k.TabIndex = 2;
            btn100k.TabStop = false;
            btn100k.Tag = "100000";
            btn100k.Text = "100k";
            btn100k.UseVisualStyleBackColor = false;
            btn100k.Click += btnQuickCash_Click;
            // 
            // btn200k
            // 
            btn200k.BackColor = Color.White;
            btn200k.Cursor = Cursors.Hand;
            btn200k.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btn200k.FlatStyle = FlatStyle.Flat;
            btn200k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn200k.ForeColor = Color.FromArgb(17, 24, 39);
            btn200k.Location = new Point(246, 0);
            btn200k.Margin = new Padding(0, 0, 5, 0);
            btn200k.Name = "btn200k";
            btn200k.Size = new Size(71, 48);
            btn200k.TabIndex = 3;
            btn200k.TabStop = false;
            btn200k.Tag = "200000";
            btn200k.Text = "200k";
            btn200k.UseVisualStyleBackColor = false;
            btn200k.Click += btnQuickCash_Click;
            // 
            // btn500k
            // 
            btn500k.BackColor = Color.White;
            btn500k.Cursor = Cursors.Hand;
            btn500k.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btn500k.FlatStyle = FlatStyle.Flat;
            btn500k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn500k.ForeColor = Color.FromArgb(17, 24, 39);
            btn500k.Location = new Point(322, 0);
            btn500k.Margin = new Padding(0);
            btn500k.Name = "btn500k";
            btn500k.Size = new Size(71, 48);
            btn500k.TabIndex = 4;
            btn500k.TabStop = false;
            btn500k.Tag = "500000";
            btn500k.Text = "500k";
            btn500k.UseVisualStyleBackColor = false;
            btn500k.Click += btnQuickCash_Click;
            // 
            // pnlChange
            // 
            pnlChange.BackColor = Color.White;
            pnlChange.Controls.Add(lblChange);
            pnlChange.Controls.Add(lblChangeTitle);
            pnlChange.Dock = DockStyle.Fill;
            pnlChange.Location = new Point(0, 411);
            pnlChange.Margin = new Padding(0, 0, 0, 13);
            pnlChange.Name = "pnlChange";
            pnlChange.Padding = new Padding(16, 8, 16, 8);
            pnlChange.Size = new Size(407, 102);
            pnlChange.TabIndex = 4;
            // 
            // lblChange
            // 
            lblChange.Dock = DockStyle.Fill;
            lblChange.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblChange.ForeColor = Color.FromArgb(17, 24, 39);
            lblChange.Location = new Point(16, 40);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(375, 54);
            lblChange.TabIndex = 1;
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblChangeTitle
            // 
            lblChangeTitle.Dock = DockStyle.Top;
            lblChangeTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChangeTitle.ForeColor = Color.FromArgb(75, 85, 99);
            lblChangeTitle.Location = new Point(16, 8);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(375, 32);
            lblChangeTitle.TabIndex = 0;
            lblChangeTitle.Text = "TIỀN THỪA TRẢ LẠI";
            lblChangeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(22, 163, 74);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.Dock = DockStyle.Fill;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(0, 702);
            btnCheckout.Margin = new Padding(0, 0, 0, 13);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(407, 62);
            btnCheckout.TabIndex = 3;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.White;
            btnClearCart.Cursor = Cursors.Hand;
            btnClearCart.Dock = DockStyle.Fill;
            btnClearCart.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.FromArgb(220, 38, 38);
            btnClearCart.Location = new Point(0, 777);
            btnClearCart.Margin = new Padding(0);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(407, 53);
            btnClearCart.TabIndex = 4;
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1257, 867);
            Controls.Add(pnlLeft);
            Controls.Add(pnlRight);
            Font = new Font("Segoe UI", 9F);
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormPOS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Màn hình Thu ngân (POS) - Mini Supermarket";
            KeyDown += FormPOS_KeyDown;
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnlCartTools.ResumeLayout(false);
            tlpCartTools.ResumeLayout(false);
            pnlBarcode.ResumeLayout(false);
            tlpBarcode.ResumeLayout(false);
            tlpBarcode.PerformLayout();
            pnlRight.ResumeLayout(false);
            tlpRight.ResumeLayout(false);
            grpCustomer.ResumeLayout(false);
            tlpCustomer.ResumeLayout(false);
            tlpCustomer.PerformLayout();
            pnlTotalCard.ResumeLayout(false);
            pnlCash.ResumeLayout(false);
            pnlCash.PerformLayout();
            flowQuickCash.ResumeLayout(false);
            pnlChange.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        // Khu vực trái: ô quét + giỏ hàng
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.TableLayoutPanel tlpBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Panel pnlGap;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlCartTools;
        private System.Windows.Forms.TableLayoutPanel tlpCartTools;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Label lblHint;

        // Khu vực phải: khách hàng + thanh toán
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.TableLayoutPanel tlpCustomer;
        private System.Windows.Forms.Label lblPhoneTitle;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblCustomerName;

        private System.Windows.Forms.Panel pnlTotalCard;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel pnlCash;
        private System.Windows.Forms.Label lblCashTitle;
        private System.Windows.Forms.TextBox txtCashReceived;
        private System.Windows.Forms.FlowLayoutPanel flowQuickCash;
        private System.Windows.Forms.Button btnExact;
        private System.Windows.Forms.Button btn50k;
        private System.Windows.Forms.Button btn100k;
        private System.Windows.Forms.Button btn200k;
        private System.Windows.Forms.Button btn500k;
        private System.Windows.Forms.Panel pnlChange;
        private System.Windows.Forms.Label lblChangeTitle;
        private System.Windows.Forms.Label lblChange;

        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
    }
}