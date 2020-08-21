using DataLogger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            bool hide = Properties.Settings.Default.HidenWindow;
            if (hide)
                ShowWindow(handle, SW_HIDE);

            DataLogger.Services.LoggingServices service = new DataLogger.Services.LoggingServices();

            string path = AppDomain.CurrentDomain.BaseDirectory;

            DebugLog.WriteLine("Read configurations");
            service.ReadConfigurations(path);

            DebugLog.WriteLine("Regist services");
            service.RegistLoggingService();

            DebugLog.WriteLine("Start services");
            service.StartLogging();

            Console.ReadLine();
        }
    }
}
