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
                processLogins(sourceserver, destserver, chkForce.Checked, chksyncOnly.Checked, chksyncDatabasePerms.Checked, usersToCopy);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void processLogins(Server sourceserver, Server destserver, bool force, bool synconly, bool syncdatabaseperms, List<string> userstoprocess)
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

                if (syncdatabaseperms)
                {
                    syncDatabasePerms(sourcelogin, destlogin, sourceserver, destserver);
                }
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

        private void syncDatabasePerms(Login sourcelogin, Login destlogin, Server destserver, Server sourceserver)
        {
            foreach(DatabaseMapping dbmap in destlogin.EnumDatabaseMappings())
            {
                string dbname = dbmap.DBName;
                Database destdb = destserver.Databases[dbname];
                Database sourcedb = sourceserver.Databases[dbname];
                string dbusername = dbmap.UserName;
                string dbloginname = dbmap.LoginName;

                if(sourcedb != null && sourcedb.Users[dbusername] == null && destdb.Users[dbusername] != null)
                {

                    try
                    {
                        destdb.Users[dbusername].Drop();
                    }
                    catch (Exception ex)
                    {
                        displayOutput(string.Format("Failed to drop user {0} From {1} on destination.", dbusername, dbname));
                        displayOutput(ex.Message);
                    }


                    foreach(DatabaseRole destrole in destdb.Roles)
                    {
                        string destrolename = destrole.Name;
                        DatabaseRole sourcerole = sourcedb.Roles[destrolename];

                        if(sourcerole != null && !sourcerole.EnumMembers().Contains(dbusername) && destrole.EnumMembers().Contains(dbusername))
                        {
                            try
                            {
                                destrole.DropMember(dbusername);
                            }

                            catch (Exception ex)
                            {
                                displayOutput(string.Format("Failed to drop user {0} From {1} on destination.", dbusername, destrolename));
                                displayOutput(ex.Message, true);
                            }
                        }

                    }

                    var destperms = destdb.EnumDatabasePermissions(dbusername);
                    var sourceperms = sourcedb.EnumDatabasePermissions(dbusername);
                    foreach(var perm in destperms)
                    {
                        PermissionState permstate = perm.PermissionState;
                        var sourceperm = sourceperms.Where(p => p.PermissionState == perm.PermissionState && p.PermissionType == perm.PermissionType);

                        if(sourceperm == null)
                        {
                            try
                            {
                                bool grantwithgrant;
                                DatabasePermissionSet permset = new DatabasePermissionSet(perm.PermissionType);
                                if (permstate == PermissionState.GrantWithGrant)
                                {
                                    grantwithgrant = true;
                                    permstate = PermissionState.Grant;
                                }
                                else
                                {
                                    grantwithgrant = false;
                                }
                                destdb.Revoke(permset, dbusername, false, grantwithgrant);
                            }
                            catch (Exception ex)
                            {
                                displayOutput(string.Format("Failed to revoke permission {0} From {1} on destination.", perm.PermissionType, dbusername));
                                displayOutput(ex.Message, true);
                            }
                        }
                    }
                }
            }

            // Add the database mappings and permissions
            foreach(DatabaseMapping dbmap in sourcelogin.EnumDatabaseMappings())
            {
                string dbname = dbmap.DBName;
                Database destdb = destserver.Databases[dbname];
                Database sourcedb = sourceserver.Databases[dbname];
                string dbusername = dbmap.UserName;
                string dbloginname = dbmap.LoginName;

                // Map the user
                if (destdb == null && destdb.Users[dbusername] == null)
                {
                    try
                    {
                        User dbuser = new User(destdb, dbusername);
                        dbuser.Login = dbusername;
                        dbuser.Create();
                        dbuser.Refresh();
                    }
                    catch (Exception ex)
                    {
                        displayOutput(string.Format("Failed to create user {0} on database {1} on destination.", dbusername, dbname));
                        displayOutput(ex.Message, true);
                    }
                }

                //Change the owner
                if(sourcedb.Owner == dbusername)
                {
                    try
                    {
                        destdb.SetOwner(dbusername);
                    }
                    catch (Exception ex)
                    {
                        displayOutput(string.Format("Failed to change dbowner on database {0} to {1} on destination.", dbname, dbusername));
                        displayOutput(ex.Message, true);
                    }
                }

                //Map the roles
                foreach(DatabaseRole role in sourcedb.Roles)
                {
                    if (role.EnumMembers().Contains(dbusername))
                    {
                        string rolename = role.Name;
                        DatabaseRole destdbrole = destdb.Roles[rolename];

                        if (destdbrole != null && dbusername != "dbo" && !destdbrole.EnumMembers().Contains(dbusername))
                        {
                            try
                            {
                                destdbrole.AddMember(dbusername);
                                destdbrole.Alter();
                            }
                            catch (Exception ex)
                            {
                                displayOutput(string.Format("Failed to add user {0} to role {1} on database {3}", rolename, dbusername, dbname));
                                displayOutput(ex.Message, true);
                            }
                        }
                    }
                }

                //Map permissions
                var sourceperms = sourcedb.EnumDatabasePermissions(dbusername);
                foreach (var perm in sourceperms)
                {
                    DatabasePermissionSet permset = new DatabasePermissionSet(perm.PermissionType);
                    PermissionState permstate = perm.PermissionState;
                    bool grantwithgrant;
                    if(permstate == PermissionState.GrantWithGrant)
                    {
                        grantwithgrant = true;
                        permstate = PermissionState.Grant;
                    }
                    else
                    {
                        grantwithgrant = false;
                    }
                    try
                    {
                        destdb.Grant(permset, dbusername, grantwithgrant);
                    }
                    catch (Exception ex)
                    {
                        displayOutput(string.Format("Failed to grant {0} on database {1} for user {2}", perm.PermissionType, dbname, dbusername));
                        displayOutput(ex.Message, true);
                    }
                }
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
