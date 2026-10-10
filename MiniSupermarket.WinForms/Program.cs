using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Mở Form Đăng nhập dưới dạng Dialog
            using (FormLogin loginForm = new FormLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // 2. Đăng nhập thành công -> Mở FormMainShell làm Form chính của ứng dụng
                    Application.Run(new FormMainShell());
                }
                else
                {
                    // Đóng FormLogin mà không đăng nhập -> Thoát ứng dụng
                    Application.Exit();
                }
            }
        }
    }
}