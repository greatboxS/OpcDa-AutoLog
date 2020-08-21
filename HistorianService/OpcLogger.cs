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
        }

        protected override void OnStart(string[] args)
        {
            DebugLog.WriteOnStartStopLogFile(true);

            DataLogger.Services.LoggingServices service = new DataLogger.Services.LoggingServices();

            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string exce = string.Empty;
            try
            {
                DebugLog.WriteLine("Read configurations");
                service.ReadConfigurations(path);

                DebugLog.WriteLine("Regist services");
                service.RegistLoggingService();

                DebugLog.WriteLine("Start services");
                service.StartLogging();
            }
            catch(Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
            }
        }

        protected override void OnStop()
        {
            DebugLog.WriteOnStartStopLogFile(false);
        }
    }
}
