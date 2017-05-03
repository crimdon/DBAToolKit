using System;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using System.Configuration;

namespace DBAToolKit.Helpers
{
    class ConnectSqlServer
    {
        public Server Connect(string SqlServer)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionString = config.ConnectionStrings.ConnectionStrings[SqlServer];
                Server server = new Server(SqlServer);
                server.ConnectionContext.ConnectionString = connectionString.ConnectionString;
                server.ConnectionContext.Connect();

                if (!server.ConnectionContext.FixedServerRoles.ToString().Any("SysAdmin".Contains))
                {
                    throw new Exception("User is not a member of sysadm");
                }

                return server;
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
