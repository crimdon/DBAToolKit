using System;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Models;

namespace DBAToolKit.Helpers
{
    class ConnectSqlServer
    {
        public Server Connect(string SqlServer)
        {
            try
            {
                using (var dbCtx = new ConfigDBContainer())
                {
                    var dbServer = dbCtx.Servers
                            .Where(s => s.ServerName == SqlServer)
                            .FirstOrDefault();
                    Server server = new Server(SqlServer);
                    server.ConnectionContext.ConnectionString = dbServer.ConnectionString;
                    server.ConnectionContext.Connect();

                    if (!server.ConnectionContext.FixedServerRoles.ToString().Any("SysAdmin".Contains))
                    {
                        throw new Exception("User is not a member of sysadm");
                    }

                    return server;
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
