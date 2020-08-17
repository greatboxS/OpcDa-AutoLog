using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;

namespace HistorianService
{
    class DebugLog
    {
        #region "Variables"  

        private string sLogFormat;
        private string sErrorTime;
        string sPathName = string.Empty;
        public DebugLog()
        {
            sPathName = AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            File.Create(sPathName);
        }

        #endregion


        #region "Local methods"  

        public void WriteToLogFile(string sErrMsg)
        {
            try
            {
                //sLogFormat used to create log format :  
                // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message  
                sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

                //this variable used to create log filename format "  
                //for example filename : ErrorLogYYYYMMDD  
                string sYear = DateTime.Now.Year.ToString();
                string sMonth = DateTime.Now.Month.ToString();
                string sDay = DateTime.Now.Day.ToString();
                sErrorTime = sYear + sMonth + sDay;

                //writing to log file  
                
                StreamWriter sw = new StreamWriter(sPathName, true);
                sw.WriteLine(sLogFormat + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                WriteToEventLog("MySite", "Logging.WriteToLogFile", "Error: " + ex.ToString(), EventLogEntryType.Error);
            }
        }
        public void WriteToEventLog(string sLog, string sSource, string message, EventLogEntryType level)
        {
            //RegistryPermission regPermission = new RegistryPermission(PermissionState.Unrestricted);  
            //regPermission.Assert();  

            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, message, level);
        }

        #endregion
    }
}
