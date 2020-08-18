namespace HistorianService
{
    partial class ProjectInstaller
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
            this.Process = new System.ServiceProcess.ServiceProcessInstaller();
            this.Install = new System.ServiceProcess.ServiceInstaller();
            // 
            // Process
            // 
            this.Process.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.Process.Password = null;
            this.Process.Username = null;
            // 
            // Install
            // 
            this.Install.DisplayName = "OpcLogger";
            this.Install.ServiceName = "OpcLogger";
            this.Install.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.Install,
            this.Process});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller Process;
        private System.ServiceProcess.ServiceInstaller Install;
    }
}