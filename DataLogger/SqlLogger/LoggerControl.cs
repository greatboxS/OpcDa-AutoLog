using DataLogger.SqlLogger;
using Microsoft.SqlServer.Server;
using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class LoggerControl
    {
        private string ConnectionString { get; set; }
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

        public int WriteLog(Opc.Da.ItemValueResult[] result)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy, hh:mm:ss");
            var coulmns = MappingItem.GetColumnResults(result);
            string cmd = SqlCmdBuilder.InsertValue(DataBase, DbTable, time, coulmns);
            return CustomSqlLog.SqlExcuteNonQuery(ConnectionString, cmd);
        }
    }
}
