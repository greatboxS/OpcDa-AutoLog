﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TitaniumAS.Opc.Client.Da.Browsing;
using OPCDataAccess.Models;

namespace OpcHistorianApp.ControlForm
{
    public partial class TagPropertyControl : UserControl
    {
        public OPCDataAccess.AppDefinition.UserDataType varType = OPCDataAccess.AppDefinition.UserDataType.UNDEFINE;
        public TagPropertyControl()
        {
            InitializeComponent();
            EventControl.AllTagSelected += EventControl_AllTagSelected;
        }

        private void EventControl_AllTagSelected(object sender, object userObject)
        {
            bool selectAll = (bool)sender;
            if (selectAll)
                this.TagSelection.Checked = true;
            else
                this.TagSelection.Checked = false;
        }

        private TagProperty TagProp;

        public TagPropertyControl(TagProperty prop)
        {
            InitializeComponent();

            UpdateTagProps(prop);

            EventControl.AllTagSelected += EventControl_AllTagSelected;
        }
        public void UpdateTagProps(TagProperty prop)
        {
            TagProp = prop;

            txtTagName.Text = prop.Name;
            txtTagType.Text = prop.TypeName;
            txtError.Text = prop.Quantity;
        }

        private void TagSelection_CheckedChanged(object sender, EventArgs e)
        {
            var state = (sender as CheckBox).Checked;
            if (state)
                this.BackColor = Color.LightSkyBlue;
            else
                this.BackColor = Color.Transparent;

            EventControl.TagItemSelectedChanged(new EventSender(state, TagProp));
        }
    }
}
