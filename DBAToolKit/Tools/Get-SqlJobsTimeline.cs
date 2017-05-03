using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;
using System.Data;
using DBAToolKit.Models;
using System.Collections.Generic;
using System.Drawing;

namespace DBAToolKit.Tools
{
    public partial class Get_SqlJobstimeline : UserControl
    {
        private Server sourceserver;
        GanttChart ganttTimeline;
        List<JobFilter> categoriesToDisplay = new List<JobFilter>();

        public Get_SqlJobstimeline()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.MinDate = DateTime.Now.AddDays(-30);
            dateTimePicker1.Value = DateTime.Now;
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

                setupFilterList();
                loadGanttChart();
                btnFilter.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void loadGanttChart()
        {
            // If the Gantt control is already loaded, dispose of it.
            if (this.Contains(ganttTimeline))
            {
                ganttTimeline.Dispose();
            }

            ganttTimeline = new GanttChart();
            ganttTimeline.AllowChange = false;
            ganttTimeline.Dock = DockStyle.Fill;
            panel1.Controls.Add(ganttTimeline);

            ganttTimeline.MouseMove += new MouseEventHandler(ganttTimeline.GanttChart_MouseMove);
            ganttTimeline.MouseMove += new MouseEventHandler(GanttTimeline_MouseMove);
            ganttTimeline.MouseDragged += new MouseEventHandler(ganttTimeline.GanttChart_MouseDragged);
            ganttTimeline.MouseLeave += new EventHandler(ganttTimeline.GanttChart_MouseLeave);
            ganttTimeline.ContextMenuStrip = ContextMenuGanttTimeline;

            DateTime dateToDisplay = this.dateTimePicker1.Value;
            ganttTimeline.FromDate = new DateTime(dateToDisplay.Year, dateToDisplay.Month, dateToDisplay.Day, 0, 0, 0);
            ganttTimeline.ToDate = new DateTime(dateToDisplay.Year, dateToDisplay.Month, dateToDisplay.Day, 23, 59, 59);

            List<BarInformation> lst1 = new List<BarInformation>();
            DataTable dt = getJobHistory();
            int index = 0;
            string prevJobName = "";

            foreach (DataRow row in dt.Rows)
            {
                JobFilter jobCategory = categoriesToDisplay.Find(x => x.CategoryName == row[4].ToString());
                if (jobCategory.IsChecked == true)
                {
                    Color mainColor = new Color();
                    if (row[1].ToString() == "1")
                    {
                        mainColor = Color.Green;
                    }
                    else
                    {
                        mainColor = Color.Red;
                    }

                    if (row[0].ToString() != prevJobName)
                    {
                        index += 1;
                    }
                    prevJobName = row[0].ToString();

                    lst1.Add(new BarInformation(row[0].ToString(), DateTime.Parse(row[2].ToString()), DateTime.Parse(row[3].ToString()), mainColor, Color.Khaki, index));
                }
            }

            foreach (BarInformation bar in lst1)
            {
                ganttTimeline.AddChartBar
                (bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
            }
        }

        private void setupFilterList()
        {
            DataTable dt = getJobCategories();

            foreach (DataRow row in dt.Rows)
            {
               categoriesToDisplay.Add(new JobFilter(row[0].ToString(), true));
            }
        }

        private DataTable getJobHistory()
        {
            string sql = @"   SELECT   j.name ,
                                    jh.run_status ,
                                    msdb.dbo.agent_datetime(run_date, run_time) AS start_time ,
                                    DATEADD(ss,
                                            run_duration % 100 + ROUND(( run_duration % 10000 ) / 100,
                                                                       0, 0) * 60
                                            + ROUND(( run_duration % 1000000 ) / 10000, 0, 0) * 3600,
                                            msdb.dbo.agent_datetime(run_date, run_time)) AS end_time,
                                    c.name AS category
                               FROM     msdb.dbo.sysjobhistory jh
                                        INNER JOIN msdb.dbo.sysjobs j ON j.job_id = jh.job_id
                                        INNER JOIN msdb.dbo.syscategories c ON j.category_id = c.category_id
                               WHERE    step_id = 0
                                        AND msdb.dbo.agent_datetime(run_date, run_time) BETWEEN DATEADD(dd, 0,
                                                                                          DATEDIFF(dd, 0,
                                                                                          '" + this.dateTimePicker1.Value.ToString("yyyyMMdd") + @"'))
                                                                                   AND    DATEADD(SECOND,
                                                                                          86399,
                                                                                          DATEADD(dd, 0,
                                                                                          DATEDIFF(dd, 0,'" + 
                                                                                          this.dateTimePicker1.Value.ToString("yyyyMMdd") + @"')))
                               ORDER BY j.name ASC, msdb.dbo.agent_datetime(run_date, run_time) ASC;";
            DataSet jobHistory = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return jobHistory.Tables[0];
        }

        private DataTable getJobCategories()
        {
            string sql = @"    SELECT   DISTINCT c.name AS CategoryName
                               FROM     msdb.dbo.sysjobhistory jh
                                        INNER JOIN msdb.dbo.sysjobs j ON j.job_id = jh.job_id
                                        INNER JOIN msdb.dbo.syscategories c ON j.category_id = c.category_id
                               WHERE    step_id = 0
                                        AND msdb.dbo.agent_datetime(run_date, run_time) BETWEEN DATEADD(dd, 0,
                                                                                          DATEDIFF(dd, 0,
                                                                                          '" + this.dateTimePicker1.Value.ToString("yyyyMMdd") + @"'))
                                                                                   AND    DATEADD(SECOND,
                                                                                          86399,
                                                                                          DATEADD(dd, 0,
                                                                                          DATEDIFF(dd, 0,'" +
                                                                                          this.dateTimePicker1.Value.ToString("yyyyMMdd") + @"')))
                               ORDER BY c.name;";
            DataSet jobCategories = sourceserver.ConnectionContext.ExecuteWithResults(sql);
            return jobCategories.Tables[0];
        }

        private void GanttTimeline_MouseMove(Object sender, MouseEventArgs e)
        {
            List<string> toolTipText = new List<string>();

            if (ganttTimeline.MouseOverRowText.Length > 0)
            {
                BarInformation val = (BarInformation)ganttTimeline.MouseOverRowValue;
                toolTipText.Add("[b]Date:");
                toolTipText.Add("From ");
                toolTipText.Add(val.FromTime.ToLongDateString() + " - " + val.FromTime.ToString("HH:mm"));
                toolTipText.Add("To ");
                toolTipText.Add(val.ToTime.ToLongDateString() + " - " + val.ToTime.ToString("HH:mm"));
            }
            else
            {
                toolTipText.Add("");
            }

            ganttTimeline.ToolTipTextTitle = ganttTimeline.MouseOverRowText;
            ganttTimeline.ToolTipText = toolTipText;

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Get_SqlJobsTimelineFilter filter = new Get_SqlJobsTimelineFilter();
            filter.CategoriesToDisplay = categoriesToDisplay;
            filter.ShowDialog();
            categoriesToDisplay = filter.CategoriesToDisplay;
            loadGanttChart();
        }
    }
}
