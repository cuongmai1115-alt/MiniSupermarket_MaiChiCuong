using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {

        // Khởi tạo HttpClient tĩnh kết nối trực tiếp đến Web API (Đảm bảo số Port https://localhost:7123 khớp với API của bạn)
        //private static readonly HttpClient _client = new HttpClient
        //{
        //    BaseAddress = new Uri("https://localhost:7132/api/")
        //};
        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7132/api/")
            };

            // Đính kèm Token vào Header theo chuẩn Bearer Authentication
            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }
            return client;
        }

        // ===== PHẦN THÊM MỚI: tạo nội dung thông báo lỗi chi tiết =====

        // Giải thích mã HTTP bằng tiếng Việt
        private static string GetReason(System.Net.HttpStatusCode code)
        {
            switch (code)
            {
                case System.Net.HttpStatusCode.BadRequest: return "Dữ liệu gửi lên không hợp lệ.";
                case System.Net.HttpStatusCode.Unauthorized: return "Chưa đăng nhập hoặc token hết hạn. Hãy đăng nhập lại.";
                case System.Net.HttpStatusCode.Forbidden: return "Bạn không có quyền thực hiện thao tác này.";
                case System.Net.HttpStatusCode.NotFound: return "Không tìm thấy dữ liệu hoặc sai đường dẫn API.";
                case System.Net.HttpStatusCode.Conflict: return "Dữ liệu bị trùng hoặc đang được sử dụng.";
                case System.Net.HttpStatusCode.InternalServerError: return "Lỗi xảy ra phía Server.";
                default: return "Lỗi không xác định.";
            }
        }

        // Đọc response lỗi từ API -> chuỗi dễ hiểu (mã lỗi, nguyên nhân, chi tiết từng trường)
        private static async Task<string> BuildErrorAsync(string action, HttpResponseMessage response)
        {
            string body = await response.Content.ReadAsStringAsync();
            var sb = new StringBuilder();
            sb.AppendLine($"{action} thất bại!");
            sb.AppendLine();
            sb.AppendLine($"Mã lỗi: {(int)response.StatusCode} ({response.StatusCode})");
            sb.AppendLine($"Nguyên nhân: {GetReason(response.StatusCode)}");

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    using var doc = JsonDocument.Parse(body);
                    var root = doc.RootElement;
                    if (root.ValueKind == JsonValueKind.Object)
                    {
                        if (root.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                        {
                            sb.AppendLine("Chi tiết:");
                            foreach (var field in errors.EnumerateObject())
                                foreach (var msg in field.Value.EnumerateArray())
                                    sb.AppendLine($"  - {field.Name}: {msg.GetString()}");
                        }
                        else if (root.TryGetProperty("message", out var m)) sb.AppendLine($"Chi tiết: {m.GetString()}");
                        else if (root.TryGetProperty("detail", out var d)) sb.AppendLine($"Chi tiết: {d.GetString()}");
                        else if (root.TryGetProperty("title", out var t)) sb.AppendLine($"Chi tiết: {t.GetString()}");
                    }
                    else sb.AppendLine($"Chi tiết: {body}");
                }
                catch (JsonException)
                {
                    sb.AppendLine($"Chi tiết: {(body.Length > 500 ? body.Substring(0, 500) + "..." : body)}");
                }
            }
            return sb.ToString();
        }

        // Tạo nội dung cho lỗi ngoại lệ (server tắt, mất mạng, timeout...)
        private static string BuildExceptionMessage(string action, Exception ex)
        {
            if (ex is HttpRequestException hre)
            {
                if (hre.StatusCode != null)
                    return $"{action} thất bại!\n\nMã lỗi: {(int)hre.StatusCode} ({hre.StatusCode})\nNguyên nhân: {GetReason(hre.StatusCode.Value)}";
                return $"{action} thất bại!\n\nKhông kết nối được tới Server.\nKiểm tra: API đã chạy chưa? Port 7132 có đúng không?\nChi tiết: {hre.Message}";
            }
            if (ex is TaskCanceledException)
                return $"{action} thất bại!\n\nHết thời gian chờ phản hồi từ Server (timeout).";
            if (ex is JsonException)
                return $"{action} thất bại!\n\nDữ liệu Server trả về sai định dạng.\nChi tiết: {ex.Message}";
            return $"{action} thất bại!\n\n{ex.GetType().Name}: {ex.Message}";
        }

        // ===============================================================

        public FormCategoryManagement()
        {
            InitializeComponent();
        }

        // Sự kiện Form vừa bật lên: Tự động tải dữ liệu từ API lên bảng
        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Hàm dùng chung: Gọi API GET lấy danh sách và đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                using var _client = GetAuthenticatedClient();
                // Gửi request GET tới endpoint "categories", tự động giải tuần tự hóa chuỗi JSON thành List<CategoryDto>
                var categories = await _client.GetFromJsonAsync<List<CategoryDto>>("categories");
                dgvCategories.DataSource = categories; // Gán nguồn dữ liệu cho bảng hiển thị
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildExceptionMessage("Tải dữ liệu", ex), "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Tải lại dữ liệu (Refresh)
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Sự kiện khi click vào một dòng trên DataGridView: Đưa dữ liệu lên các ô nhập (TextBox) để chuẩn bị Sửa/Xóa
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cat = dgvCategories.Rows[e.RowIndex].DataBoundItem as CategoryDto;
                if (cat != null)
                {
                    txtId.Text = cat.CategoryId.ToString();
                    txtCategoryName.Text = cat.CategoryName;
                    txtDescription.Text = cat.Description ?? string.Empty;
                }
            }
        }

        // Nút THÊM MỚI (CREATE): Gửi dữ liệu POST lên Web API
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newCat = new
            {
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text
            };
            try
            {
                using var _client = GetAuthenticatedClient();
                // Gửi request POST kèm theo đối tượng dạng JSON
                var response = await _client.PostAsJsonAsync("categories", newCat);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync(); // Tải lại danh sách mới
                    ClearInputs();         // Xóa sạch ô nhập
                }
                else
                {
                    MessageBox.Show(await BuildErrorAsync("Thêm mới", response), "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildExceptionMessage("Thêm mới", ex), "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút CẬP NHẬT (UPDATE): Gửi dữ liệu PUT lên Web API theo ID
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var updateCat = new
            {
                CategoryId = id,
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text
            };
            try
            {
                using var _client = GetAuthenticatedClient();

                // Gửi request PUT kèm ID trên đường dẫn URI
                var response = await _client.PutAsJsonAsync($"categories/{id}", updateCat);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(await BuildErrorAsync("Cập nhật", response), "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildExceptionMessage("Cập nhật", ex), "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút XÓA (DELETE): Gửi request DELETE lên Web API theo ID
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa nhóm hàng ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using var _client = GetAuthenticatedClient();
                    var response = await _client.DeleteAsync($"categories/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show(await BuildErrorAsync("Xóa", response), "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(BuildExceptionMessage("Xóa", ex), "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Nút TÌM KIẾM (SEARCH): Gọi API lọc danh mục theo từ khóa Query String
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync(); // Nếu ô tìm kiếm trống thì tải lại toàn bộ
                return;
            }

            try
            {
                using var _client = GetAuthenticatedClient();
                // Gọi API dạng: GET /api/categories/search?keyword=abc
                var result = await _client.GetFromJsonAsync<List<CategoryDto>>($"categories/search?keyword={keyword}");
                dgvCategories.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildExceptionMessage("Tìm kiếm", ex), "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm phụ trợ: Xóa trắng các ô nhập liệu sau khi thao tác xong
        private void ClearInputs()
        {
            txtId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";
        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCategories_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // Lớp DTO trung gian tại Client hứng dữ liệu JSON trả về từ Server
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}