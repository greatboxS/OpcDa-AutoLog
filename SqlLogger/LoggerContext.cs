using OPCDataAccess.Models.Implements;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLogger
{
    public class LoggerContext: DbContext
    {
        public LoggerContext(string connectionString) : base(connectionString) { }

        public DbSet<LogModel> LogModel { get; set; }
    }
}
