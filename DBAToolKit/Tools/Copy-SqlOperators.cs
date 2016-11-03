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
    public partial class Copy_SqlOperators : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_SqlOperators()
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

                if (itemsToCopy.Count == 0)
                {
                    setupJobList();
                };

                DateTime started = DateTime.Now;
                showOutput.Clear();
                showOutput.displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyOperators(destserver);
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyOperators (Server destserver)
        {
            foreach (Operator op in sourceserver.JobServer.Operators)
            {
                string opname = op.Name;
                ItemToCopy item = itemsToCopy.Find(x => x.Name == opname);
                if (item.IsChecked)
                {
                    if (DBChecks.OperatorExists(destserver, opname))
                    {
                        showOutput.displayOutput(string.Format("Operator {0} already exists in destination. Skipping.", opname));
                        continue;
                    }

                    try
                    {
                        StringCollection sql = op.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.JobServer.Refresh();

                        showOutput.displayOutput(string.Format("Copied operator {0} to {1}", opname, destserver.Name));
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
            foreach (JobCategory cat in sourceserver.JobServer.JobCategories)
            {
                itemsToCopy.Add(new ItemToCopy(cat.Name, false));
            }
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            itemsToCopy.Clear();
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
    }
}
