using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;

namespace DBAToolKit.Tools
{
    public partial class Get_DatabaseDetails : UserControl
    {
        public Get_DatabaseDetails()
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

            listDatabases.View = System.Windows.Forms.View.Details;

            listDatabases.Columns.Add("Database Name");
            listDatabases.Columns.Add("ID");
            listDatabases.Columns.Add("Recovery Model");
            listDatabases.Columns.Add("Owner");
            listDatabases.Columns.Add("Compatibility Level");
            listDatabases.Columns.Add("Create Date");
            listDatabases.Columns.Add("Size (MB)");

            foreach (Database db in sourceserver.Databases)
            {
                if (db.Status == DatabaseStatus.Normal)
                    listDatabases.Items.Add(new ListViewItem(new string[] { db.Name, db.ID.ToString(), db.RecoveryModel.ToString(), db.Owner, db.CompatibilityLevel.ToString(),
                    db.CreateDate.ToShortDateString(), db.Size.ToString() }));
            }

            listDatabases.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listDatabases.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listDatabases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listDatabases.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listDatabases.SelectedItems;

                ListViewItem lvItem = items[0];
                MainForm.dbServer = this.registeredServersSource.SelectedServer;
                MainForm.dbName = lvItem.Text;
                Show_DatabaseDetails frm = new Show_DatabaseDetails();
                frm.Show();
            }
        }
    }
}
