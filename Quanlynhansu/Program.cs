using Quanlynhansuquancaphe;

namespace Quanlynhansu
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new fLogin());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Xảy ra lỗi không mong muốn: " + e.Exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



    }
}