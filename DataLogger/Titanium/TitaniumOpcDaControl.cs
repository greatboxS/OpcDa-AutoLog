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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private static string ServerName = string.Empty;
        public static IList<OpcDaGroupWrapper> OpcDaWrappers = new List<OpcDaGroupWrapper>();
        private static List<OpcDaBrowseElement> Items = new List<OpcDaBrowseElement>();

        public static bool StartOpcDaServer(string server)
        {
            OpcDaWrappers = new List<OpcDaGroupWrapper>();
            try
            {
                if (server != ServerName)
                    ServerName = server;
                OpcDaServer = new OpcDaServer(UrlBuilder.Build(server));
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
            //throw new OpcDaQueryException(OpcDaException.SERVER_HAS_NOT_CONNECTED_YET, "OpcDa has not created yet");

            var gsearch = OpcDaServer.Groups.Where(i => i.Name == LoggingGroup.GroupName);


            Console.WriteLine("CreateOpcDaGroup: Start registing");

            if (gsearch.Count() > 0)
            {
                Console.WriteLine("CreateOpcDaGroup: Group name is existing");

                var removeGroup = gsearch.ToList();
                foreach (var rm in removeGroup)
                {
                    LoggingGroup.GroupName = Guid.NewGuid().ToString();
                    Console.WriteLine("CreateOpcDaGroup: Generating new Group name");
                }
            }

            var group = OpcDaServer.AddGroup(LoggingGroup.GroupName);

            group.IsActive = true;

            group.UpdateRate = TimeSpan.FromMilliseconds(updateTime);

            Console.WriteLine("CreateOpcDaGroup: Add group successfuly");

            var GroupItems = MappingItem.GetOpcDaItemDefinition(LoggingGroup);

            group.AddItems(GroupItems);

            if (active)
            {
                OpcDaGroupWrapper groupWrapper = new OpcDaGroupWrapper(group, LoggingGroup);

                groupWrapper.Id = LoggingGroup.Id;

                Timer Timer = new Timer(GroupTimerCallback, groupWrapper, 0, LoggingGroup.IntervalUpdateTime);

                groupWrapper.Timer = Timer;

                OpcDaWrappers.Add(groupWrapper);

                var columnDefinitions = MappingItem.GetTableColumns(LoggingGroup.GroupTags);

                CustomSqlLog SqlLog = new CustomSqlLog(LoggingGroup.SqlSetting);

                if (SqlLog.AddColumnIfNotExist(LoggingGroup.SqlSetting.Table, columnDefinitions) != -1) Console.WriteLine("Success");

                Console.WriteLine("CreateOpcDaGroup: Group is active now");
            }
            return group;
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

        private static void GroupTimerCallback(object state)
        {
            try
            {
                var wrapper = state as OpcDaGroupWrapper;
                Console.WriteLine($"From Group Id {wrapper.Id}");
                wrapper.OpcDaItemValues = new List<OpcDaItemValue>(wrapper.Group.Read(wrapper.Group.Items, OpcDaDataSource.Cache));
                if (wrapper.WriteLog() == -1)
                    Console.WriteLine("Log error");
                else
                    Console.WriteLine("Log success");
            }
            catch { }
        }

        public static IList<OpcServerDescription> GetOpcDaServer()
        {
            List<string> servers = new List<string>();
            var enumerator = new OpcServerEnumeratorAuto();
            var serverDescriptions = enumerator.Enumerate("", OpcServerCategory.OpcDaServers);
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

            List<TagProperty> TagProps = new List<TagProperty>();

            var list = GetElements(OpcDaServer, opcBrowser);

            var group = CreateOpcDaGroup(OpcDaServer, list);

            if (group == null) return null;

            var result = ValidatingTag(group, list);

            OpcDaServer.RemoveGroup(group);

            OpcDaServer.Disconnect();

            return result;
        }

        /// <summary>
        /// Get all item properties
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="opcBrowser"></param>
        /// <returns></returns>
        public static IList<OpcDaBrowseElement> GetElements(OpcDaServer server, OpcDaBrowser1 opcBrowser, string elementId = "")
        {
            Items = new List<OpcDaBrowseElement>();
            try
            {
                var branches = opcBrowser.GetElements(elementId); // Expand root

                if (branches == null)
                    return Items;

                ElementBrowser(server, opcBrowser, branches, elementId);
            }
            catch { }

            return Items;
        }

        /// <summary>
        /// Get item properties in loop
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="opcBrowser"></param>
        /// <param name="elements"></param>
        private static void ElementBrowser(OpcDaServer server, OpcDaBrowser1 opcBrowser, OpcDaBrowseElement[] elements, string elementId = "")
        {
            foreach (var opcDaBrowseElement in elements)
            {
                if (opcDaBrowseElement.HasChildren)
                {
                    try
                    {
                        var branchs = opcBrowser.GetElements(opcDaBrowseElement.ItemId); // Expand root

                        ElementBrowser(server, opcBrowser, branchs, opcDaBrowseElement.ItemId);
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (opcDaBrowseElement.IsItem)
                {
                    try
                    {

                        if (opcDaBrowseElement.ItemId == null) continue;

                        if (opcDaBrowseElement.ItemId.IndexOf("Receive") > -1 ||
                            opcDaBrowseElement.ItemId.IndexOf("Connected") > -1 ||
                            opcDaBrowseElement.ItemId.IndexOf("Transmit") > -1)
                            continue;

                        OpcDaBrowser3 properties = new OpcDaBrowser3(server);

                        if (properties == null) throw new OpcDaQueryException(OpcDaException.GET_CLIENT_GROUP_ITEM_ERROR, "");

                        var prop = properties.GetProperties(new string[] { opcDaBrowseElement.ItemId }, new OpcDaPropertiesQuery(false));

                        opcDaBrowseElement.ItemProperties.Properties = prop[0].Properties;

                        Items.Add(opcDaBrowseElement);
                    }
                    catch (Exception e)
                    {
                        throw new OpcDaQueryException(OpcDaException.GET_CLIENT_GROUP_ITEM_ERROR, e.Message);
                    }
                }
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
