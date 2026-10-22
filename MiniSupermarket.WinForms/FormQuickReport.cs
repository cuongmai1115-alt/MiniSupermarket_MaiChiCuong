using System;
using System.Collections.Generic;
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
                    ApplyExtraReport(report);
                    await LoadOrdersAsync(from, to);
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
        private ChartPanel _pnlChart = null!;
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

            _tabs = new TabControl { Dock = DockStyle.Fill };

            var tabTop = new TabPage("Top 5 bán chạy");
            dgvTopProducts.Dock = DockStyle.Fill;
            tabTop.Controls.Add(dgvTopProducts);

            // Tab doanh thu theo ngày: biểu đồ cột + bảng chi tiết
            var tabDaily = new TabPage("Doanh thu theo ngày");
            _dgvDaily = NewGrid();
            AddCol(_dgvDaily, "Date", "Ngày", 0, "dd/MM/yyyy", false);
            AddCol(_dgvDaily, "Orders", "Số đơn", 140, "N0", true);
            AddCol(_dgvDaily, "Revenue", "Doanh thu", 200, "#,##0 đ", true);
            _pnlChart = new ChartPanel { Dock = DockStyle.Top, Height = 190 };
            _pnlChart.Paint += PnlChart_Paint;
            _pnlChart.MouseMove += PnlChart_MouseMove;
            _pnlChart.MouseLeave += (s, e) => { _hover = -1; _tip.Hide(_pnlChart); _pnlChart.Invalidate(); };
            tabDaily.Controls.Add(_dgvDaily);   // Fill thêm trước, Top thêm sau để không bị che
            tabDaily.Controls.Add(_pnlChart);

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
            AddCol(_dgvOrders, "TotalItems", "SL hàng", 80, "N0", true);
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

            _tabs.TabPages.Add(tabTop);
            _tabs.TabPages.Add(tabDaily);
            _tabs.TabPages.Add(tabCategory);
            _tabs.TabPages.Add(tabCashier);
            _tabs.TabPages.Add(_tabOrders);
            _tabs.TabPages.Add(_tabLow);
            grpTopProducts.Controls.Add(_tabs);
        }

        private static DataGridView NewGrid() => new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };

        private static void AddCol(DataGridView g, string prop, string header, int width, string? format, bool right)
        {
            var col = new DataGridViewTextBoxColumn { DataPropertyName = prop, HeaderText = header };
            if (width <= 0) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; else col.Width = width;
            if (format != null) col.DefaultCellStyle.Format = format;
            if (right) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns.Add(col);
        }

        private void ApplyExtraReport(ReportSummaryDto r)
        {
            // So sánh doanh thu với kỳ trước, hiện ngay trên tiêu đề thẻ doanh thu
            if (r.RevenueChangePercent.HasValue)
            {
                decimal pct = r.RevenueChangePercent.Value;
                string arrow = pct > 0 ? "▲" : pct < 0 ? "▼" : "=";
                lblRevenueTitle.Text = $"TỔNG DOANH THU  {arrow} {Math.Abs(pct):0.#}%";
            }
            else
            {
                lblRevenueTitle.Text = "TỔNG DOANH THU";
            }

            _daily = r.DailyRevenue ?? new List<DailyRevenueDto>();
            _dgvDaily.DataSource = _daily;
            _pnlChart.Invalidate();

            _dgvCashier.DataSource = r.ByCashier ?? new List<CashierRevenueDto>();
            _dgvCategory.DataSource = r.ByCategory ?? new List<CategoryRevenueDto>();

            var low = r.LowStockProducts ?? new List<LowStockDto>();
            _dgvLowStock.DataSource = low;
            _tabLow.Text = $"Hàng sắp hết ({low.Count})";
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
            _pnlChart.Invalidate();
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
            var pnl = new Panel { Dock = DockStyle.Top, Height = 44, BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(15, 7, 15, 7) };

            var btnExport = new Button
            {
                Text = "Xuất CSV",
                Dock = DockStyle.Right,
                Width = 110,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(22, 163, 74),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += (s, e) => ExportCurrentTabToCsv();

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
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            b.Click += async (s, e) =>
            {
                dtpFromDate.Value = from;
                dtpToDate.Value = to;
                await LoadReportDataAsync();
            };
            return b;
        }

        private void ExportCurrentTabToCsv()
        {
            var grid = _tabs.SelectedTab?.Controls.OfType<DataGridView>().FirstOrDefault();
            if (grid == null || grid.Rows.Count == 0)
            {
                MessageBox.Show("Tab hiện tại chưa có dữ liệu để xuất.", "Xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tabName = (_tabs.SelectedTab?.Text ?? "BaoCao").Split('(')[0].Trim().Replace(' ', '_');
            using var dlg = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"BaoCao_{tabName}_{dtpFromDate.Value:yyyyMMdd}-{dtpToDate.Value:yyyyMMdd}.csv"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

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

            try
            {
                File.WriteAllText(dlg.FileName, sb.ToString(), new UTF8Encoding(true)); // BOM để Excel đọc đúng tiếng Việt
                MessageBox.Show("Đã xuất file:\n" + dlg.FileName, "Xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không ghi được file: " + ex.Message, "Xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        public decimal Revenue { get; set; }
    }

    public class CashierRevenueDto
    {
        public string Cashier { get; set; } = string.Empty;
        public int Orders { get; set; }
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

    public class LowStockDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }
}