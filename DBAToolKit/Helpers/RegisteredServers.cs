using System;
using System.Windows.Forms;
using DBAToolKit.Models;

namespace DBAToolKit.Helpers
{
    public partial class RegisteredServers : UserControl
    {
        public string SelectedServer { get; set; }
        public event EventHandler SelectedServerChanged;
        public RegisteredServers()
        {
            InitializeComponent();
        }

        private void cboServerName_DropDown(object sender, EventArgs e)
        {
            cboServerName.Items.Clear();
            using (var dbCtx = new ConfigDBContainer())
            {
                foreach (Servers server in dbCtx.Servers)
                {
                    cboServerName.Items.Add(server.ServerName);
                }
            }
        }
        private void cboServerName_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedServer = cboServerName.SelectedItem.ToString();
            this.OnSelectedServerChanged(EventArgs.Empty);
        }
        protected virtual void OnSelectedServerChanged (EventArgs e)
        {
            EventHandler hander = this.SelectedServerChanged;
            if (hander != null)
            {
                hander(this, e);
            }
        }
    }
}
