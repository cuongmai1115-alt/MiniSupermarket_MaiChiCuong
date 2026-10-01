using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // Địa chỉ Base URL tới API của bạn (thay đổi port nếu cần)
        private const string BaseApiUrl = "https://localhost:7123/api/";

        public FormCustomerManagement()
        {
            InitializeComponent();
        }

        // Tạo instance HttpClient thủ công
        private HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseApiUrl)
            };
            // Nếu API có cấu hình JWT Auth, bạn có thể gán Bearer Token ở đây:
            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "YOUR_TOKEN_HERE");
            return client;
        }

        // Đọc thông tin lỗi từ HttpResponseMessage nếu status không phải Success
        private async Task<string> GetErrorMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var errorObj = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                if (!string.IsNullOrEmpty(errorObj?.Message))
                {
                    return errorObj.Message;
                }
            }
            catch
            {
                // Fallback nếu không parse được Json dạng { message: "..." }
            }
            return $"Mã lỗi: {(int)response.StatusCode} ({response.ReasonPhrase})";
        }

        private async void FormCustomerManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Gọi API GET lấy danh sách và đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                using var client = GetHttpClient();
                var response = await client.GetAsync("customers");
                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = await GetErrorMessageAsync(response);
                    MessageBox.Show($"Tải dữ liệu thất bại: {errorMsg}", "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvCustomers.DataSource = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối đến máy chủ: {ex.Message}", "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCustomers.Rows[e.RowIndex].DataBoundItem is CustomerDto c)
            {
                txtCustomerId.Text = c.CustomerId.ToString();
                txtCustomerName.Text = c.CustomerName;
                txtPhoneNumber.Text = c.PhoneNumber;
                txtAddress.Text = c.Address ?? string.Empty;
                txtRewardPoints.Text = c.RewardPoints.ToString();
                txtMembershipRank.Text = c.MembershipRank;
            }
        }

        private bool TryReadInputs(out int points)
        {
            points = 0;
            var errors = new List<string>();
            string name = txtCustomerName.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();

            if (string.IsNullOrEmpty(name)) errors.Add("- Tên khách hàng không được để trống.");
            else if (name.Length > 100) errors.Add("- Tên khách hàng tối đa 100 ký tự.");

            if (string.IsNullOrEmpty(phone)) errors.Add("- Số điện thoại không được để trống.");
            else if (phone.Length > 15 || !phone.All(char.IsDigit)) errors.Add("- Số điện thoại chỉ gồm chữ số, tối đa 15 ký tự.");

            if (txtAddress.Text.Length > 200) errors.Add("- Địa chỉ tối đa 200 ký tự.");

            string pointsText = txtRewardPoints.Text.Trim();
            if (pointsText.Length > 0 && (!int.TryParse(pointsText, out points) || points < 0))
                errors.Add("- Điểm thưởng phải là số nguyên không âm.");

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private object BuildPayload(int? id, int points)
        {
            string rank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text.Trim();
            return new
            {
                CustomerId = id ?? 0,
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = points,
                MembershipRank = rank
            };
        }

        // THÊM MỚI (POST)
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadInputs(out int points)) return;
            try
            {
                using var client = GetHttpClient();
                var response = await client.PostAsJsonAsync("customers", BuildPayload(null, points));
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    string errorMsg = await GetErrorMessageAsync(response);
                    MessageBox.Show($"Thêm mới thất bại: {errorMsg}", "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CẬP NHẬT (PUT)
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCustomerId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryReadInputs(out int points)) return;
            try
            {
                using var client = GetHttpClient();
                var response = await client.PutAsJsonAsync($"customers/{id}", BuildPayload(id, points));
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    string errorMsg = await GetErrorMessageAsync(response);
                    MessageBox.Show($"Cập nhật thất bại: {errorMsg}", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // XÓA (DELETE)
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCustomerId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            try
            {
                using var client = GetHttpClient();
                var response = await client.DeleteAsync($"customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    string errorMsg = await GetErrorMessageAsync(response);
                    MessageBox.Show($"Xóa thất bại: {errorMsg}", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TÌM KIẾM
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync();
                return;
            }
            try
            {
                using var client = GetHttpClient();
                var response = await client.GetAsync($"customers/search?keyword={Uri.EscapeDataString(keyword)}");
                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = await GetErrorMessageAsync(response);
                    MessageBox.Show($"Tìm kiếm thất bại: {errorMsg}", "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var result = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
                dgvCustomers.DataSource = result;
                if (result == null || result.Count == 0)
                    MessageBox.Show($"Không có khách hàng nào khớp với \"{keyword}\".", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtCustomerId.Text = "";
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtRewardPoints.Text = "";
            txtMembershipRank.Text = "";
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

    // DTO đọc chuỗi JSON báo lỗi { "message": "..." }
    public class ErrorResponse
    {
        public string? Message { get; set; }
    }
}