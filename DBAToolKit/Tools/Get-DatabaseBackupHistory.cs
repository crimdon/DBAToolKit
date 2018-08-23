using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Data;

namespace DBAToolKit.Tools
{
    public partial class Get_DatabaseBackupHistory : UserControl
    {
        private Server sourceserver;
        public Get_DatabaseBackupHistory()
        {
            InitializeComponent();
            listDatabses.Hide();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registeredServersSource.SelectedServer) == true)
                {
                    throw new Exception("Enter a Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(registeredServersSource.SelectedServer);
                
                if (sourceserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                listDatabases();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listDatabases()
        {
            foreach (Database db in sourceserver.Databases)
            {
                if (db.Status == DatabaseStatus.Normal)
                    listDatabses.Items.Add(db.Name);
            }
            listDatabses.Show();
        }

        private void listDatabses_SelectedValueChanged(object sender, EventArgs e)
        {
            listBackupHistory.Items.Clear();
            displayBackupHistory(listDatabses.SelectedItem.ToString());
            listBackupHistory.Show();
        }

        private void displayBackupHistory(string database)
        {
            Database db = sourceserver.Databases[database];
            DataTable backupHistory = getBackupHistory(database);

            foreach (DataRow row in backupHistory.Rows)
            {
                TimeSpan duration = TimeSpan.FromSeconds(Int64.Parse(row[2].ToString()));

                listBackupHistory.Items.Add(new ListViewItem(new string[]
                {
                    DateTime.Parse(row[0].ToString()).ToString("dd/MM/yyyy HH:mm"),
                    DateTime.Parse(row[1].ToString()).ToString("dd/MM/yyyy HH:mm"),
                    duration.ToString(@"hh\:mm\:ss"),
                    row[3].ToString(),
                    row[4].ToString(),
                    (Int64.Parse(row[5].ToString())/1024/1024).ToString() + " Mb",
                    row[6].ToString(),
                    row[7].ToString(),
                    row[8].ToString(),
                    row[9].ToString()
                }));
            }

            listBackupHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listBackupHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private DataTable getBackupHistory(string database)
        {
            string sql = @"SELECT  DISTINCT  
                                   msdb.dbo.backupset.backup_start_date,  
                                   msdb.dbo.backupset.backup_finish_date, 
                                   datediff(second,msdb.dbo.backupset.backup_start_date,msdb.dbo.backupset.backup_finish_date) duration_seconds,
                                   msdb.dbo.backupset.expiration_date, 
                                        CASE WHEN backupset.type = 'D' THEN 'Full backup'
                                             WHEN backupset.type = 'I' THEN 'Differential'
                                             WHEN backupset.type = 'L' THEN 'Log'
                                             WHEN backupset.type = 'F' THEN 'File/Filegroup'
                                             WHEN backupset.type = 'G' THEN 'Differential file'
                                             WHEN backupset.type = 'P' THEN 'Partial'
                                             WHEN backupset.type = 'Q' THEN 'Differential partial'
                                             ELSE 'Unknown (' + backupset.type + ')'
                                        END AS [Backup Type],  
                                   msdb.dbo.backupset.backup_size,  
                                   msdb.dbo.backupmediafamily.logical_device_name,  
                                   msdb.dbo.backupmediafamily.physical_device_name,   
                                   msdb.dbo.backupset.name AS backupset_name, 
                                   msdb.dbo.backupset.description 
                                FROM   msdb.dbo.backupmediafamily  
                                   INNER JOIN msdb.dbo.backupset ON msdb.dbo.backupmediafamily.media_set_id = msdb.dbo.backupset.media_set_id 
                                WHERE  msdb.dbo.backupset.database_name = '" + database + @"'  
                                ORDER BY  msdb.dbo.backupset.backup_finish_date DESC";
            DataSet ds = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return ds.Tables[0];
        }
    }
}
