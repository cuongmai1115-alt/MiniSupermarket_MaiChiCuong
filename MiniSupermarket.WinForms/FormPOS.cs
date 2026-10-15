using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        // BindingList để lưới tự cập nhật khi thêm/xóa dòng (đổi số lượng thì gọi ResetBindings)
        private readonly BindingList<CartItemDto> _cart = new BindingList<CartItemDto>();

        private bool _isFormattingCash;   // chặn vòng lặp khi tự định dạng ô tiền
        private bool _isCheckingOut;      // chặn bấm thanh toán 2 lần (tránh trừ kho 2 lần)

        private static readonly Color ColorText = Color.FromArgb(17, 24, 39);
        private static readonly Color ColorMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color ColorOk = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorWarn = Color.FromArgb(217, 119, 6);
        private static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
            UpdateCartDisplay();

            // Focus ô quét SAU khi form đã hiện (không gán ActiveControl lúc khởi tạo để tránh lỗi khi nhúng/đóng form con)
            Load += (s, e) => BeginInvoke(new Action(() => { if (!IsDisposed) txtBarcode.Focus(); }));
        }

        #region Cấu hình lưới giỏ hàng

        /// <summary>
        /// Cấu hình giao diện + các cột cho DataGridView giỏ hàng
        /// </summary>
        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            // Giao diện tổng thể: nền trắng, bỏ cột tiêu đề dòng trống, chỉ kẻ ngang
            dgvCart.RowHeadersVisible = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCart.GridColor = Color.FromArgb(229, 231, 235);
            dgvCart.AllowUserToResizeRows = false;
            dgvCart.AllowUserToResizeColumns = false;
            dgvCart.MultiSelect = false;
            dgvCart.RowTemplate.Height = 42;

            // Tiêu đề cột
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCart.ColumnHeadersHeight = 42;
            dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(31, 41, 55),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(31, 41, 55),
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };

            // Ô dữ liệu: chữ to dễ đọc, dòng chọn màu xanh nhạt, dòng chẵn lẻ xen kẽ
            dgvCart.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = ColorText,
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = ColorText,
                Font = new Font("Segoe UI", 11F),
                Padding = new Padding(8, 0, 8, 0)
            };
            dgvCart.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            AddColumn("Mã SP", "ProductId", 80, false, null, DataGridViewContentAlignment.MiddleLeft);
            AddColumn("Tên Sản Phẩm", "ProductName", 0, true, null, DataGridViewContentAlignment.MiddleLeft);
            AddColumn("Đơn Giá", "UnitPrice", 120, false, "N0", DataGridViewContentAlignment.MiddleRight);
            AddColumn("SL", "Quantity", 70, false, null, DataGridViewContentAlignment.MiddleCenter);
            AddColumn("Thành Tiền", "TotalPrice", 140, false, "N0", DataGridViewContentAlignment.MiddleRight, bold: true);

            dgvCart.DataSource = _cart;
        }

        private void AddColumn(string header, string property, int width, bool fill, string? format,
            DataGridViewContentAlignment align, bool bold = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                DataPropertyName = property,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            if (fill)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = 160;
            }
            else
            {
                col.Width = width;
            }

            col.DefaultCellStyle.Alignment = align;
            col.HeaderCell.Style.Alignment = align;
            if (format != null) col.DefaultCellStyle.Format = format;
            if (bold) col.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            dgvCart.Columns.Add(col);
        }

        #endregion

        #region Quét mã vạch & giỏ hàng

        /// <summary>
        /// Sự kiện khi quét hoặc nhập mã vạch và nhấn Enter
        /// </summary>
        private async void txtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // bỏ tiếng "ding" của Windows
                if (string.IsNullOrWhiteSpace(txtBarcode.Text)) return;

                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
                txtBarcode.Focus(); // sẵn sàng cho lần quét kế tiếp
            }
        }

        /// <summary>
        /// Gọi API tra cứu sản phẩm theo mã vạch và thêm vào giỏ hàng
        /// </summary>
        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                var res = await ApiClientService.Client.GetAsync($"products/barcode/{Uri.EscapeDataString(barcode)}");

                // API trả 404 khi không có mã vạch này: báo đúng nguyên nhân thay vì "lỗi kết nối"
                if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    ShowWarning($"Không tìm thấy sản phẩm có mã vạch: {barcode}");
                    return;
                }
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show(await BuildErrorAsync("Tra cứu sản phẩm", res), "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var product = await res.Content.ReadFromJsonAsync<ProductDto>();
                if (product == null)
                {
                    ShowWarning("Không tìm thấy sản phẩm có mã vạch này!");
                    return;
                }

                if (product.StockQuantity <= 0)
                {
                    ShowWarning($"'{product.ProductName}' đã hết hàng trong kho!");
                    return;
                }

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                CartItemDto target;
                if (existingItem != null)
                {
                    if (existingItem.Quantity + 1 > product.StockQuantity)
                    {
                        ShowWarning($"'{product.ProductName}' chỉ còn {product.StockQuantity} trong kho!");
                        return;
                    }
                    existingItem.Quantity++;
                    existingItem.StockQuantity = product.StockQuantity;
                    target = existingItem;
                }
                else
                {
                    target = new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1,
                        StockQuantity = product.StockQuantity
                    };
                    _cart.Add(target);
                }

                UpdateCartDisplay();
                SelectCartItem(target); // dòng vừa quét luôn được chọn + cuộn vào tầm nhìn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật giao diện hiển thị giỏ hàng, số lượng và tổng tiền
        /// </summary>
        private void UpdateCartDisplay()
        {
            int keepIndex = dgvCart.CurrentRow?.Index ?? -1;

            _cart.ResetBindings(); // làm mới lưới (cần khi đổi Quantity của dòng có sẵn)

            if (keepIndex >= 0 && keepIndex < dgvCart.Rows.Count)
                dgvCart.CurrentCell = dgvCart.Rows[keepIndex].Cells[1];

            decimal total = GetTotal();
            int totalQty = _cart.Sum(x => x.Quantity);

            lblTotalAmount.Text = $"{total:N0} đ";
            lblItemCount.Text = _cart.Count == 0
                ? "Giỏ hàng trống"
                : $"{_cart.Count} mặt hàng  •  {totalQty} sản phẩm";

            CalculateChange();
        }

        private void SelectCartItem(CartItemDto item)
        {
            int idx = _cart.IndexOf(item);
            if (idx < 0 || idx >= dgvCart.Rows.Count) return;

            dgvCart.ClearSelection();
            dgvCart.CurrentCell = dgvCart.Rows[idx].Cells[1]; // FullRowSelect => chọn cả dòng
            dgvCart.Rows[idx].Selected = true;
        }

        private CartItemDto? GetSelectedItem() => dgvCart.CurrentRow?.DataBoundItem as CartItemDto;

        private decimal GetTotal() => _cart.Sum(x => x.TotalPrice);

        #endregion

        #region Thao tác trên dòng giỏ hàng (+, -, xóa)

        private void ChangeSelectedQuantity(int delta)
        {
            var item = GetSelectedItem();
            if (item == null) return;

            int newQty = item.Quantity + delta;
            if (newQty < 1) return; // muốn bỏ hẳn thì dùng nút Xóa dòng

            if (item.StockQuantity > 0 && newQty > item.StockQuantity)
            {
                ShowWarning($"'{item.ProductName}' chỉ còn {item.StockQuantity} trong kho!");
                return;
            }

            item.Quantity = newQty;
            UpdateCartDisplay();
        }

        private void RemoveSelectedItem()
        {
            var item = GetSelectedItem();
            if (item == null) return;

            _cart.Remove(item);
            UpdateCartDisplay();
        }

        private void btnPlus_Click(object? sender, EventArgs e)
        {
            ChangeSelectedQuantity(+1);
            txtBarcode.Focus();
        }

        private void btnMinus_Click(object? sender, EventArgs e)
        {
            ChangeSelectedQuantity(-1);
            txtBarcode.Focus();
        }

        private void btnRemoveItem_Click(object? sender, EventArgs e)
        {
            RemoveSelectedItem();
            txtBarcode.Focus();
        }

        /// <summary>
        /// Phím tắt toàn màn hình (KeyPreview = true):
        /// F9 thanh toán, F2 về ô quét mã, Delete xóa dòng, +/- đổi số lượng.
        /// Khi đang gõ trong ô nhập thì Delete và +/- vẫn hoạt động như bình thường.
        /// </summary>
        private void FormPOS_KeyDown(object? sender, KeyEventArgs e)
        {
            bool typing = ActiveControl is TextBox;

            switch (e.KeyCode)
            {
                case Keys.F9:
                    e.SuppressKeyPress = true;
                    btnCheckout.PerformClick();
                    break;

                case Keys.F2:
                    e.SuppressKeyPress = true;
                    txtBarcode.Focus();
                    txtBarcode.SelectAll();
                    break;

                case Keys.Delete when !typing:
                    e.SuppressKeyPress = true;
                    RemoveSelectedItem();
                    break;

                case Keys.Add when !typing:
                case Keys.Oemplus when !typing:
                    e.SuppressKeyPress = true;
                    ChangeSelectedQuantity(+1);
                    break;

                case Keys.Subtract when !typing:
                case Keys.OemMinus when !typing:
                    e.SuppressKeyPress = true;
                    ChangeSelectedQuantity(-1);
                    break;
            }
        }

        #endregion

        #region Khách hàng (tra cứu theo SĐT)

        private void txtCustomerPhone_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBarcode.Focus(); // rời ô SĐT => sự kiện Leave sẽ tra cứu khách
            }
        }

        private async void txtCustomerPhone_Leave(object? sender, EventArgs e)
        {
            await LookupCustomerAsync();
        }

        /// <summary>
        /// Tra cứu khách theo số điện thoại để cashier thấy ngay tên, hạng thẻ, điểm hiện có
        /// </summary>
        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();
            if (phone.Length == 0)
            {
                ResetCustomerLabel();
                return;
            }

            try
            {
                var list = await ApiClientService.Client
                    .GetFromJsonAsync<List<PosCustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(phone)}");

                // Người dùng đã sửa SĐT trong lúc chờ API thì bỏ kết quả cũ
                if (txtCustomerPhone.Text.Trim() != phone) return;

                var customer = list?.FirstOrDefault(c => c.PhoneNumber == phone);
                if (customer != null)
                {
                    lblCustomerName.Text = $"{customer.CustomerName}\nHạng {customer.MembershipRank}  •  {customer.RewardPoints:N0} điểm";
                    lblCustomerName.ForeColor = ColorOk;
                }
                else
                {
                    lblCustomerName.Text = "Chưa có trong hệ thống - đơn này không tích điểm";
                    lblCustomerName.ForeColor = ColorWarn;
                }
            }
            catch
            {
                lblCustomerName.Text = "Không tra cứu được khách hàng (lỗi kết nối)";
                lblCustomerName.ForeColor = ColorDanger;
            }
        }

        private void ResetCustomerLabel()
        {
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.ForeColor = ColorMuted;
        }

        #endregion

        #region Tiền khách đưa & tiền thừa

        /// <summary>
        /// Tự định dạng ô tiền khách đưa (1.000.000) và tính lại tiền thừa mỗi khi gõ
        /// </summary>
        private void txtCashReceived_TextChanged(object? sender, EventArgs e)
        {
            if (_isFormattingCash) return;

            _isFormattingCash = true;
            try
            {
                string digits = GetCashDigits();
                if (digits.Length > 12) digits = digits.Substring(0, 12);

                string formatted = digits.Length == 0
                    ? string.Empty
                    : decimal.Parse(digits, CultureInfo.InvariantCulture).ToString("N0");

                if (formatted != txtCashReceived.Text)
                {
                    txtCashReceived.Text = formatted;
                    txtCashReceived.SelectionStart = formatted.Length; // giữ con trỏ ở cuối
                }
            }
            finally
            {
                _isFormattingCash = false;
            }

            CalculateChange();
        }

        private string GetCashDigits() => new string(txtCashReceived.Text.Where(char.IsDigit).ToArray());

        private decimal GetCashReceived()
        {
            string digits = GetCashDigits();
            return digits.Length == 0 ? 0 : decimal.Parse(digits, CultureInfo.InvariantCulture);
        }

        private void CalculateChange()
        {
            if (GetCashDigits().Length == 0)
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = ColorText;
                return;
            }

            decimal change = GetCashReceived() - GetTotal();
            if (change >= 0)
            {
                lblChange.Text = $"{change:N0} đ";
                lblChange.ForeColor = ColorText;
            }
            else
            {
                lblChange.Text = $"Thiếu {-change:N0} đ";
                lblChange.ForeColor = ColorDanger;
            }
        }

        /// <summary>
        /// Nút mệnh giá nhanh: "Vừa đủ" (Tag = EXACT) hoặc số tiền ghi trong Tag
        /// </summary>
        private void btnQuickCash_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            string tag = btn.Tag?.ToString() ?? string.Empty;
            decimal amount = tag == "EXACT"
                ? GetTotal()
                : decimal.Parse(tag, CultureInfo.InvariantCulture);

            txtCashReceived.Text = amount > 0 ? amount.ToString("N0") : string.Empty;
            txtBarcode.Focus();
        }

        #endregion

        #region Thanh toán & hủy giỏ hàng

        /// <summary>
        /// Xử lý sự kiện bấm nút Thanh toán (hoặc phím tắt F9)
        /// </summary>
        private async void btnCheckout_Click(object? sender, EventArgs e)
        {
            if (_isCheckingOut) return;

            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                return;
            }

            decimal total = GetTotal();

            // Đã nhập tiền khách đưa mà chưa đủ thì không cho thanh toán
            if (GetCashDigits().Length > 0 && GetCashReceived() < total)
            {
                MessageBox.Show($"Tiền khách đưa chưa đủ. Còn thiếu {total - GetCashReceived():N0} đ.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCashReceived.Focus();
                return;
            }

            // Giữ nguyên payload cũ; server vẫn tự lấy giá từ DB, không tin UnitPrice client gửi lên
            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            SetCheckoutBusy(true);
            try
            {
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CheckoutResultDto>();
                    decimal paid = result != null && result.TotalAmount > 0 ? result.TotalAmount : total;
                    decimal cash = GetCashReceived();

                    string msg = $"Thanh toán thành công!\n\nTổng tiền: {paid:N0} đ";
                    if (cash > 0) msg += $"\nKhách đưa: {cash:N0} đ\nTiền thừa trả khách: {cash - paid:N0} đ";
                    if (result != null && result.PointsEarned > 0) msg += $"\nĐiểm tích lũy: +{result.PointsEarned}";

                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Dọn dẹp giỏ hàng sau khi thanh toán thành công
                    _cart.Clear();
                    UpdateCartDisplay();
                    txtCashReceived.Clear();
                    txtCustomerPhone.Clear();
                    ResetCustomerLabel();
                }
                else
                {
                    MessageBox.Show(await BuildErrorAsync("Thanh toán", response), "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetCheckoutBusy(false);
                txtBarcode.Focus();
            }
        }

        private void SetCheckoutBusy(bool busy)
        {
            _isCheckingOut = busy;
            btnCheckout.Enabled = !busy;
            btnCheckout.Text = busy ? "ĐANG XỬ LÝ..." : "THANH TOÁN (F9)";
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        /// <summary>
        /// Nút hủy toàn bộ giỏ hàng hiện tại
        /// </summary>
        private void btnClearCart_Click(object? sender, EventArgs e)
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
            txtBarcode.Focus();
        }

        #endregion

        #region Hỗ trợ thông báo lỗi

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static string GetReason(HttpStatusCode code) => code switch
        {
            HttpStatusCode.BadRequest => "Dữ liệu gửi lên không hợp lệ.",
            HttpStatusCode.Unauthorized => "Chưa đăng nhập hoặc token hết hạn. Hãy đăng nhập lại.",
            HttpStatusCode.Forbidden => "Bạn không có quyền thực hiện thao tác này.",
            HttpStatusCode.NotFound => "Không tìm thấy dữ liệu hoặc sai đường dẫn API.",
            HttpStatusCode.Conflict => "Dữ liệu bị trùng hoặc đang được sử dụng.",
            HttpStatusCode.InternalServerError => "Lỗi xảy ra phía Server.",
            _ => "Lỗi không xác định."
        };

        private static async Task<string> BuildErrorAsync(string action, HttpResponseMessage res)
        {
            string body = await res.Content.ReadAsStringAsync();
            string serverMsg = string.Empty;
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                    doc.RootElement.TryGetProperty("message", out var m))
                    serverMsg = m.GetString() ?? string.Empty;
            }
            catch { /* body không phải JSON */ }

            return $"{action} thất bại!\n\nMã lỗi: {(int)res.StatusCode} ({res.StatusCode})\n" +
                   $"Nguyên nhân: {GetReason(res.StatusCode)}" +
                   (string.IsNullOrEmpty(serverMsg) ? "" : $"\nChi tiết: {serverMsg}");
        }

        #endregion

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        // Tồn kho tại lúc quét, dùng để chặn tăng số lượng vượt kho (không hiển thị trên lưới)
        public int StockQuantity { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    // DTO riêng của màn hình POS (tên khác CustomerDto trong FormCustomerManagement để khỏi trùng)
    public class PosCustomerDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }

    // Khớp với JSON server trả về: { totalAmount, pointsEarned, cashier }
    public class CheckoutResultDto
    {
        public decimal TotalAmount { get; set; }
        public int PointsEarned { get; set; }
    }
    #endregion
}