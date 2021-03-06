﻿namespace DBAToolKit
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.getConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryConsumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.listDatabasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixOrphanUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autogrowSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyServerTriggersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDatabaseMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyOperatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewJobTimelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewConnectionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addNewConnectionToolStripMenuItem
            // 
            this.addNewConnectionToolStripMenuItem.Name = "addNewConnectionToolStripMenuItem";
            this.addNewConnectionToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.addNewConnectionToolStripMenuItem.Text = "Maintain Registered Servers";
            this.addNewConnectionToolStripMenuItem.Click += new System.EventHandler(this.addNewConnectionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getConfigurationToolStripMenuItem,
            this.currentProcessesToolStripMenuItem,
            this.memoryConsumpToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem2.Text = "Server";
            // 
            // getConfigurationToolStripMenuItem
            // 
            this.getConfigurationToolStripMenuItem.Name = "getConfigurationToolStripMenuItem";
            this.getConfigurationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.getConfigurationToolStripMenuItem.Text = "Get Configuration";
            this.getConfigurationToolStripMenuItem.Click += new System.EventHandler(this.getConfigurationToolStripMenuItem_Click);
            // 
            // currentProcessesToolStripMenuItem
            // 
            this.currentProcessesToolStripMenuItem.Name = "currentProcessesToolStripMenuItem";
            this.currentProcessesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.currentProcessesToolStripMenuItem.Text = "Active Processes";
            this.currentProcessesToolStripMenuItem.Click += new System.EventHandler(this.currentProcessesToolStripMenuItem_Click);
            // 
            // memoryConsumpToolStripMenuItem
            // 
            this.memoryConsumpToolStripMenuItem.Name = "memoryConsumpToolStripMenuItem";
            this.memoryConsumpToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.memoryConsumpToolStripMenuItem.Text = "Memory Consumption";
            this.memoryConsumpToolStripMenuItem.Click += new System.EventHandler(this.memoryConsumpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listDatabasesToolStripMenuItem,
            this.fixOrphanUsersToolStripMenuItem,
            this.databaseSizeToolStripMenuItem,
            this.backupHistoryToolStripMenuItem,
            this.autogrowSettingsToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem3.Text = "Databases";
            // 
            // listDatabasesToolStripMenuItem
            // 
            this.listDatabasesToolStripMenuItem.Name = "listDatabasesToolStripMenuItem";
            this.listDatabasesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.listDatabasesToolStripMenuItem.Text = "List Databases";
            this.listDatabasesToolStripMenuItem.Click += new System.EventHandler(this.listDatabasesToolStripMenuItem_Click);
            // 
            // fixOrphanUsersToolStripMenuItem
            // 
            this.fixOrphanUsersToolStripMenuItem.Name = "fixOrphanUsersToolStripMenuItem";
            this.fixOrphanUsersToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.fixOrphanUsersToolStripMenuItem.Text = "Fix Orphan Users";
            this.fixOrphanUsersToolStripMenuItem.Click += new System.EventHandler(this.fixOrphanUsersToolStripMenuItem_Click);
            // 
            // databaseSizeToolStripMenuItem
            // 
            this.databaseSizeToolStripMenuItem.Name = "databaseSizeToolStripMenuItem";
            this.databaseSizeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.databaseSizeToolStripMenuItem.Text = "Database Size";
            this.databaseSizeToolStripMenuItem.Click += new System.EventHandler(this.databaseSizeToolStripMenuItem_Click);
            // 
            // backupHistoryToolStripMenuItem
            // 
            this.backupHistoryToolStripMenuItem.Name = "backupHistoryToolStripMenuItem";
            this.backupHistoryToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.backupHistoryToolStripMenuItem.Text = "Backup History";
            this.backupHistoryToolStripMenuItem.Click += new System.EventHandler(this.backupHistoryToolStripMenuItem_Click);
            // 
            // autogrowSettingsToolStripMenuItem
            // 
            this.autogrowSettingsToolStripMenuItem.Name = "autogrowSettingsToolStripMenuItem";
            this.autogrowSettingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.autogrowSettingsToolStripMenuItem.Text = "File Sizes";
            this.autogrowSettingsToolStripMenuItem.Click += new System.EventHandler(this.autogrowSettingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem1.Text = "Security";
            // 
            // loginsToolStripMenuItem
            // 
            this.loginsToolStripMenuItem.Name = "loginsToolStripMenuItem";
            this.loginsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loginsToolStripMenuItem.Text = "Copy SQL Logins";
            this.loginsToolStripMenuItem.Click += new System.EventHandler(this.loginsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyServerTriggersToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem4.Text = "Server Objects";
            // 
            // copyServerTriggersToolStripMenuItem
            // 
            this.copyServerTriggersToolStripMenuItem.Name = "copyServerTriggersToolStripMenuItem";
            this.copyServerTriggersToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyServerTriggersToolStripMenuItem.Text = "Copy Server Triggers";
            this.copyServerTriggersToolStripMenuItem.Click += new System.EventHandler(this.copyServerTriggersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyDatabaseMailToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem5.Text = "Management";
            // 
            // copyDatabaseMailToolStripMenuItem
            // 
            this.copyDatabaseMailToolStripMenuItem.Name = "copyDatabaseMailToolStripMenuItem";
            this.copyDatabaseMailToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copyDatabaseMailToolStripMenuItem.Text = "Copy Database Mail";
            this.copyDatabaseMailToolStripMenuItem.Click += new System.EventHandler(this.copyDatabaseMailToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyJobsToolStripMenuItem,
            this.copyCategoriesToolStripMenuItem,
            this.copyAlertsToolStripMenuItem,
            this.copyOperatorsToolStripMenuItem,
            this.viewJobTimelineToolStripMenuItem});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem6.Text = "SQL Server Agent";
            // 
            // copyJobsToolStripMenuItem
            // 
            this.copyJobsToolStripMenuItem.Name = "copyJobsToolStripMenuItem";
            this.copyJobsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyJobsToolStripMenuItem.Text = "Copy SQL Jobs";
            this.copyJobsToolStripMenuItem.Click += new System.EventHandler(this.copyJobsToolStripMenuItem_Click);
            // 
            // copyCategoriesToolStripMenuItem
            // 
            this.copyCategoriesToolStripMenuItem.Name = "copyCategoriesToolStripMenuItem";
            this.copyCategoriesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyCategoriesToolStripMenuItem.Text = "Copy Categories";
            this.copyCategoriesToolStripMenuItem.Click += new System.EventHandler(this.copyCategoriesToolStripMenuItem_Click);
            // 
            // copyAlertsToolStripMenuItem
            // 
            this.copyAlertsToolStripMenuItem.Name = "copyAlertsToolStripMenuItem";
            this.copyAlertsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyAlertsToolStripMenuItem.Text = "Copy Alerts";
            this.copyAlertsToolStripMenuItem.Click += new System.EventHandler(this.copyAlertsToolStripMenuItem_Click);
            // 
            // copyOperatorsToolStripMenuItem
            // 
            this.copyOperatorsToolStripMenuItem.Name = "copyOperatorsToolStripMenuItem";
            this.copyOperatorsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyOperatorsToolStripMenuItem.Text = "Copy Operators";
            this.copyOperatorsToolStripMenuItem.Click += new System.EventHandler(this.copyOperatorsToolStripMenuItem_Click);
            // 
            // viewJobTimelineToolStripMenuItem
            // 
            this.viewJobTimelineToolStripMenuItem.Name = "viewJobTimelineToolStripMenuItem";
            this.viewJobTimelineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.viewJobTimelineToolStripMenuItem.Text = "View Jobs Timeline";
            this.viewJobTimelineToolStripMenuItem.Click += new System.EventHandler(this.viewJobTimelineToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 425);
            this.panel1.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 449);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DBA Tool Kit";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem copyJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyOperatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDatabaseMailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listDatabasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyServerTriggersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixOrphanUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentProcessesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewJobTimelineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryConsumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autogrowSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

