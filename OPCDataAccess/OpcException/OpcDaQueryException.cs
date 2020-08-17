using OPCDataAccess.AppDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.OpcException
{
    public class OpcDaQueryException : Exception
    {
        public OpcDaQueryException(OpcDaException expCode, string exp)
        {
            ExceptionString = exp;
        }
        public string ExceptionString { get; set; }
        public OpcDaException ExceptionCode { get; set; }
    }
}
