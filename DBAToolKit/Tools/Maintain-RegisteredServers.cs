using DBAToolKit.Helpers;
using System;
using System.Configuration;
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
            foreach (ListViewItem server in listRegisteredServers.CheckedItems)
            {
                ConnectionManager.deleteConnectionString(server.SubItems[0].Text);
            }
            LoadListView();
        }
        private void Maintain_RegisteredServers_Load(object sender, EventArgs e)
        {
            LoadListView();
        }
        private void LoadListView()
        {
            listRegisteredServers.Items.Clear();
            ConfigurationManager.RefreshSection("connectionStrings");
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                if (css.Name.ToString() != "LocalSqlServer")
                {
                    string maskedConnectionString = Regex.Replace(css.ConnectionString, "Password=[^;]*;", "Password=******;");
                    listRegisteredServers.Items.Add(new ListViewItem(new string[]
                        {
                            css.Name.ToString(),
                            maskedConnectionString
                        }));
                }
            }
        }
    }
}
