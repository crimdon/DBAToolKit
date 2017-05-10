using Microsoft.Win32;
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
            RegistryKey ProgSettings = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
            string serverToAdd = ProgSettings.GetValue(serverName, false).ToString();
            if (serverToAdd == "False")
            {
                ProgSettings.SetValue(serverName, conn);
                ProgSettings.Close();
            }
            else
            {
                ProgSettings.Close();
                throw new Exception("Server already exists.");
            }
        }
        public static void deleteConnectionString(string serverName)
        {
            RegistryKey ProgSettings = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
            ProgSettings.DeleteValue(serverName);
            ProgSettings.Close();
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
