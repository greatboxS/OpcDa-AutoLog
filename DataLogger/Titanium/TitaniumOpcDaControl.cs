using DataLogger.SqlLogger;
using OPCDataAccess.AppDefinition;
using OPCDataAccess.Models;
using OPCDataAccess.OpcException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using TitaniumAS;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;
using TitaniumAS.Opc.Client.Da.Wrappers;

namespace DataLogger
{
    public class TitaniumOpcDaControl
    {
        public static OpcDaServer OpcDaServer;
        public static string ServerName = string.Empty;
        public static IList<OpcDaGroupWrapper> OpcDaWrappers = new List<OpcDaGroupWrapper>();

        public static bool StartOpcDaServer(string server, bool reconnect = false)
        {
            //OpcDaWrappers = new List<OpcDaGroupWrapper>();
            try
            {
                if (server != ServerName)
                    ServerName = server;
                if (!reconnect)
                    OpcDaServer = new OpcDaServer(UrlBuilder.Build(server, "localhost"));
                OpcDaServer.Connect();
            }
            catch (Exception ex)
            {
                return false;
            }
            return OpcDaServer.IsConnected;
        }

        public static bool OpcDaClientConnected()
        {
            if (OpcDaServer == null)
            {
                return false;
                //throw new OpcDaQueryException(OpcDaException.OPCDA_HAS_NOT_CREATED, "OpcDa has not created yet");
            }

            if (OpcDaServer.IsConnected)
                return true;
            else
            {
                return StartOpcDaServer(ServerName);
            }
        }

        /// <summary>
        /// Check if group is existing, then remove and add a new one.
        /// </summary>
        /// <param name="LoggingGroup"></param>
        /// <param name="active">true - Start logging data of group</param>
        /// <returns></returns>
        public static OpcDaGroup CreateOpcDaGroup(LoggingGroup LoggingGroup, bool active = false, int updateTime = 1000)
        {
            if (!OpcDaClientConnected())
                return null;

            var group = OpcDaServer.AddGroup(Guid.NewGuid().ToString());

            group.IsActive = true;
            group.KeepAlive = Timeout.InfiniteTimeSpan;
            group.UpdateRate = TimeSpan.FromMilliseconds(updateTime);

            Console.WriteLine("CreateOpcDaGroup: Add group successfuly");

            var GroupItems = MappingItem.GetOpcDaItemDefinition(LoggingGroup);

            var result = group.AddItems(GroupItems);

            if (active)
            {
                OpcDaGroupWrapper groupWrapper = new OpcDaGroupWrapper(group, LoggingGroup);
                OpcDaWrappers.Add(groupWrapper);
                DebugLog.WriteLine($"Group {group.Name} is activated");
            }
            return group;
        }

        private static OpcDaGroup ReNewGroup(object obj)
        {
            var group = (obj as OpcDaGroup);

            var gsearch = OpcDaServer.Groups.Where(i => i.Name == group.Name);

            Console.WriteLine("CreateOpcDaGroup: Start registing");

            if (gsearch.Count() > 0)
            {
                Console.WriteLine("CreateOpcDaGroup: Group name is existing");

                var removeGroup = gsearch.ToList();
                foreach (var rm in removeGroup)
                {
                    OpcDaServer.RemoveGroup(rm);
                    Console.WriteLine("CreateOpcDaGroup: Generating new Group name");
                }
            }

            List<OpcDaItemDefinition> items = new List<OpcDaItemDefinition>();
            foreach (var item in group.Items)
            {
                items.Add(new OpcDaItemDefinition { ItemId = item.ItemId, IsActive=true });
            }

            var newgroup = OpcDaServer.AddGroup(Guid.NewGuid().ToString());

            newgroup.AddItems(items);

            return newgroup;
        }

        public static OpcDaGroup CreateOpcDaGroup(OpcDaServer server, IList<OpcDaBrowseElement> elements)
        {
            if (!OpcDaClientConnected())
                return null;

            var group = server.AddGroup(Guid.NewGuid().ToString());

            group.IsActive = true;

            group.UpdateRate = TimeSpan.FromMilliseconds(100);

            var GroupItems = MappingItem.GetOpcDaItemDefinition(elements);

            group.AddItems(GroupItems);

            return group;
        }

