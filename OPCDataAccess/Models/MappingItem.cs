using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Models
{
    public class MappingItem
    {
        public static IList<SqlTableColumn> GetColumnProperties(OpcDaItem[] result)
        {
            List<SqlTableColumn> columns = new List<SqlTableColumn>();

            foreach (var element in result)
            {
                if (element.ItemName != null)
                {
                    columns.Add(new SqlTableColumn(element.ItemName, element.TypeName));
                }
            }
            return columns;
        }

        public static IList<SqlTableColumn> GetColumnResults(Opc.Da.ItemValueResult[] itemResults)
        {
            List<SqlTableColumn> columns = new List<SqlTableColumn>();

            foreach (var item in itemResults)
            {
                columns.Add(new SqlTableColumn(item.ItemName, "null", item.Value.ToString()));
            }

            return columns;
        }
    }

    public class SqlTableColumn
    {
        public string ColumnName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public SqlTableColumn(string name, string type)
        {
            ColumnName = name;
            Type = GetType(type);
        }

        public SqlTableColumn(string name, string type, string value)
        {
            Value = value;
            Type = GetType(type);
            ColumnName = name;
        }

        private string GetType(string type)
        {
            string sqlType = string.Empty;
            switch (type)
            {
                case "Int32":
                    sqlType = "[int] NULL";
                    break;

                case "UInt16":
                    sqlType = "[int] NULL";
                    break;

                case "Boolean":
                    sqlType = "[bit] NULL";
                    break;

                case "Single": //float
                    sqlType = "[float] NULL";
                    break;

                case "Double":
                    sqlType = "[float] NULL";
                    break;

                case "String":
                    sqlType = "[nvarchar](max) NULL";
                    break;

                case "DateTime":
                    sqlType = "[datetime] NULL";
                    break;

                default:
                    sqlType = "[nvarchar](max) NULL";
                    break;
            }

            return sqlType;
        }
    }
}
