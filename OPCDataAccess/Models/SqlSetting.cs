using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Models
{
    public class SqlSetting
    {
        public string ServerName { get; set; } = @"DELL-PC\SQLEXPRESS";
        public string DataBase { get; set; } = "OpcDaLogger";
        public string Table { get; set; } = "TEST_TABLE";
        public bool UseLogin { get; set; } = false;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
