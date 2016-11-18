using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Data;

namespace DBAToolKit.Tools
{
    public partial class Get_DatabaseFileSizes : UserControl
    {
        private Server sourceserver;
        public Get_DatabaseFileSizes()
        {
            InitializeComponent();
            cmbFilter.Items.Add("Show all files");
            cmbFilter.Items.Add("Show only file that use default settings");
            cmbFilter.SelectedItem = "Show all files";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text) == true)
                {
                    throw new Exception("Enter a Server!");
                }

                ConnectSqlServer connection = new ConnectSqlServer();
                sourceserver = connection.Connect(txtSource.Text);
                
                if (sourceserver.VersionMajor < 9)
                {
                    throw new Exception("SQL Server versions prior to 2005 are not supported!");
                }

                showDatabaseFiles();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showDatabaseFiles ()
        {
            listDatabaseFiles.Items.Clear();
            DataTable files = getDatabaseFiles();

            foreach (DataRow file in files.Rows)
            {
                listDatabaseFiles.Items.Add(new ListViewItem(new string[] 
                    {
                        file[0].ToString(),
                        file[1].ToString(),
                        file[2].ToString(),
                        file[3].ToString(),
                        file[4].ToString(),
                        file[5].ToString(),
                        file[6].ToString()
                    }));
            }
        }
        
        private DataTable getDatabaseFiles()
        {
            string sql;
            if (this.cmbFilter.SelectedItem.ToString() == "Show all files")
            {
                sql = @"-- Drop temporary table if it exists
                            IF OBJECT_ID('tempdb..#info') IS NOT NULL
                                DROP TABLE #info;
 
                            -- Create table to house database file information
                            CREATE TABLE #info
                                (
                                  databasename VARCHAR(128) ,
                                  name VARCHAR(128) ,
                                  fileid INT ,
                                  filename VARCHAR(1000) ,
                                  filetype CHAR(4) ,
                                  filegroup VARCHAR(128) ,
                                  size VARCHAR(25) ,
                                  maxsize VARCHAR(25) ,
                                  growth VARCHAR(25) ,
                                  usage VARCHAR(25)
                                );
    
                            -- Get database file information for each database   
                            SET NOCOUNT ON; 
                            INSERT  INTO #info
                                    EXEC sp_MSforeachdb 'use ? 
                            select ''?'',name,  fileid, filename,
                            filetype = CASE groupid WHEN 0 THEN ''Log'' ELSE ''Data'' END,
                            filegroup = filegroup_name(groupid),
                            ''size'' = convert(nvarchar(15), convert (bigint, size) * 8) + N'' KB'',
                            ''maxsize'' = (case maxsize when -1 then N''Unlimited''
                            else
                            convert(nvarchar(15), convert (bigint, maxsize) * 8) + N'' KB'' end),
                            ''growth'' = (case status & 0x100000 when 0x100000 then
                            convert(nvarchar(15), growth) + N''%''
                            else
                            convert(nvarchar(15), convert (bigint, growth) * 8) + N'' KB'' end),
                            ''usage'' = (case status & 0x40 when 0x40 then ''log only'' else ''data only'' end)
                            from sysfiles
                            ';
 
                            -- Identify database files that use default auto-grow properties
                            SELECT  databasename AS [Database Name] ,
                                    name AS [Logical Name] ,
                                    filetype AS [Type] , 
                                    filename AS [Physical File Name] ,
                                    size AS [Size] ,
                                    maxsize AS [MaxSize] ,
                                    growth AS [Auto-grow Setting]
                            FROM    #info
                            WHERE   DB_ID(databasename) > 4
                            ORDER BY databasename;
 
                            -- get rid of temp table 
                            DROP TABLE #info;";
            }
            else
            {
                sql = @"-- Drop temporary table if it exists
                            IF OBJECT_ID('tempdb..#info') IS NOT NULL
                                DROP TABLE #info;
 
                            -- Create table to house database file information
                            CREATE TABLE #info
                                (
                                  databasename VARCHAR(128) ,
                                  name VARCHAR(128) ,
                                  fileid INT ,
                                  filename VARCHAR(1000) ,
                                  filetype CHAR(4) ,
                                  filegroup VARCHAR(128) ,
                                  size VARCHAR(25) ,
                                  maxsize VARCHAR(25) ,
                                  growth VARCHAR(25) ,
                                  usage VARCHAR(25)
                                );
    
                            -- Get database file information for each database   
                            SET NOCOUNT ON; 
                            INSERT  INTO #info
                                    EXEC sp_MSforeachdb 'use ? 
                            select ''?'',name,  fileid, filename,
                            filetype = CASE groupid WHEN 0 THEN ''Log'' ELSE ''Data'' END,
                            filegroup = filegroup_name(groupid),
                            ''size'' = convert(nvarchar(15), convert (bigint, size) * 8) + N'' KB'',
                            ''maxsize'' = (case maxsize when -1 then N''Unlimited''
                            else
                            convert(nvarchar(15), convert (bigint, maxsize) * 8) + N'' KB'' end),
                            ''growth'' = (case status & 0x100000 when 0x100000 then
                            convert(nvarchar(15), growth) + N''%''
                            else
                            convert(nvarchar(15), convert (bigint, growth) * 8) + N'' KB'' end),
                            ''usage'' = (case status & 0x40 when 0x40 then ''log only'' else ''data only'' end)
                            from sysfiles
                            ';
 
                            -- Identify database files that use default auto-grow properties
                            SELECT  databasename AS [Database Name] ,
                                    name AS [Logical Name] ,
                                    filetype AS [Type],
                                    filename AS [Physical File Name] ,
                                    size AS [Size] ,
                                    maxsize AS [MaxSize] ,
                                    growth AS [Auto-grow Setting]
                            FROM    #info
                            WHERE   DB_ID(databasename) > 4
                                    AND (( usage = 'data only'
                                      AND growth = '1024 KB'
                                    )
                                    OR ( usage = 'log only'
                                         AND growth = '10%'
                                       ))
                            ORDER BY databasename;
 
                            -- get rid of temp table 
                            DROP TABLE #info;";
            }
            DataSet ds = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return ds.Tables[0];
        }

        private void changeAutogrowSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDatabaseFiles.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listDatabaseFiles.SelectedItems;

                ListViewItem lvItem = items[0];
                Change_Autogrowth frm = new Change_Autogrowth();
                frm.Text = "Change Autogrowth for " + lvItem.SubItems[0].Text;
                frm.dbServer = sourceserver;
                frm.dbName = lvItem.SubItems[0].Text;
                frm.dbLogicalFile = lvItem.SubItems[1].Text;
                frm.dbFileType = lvItem.SubItems[2].Text.Trim();
                frm.ShowDialog();
                frm.Dispose();
            }
        }
    }
}
