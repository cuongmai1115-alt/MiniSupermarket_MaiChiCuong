using System;
using System.Collections.Generic;
using System.Net.Http.Json;
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
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0 đ" }
            });
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync()
        {
            btnFilter.Enabled = false;
            btnFilter.Text = "Đang tải...";

            try
            {
                string from = dtpFromDate.Value.ToString("yyyy-MM-dd");
                string to = dtpToDate.Value.ToString("yyyy-MM-dd");

                var report = await ApiClientService.Client.GetFromJsonAsync<ReportSummaryDto>($"reports/summary?fromDate={from}&toDate={to}");

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