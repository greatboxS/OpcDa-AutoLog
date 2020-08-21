using DataLogger;
using DataLogger.Services;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace OpcHistorianApp.ControlForm
{
    public partial class LogScheduleForm : Form
    {
        private string FilePath;
        private ExcelHandle ExcelConfigFile;
        private IList<LoggingGroup> LoggingGroup;
        private bool TestStarted = false;
        private Timer TestTimer;
        private TimeSpan TestTime;
        private AboutForm AboutForm;
        public LogScheduleForm()
        {
            InitializeComponent();
            this.TopLevel = false;
            EventControl.NewGroupChanged += EventControl_NewGroupChanged;
            EventControl.GroupSaveChanged += EventControl_GroupSaveChanged;
            EventControl.UpdateCurrentGroupEvent += EventControl_UpdateCurrentGroupEvent;


            ExcelConfigFile = new DataLogger.ExcelHandle();
            LoggingGroup = new List<LoggingGroup>();
            RefreshForm();
        }

        private void EventControl_UpdateCurrentGroupEvent(object sender, object userObject)
        {
            int groupId = (int)userObject;
            List<OpcDaItem> tags = sender as List<OpcDaItem>;
            var current = LoggingGroup.Where(i => i.Id == groupId).FirstOrDefault();
            if (current !=null)
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    if (current.Items.Where(index => index.ItemName == tags[i].ItemName).Count() == 0)
                        current.Items.Add(tags[i]);
                }

                EventControl.UpdateTagList(tags, groupId);

                EventControl.AddGroupComplete(this.LoggingGroup);
            }
        }

        private void EventControl_GroupSaveChanged(object sender, object userObject)
        {
            var group = sender as LoggingGroup;
            var grp = LoggingGroup.Where(i => i.Id == group.Id).FirstOrDefault();

            if (grp != null)
                grp = group;
        }

        private void EventControl_NewGroupChanged(object sender, object userObject)
        {
            var newGroup = sender as LoggingGroup;
            bool autoName = (bool)userObject;
            AddGroup(newGroup, autoName);
        }

        public void RefreshForm()
        {
            this.Refresh();
            TestTimer = new Timer { Interval = 1000, Enabled = true, };
            TestTimer.Tick += TestTimer_Tick;
            TestTimer.Stop();

            ExcelConfigFile = new ExcelHandle();
            FilePath = string.Empty;
            LoggingGroup = new List<LoggingGroup>();
            TabControl.TabPages.Clear();

            if(TestStarted)
            {
                TestStarted = false;
                TestingMode(false);
            }

            TestTime = new TimeSpan(0, 0, 0);
        }

        private void TestTimer_Tick(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                TestTime = TestTime.Add(new TimeSpan(0, 0, 1));
                txtTestTime.Text = $"Runing: {TestTime}";
            }));
        }

        public void AddGroup(LoggingGroup newGroup, bool AddName = false)
        {
            newGroup.Id = LoggingGroup.Count + 1;
            if(AddName)
                newGroup.GroupName = string.Format("Group#{0}", LoggingGroup.Count + 1);

            LoggingGroup.Add(newGroup);
            TabPage NewTab = new TabPage(newGroup.GroupName);
            GroupConfigForm groupConfigForm = new GroupConfigForm(newGroup, newGroup.Id);
            NewTab.Controls.Add(groupConfigForm);
            groupConfigForm.Dock = DockStyle.Fill;
            groupConfigForm.Show();
            TabControl.TabPages.Add(NewTab);

            EventControl.AddGroupComplete(this.LoggingGroup);
        }

        private void SaveAsMenu_Click(object sender, EventArgs e)
        {
            if(LoggingGroup.Count==0)
            {
              MessageBox.Show("Configuration is empty");
                return;
            }
            SaveAs();
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() != DialogResult.OK)
                return;

            string path = open.FileName;
            var group = ExcelConfigFile.ReadConfigFile(path);

            RefreshForm();

            foreach (var item in group)
            {
                AddGroup(item);
            }

            FilePath = path;
        }

        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            FilePath = saveFileDialog.FileName;
            FilePath = string.Format("{0}.xlsx", FilePath);

            if (LoggingGroup.Count > 0)
            { // save setting
                Properties.Settings.Default.SqlSettingJson = JsonConvert.SerializeObject(LoggingGroup[0].SqlSetting);
                Properties.Settings.Default.Save();
            }
            if (!ExcelConfigFile.SaveAs(LoggingGroup, FilePath))
                MessageBox.Show("Successfull");
            else
                MessageBox.Show("Failed");
        }

        private void ConfigMenu_Click(object sender, EventArgs e)
        {
            if (LoggingGroup.Count == 0)
                TestConfigMenu.Enabled = false;
            else
                TestConfigMenu.Enabled = true;
        }

        private void TestingMode(bool start)
        {
            if (start)
            {
                //LoggingServices.OpcDaClient.CreateOpcDaGroup(LoggingGroup[0], true);


                lbBeginTime.Text = $"Test begin: {DateTime.Now}";
                StartTestMenu.Text = "Stop testing";
                TestTime = new TimeSpan(0, 0, 0);
                TestTimer.Start();
            }
            else
            {
                StartTestMenu.Text = "Start testing";
                string temp = txtTestTime.Text;
                temp = temp.Replace("Runing:", "Stopping:");
                txtTestTime.Text = temp;
                TestTimer.Stop();
                foreach (var item in LoggingServices.WrapColletion.OpcDaSubcriptionWrapper)
                {
                    if (item.Timer != null)
                    {
                        try
                        {
                            item.Timer.Dispose();
                        }
                        catch (Exception ex)
                        {
                            DebugLog.WriteExceptionLogFile(ex.ToString());
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        private void StartTestMenu_Click(object sender, EventArgs e)
        {
            TestStarted = !TestStarted;
            TestingMode(TestStarted);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm = new AboutForm();
            AboutForm.ShowDialog();
        }
    }
}
