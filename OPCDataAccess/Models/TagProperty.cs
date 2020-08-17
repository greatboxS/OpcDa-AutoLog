using OPCDataAccess.AppDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Models
{
    public class TagProperty
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
        public string  Error { get; set; }
        public string Quantity { get; set; }
        public string UpdateTime { get; set; }
    }
}
