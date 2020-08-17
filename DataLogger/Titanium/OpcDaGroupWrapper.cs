using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da;

namespace DataLogger
{
    public class OpcDaGroupWrapper
    {
        public int Id { get; set; }
        public OpcDaGroup Group { get; set; }
        public System.Threading.Timer Timer { get; set; }
        public IList<OpcDaItemValue> OpcDaItemValues { get; set; }
        public LoggerControl LoggerControl { get; set; }
        public LoggingGroup LoggingGroup { get; set; }
        public OpcDaGroupWrapper(OpcDaGroup OpcDaGroup, LoggingGroup loggingGroup)
        {
            OpcDaItemValues = new List<OpcDaItemValue>();
            Group = OpcDaGroup;
            LoggingGroup = loggingGroup;
            LoggerControl = new LoggerControl(LoggingGroup.SqlSetting);
        }

        public int WriteLog()
        {
            return LoggerControl.WriteLog(OpcDaItemValues.ToArray());
        }

        public bool Loging()
        {
            return LoggerControl.LogingData(OpcDaItemValues);
        }
    }
}
