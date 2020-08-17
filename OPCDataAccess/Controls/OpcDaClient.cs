using Opc;
using Opc.Da;
using OPCDataAccess.AppDefinition;
using OPCDataAccess.OpcException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.Controls
{
    public class OpcDaClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="OpcDaQueryException"></exception>
        /// <returns></returns>
        public static IList<string> GetServer(Specification ver)
        {
            var serverList = new List<string>();
            try
            {
                IDiscovery discovery = new OpcCom.ServerEnumerator();
                var localservers =
                    discovery.GetAvailableServers(Specification.COM_DA_20)
                    .Select(s => s.Url)
                    .ToArray();

                if (localservers.Count() <= 0)
                {
                    throw new OpcDaQueryException(OpcDaException.GET_SERVER_LIST_ERROR,
                        string.Format("Your computer has no server version: {0}", ver.ToString()));
                }

                foreach (var sv in localservers)
                {
                    serverList.Add(sv.Path);
                }

                return serverList;
            }
            catch (Exception e)
            {
                throw new OpcDaQueryException(OpcDaException.GET_SERVER_LIST_ERROR, e.Message);
            }
        }

        public static IList<string> GetOpcDaServer()
        {
            var serverList = new List<string>();
            string exception = string.Empty;
            try
            {
                serverList.AddRange(GetServer(Specification.COM_DA_10));
            }
            catch (OpcDaQueryException e)
            {
                exception += string.Format("DA_10 - {0}\r\n", e.ExceptionString);
            }

            try
            {
                serverList.AddRange(GetServer(Specification.COM_DA_20));
            }
            catch (OpcDaQueryException e)
            {
                exception += string.Format("DA_20 - {0}\r\n", e.ExceptionString);
            }

            try
            {
                serverList.AddRange(GetServer(Specification.COM_DA_30));
            }
            catch (OpcDaQueryException e)
            {
                exception += string.Format("DA_30 - {0}\r\n", e.ExceptionString);
            }

            if (serverList.Count == 0)
                throw new OpcDaQueryException(OpcDaException.GET_SERVER_LIST_ERROR, exception);

            return serverList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="var"></param>
        /// <param name="server"></param>
        /// <exception cref="OpcDaQueryException"></exception>
        /// <returns></returns>
        public static IList<string> GetGroupTag(System.Runtime.InteropServices.VarEnum var, Opc.Da.Server server)
        {
            if (!server.IsConnected)
            {
                throw new OpcDaQueryException(OpcDaException.GET_CLIENT_GROUP_ITEM_ERROR, "Server is not connected");
            }

            // Create group
            Opc.Da.Subscription group;
            Opc.Da.SubscriptionState groupState = new Opc.Da.SubscriptionState();
            groupState.Name = "QueryGroup";
            groupState.Active = true;
            groupState.UpdateRate = 200;

            // Short circuit if group already exists
            Opc.Da.SubscriptionCollection existingCollection = server.Subscriptions;

            bool isCreated = false;
            if (existingCollection.Count > 0)
            {
                for (int i = 0; i < existingCollection.Count; i++)
                {
                    if (existingCollection[i].Name == "QueryGroup")
                    {
                        group = existingCollection[i];
                        isCreated = true;
                        break;
                    }
                }
            }

            if (!isCreated)
                group = (Opc.Da.Subscription)server.CreateSubscription(groupState);


            

            //    Item[] itemss = server.getite


            //    // Create list of items to monitor
            //    Opc.Da.Item[] opcItems = new Opc.Da.Item[1];
            //    int j = 0;
            //    foreach (string tag in tagList)
            //    {
            //        opcItems[j] = new Item();
            //        opcItems[j].ItemName = tag;
            //        j++;
            //    }

            //    // Attach items and event to group
            //    group.AddItems(opcItems);
            //    //group.DataChanged += new Opc.Da.DataChangedEventHandler(OPCSubscription_DataChanged);
            //    group.DataChanged += new Opc.Da.DataChangedEventHandler(onDataChange);
            //}

            return null;
        }
    }
}