        public static IList<OpcServerDescription> GetOpcDaServer()
        {
            List<string> servers = new List<string>();
            var enumerator = new OpcServerEnumeratorAuto();
            var serverDescriptions = enumerator.Enumerate("localhost", OpcServerCategory.OpcDaServer30);
            return new List<OpcServerDescription>(serverDescriptions);
        }

        public static IList<OpcDaBrowseElement> GetElements(string opcProgId)
        {
            if (!OpcDaClientConnected())
                return null;

            var uri = UrlBuilder.Build(opcProgId);
            OpcDaServer server = new OpcDaServer(uri);
            server.Connect();
            var opcBrowser = new OpcDaBrowser1(server);

            OpcDaBrowseElement[] elements = opcBrowser.GetElements("", null, new OpcDaPropertiesQuery(true, OpcDaItemPropertyIds.OPC_PROP_VALUE));

            server.Disconnect();

            return elements.ToList();
        }

        /// <summary>
        /// Get all tags of current server
        /// </summary>
        /// <param name="opcProgId">OPC Da Server name</param>
        /// <returns></returns>
        public static IList<TagProperty> GetServerTags(string opcProgId)
        {
            var uri = UrlBuilder.Build(opcProgId);
            OpcDaServer = new OpcDaServer(uri);
            OpcDaServer.Connect();
            var opcBrowser = new OpcDaBrowser1(OpcDaServer);

            var elements = opcBrowser.GetElements("");

            List<OpcDaBrowseElement> tags = new List<OpcDaBrowseElement>();

            foreach (var element in elements)
            {
                GetTag(opcBrowser, element, tags);
            }    

            var group = CreateOpcDaGroup(OpcDaServer, tags);

            if (group == null) return null;

            var result = ValidatingTag(group, tags);

            OpcDaServer.RemoveGroup(group);

            OpcDaServer.Disconnect();

            return result;
        }

        private static void GetTag(OpcDaBrowser1 browser, OpcDaBrowseElement element, List<OpcDaBrowseElement> items)
        {
            try
            {
                if(element.HasChildren)
                {
                    var opcBrowser = new OpcDaBrowser1(OpcDaServer);
                    var elements  = browser.GetElements(element.ItemId);

                    foreach (var item in elements)
                    {
                        GetTag(browser, item, items);
                    }
                }
                else
                {
                    if (element.ItemId == null) return;

                    if (element.ItemId.IndexOf("Receive") > -1 ||
                        element.ItemId.IndexOf("Connected") > -1 ||
                        element.ItemId.IndexOf("Transmit") > -1)
                        return;

                    OpcDaBrowser3 properties = new OpcDaBrowser3(OpcDaServer);

                    if (properties == null) throw new OpcDaQueryException(OpcDaException.GET_CLIENT_GROUP_ITEM_ERROR, "");

                    var prop = properties.GetProperties(new string[] { element.ItemId }, new OpcDaPropertiesQuery(false));

                    element.ItemProperties.Properties = prop[0].Properties;

                    items.Add(element);
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
        }

        public static IList<TagProperty> ValidatingTag(LoggingGroup LoggingGroup)
        {
            var opcDAGroup = CreateOpcDaGroup(LoggingGroup);

            var GroupItems = MappingItem.GetOpcDaItemDefinition(LoggingGroup);

            var result = opcDAGroup.ValidateItems(GroupItems);

            var value = opcDAGroup.Read(opcDAGroup.Items, OpcDaDataSource.Cache);

            return MappingItem.GetTags(value);
        }

        public static IList<TagProperty> ValidatingTag(OpcDaGroup group, IList<OpcDaBrowseElement> element)
        {
            var GroupItems = MappingItem.GetOpcDaItemDefinition(element);

            var result = group.ValidateItems(GroupItems);

            var value = group.Read(group.Items, OpcDaDataSource.Cache);

            return MappingItem.GetTags(value);
        }
    }
}
