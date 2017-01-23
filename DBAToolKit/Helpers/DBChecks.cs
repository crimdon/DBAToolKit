using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;

namespace DBAToolKit.Helpers
{
    public static class DBChecks
    {
        public static bool DatabaseExists(Server dbserver, string dbname)
        {
            foreach (Database db in dbserver.Databases)
            {
                if (db.Name.ToLower() == dbname.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool DatabaseUserExists(Database db, string user)
        {
            if (db.Status == DatabaseStatus.Normal)
            {
                foreach (User dbuser in db.Users)
                {
                    if (user.ToLower() == dbuser.Name.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool LoginExists(Server server, string login)
        {
            foreach (Login serverlogin in server.Logins)
                if (serverlogin.Name.ToLower() == login.ToLower())
                {
                    return true;
                }
            return false;
        }

        public static bool DBRoleExists(Database db, string rolename)
        {
            foreach (DatabaseRole role in db.Roles)
            {
                if (role.Name.ToLower() == rolename.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CredentialExists(Server dbserver, string credentialname)
        {
            foreach (Credential credential in dbserver.Credentials)
            {
                if (credential.Name.ToLower() == credentialname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ProxyExists(Server dbserver, string proxyname)
        {
            foreach (ProxyAccount proxy in dbserver.JobServer.ProxyAccounts)
            {
                if (proxy.Name.ToLower() == proxyname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool JobExists(Server dbserver, string jobname)
        {
            foreach (Job job in dbserver.JobServer.Jobs)
            {
                if (job.Name.ToLower() == jobname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool OperatorExists(Server dbserver, string operatorname)
        {
            foreach (Operator op in dbserver.JobServer.Operators)
            {
                if (op.Name.ToLower() == operatorname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AlertExists(Server dbserver, string alertname)

        {
            foreach (Alert alert in dbserver.JobServer.Alerts)
            {
                if (alert.Name.ToLower() == alertname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CategoryExists(Server dbserver, string categoryname)
        {
            foreach (JobCategory cat in dbserver.JobServer.JobCategories)
            {
                if (cat.Name.ToLower() == categoryname.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
