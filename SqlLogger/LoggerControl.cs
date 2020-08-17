using OPCDataAccess.Controls;
using OPCDataAccess.Models.Implements;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLogger
{
    public class LoggerControl
    {
        public static string ConnectionStringBuider(SqlSetting sqlSetting)
        {
            //Data Source=DELL-PC\SQLEXPRESS;Initial Catalog=AssetWorX;Integrated Security=True;
            if (sqlSetting.UseLogin)
                return string.Format("Data Source = {0};Initial Catalog={1};User Id={2};Password={3};Integrated Security=True;",
                    sqlSetting.ServerName, sqlSetting.DataBase, sqlSetting.UserName, sqlSetting.Password);
            else
                return string.Format("Data Source = {0};Initial Catalog={1};Integrated Security=True;",
                sqlSetting.ServerName, sqlSetting.DataBase);
        }

        private string ConnectionString { get; set; }
        private LoggerContext Context;
        private string DbTable = string.Empty;
        public LoggerControl(SqlSetting sqlSetting)
        {
            ConnectionString = ConnectionStringBuider(sqlSetting);
            DbTable = sqlSetting.Table;
        }

        public bool LogingData(IList<LogModel> logTags)
        {
            try
            {
                using (Context = new LoggerContext(ConnectionString))
                {
                    Context.LogModel.AddRange(logTags);
                    Context.SaveChangesAsync();
                }
            }
            catch { return false; }
            return true;
        }
    }
}
