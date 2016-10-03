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
    }
}
