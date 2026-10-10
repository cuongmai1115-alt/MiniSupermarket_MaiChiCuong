using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    // Lưu phiên đăng nhập dùng chung toàn ứng dụng
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;

        public static bool IsAdmin =>
            string.Equals(CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);

        // Xóa phiên khi đăng xuất
        public static void Clear()
        {
            JwtToken = string.Empty;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
        }
    }

    public static class ApiClientService
    {
        // Nhớ chỉnh port cho đúng với API đang chạy (launchSettings: https://localhost:7132)
        private const string BaseUrl = "https://localhost:7132/api/";

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };

        // HttpClient dùng chung: mỗi lần lấy ra đều tự gắn Bearer Token mới nhất
        public static HttpClient Client
        {
            get
            {
                _client.DefaultRequestHeaders.Authorization =
                    string.IsNullOrEmpty(SessionManager.JwtToken)
                        ? null
                        : new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
                return _client;
            }
        }

        // Đăng nhập: trả về (thành công?, thông báo lỗi nếu có)
        public static async Task<(bool Ok, string Message)> LoginAsync(string username, string password)
        {
            var response = await _client.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string msg = response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                    ? "Sai tài khoản hoặc mật khẩu!"
                    : "Đăng nhập thất bại (" + (int)response.StatusCode + ").";
                try
                {
                    using var err = JsonDocument.Parse(json);
                    if (err.RootElement.TryGetProperty("message", out var m))
                        msg = m.GetString() ?? msg;
                }
                catch (JsonException) { }
                return (false, msg);
            }

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            SessionManager.JwtToken = root.GetProperty("token").GetString() ?? string.Empty;
            SessionManager.CurrentRole = root.GetProperty("role").GetString() ?? string.Empty;
            SessionManager.CurrentUsername = root.GetProperty("username").GetString() ?? username;
            SessionManager.CurrentFullName = root.GetProperty("fullName").GetString() ?? string.Empty;
            return (true, string.Empty);
        }

        // Giữ lại hàm cũ của Buổi 2 để code cũ không lỗi
        public static async Task<string> GetDataWithTokenAsync(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("Phiên làm việc hết hạn hoặc chưa đăng nhập!");
            throw new Exception("Lỗi khi gọi dữ liệu từ Server.");
        }
    }
}