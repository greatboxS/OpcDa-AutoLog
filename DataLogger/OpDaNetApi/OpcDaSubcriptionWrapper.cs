using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class OpcDaSubcriptionWrapper
    {
        public string Id { get; set; }
        public System.Threading.Timer Timer { get; set; }
        public LoggerControl LoggerControl { get; set; }
        public LoggingGroup LoggingGroup { get; set; }
        public Opc.Da.Subscription OpcDaSubscription { get; set; }
        public OpcDaSubcriptionWrapper(LoggingGroup loggingGroup)
        {
            LoggingGroup = loggingGroup;
            LoggerControl = new LoggerControl(loggingGroup.SqlSetting);
        }
        public OpcDaSubcriptionWrapper(Opc.Da.Server opcDaServer, LoggingGroup loggingGroup)
        {
            LoggingGroup = LoggingGroup;
            LoggerControl = new LoggerControl(loggingGroup.SqlSetting);
            RegistSubscription(opcDaServer);
        }
        public void RegistSubscription(Opc.Da.Server opcDaServer)
        {
            Opc.Da.SubscriptionState subscriptionState = new Opc.Da.SubscriptionState
            {
                Name = Guid.NewGuid().ToString(),
                Active = true,
                UpdateRate = LoggingGroup.IntervalUpdateTime,
                KeepAlive = System.Threading.Timeout.Infinite,
            };

            Id = subscriptionState.Name;

            OpcDaSubscription = (Opc.Da.Subscription)opcDaServer.CreateSubscription(subscriptionState);

            List<Opc.Da.Item> Items = new List<Opc.Da.Item>();
            foreach (var item in LoggingGroup.Items)
            {
                Items.Add(item);
            }
            OpcDaSubscription.AddItems(Items.ToArray());
        }
        public int WriteLogging(Opc.Da.ItemValueResult[] itemsResults)
        {
            return LoggerControl.WriteLog(itemsResults);
        }

        public int WriteLogging()
        {
            try
            {
                Console.WriteLine(OpcDaSubscription.Active);

                var result = OpcDaSubscription.Read(OpcDaSubscription.Items);
                return WriteLogging(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
