using System;
using System.Data;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Security;

namespace DBAToolKit.Helpers
{
    public static class DBFunctions
    {
        private static string sql;
        private static object hashedpass;

        public static void KillConnections(Server destserver, string username)
        {
            DataTable processes = destserver.EnumProcesses(username);
            foreach (DataRow r in processes.Rows)
            {
                destserver.KillProcess(Int32.Parse(r["Spid"].ToString()));
            }
        }

        public static void ChangeDbOwner(Server destserver, string username)
        {
            var databases = destserver.Databases;
            foreach (Database db in databases)
            {
                if (db.Owner == username)
                {
                    db.SetOwner("sa");
                    db.Alter();
                }
            }
        }

        public static void ChangeJobOwner(Server destserver, string username)
        {
            var jobs = destserver.JobServer;
            foreach (Job j in jobs.Jobs)
            {
                if (j.OwnerLoginName == username)
                {
                    j.OwnerLoginName = "sa";
                    j.Alter();
                }
            }
        }

        public static SecureString GetHashedPassword (Server server, Login login)
        {
            if (server.VersionMajor == 9)
            {
                sql = "SELECT convert(varbinary(256),password_hash) as hashedpass FROM sys.sql_logins where name='" + login.Name + "'";
            }
            else
            {
                sql = "SELECT CAST(CONVERT(varchar(256), CAST(LOGINPROPERTY(name,'PasswordHash') AS varbinary (256)), 1) AS nvarchar(max)) as hashedpass FROM sys.server_principals WHERE principal_id = " + login.ID + "";
            }

            hashedpass = server.ConnectionContext.ExecuteScalar(sql);

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
    }
}
