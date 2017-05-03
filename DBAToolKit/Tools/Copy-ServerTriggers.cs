using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using DBAToolKit.Models;

namespace DBAToolKit.Tools
{
    public partial class Copy_ServerTriggers : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_ServerTriggers()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            showOutput.displayOutput("Attempting to connect to SQL Servers...");
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
                copyServerTriggers(destserver, chkDisableOnSource.Checked, chkDisableOnDest.Checked, chkDropDest.Checked);
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyServerTriggers(Server destserver, bool disableonsource, bool disableondest, bool dropdest)
        {
            ServerDdlTriggerCollection desttriggers = destserver.Triggers;

            foreach (ServerDdlTrigger trigger in sourceserver.Triggers)
            {
                string triggername = trigger.Name;
                ItemToCopy item = itemsToCopy.Find(x => x.Name == triggername);
                if (item.IsChecked)
                {
                    if (desttriggers[triggername] != null)                        
                    {
                        if (!dropdest)
                        {
                            showOutput.displayOutput(string.Format("Trigger {0} already exists in destination. Skipping.", triggername));
                            continue;
                        }
                        try
                        {
                            showOutput.displayOutput(string.Format("Dropping trigger {0}.", triggername));
                            ServerDdlTrigger desttrigger = desttriggers[triggername];
                            desttriggers[triggername].Drop();
                            desttriggers.Refresh();
                        }
                        catch (Exception ex)
                        {
                            showOutput.displayOutput(ex.Message);
                            continue;
                        }
                    }

                    try
                    {
                        StringCollection sql = trigger.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.Triggers.Refresh();
                        showOutput.displayOutput(string.Format("Copied trigger {0} to {1}", triggername, destserver.Name));

                        if (disableonsource)
                        {
                            trigger.IsEnabled = false;
                            trigger.Alter();
                        }

                        if(disableondest)
                        {
                            destserver.Triggers[triggername].IsEnabled = false;
                            destserver.Triggers[triggername].Alter();
                            desttriggers.Refresh();
                        }

                    }
                    catch (Exception ex)
                    {
                        showOutput.displayOutput(string.Format("Error copying trigger {0} to {1}", triggername, destserver.Name));
                        showOutput.displayOutput(ex.Message);
                        continue;
                    }
                }
            }
        }

        private void setupJobList()
        {
            foreach (ServerDdlTrigger trigger in sourceserver.Triggers)
            {
                itemsToCopy.Add(new ItemToCopy(trigger.Name, false));
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
