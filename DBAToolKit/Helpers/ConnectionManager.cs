using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using DBAToolKit.Models;


namespace DBAToolKit.Helpers
{
    class ConnectionManager
    {
        public string makeConnectionString(string strServerName, int authenticationType, string userName, string password, string strDatabaseName)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = strServerName;
            switch (authenticationType)
            {
                case 0:
                    connectionString.IntegratedSecurity = true;
                    break;
                case 1:
                    connectionString.UserID = userName;
                    connectionString.Password = password;
                    break;
            }
            connectionString.InitialCatalog = strDatabaseName;
            connectionString.ConnectTimeout = 3;
            connectionString.ApplicationName = "DBA Toolkit";
            return connectionString.ConnectionString;
        }
        public void saveConnectionString(string serverName, string conn)
        {
            using (var dbCtx = new ConfigDBContainer())
            {
                var serverToAdd = dbCtx.Servers.FirstOrDefault(s => s.ServerName == serverName);
                if (serverToAdd == null)
                {
                    dbCtx.Servers.Add(new Servers { ServerName = serverName, ConnectionString = conn });
                    dbCtx.SaveChanges();
                }
                else
                {
                    throw new Exception("Server already exists. Delete first!");
                }
            }   
        }
        public static void deleteConnectionString(string serverName)
        {
            using (var dbCtx = new ConfigDBContainer())
            {
                var serverToRemove = dbCtx.Servers.FirstOrDefault(s => s.ServerName == serverName);
                dbCtx.Servers.Remove(serverToRemove);
                dbCtx.SaveChanges();
            }
        }
        public bool testConnection(string conSTR)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conSTR))
                {
                    sqlConn.Open();
                    sqlConn.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void AddConnectionStringSettings(Configuration config, ConnectionStringSettings conStringSettings)
        {
            ConnectionStringsSection connectionStringsSection = config.ConnectionStrings;
            connectionStringsSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
            connectionStringsSection.ConnectionStrings.Add(conStringSettings);
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
