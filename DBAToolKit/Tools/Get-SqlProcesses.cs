using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Data;

namespace DBAToolKit.Tools
{
    public partial class Get_SqlProcesses : UserControl
    {
        Server sourceserver;
        public Get_SqlProcesses()
        {
            InitializeComponent();
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

                showProcesses(sourceserver);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showProcesses(Server sourceserver)
        {
            string sql = @"
                SELECT  SPID = er.session_id ,
                        BlkBy = CASE WHEN lead_blocker = 1 THEN -1
                        ELSE er.blocking_session_id
                        END ,
                        ElapsedMS = er.total_elapsed_time ,
                        CPU = er.cpu_time ,
                        IOReads = er.logical_reads + er.reads ,
                        IOWrites = er.writes ,
                        Executions = ec.execution_count ,
                        CommandType = er.command ,
                        LastWaitType = er.last_wait_type ,
                        ObjectName = OBJECT_SCHEMA_NAME(qt.objectid, qt.dbid) + '.'
                        + OBJECT_NAME(qt.objectid, qt.dbid) ,
                        STATUS = ses.status ,
                        [Login] = ses.login_name ,
                        Host = ses.host_name ,
                        DBName = DB_NAME(er.database_id) ,
                        StartTime = er.start_time ,
                        Protocol = con.net_transport ,
                        transaction_isolation = CASE ses.transaction_isolation_level
                                          WHEN 0 THEN 'Unspecified'
                                          WHEN 1 THEN 'Read Uncommitted'
                                          WHEN 2 THEN 'Read Committed'
                                          WHEN 3 THEN 'Repeatable'
                                          WHEN 4 THEN 'Serializable'
                                          WHEN 5 THEN 'Snapshot'
                                        END ,
                        ConnectionWrites = con.num_writes ,
                        ConnectionReads = con.num_reads ,
                        ClientAddress = con.client_net_address ,
                        Authentication = con.auth_scheme
                        FROM    sys.dm_exec_requests er
                        LEFT JOIN sys.dm_exec_sessions ses ON ses.session_id = er.session_id
                        LEFT JOIN sys.dm_exec_connections con ON con.session_id = ses.session_id
                        OUTER APPLY sys.dm_exec_sql_text(er.sql_handle) AS qt
                        OUTER APPLY ( SELECT    execution_count = MAX(cp.usecounts)
                                      FROM      sys.dm_exec_cached_plans cp
                                      WHERE     cp.plan_handle = er.plan_handle
                                    ) ec
                        OUTER APPLY ( SELECT    lead_blocker = 1
                                      FROM      master.dbo.sysprocesses sp
                                      WHERE     sp.spid IN ( SELECT blocked
                                                             FROM   master.dbo.sysprocesses )
                                                AND sp.blocked = 0
                                                AND sp.spid = er.session_id
                                    ) lb
                WHERE   er.sql_handle IS NOT NULL
                        AND er.session_id != @@SPID
                        AND con.net_transport != 'Session'
                ORDER BY CASE WHEN lead_blocker = 1 THEN -1 * 1000
                              ELSE -er.blocking_session_id
                         END ,
                        er.blocking_session_id DESC ,
                        er.logical_reads + er.reads DESC ,
                        er.session_id;  
            ";
            DataSet processes = sourceserver.ConnectionContext.ExecuteWithResults(sql);

            listProcesses.BeginUpdate();
            listProcesses.DataSource = processes.Tables[0];
            listProcesses.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listProcesses.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listProcesses.EndUpdate();
        }

        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedspids in listProcesses.SelectedItems)
            {
                sourceserver.ConnectionContext.ExecuteNonQuery("KILL " + selectedspids.Text);
                listProcesses.Items.Remove(selectedspids);
            }
            listProcesses.Refresh();
        }
    }
}
