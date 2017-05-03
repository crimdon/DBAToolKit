using System;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo.Mail;
using System.Collections.Generic;
using System.Linq;
using DBAToolKit.Models;

namespace DBAToolKit.Tools
{
    public partial class Copy_DatabaseMail : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
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
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true || string.IsNullOrEmpty(registeredServersDestination.SelectedServer) == true)
                {
                    throw new Exception("Enter a Source and Destination Server!");
                }

                if (registeredServersSource.SelectedServer == registeredServersDestination.SelectedServer)
                {
                    throw new Exception("Source and destination cannot be the same!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(registeredServersSource.SelectedServer);
                Server destserver = connection.Connect(registeredServersDestination.SelectedServer);

                if (sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("Migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                DateTime started = DateTime.Now;
                showOutput.Clear();
                showOutput.displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                switch (cboAction.SelectedItem.ToString())
                {
                    case "Copy Accounts":
                        copyAccounts(destserver, chkDropDest.Checked);
                        break;

                    case "Copy Profiles":
                        copyProfiles(destserver, chkDropDest.Checked);
                        break;

                    default:
                        break;
                }
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyAccounts(Server destserver, bool dropDest)
        {
            MailAccountCollection sourceaccounts = sourceserver.Mail.Accounts;
            MailAccountCollection destaccounts = destserver.Mail.Accounts;
            SqlMail sm;
            sm = destserver.Mail;
            try
            {
                foreach (MailAccount account in sourceaccounts)
                {
                    string accountname = account.Name;
                    ItemToCopy item = itemsToCopy.Find(x => x.Name == accountname);
                    if (item.IsChecked)
                    {
                        if (destaccounts.Contains(accountname))
                        {
                            if (!dropDest)
                            {
                                showOutput.displayOutput(string.Format("Account {0} already exists on destination. Skipping", accountname));
                                continue;
                            }

                            destaccounts[accountname].Drop();
                            destaccounts.Refresh();
                        }

                        MailAccount newAccount = default(MailAccount);
                        newAccount = new MailAccount(sm, account.Name, account.Description, account.DisplayName, account.EmailAddress);
                        newAccount.ReplyToAddress = account.ReplyToAddress;
                        newAccount.Create();
                        newAccount.MailServers[0].Rename(account.MailServers[0].Name);
                        newAccount.Alter();

                        showOutput.displayOutput(string.Format("Copied mail account {0}", accountname));
                    }
                }
            }
            catch (Exception ex)
            {
                showOutput.displayOutput("Failed to copy account");
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyProfiles(Server destserver, bool dropDest)
        {
            MailProfileCollection sourceprofiles = sourceserver.Mail.Profiles;
            MailProfileCollection destprofiles = destserver.Mail.Profiles;
            try
            {
                foreach (MailProfile profile in sourceprofiles)
                {
                    string profilename = profile.Name;
                    ItemToCopy item = itemsToCopy.Find(x => x.Name == profilename);
                    if (item.IsChecked)
                    {
                        if (destprofiles.Contains(profilename))
                        {
                            if (!dropDest)
                            {
                                showOutput.displayOutput(string.Format("Profile {0} already exists on destination. Skipping", profilename));
                                continue;
                            }

                            destprofiles[profilename].Drop();
                            destprofiles.Refresh();
                        }

                        StringCollection sql = profile.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        showOutput.displayOutput(string.Format("Copied mail profile {0}", profilename));
                    }
                }
            }
            catch (Exception ex)
            {
                showOutput.displayOutput("Failed to copy profile");
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true)
                {
                    throw new Exception("Enter a Source Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(registeredServersSource.SelectedServer);

                if (itemsToCopy.Count == 0)
                {
                    switch (cboAction.SelectedItem.ToString())
                    {
                        case "Copy Accounts":
                            foreach (MailAccount account in sourceserver.Mail.Accounts)
                            {
                                itemsToCopy.Add(new ItemToCopy(account.Name, false));
                            }
                            break;

                        case "Copy Profiles":
                            foreach (MailProfile profile in sourceserver.Mail.Profiles)
                            {
                                itemsToCopy.Add(new ItemToCopy(profile.Name, false));
                            }
                            break;

                        default:
                            break;
                    }
                }

                SelectItemsToCopy form = new SelectItemsToCopy();
                form.ItemsToCopy = itemsToCopy;
                form.ShowDialog();
                itemsToCopy = form.ItemsToCopy;
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void cboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
        }

        private void registeredServersSource_SelectedServerChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
        }
    }
}
