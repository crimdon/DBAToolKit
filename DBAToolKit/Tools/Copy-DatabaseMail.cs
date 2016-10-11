using System;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo.Mail;
using System.Collections.Generic;
using System.Linq;

namespace DBAToolKit.Tools
{
    public partial class Copy_DatabaseMail : UserControl
    {
        public Copy_DatabaseMail()
        {
            InitializeComponent();

            //Setup Action combo box
            cboAction.Items.Add("Copy Accounts");
            cboAction.Items.Add("Copy Profiles");
            cboAction.SelectedItem = "Copy Accounts";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text) == true || string.IsNullOrEmpty(txtDestination.Text) == true)
                {
                    throw new Exception("Enter a Source and Destination Server!");
                }

                if (txtSource.Text == txtDestination.Text)
                {
                    throw new Exception("Source and destination cannot be the same!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                Server sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

                if (sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("Migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                List<String> itemsToCopy = txtItemToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                switch (cboAction.SelectedItem.ToString())
                {
                    case "Copy Accounts":
                        copyAccounts(sourceserver, destserver, itemsToCopy, chkDropDest.Checked);
                        break;

                    case "Copy Profiles":
                        copyProfiles(sourceserver, destserver, itemsToCopy, chkDropDest.Checked);
                        break;

                    default:
                        break;
                }
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyAccounts(Server sourceserver, Server destserver, List<string> itemsToCopy, bool dropDest)
        {
            MailAccountCollection sourceaccounts = sourceserver.Mail.Accounts;
            MailAccountCollection destaccounts = destserver.Mail.Accounts;
            try
            {
                foreach (MailAccount account in sourceaccounts)
                {
                    string accountname = account.Name;
                    if (sourceaccounts.Count > 0 && !sourceaccounts.Contains(accountname))
                    {
                        if (destaccounts.Contains(accountname))
                        {
                            if (!dropDest)
                            {
                                displayOutput(string.Format("Account {0} already exists on destination. Skipping", accountname));
                                continue;
                            }

                            destaccounts[accountname].Drop();
                            destaccounts.Refresh();
                        }

                        StringCollection sql = account.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        displayOutput(string.Format("Copied mail account {0}", accountname));
                    }
                }
            }
            catch (Exception ex)
            {
                displayOutput("Failed to copy account");
                displayOutput(ex.Message, true);
            }
        }

        private void copyProfiles(Server sourceserver, Server destserver, List<string> itemsToCopy, bool dropDest)
        {
            MailProfileCollection sourceprofiles = sourceserver.Mail.Profiles;
            MailProfileCollection destprofiles = destserver.Mail.Profiles;
            try
            {
                foreach (MailProfile profile in sourceprofiles)
                {
                    string profilename = profile.Name;
                    if (sourceprofiles.Count > 0 && !sourceprofiles.Contains(profilename))
                    {
                        if (destprofiles.Contains(profilename))
                        {
                            if (!dropDest)
                            {
                                displayOutput(string.Format("Profile {0} already exists on destination. Skipping", profilename));
                                continue;
                            }

                            destprofiles[profilename].Drop();
                            destprofiles.Refresh();
                        }

                        StringCollection sql = profile.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        displayOutput(string.Format("Copied mail profile {0}", profilename));
                    }
                }
            }
            catch (Exception ex)
            {
                displayOutput("Failed to copy profile");
                displayOutput(ex.Message, true);
            }
        }

        private void displayOutput(string message, bool errormessage = false)
        {
            if (errormessage)
            {
                txtOutput.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtOutput.ForeColor = System.Drawing.Color.Black;
            }

            if (txtOutput.Text.Length == 0)
            {
                txtOutput.Text = message;
            }
            else
            {
                txtOutput.AppendText("\r\n" + message);
            }
        }
    }
}
