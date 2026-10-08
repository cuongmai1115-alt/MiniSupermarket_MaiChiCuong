using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        // Biến lưu trữ Form con đang được kích hoạt hiển thị
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
            // Label mặc định coi ký tự & là phím tắt nên làm mất chữ '&' trong tiêu đề
            lblTitle.UseMnemonic = false;
            lblUserInfo.UseMnemonic = false;
            WireEvents();
        }

        /// <summary>
        /// Gắn sự kiện bằng code: gỡ trước rồi gắn lại nên không bao giờ bị chạy 2 lần,
        /// dù trong Designer bạn đã bấm đúp gắn sự kiện hay chưa.
        /// </summary>
        private void WireEvents()
        {
            Load -= FormMainShell_Load; Load += FormMainShell_Load;
            btnPOS.Click -= btnPOS_Click; btnPOS.Click += btnPOS_Click;
            btnCategory.Click -= btnCategory_Click; btnCategory.Click += btnCategory_Click;
            btnProduct.Click -= btnProduct_Click; btnProduct.Click += btnProduct_Click;
            btnCustomer.Click -= btnCustomer_Click; btnCustomer.Click += btnCustomer_Click;
            btnReports.Click -= btnReports_Click; btnReports.Click += btnReports_Click;
            btnUserManage.Click -= btnUserManage_Click; btnUserManage.Click += btnUserManage_Click;
            btnLogout.Click -= btnLogout_Click; btnLogout.Click += btnLogout_Click;
        }

        /// <summary>
        /// Chạy khi form mở: hiện thông tin phiên, áp dụng phân quyền, mở màn hình mặc định
        /// </summary>
        private void FormMainShell_Load(object? sender, EventArgs e)
        {
            // 1. Hiển thị thông tin phiên người dùng đăng nhập
            lblUserInfo.Text = $"Nhân viên: {SessionManager.CurrentFullName} ({SessionManager.CurrentUsername}) | Vai trò: [{SessionManager.CurrentRole}]";

            // 2. Kích hoạt phân quyền giao diện theo vai trò (Role-Based Access)
            if (!ApplyRolePermissions(SessionManager.CurrentRole)) return;

            // 3. Mở màn hình mặc định tương ứng với vai trò
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        // ===================== BƯỚC 2: NẠP FORM ĐỘNG VÀO MAIN PANEL =====================

        /// <summary>
        /// Hàm nhúng động một Form con vào vùng panelMainContent
        /// </summary>
        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)
        {
            // Đóng form con đang mở (nếu có) trước khi nạp form mới
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            HighlightActiveButton(senderButton);

            _activeForm = childForm;
            childForm.TopLevel = false;                          // Bỏ thuộc tính cửa sổ độc lập cấp cao nhất
            childForm.FormBorderStyle = FormBorderStyle.None;     // Bỏ viền và thanh điều khiển Windows
            childForm.Dock = DockStyle.Fill;                     // Tràn toàn bộ vùng panelMainContent

            panelMainContent.Controls.Clear();                   // Dọn dẹp màn hình cũ
            panelMainContent.Controls.Add(childForm);            // Thêm form mới vào vùng chứa
            panelMainContent.Tag = childForm;

            lblTitle.Text = screenTitle;                        // Đồng bộ tên màn hình trên Header
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Làm nổi bật nút menu bên Sidebar đang được chọn
        /// </summary>
        private void HighlightActiveButton(Button activeButton)
        {
            ResetSidebarButtons(panelSidebar);
            activeButton.BackColor = Color.FromArgb(41, 100, 180); // Màu xanh active
        }

        // Duyệt panelSidebar (kể cả panel con) để trả các nút về màu gốc, trừ nút Đăng xuất
        private void ResetSidebarButtons(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = Color.FromArgb(24, 30, 48); // Màu gốc
                }
                else if (ctrl.HasChildren)
                {
                    ResetSidebarButtons(ctrl);
                }
            }
        }

        /// <summary>
        /// Form tạm cho các màn hình chưa xây dựng (POS, Sản phẩm, Báo cáo, Tài khoản).
        /// Khi bạn làm xong form thật, thay lời gọi CreatePlaceholderForm(...) bằng new FormXxx().
        /// </summary>
        private static Form CreatePlaceholderForm(string message)
        {
            var f = new Form();
            f.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16F),
                ForeColor = Color.Gray,
                Text = message
            });
            return f;
        }

        // ===================== BƯỚC 3: PHÂN QUYỀN (ADMIN / CASHIER / WAREHOUSE) =====================

        /// <summary>
        /// Phân định quyền truy cập hiển thị theo vai trò người dùng.
        /// Trả về false nếu vai trò không hợp lệ (form sẽ tự đóng).
        /// </summary>
        private bool ApplyRolePermissions(string role)
        {
            switch ((role ?? string.Empty).ToUpper())
            {
                case "ADMIN":
                    // Quản trị viên: Có toàn quyền sử dụng tất cả các nút
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    return true;

                case "CASHIER":
                    // Thu ngân: Chỉ truy cập màn hình Bán hàng (POS) và Khách hàng
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    btnCategory.Visible = false;
                    btnProduct.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    return true;

                case "WAREHOUSE":
                    // Thủ kho: Chỉ quản lý Danh mục nhóm hàng và Sản phẩm tồn kho
                    btnPOS.Visible = false;
                    btnCustomer.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    return true;

                default:
                    // Vai trò không xác định: Khóa toàn bộ
                    MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return false;
            }
        }

        /// <summary>
        /// Điều hướng ngay vào màn hình đúng chuyên môn của từng vai trò
        /// (theo bảng tổng kết: Admin -> Danh mục, Cashier -> POS, Warehouse -> Sản phẩm)
        /// </summary>
        private void OpenDefaultScreenByRole(string role)
        {
            switch ((role ?? string.Empty).ToUpper())
            {
                case "ADMIN":
                    btnCategory_Click(btnCategory, EventArgs.Empty);
                    break;
                case "CASHIER":
                    btnPOS_Click(btnPOS, EventArgs.Empty);
                    break;
                case "WAREHOUSE":
                    btnProduct_Click(btnProduct, EventArgs.Empty);
                    break;
            }
        }

        // ================= CÁC SỰ KIỆN CLICK NÚT TRÊN SIDEBAR (đã gắn sẵn trong Designer) =================

        // 1. Quản lý Danh mục (Admin & Warehouse)
        private void btnCategory_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
        }

        // 2. Bán hàng POS (Admin & Cashier) - màn hình thật sẽ hoàn thiện ở Buổi 5
        private void btnPOS_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
        }

        // 3. Quản lý Sản phẩm (Admin & Warehouse)
        private void btnProduct_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);
        }

        // 4. Quản lý Khách hàng (Admin & Cashier)
        private void btnCustomer_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
        }

        // 5. Báo cáo Doanh thu (Chỉ Admin) - có thêm lớp bảo vệ logic ngoài việc ẩn nút
        private void btnReports_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT", btnReports);
        }

        // 6. Quản trị Tài khoản Người dùng (Chỉ Admin)
        private void btnUserManage_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Admin mới được quản trị tài khoản!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            OpenChildForm(new FormUserManagement(), "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG", btnUserManage);
        }

        // 7. Đăng xuất: xóa phiên rồi đóng khung chính, FormLogin sẽ hiện lại
        private void btnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất phiên làm việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                SessionManager.Clear();
                this.Close();
            }
        }

        // Giữ lại để không lỗi nếu Designer của bạn đã gắn sự kiện Paint cho panelMainContent
        private void panelMainContent_Paint(object? sender, PaintEventArgs e)
        {
        }
    }
}