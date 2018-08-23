using System;
using System.Data;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Security;
using System.Linq;

namespace DBAToolKit.Helpers
{
    public static class DBFunctions
    {
        public static void KillConnections(Server dbserver, string username)
        {
            DataTable processes = dbserver.EnumProcesses(username);
            foreach (DataRow r in processes.Rows)
            {
                dbserver.KillProcess(Int32.Parse(r["Spid"].ToString()));
            }
        }

        public static void ChangeDbOwner(Server dbserver, string username, string newname = "sa", string dbname = null)
        {
            var databases = dbserver.Databases;
            foreach (Database db in databases)
            {
                if (string.IsNullOrEmpty(dbname) || db.Name == dbname)
                {
                    if (db.Owner == username || string.IsNullOrEmpty(username))
                    {
                        db.SetOwner(newname);
                        db.Alter();
                    }
                }
            }
        }

        public static void ChangeJobOwner(Server dbserver, string username, string newname = "sa", string jobname = null)
        {
            var jobs = dbserver.JobServer;
            foreach (Job j in jobs.Jobs)
            {
                if (string.IsNullOrEmpty(jobname) || j.Name == jobname)
                {
                    if (j.OwnerLoginName == username)
                    {
                        j.OwnerLoginName = newname;
                        j.Alter();
                    }
                }
            }
        }

        public static SecureString GetHashedPassword(Server server, Login login)
        {
            string sql;
            if (server.VersionMajor == 9)
            {
                sql = "SELECT convert(varbinary(256),password_hash) as hashedpass FROM sys.sql_logins where name='" + login.Name + "'";
            }
            else
            {
                sql = "SELECT CAST(CONVERT(varchar(256), CAST(LOGINPROPERTY(name,'PasswordHash') AS varbinary (256)), 1) AS nvarchar(max)) as hashedpass FROM sys.server_principals WHERE principal_id = " + login.ID + "";
            }

            object hashedpass = server.ConnectionContext.ExecuteScalar(sql);

            if (hashedpass.GetType().Name != "String")
            {
                StringBuilder passtring = new StringBuilder();
                passtring.Append("0x");
                foreach (byte b in Utilities.ObjectToByteArray(hashedpass))
                {
                    passtring.AppendFormat("{0:X2}", b).ToString();
                }
                return Utilities.MakeSecureString(passtring.ToString());
            }
            else
            {
                return Utilities.MakeSecureString(hashedpass.ToString());
            }
        }

        public static void GrantDBPerms(Database sourcedb, Database destdb, string dbusername)
        {
            var sourceperms = sourcedb.EnumDatabasePermissions(dbusername);
            foreach (var perm in sourceperms)
            {
                DatabasePermissionSet permset = new DatabasePermissionSet(perm.PermissionType);
                PermissionState permstate = perm.PermissionState;
                bool grantwithgrant;
                if (permstate == PermissionState.GrantWithGrant)
                {
                    grantwithgrant = true;
                    permstate = PermissionState.Grant;
                }
                else
                {
                    grantwithgrant = false;
                }
                destdb.Grant(permset, dbusername, grantwithgrant);
            }
        }

        public static void RevokeDBPerms(Database sourcedb, Database destdb, string dbusername)
        {
            var destperms = destdb.EnumDatabasePermissions(dbusername);
            var sourceperms = sourcedb.EnumDatabasePermissions(dbusername);
            foreach (var perm in destperms)
            {
                PermissionState permstate = perm.PermissionState;
                var sourceperm = sourceperms.Where(p => p.PermissionState == perm.PermissionState && p.PermissionType == perm.PermissionType);

                if (sourceperm == null)
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
            }
        }

        public static void DropDBUser(Database sourcedb, Database destdb, string dbusername)
        {
            foreach (DatabaseRole destrole in destdb.Roles)
            {
                string destrolename = destrole.Name;
                DatabaseRole sourcerole = sourcedb.Roles[destrolename];

                if (!DBChecks.DBRoleExists(sourcedb, destrolename) && !sourcerole.EnumMembers().Contains(dbusername) && destrole.EnumMembers().Contains(dbusername))
                {
                    destrole.DropMember(dbusername);
                }

            }

            destdb.Users[dbusername].Drop();
        }

        public static void AddDBUser(Database db, string dbusername)
        {
            // Map the user
            if (!DBChecks.DatabaseUserExists(db, dbusername))
            {
                User dbuser = new User(db, dbusername);
                dbuser.Login = dbusername;
                dbuser.Create();
                dbuser.Refresh();
            }
        }

        public static void AddUserToDBRoles(Database sourcedb, Database destdb, string dbusername)
        {
            foreach (DatabaseRole role in sourcedb.Roles)
            {
                if (role.EnumMembers().Contains(dbusername))
                {
                    string rolename = role.Name;
                    DatabaseRole destdbrole = destdb.Roles[rolename];

                    if (DBChecks.DBRoleExists(destdb, rolename) && dbusername != "dbo" && !destdbrole.EnumMembers().Contains(dbusername))
                    {
                        destdbrole.AddMember(dbusername);
                        destdbrole.Alter();
                    }
                }
            }
        }

        public static void ChangeJobStatus(Server dbserver, string jobname, bool enabled)
        {
            dbserver.JobServer.Jobs.Refresh();
            foreach (Job job in dbserver.JobServer.Jobs)
            {
                if (job.Name.ToLower() == jobname.ToLower())
                {
                    job.IsEnabled = enabled;
                    job.Alter();
                }
            }
        }
    }
}
