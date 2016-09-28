using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DBAToolKit.Helpers
{
    class ConnectSqlServer
    {
        public Server Connect(string SqlServer, bool Connection = false)
        {
            try
            {
                Server server = new Server(SqlServer);
                if (Connection)
                {
                    server.ConnectionContext.ConnectTimeout = 2;
                }
                else
                {
                    server.ConnectionContext.ConnectTimeout = 3;
                }
                server.ConnectionContext.ApplicationName = "DBA Tools";
                server.ConnectionContext.Connect();

                if (!server.ConnectionContext.FixedServerRoles.ToString().Any("SysAdmin".Contains))
                {
                    throw new Exception("User is not a member of SysAdmin");
                }

                if (!Connection)
                {
                    if (server.VersionMajor == 8)
                    {
                        // 2000
                        server.SetDefaultInitFields(typeof(Database), "OverLappingIndexesReplicationOptions", "Collation", "CompatibilityLevel", "CreateDate", "ID", "IsAccessible", "IsFullTextEnabled", "IsUpdateable", "LastBackupDate", "LastDifferentialBackupDate", "LastLogBackupDate", "Name", "Owner", "PrimaryFilePath", "ReadOnly", "RecoveryModel", "Status", "Version");
                        server.SetDefaultInitFields(typeof(Login), "CreateDate", "DateLastModified", "DefaultDatabase", "DenyWindowsLogin", "IsSystemObject", "Language", "LanguageAlias", "LoginType", "Name", "Sid", "WindowsLoginAccessType");
                    }

                    else if (server.VersionMajor == 9 || server.VersionMajor == 10)
                    {
                        // 2005 and 2008
                        server.SetDefaultInitFields(typeof(Database), "ReplicationOptions", "BrokerEnabled", "Collation", "CompatibilityLevel", "CreateDate", "ID", "IsAccessible", "IsFullTextEnabled", "IsMirroringEnabled", "IsUpdateable", "LastBackupDate", "LastDifferentialBackupDate", "LastLogBackupDate", "Name", "Owner", "PrimaryFilePath", "ReadOnly", "RecoveryModel", "Status", "Trustworthy", "Version");
                        server.SetDefaultInitFields(typeof(Login), "AsymmetricKey", "Certificate", "CreateDate", "Credential", "DateLastModified", "DefaultDatabase", "DenyWindowsLogin", "ID", "IsDisabled", "IsLocked", "IsPasswordExpired", "IsSystemObject", "Language", "LanguageAlias", "LoginType", "MustChangePassword", "Name", "PasswordExpirationEnabled", "PasswordPolicyEnforced", "Sid", "WindowsLoginAccessType");
                    }

                    else
                    {
                        // 2012 and above
                        server.SetDefaultInitFields(typeof(Database), "ReplicationOptions", "ActiveConnections", "AvailabilityDatabaseSynchronizationState", "AvailabilityGroupName", "BrokerEnabled", "Collation", "CompatibilityLevel", "ContainmentType", "CreateDate", "ID", "IsAccessible", "IsFullTextEnabled", "IsMirroringEnabled", "IsUpdateable", "LastBackupDate", "LastDifferentialBackupDate", "LastLogBackupDate", "Name", "Owner", "PrimaryFilePath", "ReadOnly", "RecoveryModel", "Status", "Trustworthy", "Version");
                        server.SetDefaultInitFields(typeof(Login), "AsymmetricKey", "Certificate", "CreateDate", "Credential", "DateLastModified", "DefaultDatabase", "DenyWindowsLogin", "ID", "IsDisabled", "IsLocked", "IsPasswordExpired", "IsSystemObject", "Language", "LanguageAlias", "LoginType", "MustChangePassword", "Name", "PasswordExpirationEnabled", "PasswordHashAlgorithm", "PasswordPolicyEnforced", "Sid", "WindowsLoginAccessType");
                    }
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
