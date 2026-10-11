using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
            SetupGrid();

            // Mặc định chọn từ đầu tháng tới hôm nay
            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Today;
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private void SetupGrid()
        {
            dgvTopProducts.AutoGenerateColumns = false;
            dgvTopProducts.Columns.Clear();

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên Sản Phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantitySold",
                HeaderText = "Số Lượng Đã Bán",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Tổng Thành Tiền",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0 đ" }
            });
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync()
        {
            DateTime fromDate = dtpFromDate.Value.Date;
            DateTime toDate = dtpToDate.Value.Date;

            // Kiểm tra ngay tại máy: báo rõ ngày nào sai, không cần gọi API
            if (fromDate > toDate)
            {
                MessageBox.Show(
                    $"Ngày bắt đầu ({fromDate:dd/MM/yyyy}) không được lớn hơn ngày kết thúc ({toDate:dd/MM/yyyy}).\n\n" +
                    "Hãy chọn lại \"Từ ngày\" nhỏ hơn hoặc bằng \"Đến\".",
                    "Khoảng ngày không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFromDate.Focus();
                return;
            }

            btnFilter.Enabled = false;
            btnFilter.Text = "Đang tải...";

            try
            {
                string from = fromDate.ToString("yyyy-MM-dd");
                string to = toDate.ToString("yyyy-MM-dd");

                using var response = await ApiClientService.Client.GetAsync($"reports/summary?fromDate={from}&toDate={to}");
                if (!response.IsSuccessStatusCode)
                {
                    string detail = await ReadErrorDetailAsync(response);
                    MessageBox.Show(
                        $"Không tải được báo cáo từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}.\n\n" +
                        $"Nguyên nhân: {detail}\n" +
                        $"Mã lỗi HTTP: {(int)response.StatusCode} ({response.StatusCode})",
                        "Thống kê thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var report = await response.Content.ReadFromJsonAsync<ReportSummaryDto>();

                if (report != null)
                {
                    lblTotalRevenue.Text = $"{report.TotalRevenue:N0} đ";
                    lblTotalOrders.Text = report.TotalOrders.ToString("N0");
                    lblTotalProductsSold.Text = report.TotalProductsSold.ToString("N0");

                    // Tính giá trị trung bình trên mỗi đơn hàng (AOV)
                    decimal avgOrderValue = report.TotalOrders > 0 ? report.TotalRevenue / report.TotalOrders : 0;
                    lblAvgOrderValue.Text = $"{avgOrderValue:N0} đ";

                    // Gán danh sách top sản phẩm bán chạy nếu có
                    dgvTopProducts.DataSource = report.TopProducts ?? new List<TopProductDto>();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không kết nối được tới Server.\n\n" +
                    "Hãy kiểm tra project MiniSupermarket.API đã chạy chưa và port có khớp không.\n" +
                    $"Chi tiết kỹ thuật: {ex.Message}",
                    "Thống kê thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu báo cáo: " + ex.Message, "Thống kê thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFilter.Enabled = true;
                btnFilter.Text = "Xem Báo Cáo";
            }
        }

        // Đọc lý do Server trả về (chuỗi thường, JSON {"message": ...} hoặc JSON chuỗi)
        private static async Task<string> ReadErrorDetailAsync(HttpResponseMessage response)
        {
            string body = (await response.Content.ReadAsStringAsync()).Trim();
            if (body.Length == 0)
            {
                return response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => "Phiên đăng nhập đã hết hạn. Hãy đăng xuất rồi đăng nhập lại.",
                    System.Net.HttpStatusCode.Forbidden => "Tài khoản hiện tại không có quyền xem báo cáo (chỉ Admin).",
                    System.Net.HttpStatusCode.NotFound => "Không tìm thấy API báo cáo. Hãy kiểm tra API đã chạy chưa.",
                    _ => "Server không nêu rõ lý do."
                };
            }
            try
            {
                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.String) return root.GetString() ?? body;
                if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("message", out var m)) return m.ToString();
            }
            catch (JsonException) { /* nội dung là chữ thường, dùng nguyên văn bên dưới */ }
            return body.Length > 300 ? body.Substring(0, 300) + "..." : body;
        }

        private void dgvTopProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class ReportSummaryDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProductsSold { get; set; }
        public List<TopProductDto>? TopProducts { get; set; }
    }

    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
    }
}