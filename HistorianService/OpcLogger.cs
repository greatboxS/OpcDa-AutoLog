using DataLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da;

namespace HistorianService
{
    partial class OpcLogger : ServiceBase
    {
        public OpcLogger()
        {
            InitializeComponent();
            service = new DataLogger.Services.LoggingServices();
        }

        public DataLogger.Services.LoggingServices service;
        protected override void OnStart(string[] args)
        {
            service = new DataLogger.Services.LoggingServices();

            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string exce = string.Empty;
            try
            {
                service.ReadConfigurations(path);

                service.RegistLoggingService();
            }
            catch(Exception ex)
            {
                exce = ex.ToString();
            }
        }

        protected override void OnStop()
        {
            
        }
    }
}
