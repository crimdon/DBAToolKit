using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using DBAToolKit.Models;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Collections.Specialized;

namespace DBAToolKit.Tools
{
    public partial class Copy_SqlJobs : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_SqlJobs()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text) == true)
                {
                    throw new Exception("Enter a Source Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(txtSource.Text);

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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            showOutput.displayOutput("Attempting to connect to SQL Servers...");
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
                sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

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
                copyJobs(destserver, chkDisableOnSource.Checked, chkDisableOnDest.Checked, chkDropDest.Checked);
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyJobs(Server destserver, bool disableonsource, bool disableondest, bool dropdest)
        {
            JobCollection destjobs = destserver.JobServer.Jobs;

            foreach (Job job in sourceserver.JobServer.Jobs)
            {
                string jobname = job.Name;
                ItemToCopy item = itemsToCopy.Find(x => x.Name == jobname);
                if (item.IsChecked)
                {
                    if (!DBChecks.LoginExists(destserver, job.OwnerLoginName))
                    {
                        showOutput.displayOutput(string.Format("[Job: {0}] Owner {1} doesn't exist ion destination. Skipping. ", jobname, job.OwnerLoginName));
                        continue;
                    }

                    foreach (JobStep jobstep in job.JobSteps)
                    {
                        if (!DBChecks.DatabaseExists(destserver, jobstep.DatabaseName))
                        {
                            showOutput.displayOutput(string.Format("[Job: {0}] Database {1} doesn't exist on destination. Skipping. ", jobname, jobstep.DatabaseName));
                            continue;
                        }

                        if (!string.IsNullOrEmpty(jobstep.ProxyName) && !DBChecks.ProxyExists(destserver, jobstep.ProxyName))
                        {
                            showOutput.displayOutput(string.Format("[Job: {0}] Proxy {1} doesn't exist on destination. Skipping. ", jobname, jobstep.ProxyName));
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(job.OperatorToEmail) && !DBChecks.OperatorExists(destserver, job.OperatorToEmail))
                    {
                        showOutput.displayOutput(string.Format("[Job: {0}] Operator {1} doesn't exist on destination. Skipping. ", jobname, job.OperatorToEmail));
                        continue;
                    }

                    if (!string.IsNullOrEmpty(job.OperatorToNetSend) && !DBChecks.OperatorExists(destserver, job.OperatorToNetSend))
                    {
                        showOutput.displayOutput(string.Format("[Job: {0}] Operator {1} doesn't exist on destination. Skipping. ", jobname, job.OperatorToNetSend));
                        continue;
                    }

                    if (!string.IsNullOrEmpty(job.OperatorToPage) && !DBChecks.OperatorExists(destserver, job.OperatorToPage))
                    {
                        showOutput.displayOutput(string.Format("[Job: {0}] Operator {1} doesn't exist on destination. Skipping. ", jobname, job.OperatorToPage));
                        continue;
                    }

                    if (DBChecks.JobExists(destserver, jobname))
                    {
                        if (!dropdest)
                        {
                            showOutput.displayOutput(string.Format("Job: {0} already exists on destination server. Skipping. ", jobname));
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
                                showOutput.displayOutput(ex.Message);
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

                        if (disableondest)
                        {
                            DBFunctions.ChangeJobStatus(destserver, jobname, false);
                        }

                        if (disableonsource)
                        {
                            DBFunctions.ChangeJobStatus(sourceserver, jobname, false);
                        }

                        showOutput.displayOutput(string.Format("Copied job {0} to {1}", jobname, destserver.Name));

                    }
                    catch (Exception ex)
                    {
                        showOutput.displayOutput(string.Format("Failed to copy job {0} to {1}", jobname, destserver.Name));
                        showOutput.displayOutput(ex.Message);
                        continue;
                    }
                    
                }
            }
        }

        private void setupJobList()
        {
            foreach (Job job in sourceserver.JobServer.Jobs)
            {
                itemsToCopy.Add(new ItemToCopy(job.Name, false));
            }
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
        }
    }
}
