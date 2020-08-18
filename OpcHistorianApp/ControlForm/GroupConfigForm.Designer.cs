namespace OpcHistorianApp.ControlForm
{
    partial class GroupConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUpdateTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOpcDaServer = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.DSSqlSetting = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GroupContainer = new System.Windows.Forms.GroupBox();
            this.TagList = new System.Windows.Forms.ListBox();
            this.DSTags = new System.Windows.Forms.BindingSource(this.components);
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.GroupHeader = new System.Windows.Forms.Panel();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TagListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TopPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSSqlSetting)).BeginInit();
            this.GroupContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSTags)).BeginInit();
            this.GroupHeader.SuspendLayout();
            this.TagListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.groupBox1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(5, 5);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(261, 222);
            this.TopPanel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtUpdateTime);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtOpcDaServer);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 222);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Update time:";
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Location = new System.Drawing.Point(79, 195);
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.Size = new System.Drawing.Size(174, 20);
            this.txtUpdateTime.TabIndex = 14;
            this.txtUpdateTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUpdateTime_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "OpcDa:";
            // 
            // txtOpcDaServer
            // 
            this.txtOpcDaServer.Location = new System.Drawing.Point(79, 172);
            this.txtOpcDaServer.Name = "txtOpcDaServer";
            this.txtOpcDaServer.Size = new System.Drawing.Size(174, 20);
            this.txtOpcDaServer.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DSSqlSetting, "UseLogin", true));
            this.checkBox1.Location = new System.Drawing.Point(183, 152);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(70, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Use login";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DSSqlSetting
            // 
            this.DSSqlSetting.DataSource = typeof(OPCDataAccess.Models.SqlSetting);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "DbTable:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DSSqlSetting, "Table", true));
            this.textBox5.Location = new System.Drawing.Point(63, 72);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(190, 20);
            this.textBox5.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DSSqlSetting, "Password", true));
            this.textBox4.Location = new System.Drawing.Point(65, 126);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(188, 20);
            this.textBox4.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User:";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DSSqlSetting, "UserName", true));
            this.textBox3.Location = new System.Drawing.Point(65, 101);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(188, 20);
            this.textBox3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DSSqlSetting, "DataBase", true));
            this.textBox2.Location = new System.Drawing.Point(65, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(188, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DSSqlSetting, "ServerName", true));
            this.textBox1.Location = new System.Drawing.Point(65, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 20);
            this.textBox1.TabIndex = 1;
            // 
            // GroupContainer
            // 
            this.GroupContainer.Controls.Add(this.TagList);
            this.GroupContainer.Controls.Add(this.BottomPanel);
            this.GroupContainer.Controls.Add(this.GroupHeader);
            this.GroupContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupContainer.Location = new System.Drawing.Point(5, 227);
            this.GroupContainer.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.GroupContainer.Name = "GroupContainer";
            this.GroupContainer.Size = new System.Drawing.Size(261, 309);
            this.GroupContainer.TabIndex = 1;
            this.GroupContainer.TabStop = false;
            this.GroupContainer.Text = "Group Item config";
            // 
            // TagList
            // 
            this.TagList.DataSource = this.DSTags;
            this.TagList.DisplayMember = "Name";
            this.TagList.Dock = System.Windows.Forms.DockStyle.Left;
            this.TagList.FormattingEnabled = true;
            this.TagList.Location = new System.Drawing.Point(3, 48);
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(250, 248);
            this.TagList.TabIndex = 3;
            // 
            // DSTags
            // 
            this.DSTags.DataSource = typeof(OPCDataAccess.Models.TagProperty);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(3, 296);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(255, 10);
            this.BottomPanel.TabIndex = 2;
            // 
            // GroupHeader
            // 
            this.GroupHeader.Controls.Add(this.txtGroup);
            this.GroupHeader.Controls.Add(this.label6);
            this.GroupHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupHeader.Location = new System.Drawing.Point(3, 16);
            this.GroupHeader.Name = "GroupHeader";
            this.GroupHeader.Size = new System.Drawing.Size(255, 32);
            this.GroupHeader.TabIndex = 0;
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(83, 7);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(128, 20);
            this.txtGroup.TabIndex = 1;
            this.txtGroup.Text = "Group1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Group name:";
            // 
            // TagListMenu
            // 
            this.TagListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenu,
            this.RestoreMenu});
            this.TagListMenu.Name = "TagListMenu";
            this.TagListMenu.Size = new System.Drawing.Size(114, 48);
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Name = "DeleteMenu";
            this.DeleteMenu.Size = new System.Drawing.Size(113, 22);
            this.DeleteMenu.Text = "Delete";
            this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // RestoreMenu
            // 
            this.RestoreMenu.Name = "RestoreMenu";
            this.RestoreMenu.Size = new System.Drawing.Size(113, 22);
            this.RestoreMenu.Text = "Restore";
            this.RestoreMenu.Click += new System.EventHandler(this.RestoreMenu_Click);
            // 
            // GroupConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 541);
            this.Controls.Add(this.GroupContainer);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GroupConfigForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.TopPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSSqlSetting)).EndInit();
            this.GroupContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DSTags)).EndInit();
            this.GroupHeader.ResumeLayout(false);
            this.GroupHeader.PerformLayout();
            this.TagListMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox GroupContainer;
        private System.Windows.Forms.Panel GroupHeader;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.BindingSource DSSqlSetting;
        private System.Windows.Forms.BindingSource DSTags;
        private System.Windows.Forms.ListBox TagList;
        private System.Windows.Forms.ContextMenuStrip TagListMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
        private System.Windows.Forms.ToolStripMenuItem RestoreMenu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOpcDaServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUpdateTime;
    }
}