using DataLogger.SqlLogger;
using OPCDataAccess.Models;
using OPCDataAccess.OpcException;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLogger.Services
{
    public class LoggingServices
    {
        private ExcelHandle ExcelHandle = new ExcelHandle();
        private CustomSqlLog SqlLog;

        public static OpcDaClient OpcDaClient { get; set; }
        public static OpcDaWrapperCollection WrapColletion { get; set; } = new OpcDaWrapperCollection();

        public void ReadConfigurations(string path)
        {
            string configPath = path;

            string[] files = Directory.GetFiles(configPath);

            int id = 0;
            foreach (string s in files)
            {
                if (s.IndexOf("xlsx") > -1)
                {
                    DebugLog.WriteLine($"config file: {s}");
                    var group = ExcelHandle.ReadConfigFile(s);

                    if (group.Count != 0)
                    {
                        foreach (var grp in group)
                        {
                            if (grp.TotalTag == 0) continue;
                            id++;
                            WrapColletion.OpcDaSubcriptionWrapper.Add(new OpcDaSubcriptionWrapper(grp));
                            DebugLog.WriteLine($"Add new file config, Id: {id}");
                        }

                    }
                }
            }

            if (WrapColletion.OpcDaSubcriptionWrapper.Count == 0)
                ReadConfigurations(path);

            CheckSQlServerService();
        }

        public bool StartConnectingToOpcDaServer()
        {
            string serverName = string.Empty;

            if (WrapColletion.OpcDaSubcriptionWrapper.Count == 0)
                return false;

            foreach (var item in WrapColletion.OpcDaSubcriptionWrapper)
            {
                if (item.LoggingGroup.OPCServerName != string.Empty)
                {
                    serverName = item.LoggingGroup.OPCServerName;
                    break;
                }

                if (serverName != string.Empty)
                    break;
            }

            DebugLog.WriteLine($"OPCDA Server name: {serverName}");

            if (serverName != string.Empty)
            {
                var serverList = OpcDaServerDefinition.GetAvailableServers(Opc.Specification.COM_DA_30);
                var sv = serverList.Where(i => i.Name == serverName).FirstOrDefault();
                if (sv == null)
                    return false;

                OpcDaClient = new OpcDaClient(sv.Url);
                return OpcDaClient.ServerDefinition.ServerStatus();
            }

            return false;
        }

        public bool CheckSQlServerService()
        {
            Dictionary<string, List<OpcDaItem>> SortedTag = new Dictionary<string, List<OpcDaItem>>();
            SqlSetting sqlSetting = null;
            try
            {
                List<LoggingGroup> lGroup = new List<LoggingGroup>();

                foreach (var item in WrapColletion.OpcDaSubcriptionWrapper)
                {
                    lGroup.Add(item.LoggingGroup);
                }

                var group = lGroup.GroupBy(i => i.SqlSetting.Table);

                foreach (var grp in group)
                {
                    List<OpcDaItem> tags = new List<OpcDaItem>();

                    foreach (var g in grp.ToList())
                    {
                        if (sqlSetting == null)
                            sqlSetting = g.SqlSetting;

                        foreach (var tag in g.Items)
                        {
                            if (tags.Where(i => i.ItemName == tag.ItemName).Count() == 0) tags.Add(tag);
                        }
                    }

                    SortedTag.Add(grp.Key, tags);
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                DebugLog.WriteLine(ex.ToString());
            }

            if (sqlSetting == null) return false;

            SqlLog = new CustomSqlLog(sqlSetting);

            if (SqlLog.CreateDatabaseIfNotExist() != -1) DebugLog.WriteLine("Success");

            foreach (var tagGroup in SortedTag)
            {
                var columnDefinitions = MappingItem.GetColumnProperties(tagGroup.Value.ToArray());

                if (SqlLog.CreatTableIfNotExist(tagGroup.Key, columnDefinitions) != -1) DebugLog.WriteLine("Success");

                if (SqlLog.AddColumnIfNotExist(tagGroup.Key, columnDefinitions) != -1) DebugLog.WriteLine("Success");
            }

            return true;
        }

        public void RegistLoggingService()
        {
            if (StartConnectingToOpcDaServer())
                DebugLog.WriteLine("Connecting to OpcDa Server successfully");
            else
            {
                DebugLog.WriteLine("Can not connect to OpcDa server");
                return;
            }

            DebugLog.WriteLine($"Total group: {WrapColletion.OpcDaSubcriptionWrapper.Count}");

            foreach (var config in WrapColletion.OpcDaSubcriptionWrapper)
            {
                DebugLog.WriteLine($"Registing new file configuration group, Id: {config.Id}");
                config.RegistSubscription(OpcDaClient.ServerDefinition.OpcDaServer);

                System.Threading.Timer CircleReadingTimer = new System.Threading.Timer(CircleReadingTimerCallback, config, 0,
                    config.LoggingGroup.IntervalUpdateTime);
            }
        }

        private void CircleReadingTimerCallback(object state)
        {
            try
            {
                var wrapper = state as OpcDaSubcriptionWrapper;
                Console.WriteLine($"Logging : {wrapper.Id}");
                if (wrapper.WriteLogging() > 0)
                    Console.WriteLine("Success");
                else
                    Console.WriteLine("Failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class OpcDaWrapperCollection
    {
        public List<OpcDaSubcriptionWrapper> OpcDaSubcriptionWrapper { get; set; }
        public int Add(OpcDaSubcriptionWrapper wrapper)
        {
            if (wrapper != null)
                OpcDaSubcriptionWrapper.Add(wrapper);

            return OpcDaSubcriptionWrapper.Count;
        }

        public OpcDaWrapperCollection()
        {
            OpcDaSubcriptionWrapper = new List<OpcDaSubcriptionWrapper>();
        }
    }
}
