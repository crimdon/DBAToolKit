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
    public partial class Copy_JobCategories : UserControl
    {
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

                List<String> categoriesToCopy = txtCategoriesToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyCategories(sourceserver, destserver, categoriesToCopy);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyCategories (Server sourceserver, Server destserver, List<string> categoriestocopy)
        {
            foreach (JobCategory cat in sourceserver.JobServer.JobCategories)
            {
                string categoryname = cat.Name;
                if (string.IsNullOrEmpty(categoriestocopy[0]) || categoriestocopy.Contains(categoryname))
                {
                    if (DBChecks.CategoryExists(destserver, categoryname))
                    {
                        displayOutput(string.Format("Category {0} already exists in destination. Skipping.", categoryname));
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
                        displayOutput(ex.Message);
                        continue;
                    }

                    displayOutput(string.Format("Copied category {0} to destination", categoryname));
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
