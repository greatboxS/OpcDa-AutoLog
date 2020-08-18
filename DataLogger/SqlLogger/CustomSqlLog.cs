using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.SqlLogger
{
    public class CustomSqlLog
    {
        public SqlSetting SqlSetting { get; set; }
        public SqlConnection SqlConnection { get; set; }
        public SqlDataAdapter SqlDataAdapter { get; set; }
        public SqlCommand SqlCommand { get; set; }
        public string ConnectionString { get; set; }
        public CustomSqlLog(SqlSetting sqlSetting)
        {
            SqlSetting = sqlSetting;
            ConnectionString = SqlCmdBuilder.ConnectionString(sqlSetting);
        }

        public int SqlExcuteNonQuery(string connectionString, string cmd)
        {
            try
            {
                SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                using (var db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    db.Open();
                    Console.WriteLine($"Command: {cmd}");
                    SqlCommand = new SqlCommand(cmd, db);
                    return SqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public int CreateDatabaseIfNotExist()
        {
            string connectionString = string.Format($"Data Source={SqlSetting.ServerName};Integrated Security=True;");
            string cmd = SqlCmdBuilder.CreateDb(SqlSetting.DataBase);
            return SqlExcuteNonQuery(connectionString, cmd);
        }

        public int CreatTableIfNotExist(string table, IList<ColumnProperty> columnProperties)
        {
            string connectionString = SqlCmdBuilder.ConnectionString(SqlSetting);
            string cmd = SqlCmdBuilder.CreatTable(table, columnProperties);
            return SqlExcuteNonQuery(connectionString, cmd);
        }

        public int AddColumnIfNotExist(string table, IList<ColumnProperty> columns)
        {
            string connectionString = SqlCmdBuilder.ConnectionString(SqlSetting);
            foreach (var c in columns)
            {
                string cmd = SqlCmdBuilder.AddColumnIfNotExistCmd(table, c);
                SqlExcuteNonQuery(connectionString, cmd);
            }
            return 1;
        }
    }
}
