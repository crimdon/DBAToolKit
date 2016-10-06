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
    public partial class Copy_SqlOperators : UserControl
    {
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

                List<String> operatorsToCopy = txtOperatorsToCopy.Text.Split(',').ToList();

                DateTime started = DateTime.Now;
                txtOutput.Clear();
                displayOutput(string.Format("Migration started: {0}", DateTime.Now.ToShortTimeString()));
                copyOperators(sourceserver, destserver, operatorsToCopy);
                displayOutput(string.Format("Migration ended: {0}", DateTime.Now.ToShortTimeString()));
            }

            catch (Exception ex)
            {
                displayOutput(ex.Message, true);
            }
        }

        private void copyOperators (Server sourceserver, Server destserver, List<string> alertstocopy)
        {
            foreach (Operator op in sourceserver.JobServer.Operators)
            {
                string opname = op.Name;
                if (string.IsNullOrEmpty(alertstocopy[0]) || alertstocopy.Contains(opname))
                {
                    if (DBChecks.OperatorExists(destserver, opname))
                    {
                        displayOutput(string.Format("Operator {0} already exists in destination. Skipping.", opname));
                        continue;
                    }

                    try
                    {
                        StringCollection sql = op.Script();
                        destserver.ConnectionContext.ExecuteNonQuery(sql);
                        destserver.JobServer.Refresh();

                        displayOutput(string.Format("Copied operator {0} to {1}", opname, destserver.Name));
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
