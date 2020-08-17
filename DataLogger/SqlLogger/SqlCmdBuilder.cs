using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class SqlCmdBuilder
    {

        public static string ConnectionString(SqlSetting sqlSetting)
        {
            //Data Source=DELL-PC\SQLEXPRESS;Initial Catalog=AssetWorX;Integrated Security=True;
            if (sqlSetting.UseLogin)
                return string.Format("Data Source = {0};Initial Catalog={1};User Id={2};Password={3};Integrated Security=True;",
                    sqlSetting.ServerName, sqlSetting.DataBase, sqlSetting.UserName, sqlSetting.Password);
            else
                return string.Format("Data Source = {0};Initial Catalog={1};Integrated Security=True;",
                sqlSetting.ServerName, sqlSetting.DataBase);
        }

        public static string AddColumn(string table, string column, string type)
        {
            string cmd = string.Empty;
            cmd = string.Format("ALTER TABLE {0} ADD {1} {2}", table, column, type);
            return cmd;
        }

        public static string InsertValue(string db, string table, string time, IList<ColumnProperty> columns)
        {
            string cmd = $"USE {db}; INSERT INTO {table}";

            string col = "([TimeStamp], ";
            string val = $"VALUES( '{time}', ";

            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Value != null)
                {
                    if (columns[i].Value != "NULL")
                    {
                        col += string.Format("[{0}],", columns[i].ColumnName);
                        val += string.Format("'{0}',", columns[i].Value);
                    }
                }
            }
            int id = col.LastIndexOf(',');
            col = col.Remove(id, 1);
            id = val.LastIndexOf(',');
            val = val.Remove(id, 1);
            col += ")";
            val += ")";

            cmd += string.Format("{0} {1}", col, val);

            return cmd;
        }

        public static string CreatTable(string table, IList<ColumnProperty> columns)
        {
            string cmd = string.Empty;

            string cmd_ext = "([Id] [int] IDENTITY(1,1) NOT NULL, TimeStamp [nvarchar](max) NULL, ";
            for (int i = 0; i < columns.Count - 1; i++)
            {
                cmd_ext += string.Format("[{0}] {1}, ", columns[i].ColumnName, columns[i].Type);
            }

            cmd_ext += string.Format("[{0}] {1})", columns[columns.Count - 1].ColumnName, columns[columns.Count - 1].Type);

            cmd = $"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}') BEGIN CREATE TABLE [dbo].[{table}] {cmd_ext} END";
            return cmd;
        }

        public static string AddColumnIfNotExistCmd(string table, ColumnProperty column)
        {
            string cmd = string.Format($"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}' AND COLUMN_NAME = '{column.ColumnName}') " +
                $"BEGIN " +
                $"ALTER TABLE {table} ADD [{column.ColumnName}] {column.Type}" +
                $" END;");
            return cmd;
        }

        public static string CreateDb(string db)
        {
            string cmd = string.Empty;

            cmd = string.Format("USE master; IF DB_ID(N'{0}') IS NULL CREATE DATABASE {1}", db, db);
            return cmd;
        }


    }
}
