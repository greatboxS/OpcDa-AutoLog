using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string  TagName { get; set; }
        public string LogTime { get; set; }
        public string Value { get; set; }
        public string Quantity { get; set; }
        public string DataType { get; set; }
    }
}
