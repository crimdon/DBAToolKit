using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;

namespace DBAToolKit.Tools
{
    public partial class Get_ServerConfiguration : UserControl
    {
        public Get_ServerConfiguration()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true)
                {
                    throw new Exception("Enter a Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                Server sourceserver = connection.Connect(registeredServersSource.SelectedServer);
                
                if (sourceserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                showConfiguration(sourceserver);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showConfiguration(Server sourceserver)
        {
            listConfig.View = System.Windows.Forms.View.Details;
            listConfig.Items.Clear();

            foreach (ConfigProperty item in sourceserver.Configuration.Properties)
            {
                listConfig.Items.Add(new ListViewItem(new string[] { item.Description, item.Minimum.ToString(), item.Maximum.ToString(), item.ConfigValue.ToString(), item.RunValue.ToString() }));
            }

            listConfig.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listConfig.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
