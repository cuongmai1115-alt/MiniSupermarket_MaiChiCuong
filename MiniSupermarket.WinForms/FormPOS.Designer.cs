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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblChangeTitle = new System.Windows.Forms.Label();
            this.txtCashReceived = new System.Windows.Forms.TextBox();
            this.lblCashTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.lblPhoneTitle = new System.Windows.Forms.Label();

            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlBarcode.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpCustomer.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlLeft (Khu vực giỏ hàng bên trái)
            // 
            this.pnlLeft.Controls.Add(this.dgvCart);
            this.pnlLeft.Controls.Add(this.pnlBarcode);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(650, 600);
            this.pnlLeft.TabIndex = 0;

            // 
            // pnlBarcode
            // 
            this.pnlBarcode.Controls.Add(this.txtBarcode);
            this.pnlBarcode.Controls.Add(this.lblBarcode);
            this.pnlBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarcode.Location = new System.Drawing.Point(10, 10);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(630, 50);
            this.pnlBarcode.TabIndex = 0;

            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.Location = new System.Drawing.Point(5, 13);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(120, 20);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Quét Mã Vạch:";

            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBarcode.Location = new System.Drawing.Point(130, 10);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(490, 29);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);

            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(10, 60);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(630, 530);
            this.dgvCart.TabIndex = 1;

            // 
            // pnlRight (Khu vực thanh toán bên phải)
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.pnlRight.Controls.Add(this.btnClearCart);
            this.pnlRight.Controls.Add(this.btnCheckout);
            this.pnlRight.Controls.Add(this.lblChange);
            this.pnlRight.Controls.Add(this.lblChangeTitle);
            this.pnlRight.Controls.Add(this.txtCashReceived);
            this.pnlRight.Controls.Add(this.lblCashTitle);
            this.pnlRight.Controls.Add(this.lblTotalAmount);
            this.pnlRight.Controls.Add(this.lblTotalTitle);
            this.pnlRight.Controls.Add(this.grpCustomer);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(650, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(334, 600);
            this.pnlRight.TabIndex = 1;

            // 
            // grpCustomer
            // 
            this.grpCustomer.Controls.Add(this.lblCustomerName);
            this.grpCustomer.Controls.Add(this.txtCustomerPhone);
            this.grpCustomer.Controls.Add(this.lblPhoneTitle);
            this.grpCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCustomer.Location = new System.Drawing.Point(15, 15);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(304, 110);
            this.grpCustomer.TabIndex = 0;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Khách Hàng";

            // 
            // lblPhoneTitle
            // 
            this.lblPhoneTitle.AutoSize = true;
            this.lblPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhoneTitle.Location = new System.Drawing.Point(10, 30);
            this.lblPhoneTitle.Name = "lblPhoneTitle";
            this.lblPhoneTitle.Size = new System.Drawing.Size(78, 15);
            this.lblPhoneTitle.TabIndex = 0;
            this.lblPhoneTitle.Text = "Số điện thoại:";

            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerPhone.Location = new System.Drawing.Point(95, 25);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new System.Drawing.Size(195, 25);
            this.txtCustomerPhone.TabIndex = 1;

            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblCustomerName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCustomerName.Location = new System.Drawing.Point(10, 68);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(99, 19);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Khách vãng lai";

            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.Location = new System.Drawing.Point(15, 150);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(167, 20);
            this.lblTotalTitle.TabIndex = 1;
            this.lblTotalTitle.Text = "TỔNG TIỀN CẦN TRẢ:";

            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalAmount.Location = new System.Drawing.Point(15, 180);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(304, 40);
            this.lblTotalAmount.TabIndex = 2;
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblCashTitle
            // 
            this.lblCashTitle.AutoSize = true;
            this.lblCashTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashTitle.Location = new System.Drawing.Point(15, 240);
            this.lblCashTitle.Name = "lblCashTitle";
            this.lblCashTitle.Size = new System.Drawing.Size(117, 19);
            this.lblCashTitle.TabIndex = 3;
            this.lblCashTitle.Text = "Tiền Khách Đưa:";

            // 
            // txtCashReceived
            // 
            this.txtCashReceived.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCashReceived.Location = new System.Drawing.Point(15, 265);
            this.txtCashReceived.Name = "txtCashReceived";
            this.txtCashReceived.Size = new System.Drawing.Size(304, 32);
            this.txtCashReceived.TabIndex = 4;
            this.txtCashReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCashReceived.TextChanged += new System.EventHandler(this.txtCashReceived_TextChanged);

            // 
            // lblChangeTitle
            // 
            this.lblChangeTitle.AutoSize = true;
            this.lblChangeTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChangeTitle.Location = new System.Drawing.Point(15, 315);
            this.lblChangeTitle.Name = "lblChangeTitle";
            this.lblChangeTitle.Size = new System.Drawing.Size(127, 19);
            this.lblChangeTitle.TabIndex = 5;
            this.lblChangeTitle.Text = "Tiền Thừa Trả Lại:";

            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChange.Location = new System.Drawing.Point(15, 340);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(304, 30);
            this.lblChange.TabIndex = 6;
            this.lblChange.Text = "0 đ";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(15, 410);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(304, 55);
            this.btnCheckout.TabIndex = 7;
            this.btnCheckout.Text = "THANH TOÁN (F9)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.Crimson;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(15, 480);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(304, 40);
            this.btnClearCart.TabIndex = 8;
            this.btnClearCart.Text = "HỦY GIỎ HÀNG";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);

            // 
            // FormPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 600);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.KeyPreview = true;
            this.Name = "FormPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Màn hình Thu ngân (POS) - Mini Supermarket";
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlBarcode.ResumeLayout(false);
            this.pnlBarcode.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.DataGridView dgvCart;

        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.Label lblPhoneTitle;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblCustomerName;

        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblCashTitle;
        private System.Windows.Forms.TextBox txtCashReceived;
        private System.Windows.Forms.Label lblChangeTitle;
        private System.Windows.Forms.Label lblChange;

        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
    }
}