using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using DBAToolKit.Models;

namespace DBAToolKit.Tools
{
    public partial class Copy_JobCategories : UserControl
    {
        private Server sourceserver;
        List<ItemToCopy> itemsToCopy = new List<ItemToCopy>();
        public Copy_JobCategories()
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
                sourceserver = connection.Connect(txtSource.Text);
                Server destserver = connection.Connect(txtDestination.Text);

                if (sourceserver.VersionMajor < 9 || destserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                if (sourceserver.VersionMajor > 10 && destserver.VersionMajor < 11)
                {
                    throw new Exception(string.Format("SQL Login migration FROM SQL Server version {0} to {1} not supported!", sourceserver.VersionMajor.ToString(), destserver.VersionMajor.ToString()));
                }

                if (itemsToCopy.Count == 0)
                {
                    setupJobList();
                }

                DateTime started = DateTime.Now;
                showOutput.Clear();
                showOutput.displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyCategories(destserver);
                showOutput.displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                showOutput.displayOutput(ex.Message, true);
            }
        }

        private void copyCategories (Server destserver)
        {
            foreach (JobCategory cat in sourceserver.JobServer.JobCategories)
            {
                string categoryname = cat.Name;
                ItemToCopy item = itemsToCopy.Find(x => x.Name == categoryname);
                if (item.IsChecked)
                {
                    if (DBChecks.CategoryExists(destserver, categoryname))
                    {
                        showOutput.displayOutput(string.Format("Category {0} already exists in destination. Skipping.", categoryname));
                        continue;
                    }

                    try
                    {
                        JobCategory destcat = new JobCategory();
                        destcat.Name = categoryname;
                        destcat.Parent = destserver.JobServer;
                        destcat.CategoryType = cat.CategoryType;                       
                        destcat.Create();
                    }
                    catch (Exception ex)
                    {
                        showOutput.displayOutput(ex.Message);
                        continue;
                    }

                    showOutput.displayOutput(string.Format("Copied category {0} to destination", categoryname));
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
