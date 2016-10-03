using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Security;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlLogin : UserControl
    {

        public Copy_SqlLogin()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            displayOutput("Attempting to connect to SQL Servers...");
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

                if(sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("SQL Login migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                if (chkForce.Checked && chksyncOnly.Checked)
                {
                    throw new Exception("Force Copy cannot be selected with Syncronise Permissions Only!");
                }

                List<String> usersToCopy = txtUsersToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                processLogins(sourceserver, destserver, chkForce.Checked, chksyncOnly.Checked, usersToCopy);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void processLogins(Server sourceserver, Server destserver, bool force, bool synconly, List<string> userstoprocess)
        {
            foreach (Login sourcelogin in sourceserver.Logins)
            {
                string username = sourcelogin.Name;
                string currentlogin = sourceserver.ConnectionContext.TrueLogin;
                string servername = sourceserver.NetName.ToLower();
                Login destlogin = destserver.Logins[username];

                if (userstoprocess.Count > 0 && !userstoprocess.Contains(username))
                {
                    continue;
                }

                if (username.StartsWith("##") || username == "sa" || username == "distributor_admin")
                {
                    displayOutput(string.Format("Skipping {0}.", username));
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
                    dropUser(destserver, destlogin, username);
                }

                if (destlogin == null && synconly)
                {
                    displayOutput(string.Format("{0} does not exist on destination. Skipping sync.", username));
                    continue;
                }

                if (destlogin != null && !force && !synconly)
                {
                    displayOutput(string.Format("{0} already exists in destination. Select Force Copy to drop and recreate.", username));
                    continue;
                }

                if (!synconly)
                {
                    copyLogin(sourceserver, destserver, sourcelogin, destlogin);
                }
                syncPermissions(sourceserver, destserver, username);
            }
        }
                
        private void copyLogin(Server sourceserver, Server destserver, Login sourcelogin, Login destlogin)
        {
            try
            {
                string username = sourcelogin.Name;
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

                if (sourcelogin.LoginType == LoginType.SqlLogin)
                {
                    destlogin.LoginType = LoginType.SqlLogin;
                    destlogin.Name = sourcelogin.Name;
                    destlogin.PasswordPolicyEnforced = sourcelogin.PasswordPolicyEnforced;
                    destlogin.PasswordExpirationEnabled = sourcelogin.PasswordExpirationEnabled;

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

                else
                {
                    throw new Exception("User type is not supported");
                }
            }

            catch (Exception ex)
            {
                displayOutput("Error processing user!", true);
                displayOutput(ex.Message);
            }
        }

        private void dropUser (Server dbserver, Login serverlogin, string username)
        {
            DBFunctions.KillConnections(dbserver, username);
            DBFunctions.ChangeDbOwner(dbserver, username);
            DBFunctions.ChangeJobOwner(dbserver, username);
            displayOutput(string.Format("Dropping {0} from destination server.", username));
            serverlogin.Drop(); 
        }

        private StringCollection destrolemembers;
        private void syncPermissions(Server sourceserver, Server destserver, string username)
        {
            foreach (ServerRole role in sourceserver.Roles)
            {
                string rolename = role.Name;
                ServerRole destrole = destserver.Roles[rolename];
                var sourcerolemembers = role.EnumMemberNames();

                if (destrole != null)
                {
                    destrolemembers = destrole.EnumMemberNames();
                    if (sourcerolemembers.Contains(username) && !destrolemembers.Contains(username))
                    {
                        destrole.AddMember(username);
                        displayOutput(string.Format("Added user {0} to role {1} on destination server", username, destrole.Name));
                    }

                    if (!sourcerolemembers.Contains(username) && destrolemembers.Contains(username))
                    {
                        destrole.DropMember(username);
                        displayOutput(string.Format("Removed user {0} to role {1} on destination server", username, destrole.Name));
                    }
                }
            }

            //First remove all permissions from destination server
            foreach (ServerPermissionInfo perm in destserver.EnumServerPermissions(username))
            {
                string permstate = perm.PermissionState.ToString();
                bool grantwithgrant = false;
                if (permstate == "GrantWithGrant")
                {
                    grantwithgrant = true;
                    permstate = "grant";
                }
                else
                {
                    grantwithgrant = false;
                }
                ServerPermissionSet permset = new ServerPermissionSet(perm.PermissionType);
                destserver.Revoke(permset, username, false, grantwithgrant);
                displayOutput(string.Format("Successfully revoked {0} to {1} on destination server", permstate, username));
            }
            //Copy permissions from source server
            foreach (ServerPermissionInfo perm in sourceserver.EnumServerPermissions(username))
            {
                string permstate = perm.PermissionState.ToString();
                bool grantwithgrant = false;
                if (permstate == "GrantWithGrant")
                {
                    grantwithgrant = true;
                    permstate = "grant";
                }
                else
                {
                    grantwithgrant = false;
                }

                ServerPermissionSet permset = new ServerPermissionSet(perm.PermissionType);
                destserver.Grant(permset, username, grantwithgrant);
                displayOutput(string.Format("Successfully performed {0} to {1} on destination server", permstate, username));
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
