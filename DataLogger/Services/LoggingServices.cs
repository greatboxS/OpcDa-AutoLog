using DataLogger.SqlLogger;
using OPCDataAccess.Models;
using OPCDataAccess.OpcException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da;

namespace DataLogger.Services
{
    public class LoggingServices
    {
        private ExcelHandle ExcelHandle = new ExcelHandle();
        public GlobalProvider GlobalProvider = new GlobalProvider();
        private CustomSqlLog SqlLog;
        public void ReadConfigurations(string path)
        {
            string configPath = path;//$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/AutoLogging/";

            string[] files = Directory.GetFiles(configPath);

            int id = 0;
            int groupId = 0;
            foreach (string s in files)
            {
                if (s.IndexOf("xlsx") > -1)
                {
                    DebugLog.WriteLine($"config file: {s}");
                    var group = ExcelHandle.ReadConfigFile(s);

                    if (group.Count != 0)
                    {
                        List<LoggingGroup> addList = new List<LoggingGroup>();
                        foreach (var grp in group)
                        {
                            if (grp.TotalTag == 0) continue;

                            groupId++;
                            grp.Id = groupId;
                            addList.Add(grp);
                        }

                        id++;
                        DebugLog.WriteLine($"Add new file config, Id: {id}");
                        GlobalProvider.ConfigurationWrapper.Add(new ConfigurationWrapper(group, id));
                    }
                }
            }

            if (GlobalProvider.ConfigurationWrapper.Count == 0)
                ReadConfigurations(path);

            CheckSQlServerService();
        }

        public bool StartConnectingToOpcDaServer()
        {
            string serverName = string.Empty;

            if (GlobalProvider.ConfigurationWrapper.Count == 0)
                return false;
            //throw new OpcDaQueryException(OPCDataAccess.AppDefinition.OpcDaException.SERVER_HAS_NOT_CONNECTED_YET, "Can not get server name from configuration file");

            foreach (var item in GlobalProvider.ConfigurationWrapper)
            {
                if (item.LoggingGroup.Count != 0)
                {
                    foreach (var grp in item.LoggingGroup)
                    {
                        if (grp.OPCServerName != string.Empty)
                        {
                            serverName = grp.OPCServerName;
                            break;
                        }
                    }
                }

                if (serverName != string.Empty)
                    break;

            }

            DebugLog.WriteLine($"OPCDA Server name: {serverName}");

            if (serverName != string.Empty)
                return TitaniumOpcDaControl.StartOpcDaServer(serverName);

            return false;
            //throw new OpcDaQueryException(OPCDataAccess.AppDefinition.OpcDaException.ERROR, "Can not find out the OpcDA server name");
        }

        public bool CheckSQlServerService()
        {
            Dictionary<string, List<TagProperty>> SortedTag = new Dictionary<string, List<TagProperty>>();
            SqlSetting sqlSetting = null;
            try
            {
                List<LoggingGroup> lGroup = new List<LoggingGroup>();

                foreach (var item in GlobalProvider.ConfigurationWrapper)
                {
                    lGroup.AddRange(item.LoggingGroup);
                }

                var group = lGroup.GroupBy(i => i.SqlSetting.Table);

                foreach (var grp in group)
                {
                    List<TagProperty> tags = new List<TagProperty>();

                    foreach (var g in grp.ToList())
                    {
                        if (sqlSetting == null)
                            sqlSetting = g.SqlSetting;

                        foreach (var tag in g.GroupTags)
                        {
                            if (tags.Where(i => i.Name == tag.Name).Count() == 0) tags.Add(tag);
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
                var columnDefinitions = MappingItem.GetTableColumns(tagGroup.Value);

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

            DebugLog.WriteLine($"Total group: {GlobalProvider.ConfigurationWrapper.Count}");

            foreach (var config in GlobalProvider.ConfigurationWrapper)
            {
                foreach (var group in config.LoggingGroup)
                {
                    DebugLog.WriteLine($"Registing new file configuration group, Id: {group.Id}");
                    TitaniumOpcDaControl.CreateOpcDaGroup(group, true);
                }
            }
        }

        public void StartLogging(LogType logType = LogType.CIRCLE_TIME)
        {
            foreach (var item in TitaniumOpcDaControl.OpcDaWrappers)
            {
                item.Group.Destroyed += Group_Destroyed;

                if (logType == LogType.VALUE_CHANGED_EVENT)
                {
                    item.Group.ValuesChanged += Group_ValuesChanged;
                }
                else
                {
                    System.Threading.Timer CircleTimer = new System.Threading.Timer(CirleTimer_Elapsed, item, 0, item.LoggingGroup.IntervalUpdateTime);
                    item.Timer = CircleTimer;
                }
            }
        }

        private void CirleTimer_Elapsed(object state)
        {
            try
            {
                var wrapper = state as OpcDaGroupWrapper;

                HandleLogging(wrapper);
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine(ex.ToString());
            }
        }

        private void Group_Destroyed(object sender, EventArgs e)
        {
            DebugLog.WriteLine($"Group {(sender as OpcDaGroup).Name}");
        }

        private void Group_ValuesChanged(object sender, OpcDaItemValuesChangedEventArgs e)
        {
            var group = sender as OpcDaGroup;

            foreach (var item in TitaniumOpcDaControl.OpcDaWrappers)
            {
                if (group.Name == item.Group.Name)
                {
                    HandleLogging(item);
                }
            }
        }

        private void HandleLogging(OpcDaGroupWrapper wrapper)
        {
            try
            {
                if (TitaniumOpcDaControl.OpcDaServer.IsConnected)
                    DebugLog.WriteLine("Server is connected");
                else
                {
                    TitaniumOpcDaControl.StartOpcDaServer(TitaniumOpcDaControl.ServerName, true);
                    DebugLog.WriteLine("Server is disconnected");
                }

                DebugLog.WriteLine($"Connect: {wrapper.Group.Server.IsConnected}");
                DebugLog.WriteLine($"From Group Id {wrapper.LoggingGroup.Id}");

                var result = wrapper.Group.Read(wrapper.Group.Items, OpcDaDataSource.Cache);

                wrapper.OpcDaItemValues = new List<OpcDaItemValue>(result);
                if (wrapper.WriteLog() == -1)
                {
                    DebugLog.WriteLine("Log error");
                    DebugLog.WriteStatictis(true);
                }
                else
                {
                    DebugLog.WriteLine("Log success");
                    DebugLog.WriteStatictis(false);
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine(ex.ToString());
            }
        }

        public enum LogType
        {
            CIRCLE_TIME,
            VALUE_CHANGED_EVENT,
        }
    }

    public class GlobalProvider
    {
        public List<ConfigurationWrapper> ConfigurationWrapper { get; set; }

        public GlobalProvider()
        {
            ConfigurationWrapper = new List<ConfigurationWrapper>();
        }
    }

    public class ConfigurationWrapper
    {
        public ConfigurationWrapper(List<LoggingGroup> group)
        {
            if (group != null)
                LoggingGroup = new List<LoggingGroup>(group);
        }

        public ConfigurationWrapper(IList<LoggingGroup> group, int id)
        {
            if (group != null)
                LoggingGroup = new List<LoggingGroup>(group);

            Id = id;
        }

        public int Id { get; set; }

        public List<LoggingGroup> LoggingGroup = new List<LoggingGroup>();
    }
}
