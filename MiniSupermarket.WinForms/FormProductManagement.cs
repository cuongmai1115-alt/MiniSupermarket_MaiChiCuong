using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private enum StatusKind { Info, Success, Warning, Error }

        private static readonly Color ColorMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color ColorOk = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorWarn = Color.FromArgb(217, 119, 6);
        private static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);

        // Hiển thị tiền Việt: 32.000
        private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");

        private List<ProductDto> _inventory = new();   // toàn bộ sản phẩm (dùng đếm cảnh báo, kiểm tra trùng mã vạch)
        private List<ProductDto> _current = new();     // kết quả server trả về theo từ khóa/danh mục đang tìm
        private int _selectedProductId;
        private int _busyCount; // đếm số request đang chạy để khóa/mở nút đúng lúc

        // Server chỉ cho Admin và Warehouse thêm/sửa/xóa (xem ProductsController); vai trò khác chỉ được xem
        private static bool CanEdit =>
            string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase);

        public FormProductManagement()
        {
            InitializeComponent();
            SetupGrid();

            // Gõ lại vào ô nào thì xóa dấu ! đỏ của ô đó
            txtBarcode.TextChanged += (s, e) => errorProvider1.SetError(txtBarcode, string.Empty);
            txtProductName.TextChanged += (s, e) => errorProvider1.SetError(txtProductName, string.Empty);
            txtPrice.TextChanged += (s, e) => errorProvider1.SetError(txtPrice, string.Empty);
            cbCategory.SelectedIndexChanged += (s, e) => errorProvider1.SetError(cbCategory, string.Empty);

            // Nhấn Enter trong ô tìm kiếm (hoặc máy quét mã vạch gửi Enter) = bấm nút Tìm kiếm
            txtKeyword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnSearch.PerformClick();
                }
            };

        }

        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            ApplyRolePermissions();
            await LoadCategoriesAsync();
            await ReloadAllAsync();
        }

        #region Giao diện lưới

        private void SetupGrid()
        {
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.GridColor = Color.FromArgb(229, 231, 235);
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.RowTemplate.Height = 34;

            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.ColumnHeadersHeight = 38;
            dgvProducts.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(31, 41, 55),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(31, 41, 55),
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.False,
                Padding = new Padding(6, 0, 6, 0)
            };
            dgvProducts.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(6, 0, 6, 0)
            };
            dgvProducts.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            // Mọi cột chia đều phần còn lại theo tỉ lệ (FillWeight) nên luôn vừa khung, không bị mất cột Tồn kho
            dgvProducts.ScrollBars = ScrollBars.Vertical;
            SetFill(colId, 9F, 50);
            SetFill(colBarcode, 20F, 105);
            SetFill(colProductName, 31F, 130);
            SetFill(colCategory, 20F, 100);
            SetFill(colPrice, 13F, 75);
            SetFill(colStock, 10F, 60);

            colPrice.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "N0",
                FormatProvider = ViCulture,
                Alignment = DataGridViewContentAlignment.MiddleRight
            };
            colStock.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };
        }

        private static void SetFill(DataGridViewColumn col, float weight, int minWidth)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = weight;
            col.MinimumWidth = minWidth;
        }

        #endregion

        #region Tạo thông báo lỗi chi tiết (cùng kiểu với form Nhóm hàng)

        private static string GetReason(string action, HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    return "Dữ liệu gửi lên chưa hợp lệ. Hãy kiểm tra lại thông tin đã nhập.";
                case HttpStatusCode.Unauthorized:
                    return "Phiên đăng nhập đã hết hạn hoặc chưa đăng nhập. Hãy đăng xuất rồi đăng nhập lại.";
                case HttpStatusCode.Forbidden:
                    return $"Tài khoản hiện tại (vai trò: {SessionManager.CurrentRole}) không có quyền \"{action}\". Chỉ Admin và Warehouse được thêm, sửa, xóa sản phẩm.";
                case HttpStatusCode.NotFound:
                    return "Không tìm thấy sản phẩm này (có thể đã bị xóa) hoặc sai địa chỉ API. Hãy bấm Tải lại để cập nhật danh sách.";
                case HttpStatusCode.Conflict:
                    return "Thao tác bị từ chối vì xung đột dữ liệu (trùng mã vạch hoặc sản phẩm đã phát sinh trong hóa đơn).";
                case HttpStatusCode.InternalServerError:
                    return "Server gặp lỗi khi xử lý yêu cầu. Xem cửa sổ log của project API để biết chi tiết.";
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                    return "Server tạm thời không phản hồi. Hãy thử lại sau ít phút.";
                default:
                    return "Lỗi không xác định.";
            }
        }

        private static string FieldLabel(string name)
        {
            string n = name.Replace("$.", string.Empty).ToLowerInvariant();
            if (n.Contains("barcode")) return "Mã vạch";
            if (n.Contains("productname")) return "Tên sản phẩm";
            if (n.Contains("price")) return "Đơn giá";
            if (n.Contains("stock")) return "Số lượng tồn";
            if (n.Contains("categoryid")) return "Danh mục";
            if (n.Contains("keyword")) return "Từ khóa";
            return name;
        }

        private static List<string> ExtractDetails(string body)
        {
            var details = new List<string>();
            if (string.IsNullOrWhiteSpace(body)) return details;
            try
            {
                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Object)
                {
                    if (root.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var field in errors.EnumerateObject())
                        {
                            if (field.Value.ValueKind != JsonValueKind.Array) continue;
                            foreach (var msg in field.Value.EnumerateArray())
                                details.Add($"{FieldLabel(field.Name)}: {msg.GetString()}");
                        }
                    }
                    else if (root.TryGetProperty("message", out var m)) details.Add(m.ToString());
                    else if (root.TryGetProperty("detail", out var d)) details.Add(d.ToString());
                    else if (root.TryGetProperty("title", out var t)) details.Add(t.ToString());
                }
                else if (root.ValueKind == JsonValueKind.String)
                {
                    details.Add(root.GetString() ?? string.Empty);
                }
            }
            catch (JsonException)
            {
                string text = body.Trim();
                if (!text.StartsWith("<")) details.Add(text.Length > 300 ? text.Substring(0, 300) + "..." : text);
            }
            return details;
        }

        private static async Task<(string Message, MessageBoxIcon Icon)> BuildApiErrorAsync(string action, HttpResponseMessage response)
        {
            string body = await response.Content.ReadAsStringAsync();
            var details = ExtractDetails(body);

            var sb = new StringBuilder();
            sb.AppendLine($"{action} thất bại!");
            sb.AppendLine();
            sb.AppendLine($"Nguyên nhân: {GetReason(action, response.StatusCode)}");
            if (details.Count == 1)
            {
                sb.AppendLine($"Chi tiết: {details[0]}");
            }
            else if (details.Count > 1)
            {
                sb.AppendLine("Chi tiết:");
                foreach (var line in details) sb.AppendLine($"  • {line}");
            }
            sb.AppendLine();
            sb.Append($"Mã lỗi HTTP: {(int)response.StatusCode} ({response.StatusCode})");

            var icon = response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Conflict
                ? MessageBoxIcon.Warning
                : MessageBoxIcon.Error;
            return (sb.ToString(), icon);
        }

        private static string BuildExceptionMessage(string action, Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{action} thất bại!");
            sb.AppendLine();

            if (ex is HttpRequestException hre)
            {
                if (hre.InnerException is System.Security.Authentication.AuthenticationException)
                {
                    sb.AppendLine("Nguyên nhân: Chứng chỉ HTTPS của Server không được tin cậy.");
                    sb.AppendLine("Cách xử lý: chạy lệnh  dotnet dev-certs https --trust  rồi mở lại ứng dụng.");
                }
                else
                {
                    sb.AppendLine($"Nguyên nhân: Không kết nối được tới Server ({ApiClientService.Client.BaseAddress}).");
                    sb.AppendLine("Hãy kiểm tra: project MiniSupermarket.API đã chạy chưa, port có khớp không, tường lửa có chặn không.");
                }
                sb.AppendLine();
                sb.Append($"Chi tiết kỹ thuật: {(hre.InnerException is SocketException se ? se.Message : hre.Message)}");
            }
            else if (ex is TaskCanceledException)
            {
                sb.AppendLine("Nguyên nhân: Hết thời gian chờ phản hồi từ Server.");
                sb.Append("Hãy kiểm tra API có đang bị treo hoặc SQL Server có đang chạy không rồi thử lại.");
            }
            else if (ex is JsonException)
            {
                sb.AppendLine("Nguyên nhân: Dữ liệu Server trả về sai định dạng.");
                sb.Append($"Chi tiết kỹ thuật: {ex.Message}");
            }
            else
            {
                sb.AppendLine($"Nguyên nhân: Lỗi không xác định ({ex.GetType().Name}).");
                sb.Append($"Chi tiết kỹ thuật: {ex.Message}");
            }
            return sb.ToString();
        }

        private async Task ShowApiErrorAsync(string action, HttpResponseMessage response)
        {
            var (message, icon) = await BuildApiErrorAsync(action, response);
            MessageBox.Show(message, $"Lỗi - {action}", MessageBoxButtons.OK, icon);
            SetStatus($"{action} thất bại (HTTP {(int)response.StatusCode}).", StatusKind.Error);
        }

        private void ShowException(string action, Exception ex)
        {
            MessageBox.Show(BuildExceptionMessage(action, ex), $"Lỗi - {action}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SetStatus($"{action} thất bại: không kết nối được Server.", StatusKind.Error);
        }

        #endregion

        #region Tiện ích giao diện

        private void SetStatus(string text, StatusKind kind)
        {
            if (IsDisposed) return;
            lblStatus.Text = text;
            lblStatus.ForeColor = kind switch
            {
                StatusKind.Success => ColorOk,
                StatusKind.Warning => ColorWarn,
                StatusKind.Error => ColorDanger,
                _ => ColorMuted
            };
        }

        // Khóa các nút trong lúc gọi API (tránh bấm 2 lần gửi trùng request)
        private void SetBusy(bool busy)
        {
            _busyCount += busy ? 1 : -1;
            if (IsDisposed) return;

            bool idle = _busyCount <= 0;
            foreach (var b in new[] { btnClear, btnSearch, btnLoad })
                b.Enabled = idle;
            // Nút ghi dữ liệu còn phụ thuộc vai trò
            foreach (var b in new[] { btnAdd, btnUpdate, btnDelete })
                b.Enabled = idle && CanEdit;
            UseWaitCursor = !idle;
        }

        // Vai trò chỉ được xem (vd Cashier): khóa ô nhập + nút ghi, đỡ phải chờ server trả 403
        private void ApplyRolePermissions()
        {
            bool can = CanEdit;
            btnAdd.Enabled = btnUpdate.Enabled = btnDelete.Enabled = can;
            txtBarcode.ReadOnly = txtProductName.ReadOnly = txtPrice.ReadOnly = !can;
            cbCategory.Enabled = numStock.Enabled = can;
            if (!can) groupBox3.Text = "Thông tin sản phẩm (chỉ xem)";
        }

        // Đưa danh sách đang chọn lên lưới và cập nhật dòng trạng thái
        private void ShowCurrent()
        {
            if (IsDisposed) return;

            dgvProducts.DataSource = _current.ToList();
            dgvProducts.ClearSelection(); // không chọn sẵn dòng đầu để tránh lệch với các ô nhập

            string keyword = txtKeyword.Text.Trim();
            if (keyword.Length == 0)
                SetStatus(_current.Count == 0 ? "Chưa có sản phẩm nào." : $"Hiển thị {_current.Count} sản phẩm.", _current.Count == 0 ? StatusKind.Warning : StatusKind.Info);
            else if (_current.Count == 0)
                SetStatus($"Không tìm thấy sản phẩm nào với từ khóa \"{keyword}\".", StatusKind.Warning);
            else
                SetStatus($"Tìm thấy {_current.Count} sản phẩm với từ khóa \"{keyword}\".", StatusKind.Info);
        }

        private void ClearInput()
        {
            _selectedProductId = 0;
            errorProvider1.Clear();
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            numStock.Value = 0;
            cbCategory.SelectedIndex = -1;
            dgvProducts.ClearSelection();
            txtBarcode.Focus();
        }

        // Đọc giá tiền người dùng gõ: chấp nhận 32000 / 32.000 / 32,000 (đơn vị đồng, số nguyên)
        private static bool TryParsePrice(string text, out decimal price)
        {
            string digits = text.Replace(".", "").Replace(",", "").Replace(" ", "");
            return decimal.TryParse(digits, NumberStyles.None, CultureInfo.InvariantCulture, out price);
        }

        // Kiểm tra dữ liệu nhập ngay tại Client, báo lỗi cạnh đúng ô nhập
        // ignoreId: ID dòng đang sửa (bỏ qua khi so trùng mã vạch); thêm mới thì để 0
        private bool ValidateInputs(int ignoreId, out decimal price)
        {
            price = 0;
            errorProvider1.Clear();
            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (barcode.Length == 0)
                return Invalid(txtBarcode, "Mã vạch không được để trống.");
            if (barcode.Length > 50)
                return Invalid(txtBarcode, $"Mã vạch tối đa 50 ký tự (đang nhập {barcode.Length}).");
            if (_inventory.Any(p => p.ProductId != ignoreId && string.Equals(p.Barcode.Trim(), barcode, StringComparison.OrdinalIgnoreCase)))
                return Invalid(txtBarcode, $"Mã vạch {barcode} đã thuộc sản phẩm khác.");
            if (name.Length == 0)
                return Invalid(txtProductName, "Tên sản phẩm không được để trống.");
            if (name.Length > 150)
                return Invalid(txtProductName, $"Tên sản phẩm tối đa 150 ký tự (đang nhập {name.Length}).");
            if (cbCategory.SelectedValue is not int catId || catId <= 0)
                return Invalid(cbCategory, "Vui lòng chọn danh mục cho sản phẩm.");
            if (!TryParsePrice(txtPrice.Text.Trim(), out price) || price <= 0)
                return Invalid(txtPrice, "Đơn giá phải là số nguyên dương (đơn vị đồng), ví dụ 32000.");
            return true;
        }

        private bool Invalid(Control field, string message)
        {
            errorProvider1.SetError(field, message);
            errorProvider1.SetIconPadding(field, -22);
            MessageBox.Show(message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            field.Focus();
            return false;
        }

        #endregion

        #region Tải dữ liệu từ API

        private async Task LoadCategoriesAsync()
        {
            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.GetAsync("categories");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Tải danh mục", response);
                    return;
                }
                var cats = await response.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? new List<CategoryDto>();

                // Combo nhập liệu của khung thông tin sản phẩm
                cbCategory.DataSource = new List<CategoryDto>(cats);
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryId";
                cbCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowException("Tải danh mục", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // GET api/products[?keyword=&categoryId=]; lỗi thì tự báo và trả về null
        private async Task<List<ProductDto>?> FetchProductsAsync(string action, string url)
        {
            try
            {
                using var response = await ApiClientService.Client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync(action, response);
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<List<ProductDto>>() ?? new List<ProductDto>();
            }
            catch (Exception ex)
            {
                ShowException(action, ex);
                return null;
            }
        }

        // Tải lại TOÀN BỘ sản phẩm (để cập nhật cảnh báo) rồi áp lại bộ lọc đang chọn
        private async Task ReloadAllAsync()
        {
            SetBusy(true);
            try
            {
                var all = await FetchProductsAsync("Tải danh sách sản phẩm", "products");
                if (all == null) return;
                _inventory = all;
                await SearchAsync();
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Tìm theo mã vạch hoặc tên bằng Query String: GET api/products?keyword=abc (server lọc)
        private async Task SearchAsync()
        {
            string keyword = txtKeyword.Text.Trim();
            if (keyword.Length == 0)
            {
                _current = _inventory; // ô tìm kiếm trống thì dùng luôn dữ liệu đã tải, đỡ gọi API
                ShowCurrent();
                return;
            }

            SetBusy(true);
            try
            {
                // Uri.EscapeDataString: từ khóa có dấu/khoảng trắng/ký tự đặc biệt không bị lỗi URL
                var result = await FetchProductsAsync("Tìm kiếm", $"products?keyword={Uri.EscapeDataString(keyword)}");
                if (result == null) return;
                _current = result;
                ShowCurrent();
            }
            finally
            {
                SetBusy(false);
            }
        }

        #endregion

        #region Sự kiện

        private async void btnSearch_Click(object? sender, EventArgs e) => await SearchAsync();

        // Nút TẢI LẠI: xóa từ khóa rồi tải lại toàn bộ
        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtKeyword.Clear();
            await ReloadAllAsync();
        }

        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductDto p) return;

            errorProvider1.Clear();
            _selectedProductId = p.ProductId;
            txtId.Text = p.ProductId.ToString();
            txtBarcode.Text = p.Barcode;
            txtProductName.Text = p.ProductName;
            txtPrice.Text = p.Price.ToString("N0", ViCulture);
            numStock.Value = Math.Clamp(p.StockQuantity, (int)numStock.Minimum, (int)numStock.Maximum);
            cbCategory.SelectedValue = p.CategoryId;
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs(0, out decimal price)) return;

            var newProduct = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                CategoryId = (int)cbCategory.SelectedValue!,
                Price = price,
                StockQuantity = (int)numStock.Value
            };

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PostAsJsonAsync("products", newProduct);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Thêm sản phẩm", response);
                    return;
                }
                string addedName = newProduct.ProductName;
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Thêm sản phẩm thành công!", StatusKind.Success);
                MessageBox.Show($"Đã thêm sản phẩm \"{addedName}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Thêm sản phẩm", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật trong danh sách.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(_selectedProductId, out decimal price)) return;

            var updateProduct = new
            {
                ProductId = _selectedProductId,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                CategoryId = (int)cbCategory.SelectedValue!,
                Price = price,
                StockQuantity = (int)numStock.Value
            };

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PutAsJsonAsync($"products/{_selectedProductId}", updateProduct);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Cập nhật sản phẩm", response);
                    return;
                }
                string updatedName = updateProduct.ProductName;
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Cập nhật sản phẩm thành công!", StatusKind.Success);
                MessageBox.Show($"Đã cập nhật sản phẩm \"{updatedName}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Cập nhật sản phẩm", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong danh sách.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtProductName.Text.Trim();
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm \"{name}\" không?\nThao tác này không thể hoàn tác.",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.DeleteAsync($"products/{_selectedProductId}");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Xóa sản phẩm", response);
                    return;
                }
                ClearInput();
                await ReloadAllAsync();
                SetStatus($"Đã xóa sản phẩm \"{name}\".", StatusKind.Success);
                MessageBox.Show($"Đã xóa sản phẩm \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Xóa sản phẩm", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInput();
            SetStatus("Sẵn sàng nhập sản phẩm mới.", StatusKind.Info);
        }

        #endregion
    }
}