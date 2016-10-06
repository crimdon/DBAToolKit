using System;
using System.Windows.Forms;
using DBAToolKit.Tools;

namespace DBAToolKit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadControl(Control control)
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
            LoadControl(copyLogins);   
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyjobs = new Copy_SqlJobs();
            LoadControl(copyjobs);
        }

        private void copyCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copycategories = new Copy_JobCategories();
            LoadControl(copycategories);
        }

        private void copyAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyalerts = new Copy_SqlAlerts();
            LoadControl(copyalerts);
        }

        private void copyOperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyoperators = new Copy_SqlOperators();
            LoadControl(copyoperators);
        }
    }
}
