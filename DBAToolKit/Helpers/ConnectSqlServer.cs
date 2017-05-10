using System;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

namespace DBAToolKit.Helpers
{
    class ConnectSqlServer
    {
        public Server Connect(string SqlServer)
        {
            try
            {
                RegistryKey ProgSettings = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
                Server server = new Server(SqlServer);
                server.ConnectionContext.ConnectionString = ProgSettings.GetValue(SqlServer, false).ToString();
                server.ConnectionContext.Connect();

                if (!server.ConnectionContext.FixedServerRoles.ToString().Any("SysAdmin".Contains))
                {
                    throw new Exception("User is not a member of sysadm");
                }
                ProgSettings.Close();
                return server;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
