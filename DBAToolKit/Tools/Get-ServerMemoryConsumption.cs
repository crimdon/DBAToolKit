using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Collections.Generic;
using DBAToolKit.Models;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace DBAToolKit.Tools
{
    public partial class Get_ServerMemoryConsumption : UserControl
    {
        private Server sourceserver;
        public Get_ServerMemoryConsumption()
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
                
                if (sourceserver.VersionMajor < 10)
                {
                    throw new Exception("SQL Server versions prior to 2008 are not supported!");
                }

                getMemoryUsage();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getMemoryUsage()
        {
            Int64 perfCounterValue;

            listDetails.Items.Clear();

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Page life expectancy'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Page life expectancy", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Buffer cache hit ratio'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Buffer cache hit ratio", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Page reads/sec'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Page reads/sec", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Page writes/sec'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Page writes/sec", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Lazy writes/sec'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Lazy writes/sec", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Total Server Memory (KB)'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Total Server Memory (KB)", perfCounterValue.ToString() }));

            perfCounterValue = Int64.Parse(sourceserver.ConnectionContext.ExecuteScalar("select cntr_value from sys.dm_os_performance_counters WHERE counter_name = 'Target Server Memory (KB)'").ToString());
            listDetails.Items.Add(new ListViewItem(new string[] { "Target Server Memory (KB)", perfCounterValue.ToString() }));

            listDetails.Refresh();

            ////Update Top Memory Consuming Components Pie Chart
            chart1.Series["Consumers"].Points.Clear();

            DataTable topConsumers = getTopConsumers();
            for (int i = 0; i < topConsumers.Rows.Count; i++)
            {
                DataRow row = topConsumers.Rows[i];
                chart1.Series["Consumers"].Points.Add(Int64.Parse(row[1].ToString()));
                chart1.Series["Consumers"].Points[i].LegendText = row[0].ToString() + "(" + row[1].ToString() + " KB)";
            }

            ////Update Buffer Pool Distribution Pie Chart
            chart2.Series["Buffers"].Points.Clear();

            DataTable pageDistrbution = getBufferPageDistribution();
            for (int i = 0; i < pageDistrbution.Rows.Count; i++)
            {
                DataRow row = pageDistrbution.Rows[i];
                chart2.Series["Buffers"].Points.Add(Int64.Parse(row[1].ToString()));
                chart2.Series["Buffers"].Points[i].LegendText = row[0].ToString() + "(" + row[1].ToString() + ")";
            }

            panel1.Show();
        }

        private DataTable getTopConsumers()
        {
            string sql = @"DECLARE @total_alcted_v_res_awe_s_res BIGINT;
                        DECLARE @tab TABLE
                            (
                              row_no INT IDENTITY ,
                              type NVARCHAR(128) COLLATE DATABASE_DEFAULT ,
                              allocated BIGINT ,
                              vertual_res BIGINT ,
                              virtual_com BIGINT ,
                              awe BIGINT ,
                              shared_res BIGINT ,
                              shared_com BIGINT ,
                              graph_type NVARCHAR(128) ,
                              grand_total BIGINT
                            );

                        SELECT  @total_alcted_v_res_awe_s_res = SUM(pages_kb
                                                                    + ( CASE
                                                                      WHEN type <> 'MEMORYCLERK_SQLBUFFERPOOL'
                                                                      THEN virtual_memory_committed_kb
                                                                      ELSE 0
                                                                      END )
                                                                    + shared_memory_committed_kb)
                        FROM    sys.dm_os_memory_clerks;

                        INSERT  INTO @tab
                                SELECT  type ,
                                        SUM(pages_kb) AS allocated ,
                                        SUM(virtual_memory_reserved_kb) AS vertual_res ,
                                        SUM(virtual_memory_committed_kb) AS virtual_com ,
                                        SUM(awe_allocated_kb) AS awe ,
                                        SUM(shared_memory_reserved_kb) AS shared_res ,
                                        SUM(shared_memory_committed_kb) AS shared_com ,
                                        CASE WHEN ( ( ( SUM(pages_kb
                                                            + ( CASE WHEN type <> 'MEMORYCLERK_SQLBUFFERPOOL'
                                                                     THEN virtual_memory_committed_kb
                                                                     ELSE 0
                                                                END )
                                                            + shared_memory_committed_kb) )
                                                      / ( @total_alcted_v_res_awe_s_res
                                                          + 0.0 ) ) >= 0.05 )
                                                  OR type = 'MEMORYCLERK_XTP'
                                             THEN type
                                             ELSE 'Other'
                                        END AS graph_type ,
                                        ( SUM(pages_kb
                                              + ( CASE WHEN type <> 'MEMORYCLERK_SQLBUFFERPOOL'
                                                       THEN virtual_memory_committed_kb
                                                       ELSE 0
                                                  END ) + shared_memory_committed_kb) ) AS grand_total
                                FROM    sys.dm_os_memory_clerks
                                GROUP BY type
                                ORDER BY ( SUM(pages_kb
                                               + ( CASE WHEN type <> 'MEMORYCLERK_SQLBUFFERPOOL'
                                                        THEN virtual_memory_committed_kb
                                                        ELSE 0
                                                   END ) + shared_memory_committed_kb) ) DESC;

                        UPDATE  @tab
                        SET     graph_type = type
                        WHERE   row_no <= 5;
                
				
                        SELECT  graph_type ,
                                SUM(grand_total) AS grand_total
                        FROM    @tab
                        GROUP BY graph_type
                        ORDER BY graph_type;";

            DataSet topConsumers = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return topConsumers.Tables[0];
        }

        private DataTable getBufferPageDistribution()
        {
            string sql = @"DECLARE @table1 TABLE
                        (
                            objecttype VARCHAR(100) COLLATE DATABASE_DEFAULT ,
                            buffers BIGINT
                        );

                    INSERT  @table1
                            EXEC ( 'dbcc memorystatus with tableresults'
                                );

                    SELECT  objecttype ,
                            buffers AS value
                    FROM    @table1
                    WHERE   objecttype IN ( 'Stolen', 'Free', 'Cached', 'Dirty', 'Kept', 'I/O',
                                            'Latched', 'Other' );";
            DataSet topConsumers = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return topConsumers.Tables[0];
        }
    
    }
}
