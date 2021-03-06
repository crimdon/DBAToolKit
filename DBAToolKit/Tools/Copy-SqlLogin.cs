﻿using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Security;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using DBAToolKit.Models;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlLogin : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_SqlLogin()
        {
            InitializeComponent();

            //Setup Action combo box
            cmbAction.Items.Add("Normal Copy");
            cmbAction.Items.Add("Normal Copy with Database Permission Sync");
            cmbAction.Items.Add("Forced Copy");
            cmbAction.Items.Add("Forced Copy with Database Permission Sync");
            cmbAction.Items.Add("Sync Logon Permissions Only");
            cmbAction.Items.Add("Sync Database Permission Only");
            cmbAction.SelectedItem = "Normal Copy";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            showOutput.displayOutput("Attempting to connect to SQL Servers...");
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

                if (itemsToCopy.Count == 0)
                {
                    setupJobList();
                }

                DateTime started = DateTime.Now;
                showOutput.Clear();
                showOutput.displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                processLogins(destserver, cmbAction.SelectedItem.ToString());
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
}

        private void processLogins(Server destserver, string action)
        {
            foreach (Login sourcelogin in sourceserver.Logins)
            {
                string username = sourcelogin.Name;
                Login destlogin = destserver.Logins[username];
                string currentlogin = sourceserver.ConnectionContext.TrueLogin;
                string servername = sourceserver.NetName.ToLower();
                ItemToCopy item = new ItemToCopy();
                var checkitem = itemsToCopy.FirstOrDefault(x => x.Name == username);
                if (checkitem == null)
                    item.IsChecked = false;
                else
                    item = (ItemToCopy)checkitem;

                if (item.IsChecked)
                {
                    if (username.StartsWith("##") || username == "sa" || username == "distributor_admin")
                    {
                        showOutput.displayOutput(string.Format("Skipping {0}.", username));
                        continue;
                    }

                    if (currentlogin == username && action.StartsWith("Force"))
                    {
                        showOutput.displayOutput("Cannot drop login performing the migration. Skipping");
                        continue;
                    }

                    string userbase = username.Split('\\')[0].ToLower();
                    if (servername == userbase || username.StartsWith("NT ") || username.StartsWith("BUILTIN)"))
                    {
                        showOutput.displayOutput(string.Format("{0} is skipped because it is a local machine name.", username));
                        continue;
                    }

                    if (sourcelogin.LoginType != LoginType.SqlLogin && sourcelogin.LoginType != LoginType.WindowsUser && sourcelogin.LoginType != LoginType.WindowsGroup)
                    {
                        showOutput.displayOutput(string.Format("{0} logins are not support. Skipping login {1}.", sourcelogin.LoginType.ToString(), username));
                        continue;
                    }

                    if (destlogin != null && action.StartsWith("Force") && username == destserver.ServiceAccount)
                    {
                        showOutput.displayOutput(string.Format("{0} is the destiation service account. Skipping drop.", username));
                        continue;
                    }

                    if (destlogin == null && action.StartsWith("Sync"))
                    {
                        showOutput.displayOutput(string.Format("{0} does not exist on destination. Skipping sync.", username));
                        continue;
                    }

                    if (destlogin != null && action.StartsWith("Normal"))
                    {
                        showOutput.displayOutput(string.Format("{0} already exists in destination. Select Force Copy to drop and recreate.", username));
                        continue;
                    }

                    switch (action)
                    {
                        case "Normal Copy":
                            copyLogin(sourceserver, destserver, sourcelogin, destlogin);
                            syncPermissions(sourceserver, destserver, username);
                            break;

                        case "Normal Copy with Database Permission Sync":
                            copyLogin(sourceserver, destserver, sourcelogin, destlogin);
                            syncPermissions(sourceserver, destserver, username);
                            destlogin = destserver.Logins[username];
                            syncDatabasePerms(sourcelogin, destlogin, sourceserver, destserver);
                            break;

                        case "Forced Copy":
                            dropUser(destserver, destlogin, username);
                            copyLogin(sourceserver, destserver, sourcelogin, destlogin);
                            syncPermissions(sourceserver, destserver, username);
                            break;

                        case "Forced Copy with Database Permission Sync":
                            dropUser(destserver, destlogin, username);
                            copyLogin(sourceserver, destserver, sourcelogin, destlogin);
                            syncPermissions(sourceserver, destserver, username);
                            destlogin = destserver.Logins[username];
                            syncDatabasePerms(sourcelogin, destlogin, sourceserver, destserver);
                            break;

                        case "Sync Logon Permissions Only":
                            syncPermissions(sourceserver, destserver, username);
                            break;

                        case "Sync Database Permission Only":
                            syncDatabasePerms(sourcelogin, destlogin, sourceserver, destserver);
                            break;

                        default:
                            showOutput.displayOutput("No Action selected", true);
                            break;
                    }
                }
            }
        }
                
        private void copyLogin(Server sourceserver, Server destserver, Login sourcelogin, Login destlogin)
        {
            try
            {
                string username = sourcelogin.Name;
                showOutput.displayOutput(string.Format("Attempting to add {0} to {1}.", username, destserver.Name));
                destlogin = new Login(destserver, username);

                destlogin.Sid = sourcelogin.Sid;
                destlogin.Language = sourcelogin.Language;

                string defaultdb = sourcelogin.DefaultDatabase;
                if (destserver.Databases[defaultdb] == null || destserver.Databases[defaultdb].Status != DatabaseStatus.Normal)
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
                    showOutput.displayOutput(string.Format("Successfully added {0} to {1}.", username, destserver.Name));
                }

                else if (sourcelogin.LoginType == LoginType.WindowsUser || sourcelogin.LoginType == LoginType.WindowsGroup)
                {
                    destlogin.LoginType = sourcelogin.LoginType;
                    destlogin.Name = sourcelogin.Name;

                    destlogin.Create();
                    destlogin.Refresh();
                    showOutput.displayOutput(string.Format("Successfully added {0} to {1}.", username, destserver.Name));
                }

                else
                {
                    throw new Exception("User type is not supported");
                }
            }

            catch (Exception ex)
            {
                showOutput.displayOutput("Error copying user!", true);
                showOutput.displayOutput(ex.Message);
            }
        }

        private void dropUser (Server dbserver, Login serverlogin, string username)
        {
            DBFunctions.KillConnections(dbserver, username);
            DBFunctions.ChangeDbOwner(dbserver, username);
            DBFunctions.ChangeJobOwner(dbserver, username);
            showOutput.displayOutput(string.Format("Dropping {0} from destination server.", username));
            serverlogin.Drop(); 
        }

        private void syncPermissions(Server sourceserver, Server destserver, string username)
        {
            foreach (ServerRole role in sourceserver.Roles)
            {
                string rolename = role.Name;
                ServerRole destrole = destserver.Roles[rolename];
                var sourcerolemembers = role.EnumMemberNames();
                StringCollection destrolemembers = new StringCollection();

                if (destrole != null)
                {
                    destrolemembers = destrole.EnumMemberNames();
                    if (sourcerolemembers.Contains(username) && !destrolemembers.Contains(username))
                    {
                        destrole.AddMember(username);
                        showOutput.displayOutput(string.Format("Added user {0} to role {1} on destination server", username, destrole.Name));
                    }

                    if (!sourcerolemembers.Contains(username) && destrolemembers.Contains(username))
                    {
                        destrole.DropMember(username);
                        showOutput.displayOutput(string.Format("Removed user {0} to role {1} on destination server", username, destrole.Name));
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
                showOutput.displayOutput(string.Format("Successfully revoked {0} to {1} on destination server", permstate, username));
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
                showOutput.displayOutput(string.Format("Successfully performed {0} to {1} on destination server", permstate, username));
            }

        }

        private void syncDatabasePerms(Login sourcelogin, Login destlogin, Server sourceserver, Server destserver)
        {
            // Remove user from destination if it does not exist on source
            if (destlogin.EnumDatabaseMappings() != null)
            {
                foreach (DatabaseMapping dbmap in destlogin.EnumDatabaseMappings())
                {
                    string dbname = dbmap.DBName;
                    Database destdb = destserver.Databases[dbname];
                    Database sourcedb = sourceserver.Databases[dbname];
                    string dbusername = dbmap.UserName;
                    string dbloginname = dbmap.LoginName;

                    if (DBChecks.DatabaseExists(sourceserver, destdb.Name) &&
                        !DBChecks.DatabaseUserExists(sourcedb, dbusername) && DBChecks.DatabaseUserExists(destdb, dbusername))
                    {

                        try
                        {
                            DBFunctions.DropDBUser(sourcedb, destdb, dbusername);
                        }
                        catch (Exception ex)
                        {
                            showOutput.displayOutput(string.Format("Failed to drop user {0} From {1} on destination.", dbusername, dbname), true);
                            showOutput.displayOutput(ex.Message);
                        }

                        try
                        {
                            DBFunctions.RevokeDBPerms(sourcedb, destdb, dbusername);
                        }
                        catch (Exception ex)
                        {
                            showOutput.displayOutput(string.Format("Failed to revoke permission for user {0} on {1}.", dbusername, dbname), true);
                            showOutput.displayOutput(ex.Message, true);
                        }
                    }
                }
            }

            // Add the database mappings and permissions
            {
                if (sourcelogin.EnumDatabaseMappings() != null)
                    foreach (DatabaseMapping dbmap in sourcelogin.EnumDatabaseMappings())
                    {
                        string dbname = dbmap.DBName;
                        Database destdb = destserver.Databases[dbname];
                        Database sourcedb = sourceserver.Databases[dbname];
                        string dbusername = dbmap.UserName;
                        string dbloginname = dbmap.LoginName;

                        // Only if database exists on destination and its status is normal
                        if (DBChecks.DatabaseExists(destserver, sourcedb.Name) &&
                            DBChecks.LoginExists(destserver, dbloginname) && !DBChecks.DatabaseUserExists(destdb, dbusername)
                            && destdb.Status == DatabaseStatus.Normal)
                        {
                            // Add DB User
                            try
                            {
                                DBFunctions.AddDBUser(destdb, dbusername);
                            }
                            catch (Exception ex)
                            {
                                showOutput.displayOutput(string.Format("Failed to add user {0} to database {1}", dbusername, dbname), true);
                                showOutput.displayOutput(ex.Message, true);
                            }

                            //Change the owner
                            if (sourcedb.Owner == dbusername)
                            {
                                DBFunctions.ChangeDbOwner(destserver, null, dbusername, dbname);
                            }

                            //Map the roles
                            try
                            {
                                DBFunctions.AddUserToDBRoles(sourcedb, destdb, dbusername);
                            }
                            catch (Exception ex)
                            {
                                showOutput.displayOutput(string.Format("Error adding user {0} to role on database {1}", dbusername, dbname), true);
                                showOutput.displayOutput(ex.Message, true);
                            }

                            //Map permissions

                            try
                            {
                                DBFunctions.GrantDBPerms(sourcedb, destdb, dbusername);
                            }
                            catch (Exception ex)
                            {
                                showOutput.displayOutput(string.Format("Error granting permission for user {0} on database {1}", dbusername, dbname), true);
                                showOutput.displayOutput(ex.Message, true);
                            }
                        }
                        showOutput.displayOutput(string.Format("Database permissions synced for user {0} on database {1}", dbusername, dbname));
                    }
            }
        }
        private void setupJobList()
        {
            foreach (Login sourcelogin in sourceserver.Logins)
            {
                string username = sourcelogin.Name;
                if (username.StartsWith("##") || username == "sa" || username == "distributor_admin")
                {
                    continue;
                }

                if (sourcelogin.LoginType != LoginType.SqlLogin && sourcelogin.LoginType != LoginType.WindowsUser && sourcelogin.LoginType != LoginType.WindowsGroup)
                {
                    continue;
                }

                itemsToCopy.Add(new ItemToCopy(sourcelogin.Name, false));
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
                    setupJobList();
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

        private void registeredServersSource_SelectedServerChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
        }
    }
}
