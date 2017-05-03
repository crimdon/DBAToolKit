using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using DBAToolKit.Helpers;

namespace DBAToolKit.Tools
{
    public partial class Get_DatabaseSize : UserControl
    {
        private Server sourceserver;
        public Get_DatabaseSize()
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
                listDatabses.Items.Add(db.Name);
            }
            listDatabses.Show();
        }

        private void listDatabses_SelectedValueChanged(object sender, EventArgs e)
        {
            getDatabaseSize(listDatabses.SelectedItem.ToString());
            panel1.Show();
        }

        private void getDatabaseSize(string database)
        {
            Database db = sourceserver.Databases[database];
            listSizes.Items.Clear();

            //Calulate data file sizes
            double datasize = 0;
            double dataused = 0;
            foreach (FileGroup fg in db.FileGroups)
            {
                foreach (DataFile df in fg.Files)
                {
                    datasize += df.Size / 1024;
                    dataused += df.UsedSpace / 1024;
                }
            }

            //Calulate Log file sizes
            double logsize = 0;
            double logused = 0;
            foreach (LogFile lf in db.LogFiles)
            {
                logsize += lf.Size / 1024;
                logused += lf.UsedSpace / 1024;
            }

            //Add the items to the listview
            listSizes.Items.Add(new ListViewItem(new string[] { "Total Space Reserved", string.Format("{0:0.00 MB}", db.Size) }));
            listSizes.Items.Add(new ListViewItem(new string[] { "Total Data Space Used", string.Format("{0:0.00 MB}", datasize) }));
            listSizes.Items.Add(new ListViewItem(new string[] { "Total Log Space Reserved", string.Format("{0:0.00 MB}", logsize) }));

            //listSizes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listSizes.Refresh();

            //Update Data pie chart
            chart1.Series["Sizes"].Points.Clear();

            chart1.Series["Sizes"].Points.Add(db.DataSpaceUsage / 1024);
            chart1.Series["Sizes"].Points[0].Label = db.DataSpaceUsage.ToString();
            chart1.Series["Sizes"].Points[0].LegendText = "Data";
            chart1.Series["Sizes"].Points[0].Label = Utilities.GetPercentage(Convert.ToInt32(db.DataSpaceUsage / 1024), Convert.ToInt32(datasize), 0) + @"%";

            chart1.Series["Sizes"].Points.Add(db.IndexSpaceUsage / 1024);
            chart1.Series["Sizes"].Points[1].Label = db.IndexSpaceUsage.ToString();
            chart1.Series["Sizes"].Points[1].LegendText = "Index";
            chart1.Series["Sizes"].Points[1].Label = Utilities.GetPercentage(Convert.ToInt32(db.IndexSpaceUsage / 1024), Convert.ToInt32(datasize), 0) + @"%";

            chart1.Series["Sizes"].Points.Add(db.SpaceAvailable / 1024);
            chart1.Series["Sizes"].Points[2].Label = (db.SpaceAvailable).ToString();
            chart1.Series["Sizes"].Points[2].LegendText = "Available";
            chart1.Series["Sizes"].Points[2].Label = Utilities.GetPercentage(Convert.ToInt32(db.SpaceAvailable / 1024), Convert.ToInt32(datasize), 0) + @"%";

            //Update Log pie chart
            chart2.Series["Sizes"].Points.Clear();

            chart2.Series["Sizes"].Points.Add(logused);
            chart2.Series["Sizes"].Points[0].Label = logused.ToString();
            chart2.Series["Sizes"].Points[0].LegendText = "Used";
            chart2.Series["Sizes"].Points[0].Label = Utilities.GetPercentage(Convert.ToInt32(logused), Convert.ToInt32(logsize), 0) + @"%";

            chart2.Series["Sizes"].Points.Add(logsize - logused);
            chart2.Series["Sizes"].Points[1].Label = (logsize - logused).ToString();
            chart2.Series["Sizes"].Points[1].LegendText = "Unused";
            chart2.Series["Sizes"].Points[1].Label = Utilities.GetPercentage(Convert.ToInt32(logsize - logused), Convert.ToInt32(logsize), 0) + @"%";
        }
    }
}
