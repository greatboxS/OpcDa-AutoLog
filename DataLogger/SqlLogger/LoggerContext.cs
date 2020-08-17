using OPCDataAccess.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class LoggerContext : DbContext
    {
        private string LogTable = "LogTable";
        public LoggerContext(string connectionString, string dbTable) : base(connectionString) { LogTable = dbTable; }

        public DbSet<LogModel> LogModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogModel>()
                .ToTable(LogTable);
        }
    }
}
