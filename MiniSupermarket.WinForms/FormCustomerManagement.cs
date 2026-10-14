using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        private enum StatusKind { Info, Success, Warning, Error }

        private static readonly Color ColorMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color ColorOk = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorWarn = Color.FromArgb(217, 119, 6);
        private static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);

        private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");

        // SĐT Việt Nam: bắt đầu bằng 0, tổng 10-11 chữ số
        private static readonly Regex PhoneRegex = new Regex(@"^0\d{9,10}$", RegexOptions.Compiled);

        private List<CustomerDto> _all = new();      // toàn bộ khách hàng (dùng kiểm tra trùng SĐT)
        private List<CustomerDto> _current = new();  // danh sách đang hiển thị (toàn bộ hoặc kết quả tìm kiếm)
        private int _selectedId;
        private int _busyCount; // đếm số request đang chạy để khóa/mở nút đúng lúc

        // Admin và Cashier được thêm/sửa khách hàng (thu ngân đăng ký thẻ tại quầy); chỉ Admin được xóa
        private static bool CanEdit =>
            string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase);
        private static bool CanDelete => SessionManager.IsAdmin;

        public FormCustomerManagement()
        {
            InitializeComponent();
            SetupGrid();

            // Gõ lại vào ô nào thì xóa dấu ! đỏ của ô đó
            txtCustomerName.TextChanged += (s, e) => errorProvider1.SetError(txtCustomerName, string.Empty);
            txtPhoneNumber.TextChanged += (s, e) => errorProvider1.SetError(txtPhoneNumber, string.Empty);
            cbRank.SelectedIndexChanged += (s, e) => errorProvider1.SetError(cbRank, string.Empty);

            // Nhấn Enter trong ô tìm kiếm = bấm nút Tìm kiếm
            txtKeyword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnSearch.PerformClick();
                }
            };
        }

        private async void FormCustomerManagement_Load(object? sender, EventArgs e)
        {
            ApplyRolePermissions();
            cbRank.SelectedIndex = 0; // mặc định hạng "Chuẩn"
            await ReloadAllAsync();
        }

        #region Giao diện lưới

        private void SetupGrid()
        {
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCustomers.GridColor = Color.FromArgb(229, 231, 235);
            dgvCustomers.AllowUserToResizeRows = false;
            dgvCustomers.RowTemplate.Height = 34;
            dgvCustomers.ScrollBars = ScrollBars.Vertical;

            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCustomers.ColumnHeadersHeight = 38;
            dgvCustomers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
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
            dgvCustomers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(6, 0, 6, 0)
            };
            dgvCustomers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            // Mọi cột chia phần còn lại theo tỉ lệ nên luôn vừa khung, không có thanh cuộn ngang
            SetFill(colId, 8F, 55);
            SetFill(colName, 24F, 130);
            SetFill(colPhone, 16F, 105);
            SetFill(colAddress, 32F, 140);
            SetFill(colPoints, 11F, 80);
            SetFill(colRank, 10F, 75);

            colPoints.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "N0",
                FormatProvider = ViCulture,
                Alignment = DataGridViewContentAlignment.MiddleRight
            };
        }

        private static void SetFill(DataGridViewColumn col, float weight, int minWidth)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = weight;
            col.MinimumWidth = minWidth;
        }

        // Font đậm dùng chung cho cột Hạng thẻ (tạo 1 lần, tránh tạo Font mới mỗi lần vẽ ô)
        private static readonly Font RankFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        // Tô màu chữ cột "Hạng thẻ" để nhìn nhanh khách VIP
        private void dgvCustomers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvCustomers.Columns[e.ColumnIndex] != colRank) return;

            var style = e.CellStyle;
            if (style == null) return; // WinForms khai báo CellStyle có thể null nên kiểm tra để hết cảnh báo CS8602

            string rank = (e.Value?.ToString() ?? string.Empty).Trim();
            style.Font = RankFont;
            if (rank.Equals("Vàng", StringComparison.OrdinalIgnoreCase))
                style.ForeColor = Color.FromArgb(180, 83, 9);
            else if (rank.Equals("Bạc", StringComparison.OrdinalIgnoreCase))
                style.ForeColor = Color.FromArgb(75, 85, 99);
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
                    return $"Tài khoản hiện tại (vai trò: {SessionManager.CurrentRole}) không có quyền \"{action}\". Admin và Cashier được thêm/sửa khách hàng, chỉ Admin được xóa.";
                case HttpStatusCode.NotFound:
                    return "Không tìm thấy khách hàng này (có thể đã bị xóa) hoặc sai địa chỉ API. Hãy bấm Tải lại để cập nhật danh sách.";
                case HttpStatusCode.Conflict:
                    return "Thao tác bị từ chối vì xung đột dữ liệu (trùng số điện thoại).";
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
            if (n.Contains("customername")) return "Tên khách hàng";
            if (n.Contains("phone")) return "Số điện thoại";
            if (n.Contains("address")) return "Địa chỉ";
            if (n.Contains("reward")) return "Điểm thưởng";
            if (n.Contains("rank")) return "Hạng thẻ";
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
            btnAdd.Enabled = btnUpdate.Enabled = idle && CanEdit;
            btnDelete.Enabled = idle && CanDelete;
            UseWaitCursor = !idle;
        }

        // Khóa ô nhập/nút theo vai trò, đỡ phải chờ server trả 403
        private void ApplyRolePermissions()
        {
            bool can = CanEdit;
            btnAdd.Enabled = btnUpdate.Enabled = can;
            btnDelete.Enabled = CanDelete;
            txtCustomerName.ReadOnly = txtPhoneNumber.ReadOnly = txtAddress.ReadOnly = !can;
            numPoints.Enabled = cbRank.Enabled = can;
            if (!can) groupBox3.Text = "Thông tin khách hàng (chỉ xem)";
        }

        private void ShowCurrent()
        {
            if (IsDisposed) return;

            dgvCustomers.DataSource = _current.ToList();
            dgvCustomers.ClearSelection(); // không chọn sẵn dòng đầu để tránh lệch với các ô nhập

            string keyword = txtKeyword.Text.Trim();
            if (_current.Count == 0)
            {
                SetStatus(keyword.Length == 0 ? "Chưa có khách hàng nào." : $"Không tìm thấy khách hàng nào với từ khóa \"{keyword}\".", StatusKind.Warning);
                return;
            }
            long totalPoints = _current.Sum(c => (long)c.RewardPoints);
            string head = keyword.Length == 0
                ? $"Hiển thị {_current.Count} khách hàng."
                : $"Tìm thấy {_current.Count} khách hàng với từ khóa \"{keyword}\".";
            SetStatus($"{head} Tổng điểm thưởng: {totalPoints.ToString("N0", ViCulture)}.", StatusKind.Info);
        }

        private void ClearInput()
        {
            _selectedId = 0;
            errorProvider1.Clear();
            txtId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            numPoints.Value = 0;
            cbRank.SelectedIndex = 0;
            dgvCustomers.ClearSelection();
            txtCustomerName.Focus();
        }

        // Bỏ khoảng trắng, dấu chấm, gạch ngang người dùng hay gõ: "0901 000.001" -> "0901000001"
        private static string NormalizePhone(string text) => Regex.Replace(text, @"[\s.\-]", string.Empty);

        // Kiểm tra dữ liệu nhập ngay tại Client, báo lỗi cạnh đúng ô nhập
        // ignoreId: ID dòng đang sửa (bỏ qua khi so trùng SĐT); thêm mới thì để 0
        private bool ValidateInputs(int ignoreId, out string phone)
        {
            errorProvider1.Clear();
            string name = txtCustomerName.Text.Trim();

            // Dùng biến cục bộ "normalized" (không dùng trực tiếp tham số out trong lambda bên dưới, C# không cho phép)
            string normalized = NormalizePhone(txtPhoneNumber.Text);
            phone = normalized;

            if (name.Length == 0)
                return Invalid(txtCustomerName, "Tên khách hàng không được để trống.");
            if (name.Length > 100)
                return Invalid(txtCustomerName, $"Tên khách hàng tối đa 100 ký tự (đang nhập {name.Length}).");
            if (normalized.Length == 0)
                return Invalid(txtPhoneNumber, "Số điện thoại không được để trống.");
            if (!PhoneRegex.IsMatch(normalized))
                return Invalid(txtPhoneNumber, "Số điện thoại phải gồm 10-11 chữ số và bắt đầu bằng số 0, ví dụ 0901234567.");
            if (_all.Any(c => c.CustomerId != ignoreId && (c.PhoneNumber ?? string.Empty).Trim() == normalized))
                return Invalid(txtPhoneNumber, $"Số điện thoại {normalized} đã thuộc về khách hàng khác.");
            if (txtAddress.Text.Trim().Length > 200)
                return Invalid(txtAddress, "Địa chỉ tối đa 200 ký tự.");
            if (cbRank.SelectedItem == null)
                return Invalid(cbRank, "Vui lòng chọn hạng thẻ.");
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

        private object BuildPayload(int id, string phone) => new
        {
            CustomerId = id,
            CustomerName = txtCustomerName.Text.Trim(),
            PhoneNumber = phone,
            Address = txtAddress.Text.Trim(),
            RewardPoints = (int)numPoints.Value,
            MembershipRank = cbRank.SelectedItem?.ToString() ?? "Chuẩn"
        };

        #endregion

        #region Tải dữ liệu từ API

        // GET api/customers hoặc api/customers/search?keyword=...; lỗi thì tự báo và trả về null
        private async Task<List<CustomerDto>?> FetchAsync(string action, string url)
        {
            try
            {
                using var response = await ApiClientService.Client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync(action, response);
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<List<CustomerDto>>() ?? new List<CustomerDto>();
            }
            catch (Exception ex)
            {
                ShowException(action, ex);
                return null;
            }
        }

        // Tải lại TOÀN BỘ khách hàng rồi áp lại từ khóa đang nhập (nếu có)
        private async Task ReloadAllAsync()
        {
            SetBusy(true);
            try
            {
                var all = await FetchAsync("Tải danh sách khách hàng", "customers");
                if (all == null) return;
                _all = all;
                await SearchAsync();
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Tìm theo tên hoặc SĐT bằng Query String: GET api/customers/search?keyword=abc (server lọc)
        private async Task SearchAsync()
        {
            string keyword = txtKeyword.Text.Trim();
            if (keyword.Length == 0)
            {
                _current = _all; // ô tìm kiếm trống thì dùng luôn dữ liệu đã tải
                ShowCurrent();
                return;
            }

            SetBusy(true);
            try
            {
                var result = await FetchAsync("Tìm kiếm", $"customers/search?keyword={Uri.EscapeDataString(keyword)}");
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

        private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCustomers.Rows[e.RowIndex].DataBoundItem is not CustomerDto c) return;

            errorProvider1.Clear();
            _selectedId = c.CustomerId;
            txtId.Text = c.CustomerId.ToString();
            txtCustomerName.Text = c.CustomerName;
            txtPhoneNumber.Text = c.PhoneNumber;
            txtAddress.Text = c.Address ?? string.Empty;
            numPoints.Value = Math.Clamp(c.RewardPoints, (int)numPoints.Minimum, (int)numPoints.Maximum);

            // Hạng lạ (không nằm trong danh sách mặc định) thì thêm vào combo để không bị mất khi sửa
            string rank = c.MembershipRank?.Trim() ?? string.Empty;
            if (rank.Length > 0 && !cbRank.Items.Contains(rank)) cbRank.Items.Add(rank);
            cbRank.SelectedItem = rank.Length > 0 ? rank : "Chuẩn";
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs(0, out string phone)) return;

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PostAsJsonAsync("customers", BuildPayload(0, phone));
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Thêm khách hàng", response);
                    return;
                }
                string addedName = txtCustomerName.Text.Trim();
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Thêm khách hàng thành công!", StatusKind.Success);
                MessageBox.Show($"Đã thêm khách hàng \"{addedName}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Thêm khách hàng", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần cập nhật trong danh sách.", "Chưa chọn khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(_selectedId, out string phone)) return;

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PutAsJsonAsync($"customers/{_selectedId}", BuildPayload(_selectedId, phone));
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Cập nhật khách hàng", response);
                    return;
                }
                string updatedName = txtCustomerName.Text.Trim();
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Cập nhật khách hàng thành công!", StatusKind.Success);
                MessageBox.Show($"Đã cập nhật khách hàng \"{updatedName}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Cập nhật khách hàng", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa trong danh sách.", "Chưa chọn khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtCustomerName.Text.Trim();
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng \"{name}\" không?\nĐiểm thưởng của khách sẽ mất theo. Thao tác này không thể hoàn tác.",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.DeleteAsync($"customers/{_selectedId}");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Xóa khách hàng", response);
                    return;
                }
                ClearInput();
                await ReloadAllAsync();
                SetStatus($"Đã xóa khách hàng \"{name}\".", StatusKind.Success);
                MessageBox.Show($"Đã xóa khách hàng \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Xóa khách hàng", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInput();
            SetStatus("Sẵn sàng nhập khách hàng mới.", StatusKind.Info);
        }

        #endregion

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // DTO hứng dữ liệu JSON từ Server
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }
}