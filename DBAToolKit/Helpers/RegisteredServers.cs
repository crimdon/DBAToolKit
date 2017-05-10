using System;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;

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
            RegistryKey ProgSettings = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
            foreach (var server in ProgSettings.GetValueNames())
            {
                cboServerName.Items.Add(server);
            }
            ProgSettings.Close();
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
