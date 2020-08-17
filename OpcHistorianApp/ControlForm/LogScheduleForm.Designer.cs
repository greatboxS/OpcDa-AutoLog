namespace OpcHistorianApp.ControlForm
{
    partial class LogScheduleForm
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Group1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.Group2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.ConfigMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TestConfigMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StartTestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.txtTestTime = new System.Windows.Forms.Label();
            this.lbBeginTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TabControl.SuspendLayout();
            this.Group1.SuspendLayout();
            this.Group2.SuspendLayout();
            this.ConfigMenu.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.Group1);
            this.TabControl.Controls.Add(this.Group2);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.ItemSize = new System.Drawing.Size(100, 20);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(308, 419);
            this.TabControl.TabIndex = 0;
            // 
            // Group1
            // 
            this.Group1.Controls.Add(this.label1);
            this.Group1.Location = new System.Drawing.Point(4, 24);
            this.Group1.Name = "Group1";
            this.Group1.Padding = new System.Windows.Forms.Padding(3);
            this.Group1.Size = new System.Drawing.Size(300, 391);
            this.Group1.TabIndex = 0;
            this.Group1.Text = "Group1";
            this.Group1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Group is empty";
            // 
            // Group2
            // 
            this.Group2.Controls.Add(this.label2);
            this.Group2.Location = new System.Drawing.Point(4, 24);
            this.Group2.Name = "Group2";
            this.Group2.Padding = new System.Windows.Forms.Padding(3);
            this.Group2.Size = new System.Drawing.Size(300, 391);
            this.Group2.TabIndex = 1;
            this.Group2.Text = "Group2";
            this.Group2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Group is empty";
            // 
            // ConfigMenu
            // 
            this.ConfigMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.ConfigMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.ConfigMenu.Location = new System.Drawing.Point(0, 0);
            this.ConfigMenu.Name = "ConfigMenu";
            this.ConfigMenu.Size = new System.Drawing.Size(126, 31);
            this.ConfigMenu.TabIndex = 1;
            this.ConfigMenu.Text = "menuStrip1";
            this.ConfigMenu.Click += new System.EventHandler(this.ConfigMenu_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenu,
            this.OpenMenu,
            this.SaveAsMenu,
            this.TestConfigMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // NewMenu
            // 
            this.NewMenu.Name = "NewMenu";
            this.NewMenu.Size = new System.Drawing.Size(180, 22);
            this.NewMenu.Text = "New config";
            // 
            // OpenMenu
            // 
            this.OpenMenu.Name = "OpenMenu";
            this.OpenMenu.Size = new System.Drawing.Size(180, 22);
            this.OpenMenu.Text = "Open";
            this.OpenMenu.Click += new System.EventHandler(this.OpenMenu_Click);
            // 
            // SaveAsMenu
            // 
            this.SaveAsMenu.Name = "SaveAsMenu";
            this.SaveAsMenu.Size = new System.Drawing.Size(180, 22);
            this.SaveAsMenu.Text = "Save as";
            this.SaveAsMenu.Click += new System.EventHandler(this.SaveAsMenu_Click);
            // 
            // TestConfigMenu
            // 
            this.TestConfigMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartTestMenu});
            this.TestConfigMenu.Name = "TestConfigMenu";
            this.TestConfigMenu.Size = new System.Drawing.Size(180, 22);
            this.TestConfigMenu.Text = "Configuration Test";
            // 
            // StartTestMenu
            // 
            this.StartTestMenu.Name = "StartTestMenu";
            this.StartTestMenu.Size = new System.Drawing.Size(137, 22);
            this.StartTestMenu.Text = "Start testing";
            this.StartTestMenu.Click += new System.EventHandler(this.StartTestMenu_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.txtTestTime);
            this.TopPanel.Controls.Add(this.lbBeginTime);
            this.TopPanel.Controls.Add(this.label3);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TopPanel.Location = new System.Drawing.Point(0, 450);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(308, 37);
            this.TopPanel.TabIndex = 1;
            // 
            // txtTestTime
            // 
            this.txtTestTime.AutoSize = true;
            this.txtTestTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestTime.Location = new System.Drawing.Point(158, 22);
            this.txtTestTime.Name = "txtTestTime";
            this.txtTestTime.Size = new System.Drawing.Size(0, 12);
            this.txtTestTime.TabIndex = 1;
            // 
            // lbBeginTime
            // 
            this.lbBeginTime.AutoSize = true;
            this.lbBeginTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBeginTime.Location = new System.Drawing.Point(3, 22);
            this.lbBeginTime.Name = "lbBeginTime";
            this.lbBeginTime.Size = new System.Drawing.Size(0, 12);
            this.lbBeginTime.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(267, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Add new group to configuration file or open existing file.";
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.pictureBox1);
            this.HeaderPanel.Controls.Add(this.ConfigMenu);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(308, 31);
            this.HeaderPanel.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::OpcHistorianApp.Properties.Resources.browser;
            this.pictureBox1.Location = new System.Drawing.Point(126, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TabControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 419);
            this.panel1.TabIndex = 1;
            // 
            // LogScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 487);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.ConfigMenu;
            this.Name = "LogScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabControl.ResumeLayout(false);
            this.Group1.ResumeLayout(false);
            this.Group1.PerformLayout();
            this.Group2.ResumeLayout(false);
            this.Group2.PerformLayout();
            this.ConfigMenu.ResumeLayout(false);
            this.ConfigMenu.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage Group1;
        private System.Windows.Forms.TabPage Group2;
        private System.Windows.Forms.MenuStrip ConfigMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem OpenMenu;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem NewMenu;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem TestConfigMenu;
        private System.Windows.Forms.Label lbBeginTime;
        private System.Windows.Forms.ToolStripMenuItem StartTestMenu;
        private System.Windows.Forms.Label txtTestTime;
    }
}