using DataLogger;
using DataLogger.SqlLogger;
using Newtonsoft.Json;
using OPCDataAccess.Controls;
using OPCDataAccess.Models;
using OpcHistorianApp.ControlForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace OpcHistorianApp
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Variables
        private static int ItemPropCounter = 0;
        private IList<TagProperty> Tags;
        private IList<TagProperty> SelectedTag;
        private LogScheduleForm LogScheduleForm;
        private CustomSqlLog CustomSqlLog;
        string CurrentOpcDaServerName = string.Empty;


        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            WindowInit();
        }
        private void UpdateTreeViewServer()
        {
            var serverList = TitaniumOpcDaControl.GetOpcDaServer();

            foreach (var sv in serverList)
            {
                SvTree.Nodes.Add(sv.ProgId, sv.ProgId);
            }
        }
        private void UpdateItemPanel(IList<TagProperty> tags)
        {
            ItemPropCounter = 0;
            ItemPropertyPanel.Controls.Clear();
            foreach (var tag in tags)
            {
                ItemPropertyPanel.Controls.Add(new TagPropertyControl(tag), 0, ItemPropCounter);
                ItemPropCounter++;
            }
        }
        private void WindowInit()
        {
            UpdateTreeViewServer();
            EventControl.TagSelected += EventControl_TagSelected;
            EventControl.AddGroupCompleted += EventControl_AddGroupCompleted;

            Tags = new List<TagProperty>();
            SelectedTag = new List<TagProperty>();
            CustomSqlLog = new CustomSqlLog(new SqlSetting());

            this.ItemPropertyPanel.ContextMenuStrip = MainWindowsMenu;

            var bools = CustomSqlLog.CreateDatabaseIfNotExist();

            this.LogScheduleForm = new LogScheduleForm();

            this.SchedulePanel.Controls.Clear();
            this.SchedulePanel.Controls.Add(LogScheduleForm);
            LogScheduleForm.Dock = DockStyle.Fill;
            this.LogScheduleForm.Show();
        }

        #region Event Handle

        private void EventControl_TagSelected(object sender, object userObject)
        {

            BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    var EventSender = sender as EventSender;

                    var same = SelectedTag.Where(i => i.Name == EventSender.TagSelection.TagProp.Name);

                    if (EventSender.TagSelection.Selected)
                    {
                        if (same.Count() != 0 && SelectedTag.Count != 0)
                            return;
                        else
                            SelectedTag.Add(EventSender.TagSelection.TagProp);
                    }
                    else
                    {
                        if (same.Count() != 0 && SelectedTag.Count != 0)
                            SelectedTag.Remove(EventSender.TagSelection.TagProp);
                        else
                            return;
                    }
                }
                catch
                {
                }
            }));
        }
        private void EventControl_AddGroupCompleted(object sender, object userObject)
        {
            IList<LoggingGroup> groups = sender as List<LoggingGroup>;
            InsertMenuItem.DropDownItems.Clear();
            if (groups.Count == 0)
            {
                InsertMenuItem.Enabled = false;
                return;
            }

            InsertMenuItem.Enabled = true;

            foreach (var item in groups)
            {
                ToolStripMenuItem addGroupItem = new ToolStripMenuItem(item.GroupName, null, UpdateGroupTagItemClick, item.Id.ToString());
                InsertMenuItem.DropDownItems.Add(addGroupItem);
            }
        }
        private void UpdateGroupTagItemClick(object sender, EventArgs e)
        {
            int itemId = int.Parse((sender as ToolStripMenuItem).Name);
            EventControl.UpdateCurrentGroup(itemId, SelectedTag);
        }
        #endregion

        #region Ui EventHandle
        private void SvTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string serverPropId = e.Node.Text;

            this.ItemPropertyPanel.Controls.Clear();
            SelectedTag = new List<TagProperty>();

            BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    this.Text = serverPropId;

                    var result = TitaniumOpcDaControl.GetServerTags(serverPropId);

                    if (result == null) return;

                    Tags = new List<TagProperty>(result);

                    CurrentOpcDaServerName = serverPropId;

                    UpdateItemPanel(Tags);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }));
        }

        private void SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            EventControl.SelectAllTags(true);
            SelectedTag = Tags;
        }

        private void UnSelectAllMenuItem_Click(object sender, EventArgs e)
        {
            EventControl.SelectAllTags(false);
            SelectedTag = new List<TagProperty>();
        }

        private void NewGroupMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTag.Count == 0)
            {
                if (MessageBox.Show("Group have no tag. Do you want to continue.", "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            SqlSetting setting = new SqlSetting();
            try
            {
                string settingJson = Properties.Settings.Default.SqlSettingJson;
                setting = JsonConvert.DeserializeObject<SqlSetting>(settingJson);
            }
            catch { }

            var newGroup = new LoggingGroup
            {
                OPCServerName = CurrentOpcDaServerName,
                GroupName = "Group",
                GroupTags = SelectedTag.ToList(),
                SqlSetting = setting != null ? setting : new SqlSetting(),
            };

            EventControl.AddNewGroup(newGroup, true);
        }

        private void MainWindowsMenu_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedTag.Count == 0)
                AddGroupMenu.Enabled = false;
            else
                AddGroupMenu.Enabled = true;
        }

        private void NewConfigMenuItem_Click(object sender, EventArgs e)
        {
            this.LogScheduleForm = new LogScheduleForm();
            this.SchedulePanel.Controls.Clear();
            this.SchedulePanel.Controls.Add(LogScheduleForm);
            this.LogScheduleForm.Show();
            LogScheduleForm.Dock = DockStyle.Fill;
        }
        #endregion
    }
}
