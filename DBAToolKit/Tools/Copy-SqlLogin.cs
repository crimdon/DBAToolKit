using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Collections.Specialized;
using System.Security;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlLogin : UserControl
    {
        private string sql;

        public Copy_SqlLogin()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            displayOutput("Attempting to connect to SQL Servers");
            try
            {
                ConnectSqlServer connection = new ConnectSqlServer();
                Server sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("SQL Login migration FROM SQL Server version {0} to {1} not supported", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                if (sourceserver.VersionMajor < 8 || destserver.VersionMajor < 8)
                {
                    throw new Exception("SQL Server 7 and below not supported");
                }

                Stopwatch elapsed = new Stopwatch();
                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", started.ToShortTimeString()));
                copyLogin(sourceserver, destserver, chkForce.Checked);
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message);
            }
        }

        private void displayOutput(string message)
        {
            if (txtOutput.Text.Length == 0)
            {
                txtOutput.Text = message;
            }
            else
            {
                txtOutput.AppendText("\r\n" + message);
            }
        }

        private void copyLogin (Server sourceserver, Server destserver, bool force)
        {
            foreach (Login sourcelogin in sourceserver.Logins)
            {
                try
                {
                    string username = sourcelogin.Name;
                    string currentlogin = sourceserver.ConnectionContext.TrueLogin;
                    string servername = sourceserver.NetName;
                    Login destlogin = destserver.Logins[username];

                    if(username.StartsWith("##") || username == "sa" || username == "distributor_admin")
                    {
                        displayOutput(string.Format("Skipping {0}.", username));
                        continue;
                    }

                    if (destlogin != null && !force)
                    {
                        displayOutput(string.Format("{0} already exists in destination. Use -force to drop and recreate.", username));
                        continue;
                    }

                    if (currentlogin == username && force)
                    {
                        displayOutput("Cannot drop login performing the migration. Skipping");
                        continue;
                    }

                    string userbase = username.Split('\\')[0].ToLower();
                    if (servername == userbase || username.StartsWith("NT ") || username.StartsWith("BUILTIN)"))
                    {
                        displayOutput(string.Format("{0} is skipped because it is a local machine name.", username));
                        continue;
                    }

                    if (sourcelogin.LoginType != LoginType.SqlLogin && sourcelogin.LoginType != LoginType.WindowsUser && sourcelogin.LoginType != LoginType.WindowsGroup)
                    {
                        displayOutput(string.Format("{0} logins are not support. Skipping login {1}.", sourcelogin.LoginType.ToString(), username));
                        continue;
                    }

                    if (destlogin != null && force)
                    {
                        if (username == destserver.ServiceAccount)
                        {
                            displayOutput(string.Format("{0} is the destiation service account. Skipping drop.", username));
                            continue;
                        }                 
                    }

                    displayOutput(string.Format("Attempting to add {0} to {1}.", username, destserver.Name));
                    destlogin = new Login(destserver, username);

                    destlogin.Sid = sourcelogin.Sid;
                    destlogin.Language = sourcelogin.Language;

                    string defaultdb = sourcelogin.DefaultDatabase;
                    if (destserver.Databases[defaultdb] == null)
                    {
                        defaultdb = "master";
                    }
                    destlogin.DefaultDatabase = defaultdb;

                    destlogin.PasswordPolicyEnforced = sourcelogin.PasswordPolicyEnforced;
                    destlogin.PasswordExpirationEnabled = sourcelogin.PasswordExpirationEnabled;

                    if (sourcelogin.LoginType == LoginType.SqlLogin)
                    {
                        destlogin.LoginType = LoginType.SqlLogin;
                        destlogin.Name = sourcelogin.Name;

                        SecureString hashedpass = DBFunctions.GetHashedPassword(sourceserver, sourcelogin);

                        destlogin.Create(hashedpass, LoginCreateOptions.IsHashed);
                        destlogin.Refresh();
                        displayOutput(string.Format("Successfully added {0} to {1}.", username, destserver.Name));
                    }

                    else if (sourcelogin.LoginType == LoginType.WindowsUser || sourcelogin.LoginType == LoginType.WindowsGroup)
                    {
                        destlogin.LoginType = sourcelogin.LoginType;
                        destlogin.Name = sourcelogin.Name;

                        destlogin.Create();
                        destlogin.Refresh();
                        displayOutput(string.Format("Successfully added {0} to {1}.", username, destserver.Name));
                    }
                }

                catch (Exception ex)
                {
                    displayOutput("Error processing user.");
                    displayOutput(ex.Message);
                }
            }

        }

        private void dropUser (Server destserver, string username)
        {
            //DBFunctions.KillConnections(destserver, username);
            //DBFunctions.ChangeDbOwner(destserver, username);
            //DBFunctions.ChangeJobOwner(destserver, username);
            displayOutput(string.Format("Dropping {0}", username));
            //destlogin.Drop(); 
        }

    }
}
