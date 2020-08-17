using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpcDaLogger
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "onstart.txt");

            DataLogger.Services.LoggingServices service = new DataLogger.Services.LoggingServices();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            service.ReadConfigurations(path);

            service.RegistLoggingService();
        }

        protected override void OnStop()
        {
        }
    }
}
