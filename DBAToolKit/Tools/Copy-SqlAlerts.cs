﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Collections.Specialized;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlAlerts : UserControl
    {
        public Copy_SqlAlerts()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text) == true || string.IsNullOrEmpty(txtDestination.Text) == true)
                {
                    throw new Exception("Enter a Source and Destination Server!");
                }

                if (txtSource.Text == txtDestination.Text)
                {
                    throw new Exception("Source and destination cannot be the same!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                Server sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

                if (sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("Migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                List<String> alertsToCopy = txtAlertsToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyAlerts(sourceserver, destserver, alertsToCopy);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyAlerts (Server sourceserver, Server destserver, List<string> alertstocopy)
        {
            foreach (Alert alert in sourceserver.JobServer.Alerts)
            {
                string alertname = alert.Name;
                if (string.IsNullOrEmpty(alertstocopy[0]) || alertstocopy.Contains(alertname))
                {
                    if (DBChecks.AlertExists(destserver, alertname))
                    {
                        displayOutput(string.Format("Alert {0} already exists in destination. Skipping.", alertname));
                        continue;
                    }

                    if (DBChecks.CategoryExists(destserver, alert.CategoryName))
                    {
                        displayOutput(string.Format("Category {0} does not exist. Skipping copy of alert {1}.", alert.CategoryName, alertname));
                        continue;
                    }

                    try
                    {
                        StringCollection sql = alert.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.JobServer.Refresh();
                        displayOutput(string.Format("Copied alert {0} to {1}", alertname, destserver.Name));
                    }
                    catch (Exception ex)
                    {
                        displayOutput(ex.Message);
                        continue;
                    }
                }
            }
        }
        private void displayOutput(string message, bool errormessage = false)
        {
            if (errormessage)
            {
                txtOutput.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtOutput.ForeColor = System.Drawing.Color.Black;
            }

            if (txtOutput.Text.Length == 0)
            {
                txtOutput.Text = message;
            }
            else
            {
                txtOutput.AppendText("\r\n" + message);
            }
        }
    }
}