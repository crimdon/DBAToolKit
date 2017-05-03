using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Collections.Specialized;
using DBAToolKit.Models;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlAlerts : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_SqlAlerts()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true || string.IsNullOrEmpty(registeredServersDestination.SelectedServer) == true)
                {
                    throw new Exception("Enter a Source and Destination Server!");
                }

                if (registeredServersSource.SelectedServer == registeredServersDestination.SelectedServer)
                {
                    throw new Exception("Source and destination cannot be the same!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(registeredServersSource.SelectedServer);
                Server destserver = connection.Connect(registeredServersDestination.SelectedServer);

                if (sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("Migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                if (itemsToCopy.Count == 0)
                {
                    setupJobList();
                }

                DateTime started = DateTime.Now;
                showOutput.Clear();
                showOutput.displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyAlerts(destserver);
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyAlerts (Server destserver)
        {
            foreach (Alert alert in sourceserver.JobServer.Alerts)
            {
                string alertname = alert.Name;
                ItemToCopy item = itemsToCopy.Find(x => x.Name == alertname);
                if (item.IsChecked)
                {
                    if (DBChecks.AlertExists(destserver, alertname))
                    {
                        showOutput.displayOutput(string.Format("Alert {0} already exists in destination. Skipping.", alertname));
                        continue;
                    }

                    if (DBChecks.CategoryExists(destserver, alert.CategoryName))
                    {
                        showOutput.displayOutput(string.Format("Category {0} does not exist. Skipping copy of alert {1}.", alert.CategoryName, alertname));
                        continue;
                    }

                    try
                    {
                        StringCollection sql = alert.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.JobServer.Refresh();
                        showOutput.displayOutput(string.Format("Copied alert {0} to {1}", alertname, destserver.Name));
                    }
                    catch (Exception ex)
                    {
                        showOutput.displayOutput(ex.Message);
                        continue;
                    }
                }
            }
        }
        private void setupJobList()
        {
            foreach (Alert alert in sourceserver.JobServer.Alerts)
            {
                itemsToCopy.Add(new ItemToCopy(alert.Name, false));
            }
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true)
                {
                    throw new Exception("Enter a Source Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(registeredServersSource.SelectedServer);

                if (itemsToCopy.Count == 0)
                {
                    setupJobList();
                }

                SelectItemsToCopy form = new SelectItemsToCopy();
                form.ItemsToCopy = itemsToCopy;
                form.ShowDialog();
                itemsToCopy = form.ItemsToCopy;
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void registeredServersSource_SelectedServerChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
        }
    }
}
