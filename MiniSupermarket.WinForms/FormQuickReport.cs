﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClosedXML.Excel;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
            SetupGrid();
            BuildExtraTabs();
            BuildQuickBar();

            // Mặc định chọn từ đầu tháng tới hôm nay
            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Today;
        }

        // =========================================================================
        // TIÊU CHÍ 44: Gọi API GET api/reports/dashboard-summary nạp 4 thẻ chỉ số tổng quan
        // =========================================================================
        private async Task LoadDashboardSummaryAsync()
        {
            try
            {
                using var response = await ApiClientService.Client.GetAsync("reports/dashboard-summary");
                if (response.IsSuccessStatusCode)
                {
                    var dash = await response.Content.ReadFromJsonAsync<DashboardSummaryDto>();
                    if (dash != null)
                    {
                        // Thẻ 1: Doanh thu hôm nay (VNĐ)
                        lblRevenueTitle.Text = "DOANH THU HÔM NAY";
                        lblTotalRevenue.Text = $"{dash.TodayRevenue:N0} đ";

                        // Thẻ 2: Tổng số hóa đơn đã xuất
                        lblOrdersTitle.Text = "TỔNG HÓA ĐƠN ĐÃ XUẤT";
                        lblTotalOrders.Text = dash.TotalOrders.ToString("N0");

                        // Thẻ 3: Số lượng khách hàng mới đăng ký
                        lblProductsTitle.Text = "KHÁCH HÀNG MỚI ĐĂNG KÝ";
                        lblTotalProductsSold.Text = $"{dash.NewCustomers:N0} KH";

                        // Thẻ 4: Số mặt hàng sắp hết hàng (tồn kho <= 10)
                        lblAvgTitle.Text = "HÀNG SẮP HẾT (<= 10)";
                        lblAvgOrderValue.Text = $"{dash.LowStockCount:N0} SP";
                        if (dash.LowStockCount > 0)
                        {
                            cardAvg.BackColor = Color.FromArgb(254, 226, 226); // Nền đỏ cảnh báo
                            lblAvgOrderValue.ForeColor = Color.FromArgb(220, 38, 38);
                        }
                        else
                        {
                            cardAvg.BackColor = Color.FromArgb(243, 232, 255);
                            lblAvgOrderValue.ForeColor = Color.FromArgb(88, 28, 135);
                        }
                    }
                }
            }
            catch { /* Bỏ qua lỗi kết nối tạm thời */ }
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
            await LoadDashboardSummaryAsync();
        }

        private static void ApplyModernGridStyle(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(229, 231, 235);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowTemplate.Height = 32;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(31, 41, 55),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(31, 41, 55),
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.False,
                Padding = new Padding(10, 0, 10, 0)
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39),
                Font = new Font("Segoe UI", 9.5F),
                Padding = new Padding(10, 0, 10, 0)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };

            dgv.ScrollBars = ScrollBars.Vertical;
            dgv.ShowCellToolTips = true;
        }

        private void SetupGrid()
        {
            ApplyModernGridStyle(dgvTopProducts);
            dgvTopProducts.Columns.Clear();

            var colName = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên Sản Phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 180
            };
            colName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var colQty = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantitySold",
                HeaderText = "Số Lượng Đã Bán",
                Width = 140,
                MinimumWidth = 120
            };
            colQty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colQty.DefaultCellStyle.Format = "N0";

            var colTotal = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Tổng Thành Tiền",
                Width = 160,
                MinimumWidth = 140
            };
            colTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Format = "#,##0 đ";

            dgvTopProducts.Columns.AddRange(colName, colQty, colTotal);
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
                //thẻ KPI trên đỉnh được quản lý bởi LoadDashboardSummaryAsync() đúng chuẩn Tiêu chí 44

                    // Gán danh sách top sản phẩm bán chạy nếu có
                    dgvTopProducts.DataSource = report.TopProducts ?? new List<TopProductDto>();
                    ApplyExtraReport(report);
                    await LoadOrdersAsync(from, to);
                    await LoadDashboardSummaryAsync();
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

        // =====================================================================
        // NÂNG CẤP: các tab phân tích (tạo bằng code, không đụng Designer)
        // =====================================================================
        private TabControl _tabs = null!;
        private TabPage _tabLow = null!;
        private TabPage _tabTop = null!;
        private TabPage _tabDaily = null!;
        private SplitContainer _splitDaily = null!;
        private ChartPanel _pnlChart = new();
        private Chart _chartRevenue = null!;
        private DataGridView _dgvDaily = null!;
        private DataGridView _dgvCashier = null!;
        private DataGridView _dgvLowStock = null!;
        private List<DailyRevenueDto> _daily = new();
        private DataGridView _dgvCategory = null!;
        private DataGridView _dgvOrders = null!;
        private TabPage _tabOrders = null!;
        private Rectangle _plot;
        private float _slot;
        private int _hover = -1;
        private readonly ToolTip _tip = new();

        private void BuildExtraTabs()
        {
            grpTopProducts.Text = " Phân tích chi tiết ";
            grpTopProducts.Controls.Remove(dgvTopProducts);

            _tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F), Padding = new Point(12, 5) };

            _tabTop = new TabPage("Sản phẩm bán chạy");
            dgvTopProducts.Dock = DockStyle.Fill;
            _tabTop.Controls.Add(dgvTopProducts);

            // Tab doanh thu theo ngày: biểu đồ cột gọn + bảng chi tiết nhiều dòng
            _tabDaily = new TabPage("Doanh thu theo ngày");
            _dgvDaily = NewGrid();
            _dgvDaily.RowTemplate.Height = 30;
            _dgvDaily.ColumnHeadersHeight = 34;
            AddCol(_dgvDaily, "Date", "Ngày", 115, "dd/MM/yyyy", false, center: true);
            AddCol(_dgvDaily, "Orders", "Số đơn", 85, "N0", true);
            AddCol(_dgvDaily, "TotalItems", "SL Sản phẩm", 140, "N0", true);
            AddCol(_dgvDaily, "TopProducts", "Sản phẩm bán ra tiêu biểu", 0, null, false);
            AddCol(_dgvDaily, "Revenue", "Doanh thu", 145, "#,##0 đ", true);
            _dgvDaily.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    _tabs.SelectedTab = _tabOrders;
                }
            };

            // Tiêu chí 45: Sử dụng System.Windows.Forms.DataVisualization.Charting.Chart (Series ChartType = Column)
            _chartRevenue = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            var chartArea = new ChartArea("MainArea") { BackColor = Color.White };
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            chartArea.AxisX.MajorTickMark.LineColor = Color.FromArgb(209, 213, 219);
            chartArea.AxisX.LineColor = Color.FromArgb(209, 213, 219);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(75, 85, 99);
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.IsMarginVisible = true;
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            chartArea.AxisY.MajorTickMark.LineColor = Color.FromArgb(209, 213, 219);
            chartArea.AxisY.LineColor = Color.FromArgb(209, 213, 219);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(107, 114, 128);
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Crossing = 0;
            chartArea.AxisY.IsMarginVisible = false;



            chartArea.AxisY.LabelStyle.Format = "#,##0";
            _chartRevenue.ChartAreas.Add(chartArea);

            var series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,  //45. khỏi tạo chart cột
                Color = Color.FromArgb(59, 130, 246),
                BackSecondaryColor = Color.FromArgb(37, 99, 235),
                BackGradientStyle = GradientStyle.VerticalCenter,
                BorderColor = Color.FromArgb(29, 78, 216),
                BorderWidth = 1,
                ShadowColor = Color.FromArgb(25, 0, 0, 0),
                ShadowOffset = 2,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                LabelForeColor = Color.FromArgb(30, 41, 59)
            };
            series["PointWidth"] = "0.55";
            series["BarLabelStyle"] = "Outside";
            series.SmartLabelStyle.Enabled = true;
            series.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No;
            series.SmartLabelStyle.MovingDirection = LabelAlignmentStyles.Top | LabelAlignmentStyles.TopLeft | LabelAlignmentStyles.TopRight;
            _chartRevenue.Series.Add(series);

            _splitDaily = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 160,
                Panel1MinSize = 90,
                Panel2MinSize = 130,
                SplitterWidth = 6,
                BackColor = Color.FromArgb(229, 231, 235)
            };
            _splitDaily.Panel1.Controls.Add(_chartRevenue);
            _splitDaily.Panel2.Controls.Add(_dgvDaily);
            _tabDaily.Controls.Add(_splitDaily);

            _splitDaily.SizeChanged += (s, e) => AdjustSplitDaily();
            _tabs.SelectedIndexChanged += (s, e) =>
            {
                if (_tabs.SelectedTab == _tabDaily)
                {
                    BeginInvoke(new Action(AdjustSplitDaily));
                }
            };

            var tabCategory = new TabPage("Theo nhóm hàng");
            _dgvCategory = NewGrid();
            AddCol(_dgvCategory, "Category", "Nhóm hàng", 0, null, false);
            AddCol(_dgvCategory, "Quantity", "Số lượng", 120, "N0", true);
            AddCol(_dgvCategory, "Revenue", "Doanh thu", 180, "#,##0 đ", true);
            AddCol(_dgvCategory, "Percent", "Tỷ trọng", 100, "0.0'%'", true);
            tabCategory.Controls.Add(_dgvCategory);

            var tabCashier = new TabPage("Theo thu ngân");
            _dgvCashier = NewGrid();
            AddCol(_dgvCashier, "Cashier", "Thu ngân", 0, null, false);
            AddCol(_dgvCashier, "Orders", "Số đơn", 140, "N0", true);
            AddCol(_dgvCashier, "Revenue", "Doanh thu", 200, "#,##0 đ", true);
            tabCashier.Controls.Add(_dgvCashier);

            _tabOrders = new TabPage("Hóa đơn");
            _dgvOrders = NewGrid();
            AddCol(_dgvOrders, "OrderCode", "Mã hóa đơn", 150, null, false);
            AddCol(_dgvOrders, "CreatedDate", "Thời gian", 140, "dd/MM/yyyy HH:mm", false);
            AddCol(_dgvOrders, "CustomerName", "Khách hàng", 0, null, false);
            AddCol(_dgvOrders, "Cashier", "Thu ngân", 110, null, false);
            AddCol(_dgvOrders, "TotalItems", "SL hàng", 95, "N0", true);
            AddCol(_dgvOrders, "FinalAmount", "Thành tiền", 130, "#,##0 đ", true);
            _dgvOrders.CellDoubleClick += DgvOrders_CellDoubleClick;
            _tabOrders.Controls.Add(_dgvOrders);

            _tabLow = new TabPage("Hàng sắp hết");
            _dgvLowStock = NewGrid();
            AddCol(_dgvLowStock, "ProductName", "Tên sản phẩm", 0, null, false);
            AddCol(_dgvLowStock, "StockQuantity", "Tồn kho", 140, "N0", true);
            _dgvLowStock.Columns[1].DefaultCellStyle.ForeColor = Color.Firebrick;
            _dgvLowStock.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _tabLow.Controls.Add(_dgvLowStock);

            _tabs.TabPages.Add(_tabTop);
            _tabs.TabPages.Add(_tabDaily);
            _tabs.TabPages.Add(tabCategory);
            _tabs.TabPages.Add(tabCashier);
            _tabs.TabPages.Add(_tabOrders);
            _tabs.TabPages.Add(_tabLow);
            grpTopProducts.Controls.Add(_tabs);
        }

        private static DataGridView NewGrid()
        {
            var g = new DataGridView { Dock = DockStyle.Fill };
            ApplyModernGridStyle(g);
            return g;
        }

        private static void AddCol(DataGridView g, string prop, string header, int width, string? format, bool right, bool center = false)
        {
            var col = new DataGridViewTextBoxColumn { DataPropertyName = prop, HeaderText = header, MinimumWidth = width > 0 ? width : 120 };
            if (width <= 0) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; else col.Width = width;
            if (format != null) col.DefaultCellStyle.Format = format;
            if (center) { col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; } else if (right)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            g.Columns.Add(col);
        }

        private void AdjustSplitDaily()
        {
            if (_splitDaily != null && _splitDaily.Height > 220)
            {
                try
                {
                    // Giữ biểu đồ cột ở mức vừa phải (~150px - 165px), dành phần lớn không gian cho bảng danh sách doanh thu
                    int targetH = Math.Min(165, Math.Max(110, (int)(_splitDaily.Height * 0.38)));
                    if (targetH >= _splitDaily.Panel1MinSize && targetH <= _splitDaily.Height - _splitDaily.Panel2MinSize)
                    {
                        _splitDaily.SplitterDistance = targetH;
                    }
                }
                catch { }
            }
        }

        private void ApplyExtraReport(ReportSummaryDto r)
        {
            // So sánh doanh thu với kỳ trước, hiện ngay trên tiêu đề thẻ doanh thu
            if (r.RevenueChangePercent.HasValue)
            {
                decimal pct = r.RevenueChangePercent.Value;
                string arrow = pct > 0 ? "▲" : pct < 0 ? "▼" : "=";
                // lblRevenueTitle.Text = $"TỔNG DOANH THU  {arrow} {Math.Abs(pct):0.#}%";
            }
            else
            {
                // lblRevenueTitle.Text = "TỔNG DOANH THU";
            }

            _daily = r.DailyRevenue ?? new List<DailyRevenueDto>();
            if (_tabTop != null)
            {
                _tabTop.Text = $"Sản phẩm bán chạy ({r.TopProducts?.Count ?? 0})";
            }

            var activeDays = _daily.Where(d => d.Orders > 0 || d.Revenue > 0).ToList();
            var tableSource = activeDays.Count > 0 ? activeDays.OrderByDescending(d => d.Date).ToList() : _daily;
            _dgvDaily.DataSource = tableSource;
            _tabDaily.Text = $"Doanh thu theo ngày ({tableSource.Count})";
            _pnlChart?.Invalidate();

            if (_chartRevenue != null && _chartRevenue.Series.IndexOf("DoanhThu") >= 0) //45.kiểm tra tồn tại series "DoanhThu" trước khi xóa và thêm điểm dữ liệu
            {
                var s = _chartRevenue.Series["DoanhThu"];
                s.Points.Clear();
                var chartList = activeDays.Count > 0 ? activeDays.OrderBy(d => d.Date).ToList() : _daily;
                foreach (var d in chartList)
                {
                    var p = new DataPoint();
                    p.SetValueXY(d.Date.ToString("dd/MM"), (double)d.Revenue);
                    p.ToolTip = $"{d.Date:dd/MM/yyyy}: {d.Revenue:N0} đ ({d.Orders} đơn, {d.TotalItems} SP)\n{d.TopProducts}";
                    if (d.Revenue > 0)
                    {
                        p.Label = Short(d.Revenue);
                    }
                    else
                    {
                        p.Label = "";
                    }
                    s.Points.Add(p);
                }
            }

            _dgvCashier.DataSource = r.ByCashier ?? new List<CashierRevenueDto>();
            _dgvCategory.DataSource = r.ByCategory ?? new List<CategoryRevenueDto>();

            var low = r.LowStockProducts ?? new List<LowStockDto>();
            _dgvLowStock.DataSource = low;
                        _tabLow.Text = $"Hàng sắp hết ({low.Count})";
            if (_tabs.SelectedTab == _tabDaily)
            {
                BeginInvoke(new Action(AdjustSplitDaily));
            }
        }

        // Biểu đồ cột doanh thu theo ngày (tự vẽ, không cần thư viện ngoài)
        private void PnlChart_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            var r = _pnlChart.ClientRectangle;

            using var txt = new SolidBrush(Color.DimGray);
            using var font = new Font("Segoe UI", 8F);

            if (_daily.Count == 0 || _daily.All(d => d.Revenue <= 0))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("Chưa có đơn hàng nào trong khoảng ngày này", font, txt, r, sf);
                return;
            }

            const int padL = 60, padR = 15, padT = 12, padB = 26;
            var plot = new Rectangle(padL, padT, r.Width - padL - padR, r.Height - padT - padB);
            if (plot.Width <= 0 || plot.Height <= 0) return;

            decimal max = _daily.Max(d => d.Revenue);

            using var gridPen = new Pen(Color.Gainsboro);
            for (int i = 0; i <= 4; i++)
            {
                int y = plot.Bottom - plot.Height * i / 4;
                g.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                g.DrawString(Short(max * i / 4), font, txt, 2, y - 8);
            }

            float slot = (float)plot.Width / _daily.Count;
            _plot = plot;
            _slot = slot;
            float barW = Math.Max(2f, slot * 0.65f);
            int labelStep = Math.Max(1, (int)Math.Ceiling(38f / slot));
            using var bar = new SolidBrush(Color.FromArgb(37, 99, 235));
            using var barHover = new SolidBrush(Color.FromArgb(29, 78, 216));

            for (int i = 0; i < _daily.Count; i++)
            {
                float h = (float)(_daily[i].Revenue / max) * plot.Height;
                float x = plot.Left + slot * i + (slot - barW) / 2f;
                g.FillRectangle(i == _hover ? barHover : bar, x, plot.Bottom - h, barW, h);
                if (i % labelStep == 0)
                    g.DrawString(_daily[i].Date.ToString("dd/MM"), font, txt, x - 6, plot.Bottom + 4);
            }
        }

        // ---- Tooltip khi rê chuột lên cột biểu đồ ----
        private void PnlChart_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_daily.Count == 0 || _slot <= 0 || !_plot.Contains(e.X, Math.Min(Math.Max(e.Y, _plot.Top), _plot.Bottom)))
            {
                if (_hover != -1) { _hover = -1; _tip.Hide(_pnlChart); _pnlChart.Invalidate(); }
                return;
            }
            int idx = (int)((e.X - _plot.Left) / _slot);
            if (idx < 0 || idx >= _daily.Count || idx == _hover) return;
            _hover = idx;
            var d = _daily[idx];
            _tip.Show($"{d.Date:dddd, dd/MM/yyyy}\nDoanh thu: {d.Revenue:N0} đ\nSố đơn: {d.Orders:N0}", _pnlChart, e.X + 14, e.Y + 14);
            _pnlChart?.Invalidate();
        }

        // ---- Danh sách hóa đơn trong kỳ + xem chi tiết (nhấp đúp) ----
        private async Task LoadOrdersAsync(string from, string to)
        {
            using var resp = await ApiClientService.Client.GetAsync($"reports/orders?fromDate={from}&toDate={to}");
            List<OrderRowDto>? rows = resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<List<OrderRowDto>>()
                : null;
            rows ??= new List<OrderRowDto>();
            _dgvOrders.DataSource = rows;
            _tabOrders.Text = $"Hóa đơn ({rows.Count})";
        }

        private async void DgvOrders_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _dgvOrders.Rows[e.RowIndex].DataBoundItem is not OrderRowDto row) return;
            try
            {
                using var resp = await ApiClientService.Client.GetAsync($"reports/orders/{row.OrderId}");
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không tải được chi tiết hóa đơn " + row.OrderCode, "Hóa đơn",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var d = await resp.Content.ReadFromJsonAsync<OrderDetailResultDto>();
                if (d == null) return;

                var sb = new StringBuilder();
                sb.AppendLine($"Hóa đơn: {d.OrderCode}");
                sb.AppendLine($"Thời gian: {d.CreatedDate:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"Khách hàng: {d.CustomerName}   |   Thu ngân: {d.Cashier}");
                sb.AppendLine(new string('-', 52));
                foreach (var l in d.Lines)
                    sb.AppendLine($"{l.ProductName}\n      {l.Quantity} x {l.UnitPrice:N0} = {l.LineTotal:N0} đ");
                sb.AppendLine(new string('-', 52));
                sb.AppendLine($"Tổng ({d.TotalItems} món): {d.FinalAmount:N0} đ");
                if (d.RewardPoints > 0) sb.AppendLine($"Điểm tích lũy: +{d.RewardPoints}");
                MessageBox.Show(sb.ToString(), "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết hóa đơn: " + ex.Message, "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- Thanh chọn nhanh khoảng ngày + Xuất CSV ----
        private void BuildQuickBar()
        {
            var pnl = new Panel { Dock = DockStyle.Top, Height = 46, BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(12, 6, 12, 6) };

            var btnExport = new Button
            {
                Text = "Xuất Excel/CSV",
                Dock = DockStyle.Right,
                Width = 135,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(22, 163, 74),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += async (s, e) => await ExportReportAsync();

            var flow = new FlowLayoutPanel { Dock = DockStyle.Left, AutoSize = true, WrapContents = false, FlowDirection = FlowDirection.LeftToRight };
            var today = DateTime.Today;
            var firstOfMonth = new DateTime(today.Year, today.Month, 1);
            var firstOfLast = firstOfMonth.AddMonths(-1);
            var quarterStart = new DateTime(today.Year, (today.Month - 1) / 3 * 3 + 1, 1);

            flow.Controls.Add(QuickButton("Hôm nay", today, today));
            flow.Controls.Add(QuickButton("7 ngày qua", today.AddDays(-6), today));
            flow.Controls.Add(QuickButton("Tháng này", firstOfMonth, today));
            flow.Controls.Add(QuickButton("Tháng trước", firstOfLast, firstOfMonth.AddDays(-1)));
            flow.Controls.Add(QuickButton("Quý này", quarterStart, today));

            pnl.Controls.Add(btnExport);
            pnl.Controls.Add(flow);

            Controls.Add(pnl);
            // Đặt ngay dưới thanh tiêu đề, trên 4 thẻ số liệu
            Controls.SetChildIndex(pnl, Controls.IndexOf(pnlHeader));
        }

        private Button QuickButton(string text, DateTime from, DateTime to)
        {
            var b = new Button
            {
                Text = text,
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 65, 85),
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(0, 1, 8, 1),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            b.Click += async (s, e) =>
            {
                dtpFromDate.Value = from;
                dtpToDate.Value = to;
                await LoadReportDataAsync();
            };
            return b;
        }

        private static DataGridView? FindGridInControl(Control? c)
        {
            if (c == null) return null;
            if (c is DataGridView g) return g;
            foreach (Control child in c.Controls)
            {
                var found = FindGridInControl(child);
                if (found != null) return found;
            }
            return null;
        }

        // =========================================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO DOANH THU & HÓA ĐƠN RA EXCEL (.XLSX) HOẶC CSV
        // Kỹ thuật: ClosedXML / Gọi API / Xuất trực tiếp WinForms
        // Nghiệp vụ: Quản lý bấm "Xuất Excel" tải toàn bộ báo cáo doanh thu
        // =========================================================================
        private async Task ExportReportAsync()
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Chức năng chỉ dành cho cấp Quản lý (Admin)!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fromStr = dtpFromDate.Value.ToString("yyyyMMdd");
            string toStr = dtpToDate.Value.ToString("yyyyMMdd");

            using var dlg = new SaveFileDialog
            {
                Title = "Xuất báo cáo doanh thu & hóa đơn",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx|Tệp CSV (*.csv)|*.csv",
                FileName = $"BaoCao_DoanhThu_HoaDon_{fromStr}-{toStr}.xlsx"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            string filePath = dlg.FileName;
            bool isCsv = filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);

            try
            {
                UseWaitCursor = true;
                bool apiSuccess = false;

                // Thử tải qua API
                try
                {
                    string endpoint = isCsv
                        ? $"reports/export-csv?fromDate={dtpFromDate.Value:yyyy-MM-dd}&toDate={dtpToDate.Value:yyyy-MM-dd}"
                        : $"reports/export-excel?fromDate={dtpFromDate.Value:yyyy-MM-dd}&toDate={dtpToDate.Value:yyyy-MM-dd}";

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

                // Nếu API chưa sẵn sàng hoặc ngoại tuyến, xuất trực tiếp bằng ClosedXML / CSV
                if (!apiSuccess)
                {
                    if (isCsv)
                    {
                        ExportCurrentTabToCsv(filePath);
                    }
                    else
                    {
                        ExportReportWithClosedXmlDirectly(filePath);
                    }
                }

                var ask = MessageBox.Show(
                    $"Xuất báo cáo doanh thu & hóa đơn thành công!\n\nĐường dẫn: {filePath}\n\nBạn có muốn mở tệp ngay không?",
                    "Xuất Excel thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (ask == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất tệp: " + ex.Message, "Xuất Excel / CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ExportReportWithClosedXmlDirectly(string filePath)
        {
            using var workbook = new XLWorkbook();

            // Export Sheet 1: Hóa đơn
            if (_dgvOrders != null && _dgvOrders.Rows.Count > 0)
            {
                AddGridToWorksheet(workbook, "Danh sách hóa đơn", "BÁO CÁO CHI TIẾT HÓA ĐƠN BÁN HÀNG", _dgvOrders, XLColor.FromHtml("#1E3A8A"), XLColor.FromHtml("#2563EB"));
            }

            // Export Sheet 2: Doanh thu theo ngày
            if (_dgvDaily != null && _dgvDaily.Rows.Count > 0)
            {
                AddGridToWorksheet(workbook, "Doanh thu theo ngày", "BÁO CÁO DOANH THU THEO TỪNG NGÀY", _dgvDaily, XLColor.FromHtml("#059669"), XLColor.FromHtml("#10B981"));
            }

            // Export Sheet 3: Top bán chạy
            if (dgvTopProducts != null && dgvTopProducts.Rows.Count > 0)
            {
                AddGridToWorksheet(workbook, "Top bán chạy", "DANH SÁCH MẶT HÀNG BÁN CHẠY NHẤT", dgvTopProducts, XLColor.FromHtml("#D97706"), XLColor.FromHtml("#F59E0B"));
            }

            // Export Sheet 4: Theo nhóm hàng
            if (_dgvCategory != null && _dgvCategory.Rows.Count > 0)
            {
                AddGridToWorksheet(workbook, "Theo nhóm hàng", "DOANH THU VÀ TỶ TRỌNG THEO NHÓM HÀNG", _dgvCategory, XLColor.FromHtml("#7C3AED"), XLColor.FromHtml("#8B5CF6"));
            }

            // Export Sheet 5: Hàng sắp hết
            if (_dgvLowStock != null && _dgvLowStock.Rows.Count > 0)
            {
                AddGridToWorksheet(workbook, "Hàng sắp hết", "CẢNH BÁO TỒN KHO MẶT HÀNG SẮP HẾT", _dgvLowStock, XLColor.FromHtml("#DC2626"), XLColor.FromHtml("#EF4444"));
            }

            // Nếu không có grid nào có dữ liệu
            if (workbook.Worksheets.Count == 0)
            {
                var ws = workbook.Worksheets.Add("Báo cáo");
                ws.Cell(1, 1).Value = "Không có dữ liệu trong khoảng thời gian đã chọn.";
            }

            workbook.SaveAs(filePath);
        }

        private void AddGridToWorksheet(XLWorkbook workbook, string sheetName, string title, DataGridView grid, XLColor headerThemeColor, XLColor colHeaderColor)
        {
            var ws = workbook.Worksheets.Add(sheetName);
            var cols = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();

            // Tiêu đề
            ws.Cell(1, 1).Value = title;
            ws.Range(1, 1, 1, cols.Count).Merge();
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            ws.Cell(1, 1).Style.Fill.BackgroundColor = headerThemeColor;
            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(1).Height = 32;

            ws.Cell(2, 1).Value = $"Kỳ báo cáo: Từ {dtpFromDate.Value:dd/MM/yyyy} đến {dtpToDate.Value:dd/MM/yyyy} | Tổng số dòng: {grid.Rows.Count:N0}";
            ws.Range(2, 1, 2, cols.Count).Merge();
            ws.Cell(2, 1).Style.Font.Italic = true;
            ws.Cell(2, 1).Style.Font.FontSize = 10;
            ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(2).Height = 20;

            // Headers
            for (int i = 0; i < cols.Count; i++)
            {
                var cell = ws.Cell(4, i + 1);
                cell.Value = cols[i].HeaderText;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = colHeaderColor;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            ws.Row(4).Height = 26;

            int rowIdx = 5;
            foreach (DataGridViewRow r in grid.Rows)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    object? v = r.Cells[cols[i].Index].Value;
                    var cell = ws.Cell(rowIdx, i + 1);

                    if (v is decimal decVal)
                    {
                        cell.Value = decVal;
                        cell.Style.NumberFormat.Format = "#,##0";
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                    else if (v is int intVal)
                    {
                        cell.Value = intVal;
                        cell.Style.NumberFormat.Format = "#,##0";
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                    else if (v is double dblVal)
                    {
                        cell.Value = dblVal;
                        cell.Style.NumberFormat.Format = "#,##0.0";
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                    else if (v is DateTime dtVal)
                    {
                        cell.Value = dtVal.ToString("dd/MM/yyyy HH:mm");
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    else
                    {
                        cell.SetValue(v?.ToString() ?? string.Empty);
                    }

                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = XLColor.FromHtml("#E5E7EB");
                }
                rowIdx++;
            }

            ws.Columns().AdjustToContents();
        }

        private void ExportCurrentTabToCsv(string? targetFileName = null)
        {
            var grid = FindGridInControl(_tabs.SelectedTab);
            if (grid == null || grid.Rows.Count == 0)
            {
                MessageBox.Show("Tab hiện tại chưa có dữ liệu để xuất.", "Xuất Excel / CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName = targetFileName ?? string.Empty;
            if (string.IsNullOrEmpty(fileName))
            {
                string tabName = (_tabs.SelectedTab?.Text ?? "BaoCao").Split('(')[0].Trim().Replace(' ', '_');
                using var dlg = new SaveFileDialog
                {
                    Filter = "Excel CSV (*.csv)|*.csv",
                    FileName = $"BaoCao_{tabName}_{dtpFromDate.Value:yyyyMMdd}-{dtpToDate.Value:yyyyMMdd}.csv"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;
                fileName = dlg.FileName;
            }

            static string Esc(string v) => "\"" + v.Replace("\"", "\"\"") + "\"";
            var cols = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            var sb = new StringBuilder();
            sb.AppendLine(string.Join(",", cols.Select(c => Esc(c.HeaderText))));
            foreach (DataGridViewRow row in grid.Rows)
            {
                sb.AppendLine(string.Join(",", cols.Select(c =>
                {
                    object? v = row.Cells[c.Index].Value;
                    string s = v switch
                    {
                        null => "",
                        DateTime d => d.ToString("yyyy-MM-dd HH:mm"),
                        IFormattable fm => fm.ToString(null, CultureInfo.InvariantCulture),
                        _ => v.ToString() ?? ""
                    };
                    return Esc(s);
                })));
            }

            File.WriteAllText(fileName, sb.ToString(), new UTF8Encoding(true)); // BOM để Excel đọc đúng tiếng Việt
        }

        private static string Short(decimal v) =>
            v >= 1000000m ? $"{v / 1000000m:0.#}tr" : v >= 1000m ? $"{v / 1000m:0.#}k" : v.ToString("0");

        private sealed class ChartPanel : Panel
        {
            public ChartPanel() { DoubleBuffered = true; ResizeRedraw = true; }
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
        public decimal PreviousRevenue { get; set; }
        public decimal? RevenueChangePercent { get; set; }
        public List<TopProductDto>? TopProducts { get; set; }
        public List<DailyRevenueDto>? DailyRevenue { get; set; }
        public List<CashierRevenueDto>? ByCashier { get; set; }
        public List<CategoryRevenueDto>? ByCategory { get; set; }
        public List<LowStockDto>? LowStockProducts { get; set; }
    }

    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public int Orders { get; set; }
        public int TotalItems { get; set; }
        public string TopProducts { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
    }

    public class CashierRevenueDto
    {
        public string Cashier { get; set; } = string.Empty;
        public int Orders { get; set; }
        public int TotalItems { get; set; }
        public string TopProducts { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
    }

    public class CategoryRevenueDto
    {
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
        public decimal Percent { get; set; }
    }

    public class OrderRowDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Cashier { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public decimal FinalAmount { get; set; }
    }

    public class OrderDetailLineDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class OrderDetailResultDto
    {
        public string OrderCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Cashier { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public decimal FinalAmount { get; set; }
        public int RewardPoints { get; set; }
        public List<OrderDetailLineDto> Lines { get; set; } = new();
    }

    public class DashboardSummaryDto
    {
        public decimal TodayRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int NewCustomers { get; set; }
        public int LowStockCount { get; set; }
    }

    public class LowStockDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }
}

