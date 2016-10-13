using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;

namespace DBAToolKit.Tools
{
    public partial class Copy_ServerTriggers : UserControl
    {
        public Copy_ServerTriggers()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            displayOutput("Attempting to connect to SQL Servers...");
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

                List<String> objectsToCopy = txtObjectsToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyServerTriggers(sourceserver, destserver, objectsToCopy, chkDisableOnSource.Checked, chkDisableOnDest.Checked, chkDropDest.Checked);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyServerTriggers(Server sourceserver, Server destserver, List<string> objectstocopy, bool disableonsource, bool disableondest, bool dropdest)
        {
            ServerDdlTriggerCollection desttriggers = destserver.Triggers;

            foreach (ServerDdlTrigger trigger in sourceserver.Triggers)
            {
                string triggername = trigger.Name;
                if (string.IsNullOrEmpty(objectstocopy[0]) || objectstocopy.Contains(triggername))
                {
                    if (desttriggers[triggername] != null)                        
                    {
                        if (!dropdest)
                        {
                            displayOutput(string.Format("Trigger {0} already exists in destination. Skipping.", triggername));
                            continue;
                        }
                        try
                        {
                            displayOutput(string.Format("Dropping trigger {0}.", triggername));
                            ServerDdlTrigger desttrigger = desttriggers[triggername];
                            desttriggers[triggername].Drop();
                            desttriggers.Refresh();
                        }
                        catch (Exception ex)
                        {
                            displayOutput(ex.Message);
                            continue;
                        }
                    }

                    try
                    {
                        StringCollection sql = trigger.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.Triggers.Refresh();
                        displayOutput(string.Format("Copied trigger {0} to {1}", triggername, destserver.Name));

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
                        displayOutput(string.Format("Error copying trigger {0} to {1}", triggername, destserver.Name));
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
