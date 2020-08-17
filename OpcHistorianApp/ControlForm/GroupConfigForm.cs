using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpcHistorianApp.ControlForm
{
    public partial class GroupConfigForm : Form
    {
        public GroupConfigForm()
        {
            InitializeComponent();
            this.TopLevel = false;
        }

        public LoggingGroup CurrentGroup;

        private LoggingGroup BackupGroup;
        private int Id = 0;
        public GroupConfigForm(LoggingGroup group, int id)
        {
            InitializeComponent();
            this.TopLevel = false;
            EventControl.UpdateTagListEvent += EventControl_UpdateTagListEvent;

            Id = id;

            BackupGroup = new LoggingGroup();
            BackupGroup = group;
            CurrentGroup = new LoggingGroup();
            CurrentGroup = group;

            DSSqlSetting.DataSource = CurrentGroup.SqlSetting;
            DSTags.DataSource = CurrentGroup.GroupTags;
            txtGroup.Text = CurrentGroup.GroupName;
            this.TagList.ContextMenuStrip = TagListMenu;
        }

        private void EventControl_UpdateTagListEvent(object sender, object userObject)
        {
            try
            {
                var tags = sender as IList<TagProperty>;
                int id = (int)userObject;

                if (id != Id) return;

                RefreshTagListBox();

                EventControl.SaveChanged(CurrentGroup);
            }catch(Exception ex)
            {

            }
        }

        private void RefreshTagListBox()
        {
            TagList.DataSource = null;
            TagList.Items.Clear();
            DSTags.DataSource = CurrentGroup.GroupTags;
            TagList.DataSource = DSTags;
            TagList.DisplayMember = "Name";
        }

        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            if (TagList.SelectedItem != null)
            {
                CurrentGroup.GroupTags.Remove((TagList.SelectedItem as TagProperty));
                RefreshTagListBox();
            }
        }

        private void RestoreMenu_Click(object sender, EventArgs e)
        {
            CurrentGroup = BackupGroup;
            RefreshTagListBox();
        }

        private void btnGroupSaveChange_Click(object sender, EventArgs e)
        {
            EventControl.SaveChanged(CurrentGroup);
        }
    }
}
