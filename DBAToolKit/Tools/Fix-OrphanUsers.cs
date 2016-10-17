using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;

namespace DBAToolKit.Tools
{
    public partial class Fix_OrphanUsers : UserControl
    {
        public Server sourceServer;

        private string databaseName;
        public Fix_OrphanUsers()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text) == true)
                {
                    throw new Exception("Enter a Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceServer = connection.Connect(txtSource.Text);
                
                if (sourceServer.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                databaseName = txtDatabaseName.Text;

                findOrphanUsers();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void findOrphanUsers()
        {
            listOrphanUsers.Clear();
            listOrphanUsers.View = System.Windows.Forms.View.Details;

            listOrphanUsers.Columns.Add("Database Name");
            listOrphanUsers.Columns.Add("User");
            listOrphanUsers.Columns.Add("Login Type");
            listOrphanUsers.Columns.Add("Matching Login Name");

            foreach (Database db in sourceServer.Databases)
            {
                if (!string.IsNullOrEmpty(databaseName) && db.Name.ToLower() != databaseName.ToLower())
                {
                    continue;
                }

                foreach (User dbUser in db.Users)
                {
                    if (!dbUser.IsSystemObject && dbUser.Login == string.Empty)
                    {
                        string matchedlogin = "";
                        if (DBChecks.LoginExists(sourceServer, dbUser.Name))
                        {
                            matchedlogin = dbUser.Name;
                        }
                        listOrphanUsers.Items.Add(new ListViewItem(new string[] { db.Name, dbUser.Name, dbUser.LoginType.ToString(), matchedlogin }));
                    }
                }              
            }

            listOrphanUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listOrphanUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listOrphanUsers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listOrphanUsers.CheckedItems.Count > 0)
            {
                btnFix.Enabled = true;
            }
            else
            {
                btnFix.Enabled = false;
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listOrphanUsers.CheckedItems)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.SubItems[3].Text))
                    {
                        Database db = sourceServer.Databases[item.Text];
                        db.ExecuteNonQuery("sp_change_users_login 'Update_one' ,'" + item.SubItems[1].Text + "', '" + item.SubItems[3].Text + "'");
                    }
                    else
                    {
                        if (!DBChecks.LoginExists(sourceServer, item.SubItems[3].Text))
                        {
                            Database db = sourceServer.Databases[item.Text];
                            db.ExecuteNonQuery("sp_change_users_login 'Auto_Fix' ,'" + item.SubItems[1].Text + "', NULL, 'P@ssw0rd'");
                            MessageBox.Show("Login [" + item.SubItems[1].Text + "] created with a password of 'P@ssw0rd'");
                        }
                        else
                        {
                            Database db = sourceServer.Databases[item.Text];
                            db.ExecuteNonQuery("sp_change_users_login 'Update_one' ,'" + item.SubItems[1].Text + "', '" + item.SubItems[3].Text + "'");
                        }
                    }

                    listOrphanUsers.Items.Remove(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
            }

            listOrphanUsers.Refresh();
        }
    }
}
