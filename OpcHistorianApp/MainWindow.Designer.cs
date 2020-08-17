namespace OpcHistorianApp
{
    partial class MainWindow
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SvTree = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GroupConfigPanel = new System.Windows.Forms.Panel();
            this.lbTotalSelectedTag = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ItemPropertyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MainWindowsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnSelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SchedulePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.MainLeftPanel = new System.Windows.Forms.Panel();
            this.NewConfigMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeftPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.GroupConfigPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MainWindowsMenu.SuspendLayout();
            this.SchedulePanel.SuspendLayout();
            this.MainLeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftPanel.Controls.Add(this.panel1);
            this.LeftPanel.Controls.Add(this.panel2);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(3, 3);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Padding = new System.Windows.Forms.Padding(3);
            this.LeftPanel.Size = new System.Drawing.Size(202, 553);
            this.LeftPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SvTree);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 514);
            this.panel1.TabIndex = 3;
            // 
            // SvTree
            // 
            this.SvTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SvTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SvTree.Location = new System.Drawing.Point(0, 0);
            this.SvTree.Name = "SvTree";
            this.SvTree.Size = new System.Drawing.Size(194, 514);
            this.SvTree.TabIndex = 0;
            this.SvTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SvTree_NodeMouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 31);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::OpcHistorianApp.Properties.Resources.computer;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(194, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.panel3);
            this.MainPanel.Controls.Add(this.LeftPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(3);
            this.MainPanel.Size = new System.Drawing.Size(615, 561);
            this.MainPanel.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.GroupConfigPanel);
            this.panel3.Controls.Add(this.ItemPropertyPanel);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(205, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(405, 553);
            this.panel3.TabIndex = 3;
            // 
            // GroupConfigPanel
            // 
            this.GroupConfigPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GroupConfigPanel.Controls.Add(this.lbTotalSelectedTag);
            this.GroupConfigPanel.Controls.Add(this.label1);
            this.GroupConfigPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupConfigPanel.Location = new System.Drawing.Point(0, 523);
            this.GroupConfigPanel.Name = "GroupConfigPanel";
            this.GroupConfigPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.GroupConfigPanel.Size = new System.Drawing.Size(401, 26);
            this.GroupConfigPanel.TabIndex = 1;
            // 
            // lbTotalSelectedTag
            // 
            this.lbTotalSelectedTag.AutoSize = true;
            this.lbTotalSelectedTag.Location = new System.Drawing.Point(367, 8);
            this.lbTotalSelectedTag.Name = "lbTotalSelectedTag";
            this.lbTotalSelectedTag.Size = new System.Drawing.Size(14, 13);
            this.lbTotalSelectedTag.TabIndex = 3;
            this.lbTotalSelectedTag.Text = "#";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selected tags:";
            // 
            // ItemPropertyPanel
            // 
            this.ItemPropertyPanel.AutoScroll = true;
            this.ItemPropertyPanel.AutoSize = true;
            this.ItemPropertyPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.ItemPropertyPanel.ColumnCount = 1;
            this.ItemPropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ItemPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemPropertyPanel.Location = new System.Drawing.Point(0, 33);
            this.ItemPropertyPanel.Name = "ItemPropertyPanel";
            this.ItemPropertyPanel.RowCount = 2;
            this.ItemPropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ItemPropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ItemPropertyPanel.Size = new System.Drawing.Size(401, 516);
            this.ItemPropertyPanel.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(401, 33);
            this.panel4.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::OpcHistorianApp.Properties.Resources.internet;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MainWindowsMenu
            // 
            this.MainWindowsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewConfigMenuItem,
            this.AddGroupMenu,
            this.SelectAllMenuItem,
            this.UnSelectAllMenuItem,
            this.InsertMenuItem});
            this.MainWindowsMenu.Name = "MainWindowsMenu";
            this.MainWindowsMenu.Size = new System.Drawing.Size(181, 136);
            this.MainWindowsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MainWindowsMenu_Opening);
            // 
            // AddGroupMenu
            // 
            this.AddGroupMenu.Enabled = false;
            this.AddGroupMenu.Name = "AddGroupMenu";
            this.AddGroupMenu.Size = new System.Drawing.Size(180, 22);
            this.AddGroupMenu.Text = "New Group";
            this.AddGroupMenu.Click += new System.EventHandler(this.NewGroupMenuItem_Click);
            // 
            // SelectAllMenuItem
            // 
            this.SelectAllMenuItem.Name = "SelectAllMenuItem";
            this.SelectAllMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SelectAllMenuItem.Text = "Select all";
            this.SelectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItem_Click);
            // 
            // UnSelectAllMenuItem
            // 
            this.UnSelectAllMenuItem.Name = "UnSelectAllMenuItem";
            this.UnSelectAllMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UnSelectAllMenuItem.Text = "UnSelect all";
            this.UnSelectAllMenuItem.Click += new System.EventHandler(this.UnSelectAllMenuItem_Click);
            // 
            // InsertMenuItem
            // 
            this.InsertMenuItem.Enabled = false;
            this.InsertMenuItem.Name = "InsertMenuItem";
            this.InsertMenuItem.Size = new System.Drawing.Size(180, 22);
            this.InsertMenuItem.Text = "Insert to";
            // 
            // SchedulePanel
            // 
            this.SchedulePanel.Controls.Add(this.label3);
            this.SchedulePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SchedulePanel.Location = new System.Drawing.Point(620, 0);
            this.SchedulePanel.Name = "SchedulePanel";
            this.SchedulePanel.Size = new System.Drawing.Size(324, 561);
            this.SchedulePanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "History Group";
            // 
            // MainLeftPanel
            // 
            this.MainLeftPanel.Controls.Add(this.MainPanel);
            this.MainLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.MainLeftPanel.Name = "MainLeftPanel";
            this.MainLeftPanel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.MainLeftPanel.Size = new System.Drawing.Size(620, 561);
            this.MainLeftPanel.TabIndex = 0;
            // 
            // NewConfigMenu
            // 
            this.NewConfigMenu.Name = "NewConfigMenu";
            this.NewConfigMenu.Size = new System.Drawing.Size(180, 22);
            this.NewConfigMenu.Text = "New Configuration";
            // 
            // NewConfigMenuItem
            // 
            this.NewConfigMenuItem.Name = "NewConfigMenuItem";
            this.NewConfigMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NewConfigMenuItem.Text = "New Configuration";
            this.NewConfigMenuItem.Click += new System.EventHandler(this.NewConfigMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 561);
            this.Controls.Add(this.SchedulePanel);
            this.Controls.Add(this.MainLeftPanel);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.LeftPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.GroupConfigPanel.ResumeLayout(false);
            this.GroupConfigPanel.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MainWindowsMenu.ResumeLayout(false);
            this.SchedulePanel.ResumeLayout(false);
            this.SchedulePanel.PerformLayout();
            this.MainLeftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel GroupConfigPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView SvTree;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel ItemPropertyPanel;
        private System.Windows.Forms.ContextMenuStrip MainWindowsMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnSelectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGroupMenu;
        private System.Windows.Forms.Panel SchedulePanel;
        private System.Windows.Forms.Panel MainLeftPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTotalSelectedTag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem InsertMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewConfigMenu;
        private System.Windows.Forms.ToolStripMenuItem NewConfigMenuItem;
    }
}

