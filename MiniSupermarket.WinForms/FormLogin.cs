namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            AcceptButton = btnLogin;              // nhấn Enter = Đăng nhập
            txtPass.UseSystemPasswordChar = true; // ẩn mật khẩu
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            try
            {
                var (ok, message) = await ApiClientService.LoginAsync(username, password);
                if (!ok)
                {
                    MessageBox.Show(message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đăng nhập thành công -> mở khung chính (Shell), ẩn form đăng nhập
                Hide();
                using (var shell = new FormMainShell())
                {
                    shell.ShowDialog();
                }

                // Shell đóng lại: nếu bấm Đăng xuất (phiên đã bị xóa) -> hiện lại màn hình đăng nhập,
                // còn nếu tắt cửa sổ bằng nút X -> thoát ứng dụng.
                if (string.IsNullOrEmpty(SessionManager.JwtToken))
                {
                    txtPass.Clear();
                    txtUser.Focus();
                    Show();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message + "\n\nKiểm tra API đã chạy chưa và port 7132 có đúng không.",
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void FormLogin_Load(object sender, EventArgs e) { }
    }
}