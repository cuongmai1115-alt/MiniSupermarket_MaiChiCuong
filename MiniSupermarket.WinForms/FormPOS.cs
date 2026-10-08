using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new List<CartItemDto>();

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        /// <summary>
        /// Cấu hình các cột cho DataGridView Giỏ hàng
        /// </summary>
        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 80
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên Sản Phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn Giá",
                Width = 110
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 70
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành Tiền",
                Width = 120
            });
        }

        /// <summary>
        /// Sự kiện khi quét hoặc nhập mã vạch và nhấn Enter
        /// </summary>
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        /// <summary>
        /// Gọi API tra cứu sản phẩm theo mã vạch và thêm vào giỏ hàng
        /// </summary>
        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Gọi API lấy thông tin sản phẩm
                var product = await ApiClientService.Client.GetFromJsonAsync<ProductDto>($"products/barcode/{barcode}");

                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật giao diện hiển thị giỏ hàng và tổng tiền
        /// </summary>
        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
            CalculateChange();
        }

        /// <summary>
        /// Tính tiền thừa khi thay đổi số tiền khách đưa
        /// </summary>
        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.TotalPrice);
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                decimal change = cashReceived - total;
                lblChange.Text = change >= 0 ? $"{change:N0} đ" : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
            }
        }

        /// <summary>
        /// Xử lý sự kiện bấm nút Thanh toán (hoặc phím tắt F9)
        /// </summary>
        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thanh toán thành công và đã in hóa đơn!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Dọn dẹp giỏ hàng sau khi thanh toán thành công
                _cart.Clear();
                UpdateCartDisplay();
                txtCashReceived.Clear();
                txtCustomerPhone.Clear();
                lblCustomerName.Text = "Khách vãng lai";
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại từ máy chủ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút hủy toàn bộ giỏ hàng hiện tại
        /// </summary>
        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn hủy giỏ hàng này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _cart.Clear();
                    UpdateCartDisplay();
                    txtCashReceived.Clear();
                }
            }
        }
    }

    #region DTO Models
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
    #endregion
}