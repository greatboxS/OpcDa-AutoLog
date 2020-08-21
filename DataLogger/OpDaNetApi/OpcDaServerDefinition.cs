using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class OpcDaServerDefinition
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public Opc.URL Url { get; set; }
        public Opc.Da.Server OpcDaServer { get; set; }
        public OpcDeviceDefinition OpcDevice { get; set; }
        public List<OpcDaItem> OpcDaItems { get; set; }
        public OpcDaServerDefinition(Opc.URL serverUrl)
        {
            Url = serverUrl;
            OpcDevice = new OpcDeviceDefinition();
            OpcDaItems = new List<OpcDaItem>();
            OpcDaConnect();
        }
        public static Opc.Server[] GetAvailableServers(Opc.Specification serverType)
        {
            Opc.IDiscovery discovery = new OpcCom.ServerEnumerator();

            var localservers = discovery.GetAvailableServers(serverType);

            return localservers;
        }
        public OpcDeviceDefinition GetOpcDaElements()
        {
            if (!OpcDaServer.IsConnected)
                OpcDaServer.Connect();

            OpcDevice = new OpcDeviceDefinition();
            GetElements(OpcDevice, OpcDevice, OpcDaServer.Name, null);

            OpcDevice = OpcDevice.LowerDevices[0];

            return OpcDevice;
        }
        private void GetElements(OpcDeviceDefinition parentDevice, OpcDeviceDefinition childDevice, string path, string name)
        {
            Opc.ItemIdentifier itemIdentifier = new Opc.ItemIdentifier(path, name);
            Opc.Da.BrowsePosition position;
            Opc.Da.BrowseFilters filters = new Opc.Da.BrowseFilters()
            {
                BrowseFilter = Opc.Da.browseFilter.all,
                ReturnAllProperties = true,
                ReturnPropertyValues = false,
            };

            Opc.Da.BrowseElement[] elements = OpcDaServer.Browse(itemIdentifier, filters, out position);

            foreach (var item in elements)
            {
                if (item.HasChildren)
                {
                    parentDevice.HasChildren = true;
                    OpcDeviceDefinition obj = new OpcDeviceDefinition();
                    obj.Name = item.ItemName;
                    parentDevice.LowerDevices.Add(obj);
                    GetElements(obj, obj, null, item.ItemName);
                }
                else
                {
                    childDevice.HasItem = true;
                    childDevice.OpcItems.Add(new Opc.Da.Item { ItemName = item.ItemName });
                }
            }
        }
        public bool GetAllOpcDaItems()
        {
            try
            {
                Opc.ItemIdentifier itemIdentifier = new Opc.ItemIdentifier(OpcDaServer.Name, null);
                Opc.Da.BrowsePosition position;
                Opc.Da.BrowseFilters filters = new Opc.Da.BrowseFilters()
                {
                    BrowseFilter = Opc.Da.browseFilter.all,
                    ReturnAllProperties = true,
                    ReturnPropertyValues = false,
                };

                Opc.Da.BrowseElement[] elements = OpcDaServer.Browse(itemIdentifier, filters, out position);

                OpcDaItems = new List<OpcDaItem>();
                foreach (var item in elements)
                {
                    GetItems(item, OpcDaItems);
                }
                return true;
            }
            catch { return false; }
        }
        private void GetItems(Opc.Da.BrowseElement element, List<OpcDaItem> items)
        {

            if (element.HasChildren)
            {

                Opc.ItemIdentifier itemIdentifier = new Opc.ItemIdentifier(null, element.ItemName);
                Opc.Da.BrowsePosition position;
                Opc.Da.BrowseFilters filters = new Opc.Da.BrowseFilters
                {
                    BrowseFilter = Opc.Da.browseFilter.all,
                    ReturnAllProperties = false,
                    ReturnPropertyValues = true,
                    PropertyIDs = new Opc.Da.PropertyID[] { new Opc.Da.PropertyID(1) }
                };

                Opc.Da.BrowseElement[] elements = OpcDaServer.Browse(itemIdentifier, filters, out position);

                foreach (var item in elements)
                {
                    GetItems(item, items);
                }
            }
            else
            {
                if (element.ItemName.IndexOf("Connect") > -1
                    || element.ItemName.IndexOf("Transmits") > -1
                    || element.ItemName.IndexOf("Receive") > -1)
                    return;

                string type = string.Empty;
                if (element.Properties != null && element.Properties.Length > 0)
                    type = (element.Properties[0].Value as Type).Name;

                items.Add(new OpcDaItem { ItemName = element.ItemName, Active = true, TypeName = type });
            }
        }
        public bool OpcDaConnect()
        {
            try
            {
                try
                {
                    OpcCom.Factory fact = new OpcCom.Factory();
                    OpcDaServer = new Opc.Da.Server(fact, null);
                    OpcDaServer.Connect(Url, new Opc.ConnectData(new System.Net.NetworkCredential()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                Console.WriteLine("Get OpcDa element as treeview model");
                GetOpcDaElements();
                Console.WriteLine("Get all items/tags of OpcDa server");
                GetAllOpcDaItems();
                Console.WriteLine("Start OpcDa successfully");
                return true;
            }
            catch { return false; }
        }
        public bool OpcDaDisconnect()
        {
            try
            {
                OpcDaServer.Disconnect();
                return true;
            }
            catch { return false; }
        }
        public bool ServerStatus()
        {
            return OpcDaServer.IsConnected;
        }
    }

    public class OpcDeviceDefinition
    {
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public bool HasItem { get; set; }
        public List<Opc.Da.Item> OpcItems { get; set; }
        public List<OpcDeviceDefinition> LowerDevices { get; set; }
        public OpcDeviceDefinition()
        {
            LowerDevices = new List<OpcDeviceDefinition>();
            OpcItems = new List<Opc.Da.Item>();
        }
    }
}
