using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        // Địa chỉ Web API (port phải khớp với project MiniSupermarket.API đang chạy)
        private const string ApiBaseUrl = "https://localhost:7132/api/";

        private enum StatusKind { Info, Success, Warning, Error }

        private static readonly Color ColorMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color ColorOk = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorWarn = Color.FromArgb(217, 119, 6);
        private static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);

        private int _busyCount; // đếm số request đang chạy để khóa/mở nút đúng lúc

        public FormCategoryManagement()
        {
            InitializeComponent();
            SetupGrid();

            // Gõ lại vào ô nào thì xóa dấu ! đỏ của ô đó
            txtCategoryName.TextChanged += (s, e) => errorProvider1.SetError(txtCategoryName, string.Empty);
            txtDescription.TextChanged += (s, e) => errorProvider1.SetError(txtDescription, string.Empty);

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

        #region Giao diện lưới

        private void SetupGrid()
        {
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCategories.GridColor = Color.FromArgb(229, 231, 235);
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.RowTemplate.Height = 34;

            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategories.ColumnHeadersHeight = 38;
            dgvCategories.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
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

            dgvCategories.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(6, 0, 6, 0)
            };
            dgvCategories.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            // Cột Mã ID cố định; Tên và Mô tả chia đều phần còn lại theo tỉ lệ 4 : 6 nên luôn vừa khung
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 80;
            colCategoryName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCategoryName.FillWeight = 40F;
            colCategoryName.MinimumWidth = 140;
            colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDescription.FillWeight = 60F;
            colDescription.MinimumWidth = 160;
        }

        #endregion

        #region Tạo thông báo lỗi chi tiết

        // Giải thích mã HTTP bằng tiếng Việt, gắn với thao tác đang làm
        private static string GetReason(string action, HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    return "Dữ liệu gửi lên chưa hợp lệ. Hãy kiểm tra lại thông tin đã nhập.";
                case HttpStatusCode.Unauthorized:
                    return "Phiên đăng nhập đã hết hạn hoặc chưa đăng nhập. Hãy đăng xuất rồi đăng nhập lại.";
                case HttpStatusCode.Forbidden:
                    return $"Tài khoản hiện tại (vai trò: {SessionManager.CurrentRole}) không có quyền \"{action}\". Chỉ Admin được thêm, sửa, xóa nhóm hàng.";
                case HttpStatusCode.NotFound:
                    return "Không tìm thấy nhóm hàng này (có thể đã bị xóa) hoặc sai địa chỉ API. Hãy bấm Tải lại để cập nhật danh sách.";
                case HttpStatusCode.Conflict:
                    return "Thao tác bị từ chối vì xung đột dữ liệu (trùng tên hoặc đang được sử dụng).";
                case HttpStatusCode.InternalServerError:
                    return "Server gặp lỗi khi xử lý yêu cầu. Xem cửa sổ log của project API để biết chi tiết.";
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                    return "Server tạm thời không phản hồi. Hãy thử lại sau ít phút.";
                default:
                    return "Lỗi không xác định.";
            }
        }

        // Tên trường kỹ thuật (JSON) -> tên hiển thị tiếng Việt
        private static string FieldLabel(string name)
        {
            string n = name.Replace("$.", string.Empty).ToLowerInvariant();
            if (n.Contains("categoryname")) return "Tên nhóm hàng";
            if (n.Contains("description")) return "Mô tả";
            if (n.Contains("categoryid")) return "Mã nhóm hàng";
            if (n.Contains("keyword")) return "Từ khóa";
            return name;
        }

        // Đọc phần thân response lỗi -> danh sách dòng chi tiết dễ hiểu
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
                    // Lỗi Data Annotations: { "errors": { "CategoryName": ["Tên nhóm hàng không được để trống!"] } }
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
                // Không phải JSON (ví dụ trang lỗi HTML của server): chỉ lấy đoạn đầu nếu là văn bản thường
                string text = body.Trim();
                if (!text.StartsWith("<")) details.Add(text.Length > 300 ? text.Substring(0, 300) + "..." : text);
            }
            return details;
        }

        // Đọc response lỗi từ API -> nội dung thông báo + biểu tượng phù hợp
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

            // Lỗi do người dùng nhập/thao tác (400, 409) dùng biểu tượng cảnh báo; lỗi hệ thống dùng biểu tượng lỗi
            var icon = response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Conflict
                ? MessageBoxIcon.Warning
                : MessageBoxIcon.Error;
            return (sb.ToString(), icon);
        }

        // Tạo nội dung cho lỗi ngoại lệ (server tắt, sai port, lỗi chứng chỉ HTTPS, timeout...)
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
                    sb.AppendLine($"Nguyên nhân: Không kết nối được tới Server ({ApiBaseUrl}).");
                    sb.AppendLine("Hãy kiểm tra:");
                    sb.AppendLine("  1. Project MiniSupermarket.API đã được chạy chưa?");
                    sb.AppendLine("  2. Port trong địa chỉ trên có khớp với port API đang chạy không?");
                    sb.AppendLine("  3. Tường lửa / phần mềm diệt virus có đang chặn kết nối không?");
                }
                sb.AppendLine();
                sb.Append($"Chi tiết kỹ thuật: {(hre.InnerException is SocketException se ? se.Message : hre.Message)}");
            }
            else if (ex is TaskCanceledException)
            {
                sb.AppendLine("Nguyên nhân: Hết thời gian chờ phản hồi từ Server (quá 15 giây).");
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

        // Hiện lỗi từ API: hộp thoại + dòng trạng thái màu đỏ/cam
        private async Task ShowApiErrorAsync(string action, HttpResponseMessage response)
        {
            var (message, icon) = await BuildApiErrorAsync(action, response);
            MessageBox.Show(message, $"Lỗi - {action}", MessageBoxButtons.OK, icon);
        }

        private void ShowException(string action, Exception ex)
        {
            MessageBox.Show(BuildExceptionMessage(action, ex), $"Lỗi - {action}", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            bool enabled = _busyCount <= 0;
            foreach (var b in new[] { btnAdd, btnUpdate, btnDelete, btnClear, btnSearch, btnLoad })
                b.Enabled = enabled;
            UseWaitCursor = !enabled;
        }

        private void BindGrid(List<CategoryDto> list)
        {
            if (IsDisposed) return;
            dgvCategories.DataSource = list;
            dgvCategories.ClearSelection(); // không chọn sẵn dòng đầu để tránh lệch với các ô nhập
        }

        // Kiểm tra dữ liệu nhập ngay tại Client, báo lỗi cạnh đúng ô nhập
        // ignoreId: ID dòng đang sửa (bỏ qua khi so trùng tên); thêm mới thì để 0
        private bool ValidateInputs(int ignoreId = 0)
        {
            errorProvider1.Clear();
            string name = txtCategoryName.Text.Trim();
            string desc = txtDescription.Text.Trim();

            if (name.Length == 0)
                return Invalid(txtCategoryName, "Tên nhóm hàng không được để trống.");
            if (name.Length > 100)
                return Invalid(txtCategoryName, $"Tên nhóm hàng tối đa 100 ký tự (đang nhập {name.Length}).");
            if (desc.Length > 255)
                return Invalid(txtDescription, $"Mô tả tối đa 255 ký tự (đang nhập {desc.Length}).");

            // Kiểm tra trùng tên với danh sách đang hiển thị (không phân biệt hoa/thường).
            // Server vẫn kiểm tra lại lần nữa, vì lưới có thể đang chỉ hiện kết quả tìm kiếm.
            if (dgvCategories.DataSource is List<CategoryDto> current &&
                current.Any(c => c.CategoryId != ignoreId &&
                                 string.Equals(c.CategoryName.Trim(), name, StringComparison.OrdinalIgnoreCase)))
                return Invalid(txtCategoryName, $"Nhóm hàng \"{name}\" đã tồn tại, hãy dùng tên khác.");
            return true;
        }

        private bool Invalid(Control field, string message)
        {
            errorProvider1.SetError(field, message);
            errorProvider1.SetIconPadding(field, -22); // đưa dấu ! vào trong ô nhập để không bị cắt mép
            MessageBox.Show(message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            field.Focus(); // đưa con trỏ về đúng ô cần sửa sau khi đóng hộp thoại
            return false;
        }

        // Lấy ID nhóm hàng đang chọn; nếu chưa chọn thì nhắc cụ thể theo thao tác
        private bool TryGetSelectedId(string verb, out int id)
        {
            if (int.TryParse(txtId.Text, out id)) return true;

            MessageBox.Show($"Vui lòng bấm chọn một nhóm hàng trong danh sách trước khi {verb}.",
                "Chưa chọn nhóm hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl),
                Timeout = TimeSpan.FromSeconds(15) // để lỗi treo server hiện ra thay vì chờ 100 giây mặc định
            };

            // Đính kèm Token vào Header theo chuẩn Bearer Authentication
            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }
            return client;
        }

        // Hàm phụ trợ: Xóa trắng các ô nhập liệu sau khi thao tác xong
        private void ClearInputs()
        {
            txtId.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            errorProvider1.Clear();
        }

        #endregion

        #region Xử lý sự kiện

        // Form vừa bật lên: tự động tải dữ liệu từ API lên bảng
        private async void FormCategoryManagement_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Hàm dùng chung: gọi API GET lấy danh sách và đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            SetBusy(true);
            try
            {
                using var client = GetAuthenticatedClient();
                using var response = await client.GetAsync("categories");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Tải dữ liệu", response);
                    return;
                }

                var list = await response.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? new List<CategoryDto>();
                BindGrid(list);
                SetStatus($"Có {list.Count} nhóm hàng.", StatusKind.Info);
            }
            catch (Exception ex)
            {
                ShowException("Tải dữ liệu", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Nút Tải lại: xóa từ khóa và tải toàn bộ danh sách
        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtKeyword.Clear();
            await LoadDataAsync();
        }

        // Click vào một dòng: đưa dữ liệu lên các ô nhập để chuẩn bị Sửa/Xóa
        private void dgvCategories_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCategories.Rows[e.RowIndex].DataBoundItem is CategoryDto cat)
            {
                errorProvider1.Clear();
                txtId.Text = cat.CategoryId.ToString();
                txtCategoryName.Text = cat.CategoryName;
                txtDescription.Text = cat.Description ?? string.Empty;
            }
        }

        // Nút Nhập mới: bỏ chọn dòng, xóa ô nhập, đưa con trỏ vào ô tên
        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            dgvCategories.ClearSelection();
            SetStatus("Sẵn sàng nhập nhóm hàng mới.", StatusKind.Info);
            txtCategoryName.Focus();
        }

        // Nút THÊM MỚI (CREATE): gửi POST lên Web API
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string name = txtCategoryName.Text.Trim();
            string desc = txtDescription.Text.Trim();
            var newCat = new { CategoryName = name, Description = desc.Length == 0 ? null : desc };

            SetBusy(true);
            try
            {
                using var client = GetAuthenticatedClient();
                using var response = await client.PostAsJsonAsync("categories", newCat);
                if (response.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                    ClearInputs();
                    SetStatus($"Đã thêm nhóm hàng \"{name}\".", StatusKind.Success);
                    MessageBox.Show($"Đã thêm nhóm hàng \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ShowApiErrorAsync("Thêm mới", response);
                }
            }
            catch (Exception ex)
            {
                ShowException("Thêm mới", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Nút CẬP NHẬT (UPDATE): gửi PUT lên Web API theo ID
        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!TryGetSelectedId("cập nhật", out int id)) return;
            if (!ValidateInputs(id)) return;

            string name = txtCategoryName.Text.Trim();
            string desc = txtDescription.Text.Trim();
            var updateCat = new { CategoryId = id, CategoryName = name, Description = desc.Length == 0 ? null : desc };

            SetBusy(true);
            try
            {
                using var client = GetAuthenticatedClient();
                using var response = await client.PutAsJsonAsync($"categories/{id}", updateCat);
                if (response.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                    ClearInputs();
                    SetStatus($"Đã cập nhật nhóm hàng \"{name}\".", StatusKind.Success);
                    MessageBox.Show($"Đã cập nhật nhóm hàng \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ShowApiErrorAsync("Cập nhật", response);
                    if (response.StatusCode == HttpStatusCode.NotFound) await LoadDataAsync(); // dòng đã bị xóa -> làm mới danh sách
                }
            }
            catch (Exception ex)
            {
                ShowException("Cập nhật", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Nút XÓA (DELETE): gửi DELETE lên Web API theo ID
        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!TryGetSelectedId("xóa", out int id)) return;

            string name = txtCategoryName.Text.Trim();
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa nhóm hàng \"{name}\" (ID = {id})?\nThao tác này không thể hoàn tác.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirm != DialogResult.Yes) return;

            SetBusy(true);
            try
            {
                using var client = GetAuthenticatedClient();
                using var response = await client.DeleteAsync($"categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                    ClearInputs();
                    SetStatus($"Đã xóa nhóm hàng \"{name}\".", StatusKind.Success);
                    MessageBox.Show($"Đã xóa nhóm hàng \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ShowApiErrorAsync("Xóa", response);
                    if (response.StatusCode == HttpStatusCode.NotFound) await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                ShowException("Xóa", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Nút TÌM KIẾM (SEARCH): gọi API lọc nhóm hàng theo từ khóa (Query String)
        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (keyword.Length == 0)
            {
                await LoadDataAsync(); // ô tìm kiếm trống thì tải lại toàn bộ
                return;
            }

            SetBusy(true);
            try
            {
                using var client = GetAuthenticatedClient();
                // GET /api/categories/search?keyword=abc (mã hóa URL để từ khóa có dấu/khoảng trắng/ký tự đặc biệt không bị lỗi)
                using var response = await client.GetAsync($"categories/search?keyword={Uri.EscapeDataString(keyword)}");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Tìm kiếm", response);
                    return;
                }

                var result = await response.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? new List<CategoryDto>();
                BindGrid(result);
                if (result.Count == 0)
                    SetStatus($"Không tìm thấy nhóm hàng nào với từ khóa \"{keyword}\".", StatusKind.Warning);
                else
                    SetStatus($"Tìm thấy {result.Count} nhóm hàng với từ khóa \"{keyword}\".", StatusKind.Info);
            }
            catch (Exception ex)
            {
                ShowException("Tìm kiếm", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        #endregion
    }

    // Lớp DTO trung gian tại Client hứng dữ liệu JSON trả về từ Server
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}