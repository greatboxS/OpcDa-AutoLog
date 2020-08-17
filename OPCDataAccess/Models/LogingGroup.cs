using OPCDataAccess.AppDefinition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OPCDataAccess.Models
{
    public class LoggingGroup
    {
        public int Id { get; set; } = 0;
        public string GroupName { get; set; } = "Group#";
        public string OPCServerName { get; set; } = string.Empty;
        public int IntervalUpdateTime { get; set; } = 1000;
        public SqlSetting SqlSetting { get; set; } = new SqlSetting();
        public IList<TagProperty> GroupTags { get; set; } = new List<TagProperty>();
        public GroupState State { get; set; } = GroupState.SETTING;
        public int TotalTag { get; set; } = 0;

        public LoggingGroup()
        {

        }  
        public LoggingGroup(string name)
        {
            GroupName = name;
        }
    }
}
