using System;
using System.Windows.Forms;
using DBAToolKit.Tools;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System.Data;

namespace DBAToolKit
{
    public partial class MainForm : Form
    {
        public static String dbServer;

        public static string dbName;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForSmo())
            {
                MessageBox.Show("SMO is not installed!");
                toolsToolStripMenuItem.Enabled = false;
            }
        }

        private void loadControl(Control control)
        {
            this.SuspendLayout();
            this.panel1.SuspendLayout();
            control.SuspendLayout();
            this.panel1.Controls.Clear();
            panel1.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            control.ResumeLayout();
            this.panel1.ResumeLayout();
            this.ResumeLayout();
        }

        private void loginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyLogins = new Copy_SqlLogin();
            loadControl(copyLogins);   
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyjobs = new Copy_SqlJobs();
            loadControl(copyjobs);
        }

        private void copyCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copycategories = new Copy_JobCategories();
            loadControl(copycategories);
        }

        private void copyAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyalerts = new Copy_SqlAlerts();
            loadControl(copyalerts);
        }

        private void copyOperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyoperators = new Copy_SqlOperators();
            loadControl(copyoperators);
        }

        private void copyDatabaseMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copydatabasemail = new Copy_DatabaseMail();
            loadControl(copydatabasemail);
        }

        private void getConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var getserverconfiguration = new Get_ServerConfiguration();
            loadControl(getserverconfiguration);
        }

        private void listDatabasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listdatabases = new Get_DatabaseDetails();
            loadControl(listdatabases);
        }

        private void copyServerTriggersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyservertriggers = new Copy_ServerTriggers();
            loadControl(copyservertriggers);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.About about = new Help.About();
            about.Show();
        }

        private void fixOrphanUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fixorphanusers = new Fix_OrphanUsers();
            loadControl(fixorphanusers);
        }

        private void currentProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sqlprocesses = new Get_SqlProcesses();
            loadControl(sqlprocesses);
        }
    }
}
