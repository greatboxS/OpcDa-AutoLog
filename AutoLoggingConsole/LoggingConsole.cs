using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoLoggingConsole
{
    class LoggingConsole
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            IntPtr handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            bool hide = Properties.Settings.Default.HidenWindow;
            if (hide)
                ShowWindow(handle, SW_HIDE);

            DataLogger.Services.LoggingServices service = new DataLogger.Services.LoggingServices();

            string path = AppDomain.CurrentDomain.BaseDirectory;

            Console.WriteLine("Start services");
            service.ReadConfigurations(path);

            Console.WriteLine("Start services");
            service.RegistLoggingService();

            Console.ReadLine();
        }
    }
}
