using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlLogin : UserControl
    {
        public Copy_SqlLogin()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            displayOutput("Attempting to connect to SQL Servers");
            try
            {
                ConnectSqlServer connection = new ConnectSqlServer();
                Server sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("SQL Login migration FROM SQL Server version {0} to {1} not supported", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                if (sourceserver.VersionMajor < 8 || destserver.VersionMajor < 8)
                {
                    throw new Exception("SQL Server 7 and below not supported");
                }

                Stopwatch elapsed = new Stopwatch();
                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", started.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message);
            }

            


        }

        private void displayOutput(string message)
        {
            if (txtOutput.Text.Length == 0)
            {
                txtOutput.Text = message;
            }
            else
            {
                txtOutput.AppendText("\r\n" + message);
            }
        }

        private void copyLogin (Server sourceserver, Server destserver, bool force)
        {
            foreach (Login sourcelogin in sourceserver.Logins)
            {
                string username = sourcelogin.Name;
                string currentlogin = sourceserver.ConnectionContext.TrueLogin;
                string servername = sourceserver.NetName;

                if (currentlogin == username && force)
                {
                    displayOutput("Cannot drop login performing the migration. Skipping");
                    continue;
                }

                string userbase = username.Split('\\')[0].ToLower();
                if (servername == userbase || username.StartsWith("NT "))
                {
                    displayOutput(string.Format("{0} is skipped because it is a local machine name.", username));
                    continue;
                }

                if (force)
                {
                    if (username == destserver.ServiceAccount)
                    {
                        displayOutput(string.Format("{0} is the destiation service account. Skipping drop.", username));
                        continue;
                    }


                }

            }

        }
    }
}
