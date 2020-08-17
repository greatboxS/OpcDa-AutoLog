using Microsoft.SqlServer.Server;
using Opc.Da;
using OpcRcw.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace OPCDataAccess.Models
{
    public class MappingItem
    {
        public static IList<OpcDaItemDefinition> GetOpcDaItemDefinition(LoggingGroup logingGroup)
        {
            IList<OpcDaItemDefinition> items = new List<OpcDaItemDefinition>();

            foreach (var itemProp in logingGroup.GroupTags)
            {
                items.Add(new OpcDaItemDefinition
                {
                    ItemId = itemProp.Name,
                    IsActive = true,
                    RequestedDataType = typeof(int),
                });
            }

            return items;
        }

        public static IList<OpcDaItemDefinition> GetOpcDaItemDefinition(IList<OpcDaBrowseElement> elements)
        {
            IList<OpcDaItemDefinition> items = new List<OpcDaItemDefinition>();

            foreach (var itemProp in elements)
            {
                items.Add(new OpcDaItemDefinition
                {
                    ItemId = itemProp.ItemId,
                    IsActive = true
                });
            }

            return items;
        }

        public static IList<TagProperty> GetTags(ICollection<OpcDaBrowseElement> elements)
        {
            IList<TagProperty> items = new List<TagProperty>();

            foreach (var item in elements)
            {
                items.Add(new TagProperty
                {
                    Name = item.ItemId,
                });
            }

            return items;
        }

        public static IList<LogModel> GetLogModels(OpcDaItemValue[] result)
        {
            List<LogModel> items = new List<LogModel>();
            if (result == null)
                return items;

            foreach (var element in result)
            {
                items.Add(new LogModel
                {
                    TagName = element.Item.ItemId,
                    LogTime = element.Timestamp.ToString("HH:mm:ss - dd/MM/yyyy"),
                    Value = element.Value != null ? element.Value.ToString() : "NULL",
                    Quantity = element.Value != null ? element.Quality.ToString() : string.Empty,
                    DataType = element.Item.CanonicalDataType != null ? element.Item.CanonicalDataType.Name : string.Empty,
                }); ;
            }
            return items;
        }

        public static IList<TagProperty> GetTags(OpcDaItemValue[] result)
        {
            List<TagProperty> items = new List<TagProperty>();
            if (result == null)
                return items;

            foreach (var element in result)
            {
                items.Add(new TagProperty
                {
                    Name = element.Item.ItemId,
                    UpdateTime = element.Timestamp.ToString("HH:mm:ss - dd/MM/yyyy"),
                    Value = element.Value != null ? element.Value.ToString() : "NULL",
                    Quantity = element.Quality.Master.ToString(),
                    TypeName = element.Item.CanonicalDataType != null ? element.Item.CanonicalDataType.Name : string.Empty,
                    Error = element.Error.ToString(),
                });
            }
            return items;
        }

        public static IList<ColumnProperty> GetCoulmnValues(OpcDaItemValue[] result)
        {
            List<ColumnProperty> columns = new List<ColumnProperty>();

            foreach (var element in result)
            {
                if (element.Item.CanonicalDataType != null)
                {
                    columns.Add(new ColumnProperty(element.Item.ItemId, element.Item.CanonicalDataType, (element.Value != null ? element.Value.ToString() : "NULL")));
                }
            }
            return columns;
        }

        public static IList<ColumnProperty> GetCoulmnValues(LoggingGroup result)
        {
            List<ColumnProperty> columns = new List<ColumnProperty>();

            foreach (var tag in result.GroupTags)
            {
                if (tag.TypeName != string.Empty)
                {
                    columns.Add(new ColumnProperty(tag.Name, tag.TypeName, tag.Value));
                }
            }
            return columns;
        }

        public static IList<ColumnProperty> GetTableColumns(IList<TagProperty> tags)
        {
            List<ColumnProperty> columns = new List<ColumnProperty>();

            foreach (var item in tags)
            {
                columns.Add(new ColumnProperty(item.Name, item.TypeName, null));
            }

            return columns;
        }
    }

    public class ColumnProperty
    {
        public string ColumnName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public ColumnProperty(string name, Type type)
        {
            Type = GetType(type.Name);
            ColumnName = name;
        }

        public ColumnProperty(string name, Type type, string value)
        {
            Value = value;
            Type = GetType(type.Name);
            ColumnName = name;
        }

        public ColumnProperty(string name, string type, string value)
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
