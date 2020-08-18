using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;

namespace DataLogger
{
    public class DebugLog
    {
        private static string ExceptionLogFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}/onException.txt";
        private static string OnStartStopLogFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}/OnStartStop.txt";
        private static string RuntimeLogging = $"{AppDomain.CurrentDomain.BaseDirectory}/RuntimeLogging.txt";

        public static void WriteOnStartStopLogFile(bool onStartUp)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(OnStartStopLogFilePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("hh:mm:ss, dd/MM/yyyy")} ==> {(onStartUp==true? "Service is starting" : "Service is stopping")}");
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLogFile(ex.ToString());
            }
        }
        public static void WriteExceptionLogFile(string error)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(ExceptionLogFilePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("hh:mm:ss, dd/MM/yyyy")} ==> Log Message:");
                    streamWriter.WriteLine(error);
                }
            }
            catch { }
        }
        public static void WriteLine(string message)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(RuntimeLogging, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("hh:mm:ss, dd/MM/yyyy")} ==> {message}");
                }
            }
            catch { }
        }
    }
}
