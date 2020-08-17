using DataLogger.SqlLogger;
using Microsoft.SqlServer.Server;
using OPCDataAccess.Controls;
using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da;

namespace DataLogger
{
    public class LoggerControl
    {
        private string ConnectionString { get; set; }
        private LoggerContext Context;
        private string DbTable = string.Empty;
        private string DataBase = string.Empty;
        private CustomSqlLog CustomSqlLog;
        public LoggerControl(SqlSetting sqlSetting)
        {
            ConnectionString = SqlCmdBuilder.ConnectionString(sqlSetting);
            CustomSqlLog = new CustomSqlLog(sqlSetting);
            DbTable = sqlSetting.Table;
            DataBase = sqlSetting.DataBase;
        }


        public int WriteLog(OpcDaItemValue[] result)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy, hh:mm:ss");
            if (result.Count() > 0)
                time = result[0].Timestamp.ToString("dd/MM/yyyy, hh:mm:ss");
            var coulmns = MappingItem.GetCoulmnValues(result);
            string cmd = SqlCmdBuilder.InsertValue(DataBase, DbTable, time, coulmns);
            return CustomSqlLog.SqlExcuteNonQuery(ConnectionString, cmd);
        }

        public bool LogingData(OpcDaItemValue[] result)
        {
            var logModels = MappingItem.GetLogModels(result);

            return LogingData(logModels);
        }

        public bool LogingData(IList<OpcDaItemValue> result)
        {
            var logModels = MappingItem.GetLogModels(result.ToArray());

            return LogingData(logModels);
        }

        public bool LogingData(IList<LogModel> logTags)
        {
            try
            {
                using (var Context = new LoggerContext(ConnectionString, DbTable))
                {
                    Context.LogModel.AddRange(logTags);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            { return false; }
            return true;
        }
    }
}
