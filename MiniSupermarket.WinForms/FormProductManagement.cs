using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using ClosedXML.Excel;

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
        private ComboBox cbFilterCategory = null!;   // lọc theo danh mục (tạo bằng code, nằm cạnh ô tìm kiếm)
        private Button btnExportExcel = null!;       // Tiêu chí 48: Nút Xuất Excel báo cáo danh mục & tồn kho
        private bool _suppressFilter;                  // true khi đang nạp lại combo, tránh tự kích hoạt tìm kiếm
        private int _busyCount; // đếm số request đang chạy để khóa/mở nút đúng lúc

        // Điều khiển phân trang (Buổi 6)
        private Button btnPrev = null!;
        private Button btnNext = null!;
        private Label lblPaging = null!;
        private Panel pnlPaging = null!;
        private int _currentPage = 1;
        private readonly int _pageSize = 5;
        private int _totalPages = 1;
        private int _totalItems = 0;

        // Server chỉ cho Admin và Warehouse thêm/sửa/xóa (xem ProductsController); vai trò khác chỉ được xem
        private static bool CanEdit =>
            string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase);

        public FormProductManagement()
        {
            InitializeComponent();
            EnsureVietnameseTexts();
            SetupGrid();
            BuildPagingControls();

            // Gõ lại vào ô nào thì xóa dấu ! đỏ của ô đó
            txtBarcode.TextChanged += (s, e) => errorProvider1.SetError(txtBarcode, string.Empty);
            txtProductName.TextChanged += (s, e) => errorProvider1.SetError(txtProductName, string.Empty);
            txtPrice.TextChanged += (s, e) => errorProvider1.SetError(txtPrice, string.Empty);
            cbCategory.SelectedIndexChanged += (s, e) => errorProvider1.SetError(cbCategory, string.Empty);

            BuildCategoryFilter();

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

        // Đảm bảo toàn bộ chữ tiếng Việt luôn hiển thị chuẩn đẹp, có dấu, không bao giờ bị lỗi dấu ?
        private void EnsureVietnameseTexts()
        {
            Text = "Quản lý sản phẩm";
            groupBox1.Text = "Tìm kiếm";
            txtKeyword.PlaceholderText = "Nhập mã vạch hoặc tên sản phẩm cần tìm, nhấn Enter";
            btnSearch.Text = "Tìm kiếm";
            btnLoad.Text = "Tải lại";
            groupBox2.Text = "Danh sách sản phẩm";
            lblStatus.Text = "Sẵn sàng.";
            groupBox3.Text = "Thông tin sản phẩm";
            lblId.Text = "Mã SP (tự sinh)";
            lblBarcode.Text = "Mã vạch (*)";
            txtBarcode.PlaceholderText = "Tối đa 50 ký tự";
            lblName.Text = "Tên sản phẩm (*)";
            txtProductName.PlaceholderText = "Tối đa 150 ký tự";
            lblCategory.Text = "Danh mục (*)";
            lblPrice.Text = "Đơn giá (đồng) (*)";
            txtPrice.PlaceholderText = "Ví dụ: 32000";
            lblStock.Text = "Số lượng tồn";
            btnAdd.Text = "Thêm mới";
            btnUpdate.Text = "Cập nhật";
            btnDelete.Text = "Xóa";
            btnClear.Text = "Nhập mới";
        }

        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            ApplyRolePermissions();
            await LoadCategoriesAsync();
            await LoadProductsPagedAsync();
        }

        #region Giao diện lưới

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
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
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.False,
                Padding = new Padding(4, 0, 4, 0)
            };
            dgvProducts.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 9.5F),
                Padding = new Padding(4, 0, 4, 0)
            };
            dgvProducts.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            // Chỉ dùng thanh cuộn dọc khi danh sách dài; không dùng thanh cuộn ngang để giao diện luôn gọn gàng
            dgvProducts.ScrollBars = ScrollBars.Vertical;
            dgvProducts.ShowCellToolTips = true;

            // Đặt tiêu đề rõ ràng, chuẩn tiếng Việt
            colId.HeaderText = "Mã SP";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 72;
            colId.MinimumWidth = 68;
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colId.HeaderCell.Style.Padding = new Padding(2, 0, 2, 0);

            colBarcode.HeaderText = "Mã vạch";
            colBarcode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colBarcode.Width = 115;
            colBarcode.MinimumWidth = 100;
            colBarcode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBarcode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cột Tên sản phẩm tự động co giãn chiếm trọn toàn bộ khoảng trống còn lại của bảng
            colProductName.HeaderText = "Tên sản phẩm";
            colProductName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProductName.MinimumWidth = 140;

            colCategory.HeaderText = "Danh mục";
            colCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCategory.Width = 135;
            colCategory.MinimumWidth = 115;

            colPrice.HeaderText = "Đơn giá (đ)";
            colPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPrice.Width = 95;
            colPrice.MinimumWidth = 85;
            colPrice.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "N0",
                FormatProvider = ViCulture,
                Alignment = DataGridViewContentAlignment.MiddleRight
            };
            colPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            colStock.HeaderText = "Tồn kho";
            colStock.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStock.Width = 75;
            colStock.MinimumWidth = 65;
            colStock.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "N0"
            };
            colStock.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvProducts.CellFormatting += dgvProducts_CellFormatting; //43.Đăng ký sự kiện
        }

        private static void SetFill(DataGridViewColumn col, float weight, int minWidth)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = weight;
            col.MinimumWidth = minWidth;
        }

        //43.Bắt sự kiện CellFormatting của DataGridView: nếu cột StockQuantity <= 10 thì đổi e.CellStyle.BackColor = Color.LightCoral
        private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvProducts.Columns[e.ColumnIndex] == colStock)
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int stock) && stock <= 10)
                {
                    e.CellStyle!.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
                }
            }
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
            cbFilterCategory.Enabled = idle;
            if (btnExportExcel != null) btnExportExcel.Enabled = idle && (CanEdit || SessionManager.IsAdmin);
            if (btnPrev != null) btnPrev.Enabled = idle && _currentPage > 1;
            if (btnNext != null) btnNext.Enabled = idle && _currentPage < _totalPages;
            UseWaitCursor = !idle;
        }

        // Vai trò chỉ được xem (vd Cashier): khóa ô nhập + nút ghi, đỡ phải chờ server trả 403
        private void ApplyRolePermissions()
        {
            bool can = CanEdit;
            btnAdd.Enabled = btnUpdate.Enabled = btnDelete.Enabled = can;
            txtBarcode.ReadOnly = txtProductName.ReadOnly = txtPrice.ReadOnly = !can;
            cbCategory.Enabled = numStock.Enabled = can;
            if (btnExportExcel != null) btnExportExcel.Enabled = can || SessionManager.IsAdmin;
            if (!can) groupBox3.Text = "Thông tin sản phẩm (chỉ xem)";
        }

        // Đưa danh sách đang chọn lên lưới và cập nhật dòng trạng thái
        private void ShowCurrent()
        {
            if (IsDisposed) return;

            dgvProducts.DataSource = _current.ToList();
            dgvProducts.ClearSelection(); // không chọn sẵn dòng đầu để tránh lệch với các ô nhập

            string keyword = txtKeyword.Text.Trim();
            string? catName = SelectedFilterCategoryId() > 0 ? (cbFilterCategory.Text) : null;

            var criteria = new List<string>();
            if (keyword.Length > 0) criteria.Add($"từ khóa \"{keyword}\"");
            if (catName != null) criteria.Add($"danh mục \"{catName}\"");

            if (criteria.Count == 0)
                SetStatus(_current.Count == 0 ? "Chưa có sản phẩm nào." : $"Hiển thị {_current.Count} sản phẩm.", _current.Count == 0 ? StatusKind.Warning : StatusKind.Info);
            else if (_current.Count == 0)
                SetStatus($"Không tìm thấy sản phẩm nào với {string.Join(" và ", criteria)}.", StatusKind.Warning);
            else
                SetStatus($"Tìm thấy {_current.Count} sản phẩm với {string.Join(" và ", criteria)}.", StatusKind.Info);
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

                // Combo lọc: thêm mục "Tất cả danh mục" (Id = 0) lên đầu, giữ lại lựa chọn cũ nếu còn
                int previous = SelectedFilterCategoryId();
                var filterList = new List<CategoryDto> { new CategoryDto { CategoryId = 0, CategoryName = "Tất cả danh mục" } };
                filterList.AddRange(cats);
                _suppressFilter = true;
                try
                {
                    cbFilterCategory.DataSource = filterList;
                    cbFilterCategory.DisplayMember = "CategoryName";
                    cbFilterCategory.ValueMember = "CategoryId";
                    cbFilterCategory.SelectedValue = filterList.Any(c => c.CategoryId == previous) ? previous : 0;
                }
                finally { _suppressFilter = false; }
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

        // Id danh mục đang chọn ở combo lọc (0 = tất cả)
        private int SelectedFilterCategoryId() =>
            cbFilterCategory?.SelectedValue is int id ? id : 0;

        // Tìm theo từ khóa (mã vạch/tên) và/hoặc danh mục qua API phân trang (Buổi 6)
        private async Task SearchAsync()
        {
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        // Tạo combo "Danh mục" và chèn vào thanh tìm kiếm (giữa ô từ khóa và nút Tìm kiếm)
        private void BuildCategoryFilter()
        {
            cbFilterCategory = new ComboBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10.5F),     // giống cbFilterRole ở form Tài khoản
                Margin = new Padding(9, 4, 0, 4),       // giống cbFilterRole: cách ô từ khóa 9px
                DropDownWidth = 320,                    // danh sách xổ rộng hơn để đọc hết tên danh mục dài
                Name = "cbFilterCategory"
            };
            cbFilterCategory.DataSource = new List<CategoryDto> { new CategoryDto { CategoryId = 0, CategoryName = "Tất cả danh mục" } };
            cbFilterCategory.DisplayMember = "CategoryName";
            cbFilterCategory.ValueMember = "CategoryId";
            cbFilterCategory.SelectedIndexChanged += async (s, e) =>
            {
                if (_suppressFilter) return;
                await SearchAsync();
            };

            // Tiêu chí 48: Nút "Xuất Excel" cho Quản lý
            btnExportExcel = new Button
            {
                Text = "Xuất Excel",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(22, 163, 74), // Màu xanh lá Excel
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Margin = new Padding(9, 3, 0, 3),
                Cursor = Cursors.Hand,
                Name = "btnExportExcel"
            };
            btnExportExcel.FlatAppearance.BorderSize = 0;
            btnExportExcel.Click += async (s, e) => await ExportExcelAsync();

            // Thêm cột cho combo (180px) và nút Xuất Excel (120px); đẩy nút Tìm kiếm / Tải lại sang phải
            tlpSearch.ColumnStyles.Clear();
            tlpSearch.ColumnCount = 5;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpSearch.SetColumn(btnSearch, 2);
            tlpSearch.SetColumn(btnLoad, 3);
            tlpSearch.Controls.Add(cbFilterCategory, 1, 0);
            tlpSearch.Controls.Add(btnExportExcel, 4, 0);
        }

        // =========================================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO TOÀN BỘ MẶT HÀNG KÈM SỐ LƯỢNG TỒN KHO RA EXCEL/CSV
        // Kỹ thuật: ClosedXML / API / xuất chuỗi CSV
        // Nghiệp vụ: Quản lý bấm nút "Xuất Excel" tải toàn bộ danh mục & tồn kho
        // =========================================================================
        private async Task ExportExcelAsync()
        {
            if (!CanEdit && !SessionManager.IsAdmin)
            {
                MessageBox.Show("Chức năng chỉ dành cho cấp Quản lý (Admin / Quản lý kho)!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Title = "Xuất báo cáo danh mục hàng hóa & số lượng tồn kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx|CSV File (*.csv)|*.csv",
                FileName = $"DanhSach_SanPham_TonKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            string filePath = dlg.FileName;
            bool isCsv = filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);

            SetBusy(true);
            try
            {
                int catId = SelectedFilterCategoryId();
                string endpoint = isCsv ? "products/export-csv" : "products/export-excel";
                if (catId > 0) endpoint += $"?categoryId={catId}";

                bool apiSuccess = false;
                try
                {
                    using var response = await ApiClientService.Client.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        await File.WriteAllBytesAsync(filePath, bytes);
                        apiSuccess = true;
                    }
                }
                catch
                {
                    apiSuccess = false;
                }

                // Nếu gọi API không thành công hoặc offline, xuất trực tiếp phía Client bằng ClosedXML hoặc CSV
                if (!apiSuccess)
                {
                    var exportData = _inventory.Count > 0 ? _inventory : _current;
                    if (exportData.Count == 0)
                    {
                        var all = await FetchProductsAsync("Tải dữ liệu để xuất", "products");
                        if (all != null && all.Count > 0)
                        {
                            exportData = all;
                            _inventory = all;
                        }
                    }

                    if (catId > 0)
                    {
                        exportData = exportData.Where(p => p.CategoryId == catId).ToList();
                    }

                    if (isCsv)
                    {
                        ExportProductsToCsvDirectly(exportData, filePath);
                    }
                    else
                    {
                        ExportProductsWithClosedXmlDirectly(exportData, filePath);
                    }
                }

                var choice = MessageBox.Show(
                    $"Xuất báo cáo danh mục hàng hóa & tồn kho thành công!\n\nĐường dẫn: {filePath}\n\nBạn có muốn mở tệp ngay không?",
                    "Xuất Excel thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (choice == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất tệp: " + ex.Message, "Lỗi xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ExportProductsWithClosedXmlDirectly(List<ProductDto> list, string filePath)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Danh mục tồn kho");

            ws.Cell(1, 1).Value = "BÁO CÁO DANH MỤC HÀNG HÓA VÀ SỐ LƯỢNG TỒN KHO";
            ws.Range("A1:G1").Merge();
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 16;
            ws.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1E3A8A");
            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(1).Height = 36;

            int totalStock = list.Sum(p => p.StockQuantity);
            ws.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Tổng mặt hàng: {list.Count:N0} | Tổng tồn kho: {totalStock:N0} sản phẩm";
            ws.Range("A2:G2").Merge();
            ws.Cell(2, 1).Style.Font.Italic = true;
            ws.Cell(2, 1).Style.Font.FontSize = 10;
            ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(2).Height = 22;

            string[] headers = { "STT", "Mã vạch (Barcode)", "Tên sản phẩm", "Nhóm hàng", "Đơn giá (VNĐ)", "Tồn kho", "Trạng thái tồn kho" };
            int headerRow = 4;
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontSize = 11;
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2563EB");
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            ws.Row(headerRow).Height = 28;

            int row = 5;
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                ws.Cell(row, 1).Value = i + 1;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 2).SetValue(p.Barcode);
                ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 3).Value = p.ProductName;
                ws.Cell(row, 4).Value = p.CategoryName;

                ws.Cell(row, 5).Value = p.Price;
                ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                ws.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Cell(row, 6).Value = p.StockQuantity;
                ws.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
                ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                string status = p.StockQuantity <= 0 ? "Hết hàng" : (p.StockQuantity <= 20 ? "Sắp hết hàng" : "Bình thường");
                var statusCell = ws.Cell(row, 7);
                statusCell.Value = status;
                statusCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (p.StockQuantity <= 0)
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#FEE2E2");
                    statusCell.Style.Font.FontColor = XLColor.FromHtml("#991B1B");
                    statusCell.Style.Font.Bold = true;
                }
                else if (p.StockQuantity <= 20)
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#FEF3C7");
                    statusCell.Style.Font.FontColor = XLColor.FromHtml("#92400E");
                    statusCell.Style.Font.Bold = true;
                }

                for (int c = 1; c <= 7; c++)
                {
                    ws.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(row, c).Style.Border.OutsideBorderColor = XLColor.FromHtml("#D1D5DB");
                }
                row++;
            }

            ws.Cell(row, 1).Value = "TỔNG CỘNG";
            ws.Range(row, 1, row, 4).Merge();
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            ws.Cell(row, 5).Value = "";
            ws.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            if (list.Count > 0)
            {
                ws.Cell(row, 6).FormulaA1 = $"=SUM(F5:F{row - 1})";
            }
            else
            {
                ws.Cell(row, 6).Value = 0;
            }
            ws.Cell(row, 6).Style.Font.Bold = true;
            ws.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
            ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            ws.Cell(row, 7).Value = $"{list.Count:N0} mặt hàng";
            ws.Cell(row, 7).Style.Font.Bold = true;
            ws.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            for (int c = 1; c <= 7; c++)
            {
                ws.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }

        private void ExportProductsToCsvDirectly(List<ProductDto> list, string filePath)
        {
            static string Esc(string? v) => "\"" + (v ?? "").Replace("\"", "\"\"") + "\"";
            var sb = new StringBuilder();
            sb.AppendLine("STT,Mã vạch,Tên sản phẩm,Nhóm hàng,Đơn giá,Số lượng tồn kho,Trạng thái");
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                string status = p.StockQuantity <= 0 ? "Hết hàng" : (p.StockQuantity <= 20 ? "Sắp hết hàng" : "Bình thường");
                sb.AppendLine($"{i + 1},{Esc(p.Barcode)},{Esc(p.ProductName)},{Esc(p.CategoryName)},{p.Price},{p.StockQuantity},{Esc(status)}");
            }
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        #endregion

        #region Sự kiện

        private async void btnSearch_Click(object? sender, EventArgs e) => await SearchAsync();

        // Nút TẢI LẠI: xóa từ khóa rồi tải lại trang 1
        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtKeyword.Clear();
            _suppressFilter = true;
            try { cbFilterCategory.SelectedValue = 0; }
            finally { _suppressFilter = false; }
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e) //41. Khi bấm vào 1 dòng trong lưới, đưa dữ liệu lên khung nhập liệu
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
                await LoadProductsPagedAsync();
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
                await LoadProductsPagedAsync();
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
                if (_current.Count == 1 && _currentPage > 1) _currentPage--;
                await LoadProductsPagedAsync();
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

        #region Điều khiển phân trang (Buổi 6)

        private void BuildPagingControls()
        {
            pnlPaging = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                BackColor = Color.White,
                Padding = new Padding(4, 4, 4, 4)
            };

            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.White
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            btnPrev = new Button
            {
                Text = "<< Trang trước",
                Anchor = AnchorStyles.Left,
                Size = new Size(130, 32),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(243, 244, 246),
                ForeColor = Color.FromArgb(31, 41, 55),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnPrev.Click += btnPrev_Click;

            lblPaging = new Label
            {
                Text = "Trang 1 / 1 (Tổng: 0 SP)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55)
            };

            btnNext = new Button
            {
                Text = "Trang sau >>",
                Anchor = AnchorStyles.Right,
                Size = new Size(130, 32),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(243, 244, 246),
                ForeColor = Color.FromArgb(31, 41, 55),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnNext.Click += btnNext_Click;

            tlp.Controls.Add(btnPrev, 0, 0);
            tlp.Controls.Add(lblPaging, 1, 0);
            tlp.Controls.Add(btnNext, 2, 0);

            pnlPaging.Controls.Add(tlp);

            groupBox2.Controls.Add(pnlPaging);
            lblStatus.SendToBack();
            pnlPaging.BringToFront();
            dgvProducts.BringToFront();
        }

        private async void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadProductsPagedAsync();
            }
        }

        private async Task LoadProductsPagedAsync() //41.nạp lưới  
        {
            string keyword = txtKeyword.Text.Trim();
            int catId = SelectedFilterCategoryId();

            SetBusy(true);
            try
            {
                var url = $"products/paged?pageNumber={_currentPage}&pageSize={_pageSize}";
                if (!string.IsNullOrWhiteSpace(keyword))
                    url += $"&keyword={Uri.EscapeDataString(keyword)}";
                if (catId > 0)
                    url += $"&categoryId={catId}";

                using var response = await ApiClientService.Client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Tải trang sản phẩm", response);
                    return;
                }

                var pagedData = await response.Content.ReadFromJsonAsync<PagedViewResult<ProductDto>>();
                if (pagedData == null) return;

                _currentPage = pagedData.CurrentPage;
                _totalPages = Math.Max(1, pagedData.TotalPages);
                _totalItems = pagedData.TotalItems;
                _current = pagedData.Items ?? new List<ProductDto>();

                dgvProducts.DataSource = _current.ToList(); //41 Lưới DataGridView
                dgvProducts.ClearSelection();

                lblPaging.Text = $"Trang {_currentPage} / {_totalPages} (Tổng: {_totalItems} SP)";
                int lowStockCount = _current.Count(p => p.StockQuantity <= 10); //43. Cảnh báo trên thanh trạng thái
                if (lowStockCount > 0)
                {
                    SetStatus($"Trang {_currentPage}/{_totalPages} (Tổng: {_totalItems} SP) • CẢNH BÁO: Có {lowStockCount} sản phẩm sắp hết kho (tồn <= 10)!", StatusKind.Warning);
                }
                else
                {
                    SetStatus($"Đã tải trang {_currentPage}/{_totalPages} (Tổng: {_totalItems} sản phẩm)", StatusKind.Info);
                }

                btnPrev.Enabled = _currentPage > 1;
                btnNext.Enabled = _currentPage < _totalPages;
            }
            catch (Exception ex)
            {
                ShowException("Tải trang sản phẩm", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        #endregion

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducts_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class PagedViewResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new();
    }
}