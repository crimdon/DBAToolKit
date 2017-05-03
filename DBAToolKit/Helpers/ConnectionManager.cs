using System;
using System.Configuration;
using System.Data.SqlClient;


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
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionString = config.ConnectionStrings.ConnectionStrings[serverName];
            if (connectionString == null)
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings
                {
                    Name = serverName,
                    ConnectionString = conn,
                    ProviderName = "System.Data.SqlClient"
                });
                config.Save(ConfigurationSaveMode.Full);
            }
            else
            {
                connectionString.ConnectionString = conn;
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
    }
}
