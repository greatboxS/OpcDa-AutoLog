namespace OpcHistorianApp.ControlForm
{
    partial class OpcDaItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TagSelection = new System.Windows.Forms.CheckBox();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.txtTagType = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TagSelection
            // 
            this.TagSelection.AutoSize = true;
            this.TagSelection.Location = new System.Drawing.Point(9, 5);
            this.TagSelection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TagSelection.Name = "TagSelection";
            this.TagSelection.Size = new System.Drawing.Size(15, 14);
            this.TagSelection.TabIndex = 1;
            this.TagSelection.UseVisualStyleBackColor = true;
            this.TagSelection.CheckedChanged += new System.EventHandler(this.TagSelection_CheckedChanged);
            // 
            // txtTagName
            // 
            this.txtTagName.Location = new System.Drawing.Point(42, 0);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTagName.Multiline = true;
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.ReadOnly = true;
            this.txtTagName.Size = new System.Drawing.Size(215, 21);
            this.txtTagName.TabIndex = 2;
            this.txtTagName.Text = "#tagId";
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(332, 0);
            this.txtError.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(61, 21);
            this.txtError.TabIndex = 4;
            this.txtError.Text = "#tagError";
            this.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTagType
            // 
            this.txtTagType.Location = new System.Drawing.Point(264, 0);
            this.txtTagType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTagType.Multiline = true;
            this.txtTagType.Name = "txtTagType";
            this.txtTagType.ReadOnly = true;
            this.txtTagType.Size = new System.Drawing.Size(61, 21);
            this.txtTagType.TabIndex = 4;
            this.txtTagType.Text = "#tagType";
            this.txtTagType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OpcDaItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtTagType);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.txtTagName);
            this.Controls.Add(this.TagSelection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OpcDaItemControl";
            this.Size = new System.Drawing.Size(399, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TagSelection;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TextBox txtTagType;
    }
}
