using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpcRcw;

namespace DataLogger
{
    public class OpcDaClient
    {
        public OpcDaServerDefinition ServerDefinition { get; set; }

        public OpcDaClient(Opc.URL serverUrl)
        {
            ServerDefinition = new OpcDaServerDefinition(serverUrl);
        }
        public OpcDaClient(OpcDaServerDefinition serverDefinition)
        {
            ServerDefinition = serverDefinition;
        }

        public Opc.Da.ItemValueResult[] Read(Opc.Da.Item[] items, int updateTime)
        {
            Opc.Da.Subscription subscriptionGroup;
            Opc.Da.SubscriptionState subscriptionState = new Opc.Da.SubscriptionState
            {
                Name = Guid.NewGuid().ToString(),
                Active = true,
                UpdateRate = updateTime,
                KeepAlive=System.Threading.Timeout.Infinite,
                Locale = "en-US",
            };

            subscriptionGroup = (Opc.Da.Subscription)ServerDefinition.OpcDaServer.CreateSubscription(subscriptionState);

            subscriptionGroup.AddItems(items);
            return subscriptionGroup.Read(subscriptionGroup.Items);
        }

        public Opc.IdentifiedResult[] Write(Opc.Da.Subscription subscriptionGroup, Opc.Da.ItemValue[] items)
        {
            return subscriptionGroup.Write(items);
        }
    }
}
