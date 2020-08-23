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
        private static string StatictisLog = $"{AppDomain.CurrentDomain.BaseDirectory}/StatictisLog.txt";
        private static int TotalRead = 0;
        private static int TotalError = 0;

        private static bool ClearRuntimeLog = false;
        private static bool ClearStartStopLog = false;
        private static bool ClearExceptionLog = false;


        public static void WriteStatictis(bool error = false)
        {
            try
            {
                if (error)
                    TotalError++;

                TotalRead++;

                using (StreamWriter streamWriter = new StreamWriter(StatictisLog, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss")} ==> Success ratio: " +
                        $"{(TotalRead - TotalError)}/{TotalRead} = {(TotalRead - TotalError) * 100 / TotalRead}%");
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLogFile(ex.ToString());
            }
        }
        public static void WriteOnStartStopLogFile(bool onStartUp)
        {
            try
            {
                if (DateTime.Now.Day == 1 && !ClearStartStopLog)
                {
                    File.WriteAllText(OnStartStopLogFilePath, "");
                    ClearStartStopLog = true;
                }
                else
                {
                    ClearStartStopLog = false;
                }

                using (StreamWriter streamWriter = new StreamWriter(OnStartStopLogFilePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss")} ==> {(onStartUp == true ? "Service is starting" : "Service is stopping")}");
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
                if (DateTime.Now.Day == 1 && !ClearExceptionLog)
                {
                    File.WriteAllText(ExceptionLogFilePath, "");
                    ClearExceptionLog = true;
                }
                else
                {
                    ClearExceptionLog = false;
                }

                using (StreamWriter streamWriter = new StreamWriter(ExceptionLogFilePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss")} ==> Log Message:");
                    streamWriter.WriteLine(error);
                }
            }
            catch { }
        }
        public static void WriteLine(string message)
        {
            try
            {
                if (DateTime.Now.Day == 1 && !ClearRuntimeLog)
                {
                    File.WriteAllText(RuntimeLogging, "");
                    ClearRuntimeLog = true;
                }
                else
                {
                    ClearRuntimeLog = false;
                }

                Console.WriteLine(message);

                using (StreamWriter streamWriter = new StreamWriter(RuntimeLogging, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss")} ==> {message}");
                }
            }
            catch { }
        }
    }
}
