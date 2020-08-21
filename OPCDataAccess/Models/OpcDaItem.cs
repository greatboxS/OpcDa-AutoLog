using OPCDataAccess.AppDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Models
{
    public class OpcDaItem :Opc.Da.Item
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
