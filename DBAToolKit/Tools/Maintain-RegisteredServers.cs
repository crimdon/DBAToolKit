using DBAToolKit.Helpers;
using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBAToolKit.Tools
{
    public partial class Maintain_RegisteredServers : UserControl
    {
        public Maintain_RegisteredServers()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewConnectionForm newConnection = new NewConnectionForm();
            newConnection.ShowDialog();
            LoadListView();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem server in listRegisteredServers.CheckedItems)
                {
                    ConnectionManager.deleteConnectionString(server.SubItems[0].Text);
                }
                LoadListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Maintain_RegisteredServers_Load(object sender, EventArgs e)
        {
            LoadListView();
        }
        private void LoadListView()
        {
            listRegisteredServers.Items.Clear();
            RegistryKey ProgSettings = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
            if (ProgSettings.ValueCount > 0)
            {
                foreach (string server in ProgSettings.GetValueNames())
                {
                    string maskedConnectionString = Regex.Replace(ProgSettings.GetValue(server, false).ToString(), "Password=[^;]*;", "Password=******;");
                    listRegisteredServers.Items.Add(new ListViewItem(new string[]
                        {
                            server,
                            maskedConnectionString
                        }));
                }
            }
            ProgSettings.Close();
            
        }
    }
}
