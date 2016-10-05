using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Collections.Specialized;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlJobs : UserControl
    {
        public Copy_SqlJobs()
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
                    throw new Exception(string.Format("SQL Login migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                List<String> jobsToCopy = txtJobsToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyJobs(sourceserver, destserver, jobsToCopy, chkDisableOnSource.Checked, chkDisableOnDest.Checked, chkDropDest.Checked);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyJobs(Server sourceserver, Server destserver, List<string> jobstocopy, bool disableonsource, bool disableondest, bool dropdest)
        {
            JobCollection destjobs = destserver.JobServer.Jobs;

            foreach (Job job in sourceserver.JobServer.Jobs)
            {
                string jobname = job.Name;
                if (string.IsNullOrEmpty(jobstocopy[0]) || jobstocopy.Contains(jobname))
                {
                    if (!DBChecks.LoginExists(destserver, job.OwnerLoginName))
                    {
                        displayOutput(string.Format("[Job: {0}] Owner {1} doesn't exist ion destination. Skipping. ", jobname, job.OwnerLoginName));
                        continue;
                    }

                    foreach (JobStep jobstep in job.JobSteps)
                    {
                        if (!DBChecks.DatabaseExists(destserver, jobstep.DatabaseName))
                        {
                            displayOutput(string.Format("[Job: {0}] Database {1} doesn't exist on destination. Skipping. ", jobname, jobstep.DatabaseName));
                            continue;
                        }

                        if (!string.IsNullOrEmpty(jobstep.ProxyName) && !DBChecks.ProxyExists(destserver, jobstep.ProxyName))
                        {
                            displayOutput(string.Format("[Job: {0}] Proxy {1} doesn't exist on destination. Skipping. ", jobname, jobstep.ProxyName));
                            continue;
                        }
                    }

                    if (DBChecks.JobExists(destserver, jobname))
                    {
                        if (!dropdest)
                        {
                            displayOutput(string.Format("Job: {0} already exists on destination server. Skipping. ", jobname));
                            continue;
                        }
                        else
                        {
                            try
                            {
                                destserver.JobServer.Jobs[jobname].Drop();
                                destserver.JobServer.Refresh();
                            }
                            catch (Exception ex)
                            {
                                displayOutput(ex.Message);
                                continue;
                            }
                        }
                    }

                    try
                    {
                        StringCollection sql = job.Script();
                        for (int i = 0; i < sql.Count; i++)
                        {
                            sql[i] = sql[i].Replace(sourceserver.Name, destserver.Name);
                        }

                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.JobServer.Refresh();

                        displayOutput(string.Format("Copied job {0} to {1}", jobname, destserver.Name));

                        if (disableondest)
                        {
                            destserver.JobServer.Jobs[jobname].IsEnabled = false;
                            destserver.JobServer.Jobs[jobname].Alter();
                        }

                        if (disableonsource)
                        {
                            sourceserver.JobServer.Jobs[jobname].IsEnabled = false;
                            sourceserver.JobServer.Jobs[jobname].Alter();
                        }

                    }
                    catch (Exception ex)
                    {
                        displayOutput(string.Format("Failed to copy job {0} to {1}", jobname, destserver.Name));
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
