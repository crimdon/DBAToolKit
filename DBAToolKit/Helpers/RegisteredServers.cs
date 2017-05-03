using System;
using System.Windows.Forms;
using System.Configuration;

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
            ConfigurationManager.RefreshSection("connectionStrings");
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                if (css.Name.ToString() != "LocalSqlServer")
                {
                    cboServerName.Items.Add(css.Name.ToString());
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
