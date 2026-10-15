using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private enum StatusKind { Info, Success, Warning, Error }

        private static readonly Color ColorMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color ColorOk = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorWarn = Color.FromArgb(217, 119, 6);
        private static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);

        // Phải khớp với ValidRoles trong UsersController
        private static readonly string[] Roles = { "Admin", "Cashier", "Warehouse" };
        private const string AllRoles = "Tất cả vai trò";

        private List<UserDto> _allUsers = new();   // toàn bộ tài khoản đã tải từ server
        private List<UserDto> _current = new();    // kết quả sau khi lọc theo từ khóa + vai trò
        private int _selectedUserId;
        private int _busyCount; // đếm số request đang chạy để khóa/mở nút đúng lúc

        public FormUserManagement()
        {
            InitializeComponent();
            SetupGrid();

            // Gõ lại vào ô nào thì xóa dấu ! đỏ của ô đó
            txtUsername.TextChanged += (s, e) => errorProvider1.SetError(txtUsername, string.Empty);
            txtPassword.TextChanged += (s, e) => errorProvider1.SetError(txtPassword, string.Empty);
            txtFullName.TextChanged += (s, e) => errorProvider1.SetError(txtFullName, string.Empty);

            // Nhấn Enter trong ô tìm kiếm = bấm nút Tìm kiếm
            txtKeyword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnSearch.PerformClick();
                }
            };
            // Đổi bộ lọc vai trò thì lọc luôn, không cần bấm nút
            cbFilterRole.SelectedIndexChanged += (s, e) => { if (_allUsers.Count > 0) ApplyFilter(); };
        }

        private async void FormUserManagement_Load(object? sender, EventArgs e)
        {
            cbRole.DataSource = Roles.ToList();
            cbRole.SelectedItem = "Cashier";

            cbFilterRole.Items.Add(AllRoles);
            cbFilterRole.Items.AddRange(Roles);
            cbFilterRole.SelectedIndex = 0;

            UpdateButtonStates();
            await ReloadAllAsync();
        }

        #region Giao diện lưới

        private void SetupGrid()
        {
            dgvUsers.AutoGenerateColumns = false; // chỉ hiện các cột đã khai báo, không tự sinh cột IsActive
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.GridColor = Color.FromArgb(229, 231, 235);
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.RowTemplate.Height = 34;

            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsers.ColumnHeadersHeight = 38;
            dgvUsers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
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
            dgvUsers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(6, 0, 6, 0)
            };
            dgvUsers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            // Mọi cột chia đều phần còn lại theo tỉ lệ (FillWeight) nên luôn vừa khung
            dgvUsers.ScrollBars = ScrollBars.Vertical;
            SetFill(colId, 8F, 50);
            SetFill(colUsername, 24F, 110);
            SetFill(colFullName, 34F, 130);
            SetFill(colRole, 16F, 90);
            SetFill(colStatus, 18F, 90);
        }

        private static void SetFill(DataGridViewColumn col, float weight, int minWidth)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = weight;
            col.MinimumWidth = minWidth;
        }

        // Tài khoản bị khóa: chữ xám, trạng thái màu đỏ để nhìn là biết
        private void dgvUsers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.Rows[e.RowIndex].DataBoundItem is not UserDto u || u.IsActive) return;

            e.CellStyle!.ForeColor = ColorMuted;
            if (dgvUsers.Columns[e.ColumnIndex] == colStatus)
            {
                e.CellStyle.ForeColor = ColorDanger;
                e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        #endregion

        #region Tạo thông báo lỗi chi tiết (cùng kiểu với form Sản phẩm)

        private static string GetReason(string action, HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    return "Dữ liệu gửi lên chưa hợp lệ hoặc thao tác không được phép. Hãy kiểm tra lại thông tin đã nhập.";
                case HttpStatusCode.Unauthorized:
                    return "Phiên đăng nhập đã hết hạn hoặc chưa đăng nhập. Hãy đăng xuất rồi đăng nhập lại.";
                case HttpStatusCode.Forbidden:
                    return $"Tài khoản hiện tại (vai trò: {SessionManager.CurrentRole}) không có quyền \"{action}\". Chỉ Admin được quản trị tài khoản.";
                case HttpStatusCode.NotFound:
                    return "Không tìm thấy tài khoản này (có thể đã bị xóa) hoặc sai địa chỉ API. Hãy bấm Tải lại để cập nhật danh sách.";
                case HttpStatusCode.Conflict:
                    return "Thao tác bị từ chối vì xung đột dữ liệu (tên đăng nhập đã tồn tại).";
                case HttpStatusCode.InternalServerError:
                    return "Server gặp lỗi khi xử lý yêu cầu. Xem cửa sổ log của project API để biết chi tiết.";
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                    return "Server tạm thời không phản hồi. Hãy thử lại sau ít phút.";
                default:
                    return "Lỗi không xác định.";
            }
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
                    if (root.TryGetProperty("message", out var m)) details.Add(m.ToString());
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
            if (details.Count > 0) sb.AppendLine($"Chi tiết: {details[0]}");
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

            UseWaitCursor = _busyCount > 0;
            UpdateButtonStates();
        }

        // Bật/tắt nút theo trạng thái: đang gọi API, đã chọn dòng hay chưa
        private void UpdateButtonStates()
        {
            bool idle = _busyCount <= 0;
            bool hasSelection = _selectedUserId != 0;

            btnSearch.Enabled = btnLoad.Enabled = btnClear.Enabled = idle;
            btnAdd.Enabled = idle && !hasSelection;     // đang chọn tài khoản có sẵn thì không thêm mới
            btnUpdate.Enabled = idle && hasSelection;
            btnToggleLock.Enabled = idle && hasSelection;
            btnResetPassword.Enabled = idle && hasSelection;
        }

        // Đưa danh sách đang lọc lên lưới và cập nhật dòng trạng thái
        private void ShowCurrent()
        {
            if (IsDisposed) return;

            dgvUsers.DataSource = _current.ToList();
            dgvUsers.ClearSelection(); // không chọn sẵn dòng đầu để tránh lệch với các ô nhập

            int locked = _allUsers.Count(u => !u.IsActive);
            string keyword = txtKeyword.Text.Trim();
            bool filtering = keyword.Length > 0 || (cbFilterRole.SelectedItem?.ToString() ?? AllRoles) != AllRoles;

            if (_allUsers.Count == 0)
                SetStatus("Chưa có tài khoản nào.", StatusKind.Warning);
            else if (filtering && _current.Count == 0)
                SetStatus("Không tìm thấy tài khoản nào phù hợp với bộ lọc.", StatusKind.Warning);
            else if (filtering)
                SetStatus($"Tìm thấy {_current.Count}/{_allUsers.Count} tài khoản.", StatusKind.Info);
            else
                SetStatus($"Hiển thị {_current.Count} tài khoản ({locked} tài khoản đang bị khóa).", locked > 0 ? StatusKind.Warning : StatusKind.Info);
        }

        private void ClearInput()
        {
            _selectedUserId = 0;
            errorProvider1.Clear();
            txtId.Clear();
            txtUsername.Clear();
            txtUsername.ReadOnly = false;
            txtPassword.Clear();
            txtPassword.Enabled = true;
            txtFullName.Clear();
            cbRole.SelectedItem = "Cashier";
            btnToggleLock.Text = "Khóa";
            dgvUsers.ClearSelection();
            UpdateButtonStates();
            txtUsername.Focus();
        }

        // Kiểm tra dữ liệu nhập ngay tại Client, báo lỗi cạnh đúng ô nhập
        // isNew = true: kiểm tra thêm tên đăng nhập + mật khẩu; false (cập nhật): chỉ họ tên + vai trò
        private bool ValidateInputs(bool isNew)
        {
            errorProvider1.Clear();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string fullName = txtFullName.Text.Trim();

            if (isNew)
            {
                if (username.Length == 0)
                    return Invalid(txtUsername, "Tên đăng nhập không được để trống.");
                if (username.Length < 4)
                    return Invalid(txtUsername, "Tên đăng nhập phải có ít nhất 4 ký tự.");
                if (username.Contains(' '))
                    return Invalid(txtUsername, "Tên đăng nhập không được chứa khoảng trắng.");
                if (_allUsers.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)))
                    return Invalid(txtUsername, $"Tên đăng nhập \"{username}\" đã tồn tại.");
                if (password.Length < 6)
                    return Invalid(txtPassword, "Mật khẩu phải có ít nhất 6 ký tự.");
            }
            if (fullName.Length == 0)
                return Invalid(txtFullName, "Họ và tên không được để trống.");
            if (cbRole.SelectedItem is not string)
                return Invalid(cbRole, "Vui lòng chọn quyền hạn cho tài khoản.");
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

        // Hộp thoại nhập mật khẩu mới (trả về null nếu bấm Hủy)
        private string? AskNewPassword(string username)
        {
            using var dlg = new Form
            {
                Text = "Đặt lại mật khẩu",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9.5F),
                ClientSize = new Size(360, 130)
            };
            var lbl = new Label { Text = $"Nhập mật khẩu mới cho \"{username}\" (tối thiểu 6 ký tự):", Left = 15, Top = 15, Width = 330, Height = 36 };
            var txt = new TextBox { Left = 15, Top = 55, Width = 330, UseSystemPasswordChar = true };
            var ok = new Button { Text = "Đồng ý", Left = 175, Top = 90, Width = 85, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Hủy", Left = 265, Top = 90, Width = 80, DialogResult = DialogResult.Cancel };
            dlg.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
            dlg.AcceptButton = ok;
            dlg.CancelButton = cancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }

        #endregion

        #region Tải dữ liệu từ API

        // GET api/users; lỗi thì tự báo và trả về null
        private async Task<List<UserDto>?> FetchUsersAsync(string action)
        {
            try
            {
                using var response = await ApiClientService.Client.GetAsync("users");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync(action, response);
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? new List<UserDto>();
            }
            catch (Exception ex)
            {
                ShowException(action, ex);
                return null;
            }
        }

        // Tải lại TOÀN BỘ tài khoản rồi áp lại bộ lọc đang chọn
        private async Task ReloadAllAsync()
        {
            SetBusy(true);
            try
            {
                var all = await FetchUsersAsync("Tải danh sách tài khoản");
                if (all == null) return;
                _allUsers = all;
                ApplyFilter();
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Số lượng tài khoản ít nên lọc ngay trên Client (không gọi API lại)
        private void ApplyFilter()
        {
            string keyword = txtKeyword.Text.Trim();
            string role = cbFilterRole.SelectedItem?.ToString() ?? AllRoles;

            _current = _allUsers.Where(u =>
                (role == AllRoles || u.Role == role) &&
                (keyword.Length == 0 ||
                 u.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                 u.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            ).ToList();
            ShowCurrent();
        }

        #endregion

        #region Sự kiện

        private void btnSearch_Click(object? sender, EventArgs e) => ApplyFilter();

        // Nút TẢI LẠI: xóa từ khóa + bộ lọc rồi tải lại toàn bộ
        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtKeyword.Clear();
            cbFilterRole.SelectedIndex = 0;
            await ReloadAllAsync();
        }

        private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.Rows[e.RowIndex].DataBoundItem is not UserDto u) return;

            errorProvider1.Clear();
            _selectedUserId = u.Id;
            txtId.Text = u.Id.ToString();
            txtUsername.Text = u.Username;
            txtUsername.ReadOnly = true;     // không cho đổi tên đăng nhập
            txtPassword.Clear();
            txtPassword.Enabled = false;     // đổi mật khẩu dùng nút "Đặt lại MK"
            txtFullName.Text = u.FullName;
            cbRole.SelectedItem = u.Role;
            btnToggleLock.Text = u.IsActive ? "Khóa" : "Mở khóa";
            UpdateButtonStates();
            SetStatus($"Đang chọn tài khoản \"{u.Username}\".", StatusKind.Info);
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs(true)) return;

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Role = cbRole.SelectedItem!.ToString()
            };

            SetBusy(true);
            try
            {
                // Endpoint đúng là POST api/users (trước đây gọi nhầm users/register)
                using var response = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Thêm tài khoản", response);
                    return;
                }
                string addedUsername = newUser.Username;
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Thêm tài khoản thành công!", StatusKind.Success);
                MessageBox.Show($"Đã thêm tài khoản \"{addedUsername}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Thêm tài khoản", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật trong danh sách.", "Chưa chọn tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(false)) return;

            var update = new
            {
                FullName = txtFullName.Text.Trim(),
                Role = cbRole.SelectedItem!.ToString()
            };

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PutAsJsonAsync($"users/{_selectedUserId}", update);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Cập nhật tài khoản", response);
                    return;
                }
                string updatedUsername = txtUsername.Text.Trim();
                ClearInput();
                await ReloadAllAsync();
                SetStatus("Cập nhật tài khoản thành công!", StatusKind.Success);
                MessageBox.Show($"Đã cập nhật tài khoản \"{updatedUsername}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Cập nhật tài khoản", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnToggleLock_Click(object? sender, EventArgs e)
        {
            var user = _allUsers.FirstOrDefault(u => u.Id == _selectedUserId);
            if (user == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản trong danh sách.", "Chưa chọn tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string action = user.IsActive ? "khóa" : "mở khóa";
            if (MessageBox.Show($"Bạn có chắc chắn muốn {action} tài khoản \"{user.Username}\" không?",
                    $"Xác nhận {action}", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PutAsync($"users/{user.Id}/toggle-lock", null);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync($"Đổi trạng thái tài khoản", response);
                    return;
                }
                ClearInput();
                await ReloadAllAsync();
                SetStatus($"Đã {action} tài khoản \"{user.Username}\".", StatusKind.Success);
                MessageBox.Show($"Đã {action} tài khoản \"{user.Username}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Đổi trạng thái tài khoản", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnResetPassword_Click(object? sender, EventArgs e)
        {
            var user = _allUsers.FirstOrDefault(u => u.Id == _selectedUserId);
            if (user == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản trong danh sách.", "Chưa chọn tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? newPassword = AskNewPassword(user.Username);
            if (newPassword == null) return; // bấm Hủy
            if (newPassword.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBusy(true);
            try
            {
                using var response = await ApiClientService.Client.PutAsJsonAsync($"users/{user.Id}/reset-password", new { NewPassword = newPassword });
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Đặt lại mật khẩu", response);
                    return;
                }
                SetStatus($"Đã đặt lại mật khẩu cho \"{user.Username}\".", StatusKind.Success);
                MessageBox.Show($"Đã đặt lại mật khẩu cho tài khoản \"{user.Username}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowException("Đặt lại mật khẩu", ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInput();
            SetStatus("Sẵn sàng nhập tài khoản mới.", StatusKind.Info);
        }

        #endregion

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    // DTO phía client: cùng tên thuộc tính với JSON của GET api/users
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string StatusText => IsActive ? "Hoạt động" : "Đã khóa";
    }
}