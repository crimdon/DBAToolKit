using DBAToolKit.Helpers;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAToolKit.Tools
{
    public partial class Show_DatabaseDetails : Form
    {
        public static Database db;
        public Show_DatabaseDetails()
        {
            InitializeComponent();

            ConnectSqlServer connection = new ConnectSqlServer();
            Server sourceserver = connection.Connect(MainForm.dbServer);

            db = sourceserver.Databases[MainForm.dbName];

            listPages.Items.Add("General");
            listPages.Items.Add("Files");
            listPages.Items.Add("Options");

            listPages.SelectedItem = "General";
        }

        private void listPages_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (listPages.SelectedItem.ToString())
            {
                case "General":
                    loadGeneral();
                    break;
                case "Files":
                    loadFiles();
                    break;
                case "Options":
                    loadOptions();
                    break;
            }

        }

        private void loadGeneral()
        {
            listDetails.Clear();
            listDetails.View = System.Windows.Forms.View.Details;
            listDetails.ShowGroups = true;

            listDetails.Columns.Add("Name");
            listDetails.Columns.Add("Value");
            listDetails.HeaderStyle = ColumnHeaderStyle.None;

            ListViewGroup backup = new ListViewGroup("Backup");
            ListViewGroup database = new ListViewGroup("Database");

            listDetails.Groups.Add(backup);
            listDetails.Groups.Add(database);

            listDetails.Items.Add(new ListViewItem(new string[] { "Last Database Backup", db.LastBackupDate.ToString() }, backup));
            listDetails.Items.Add(new ListViewItem(new string[] { "Last Database Log Backup", db.LastLogBackupDate.ToString() }, backup));

            listDetails.Items.Add(new ListViewItem(new string[] { "Name", db.Name }, database));
            listDetails.Items.Add(new ListViewItem(new string[] { "Status", db.Status.ToString() }, database));
            listDetails.Items.Add(new ListViewItem(new string[] { "Owner", db.Owner }, database));
            listDetails.Items.Add(new ListViewItem(new string[] { "Size", db.Size.ToString() }, database));
            listDetails.Items.Add(new ListViewItem(new string[] { "Space Available", db.SpaceAvailable.ToString() }, database));
            listDetails.Items.Add(new ListViewItem(new string[] { "Active Connections", db.ActiveConnections.ToString() }, database));

            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void loadFiles()
        {
            listDetails.Clear();
            listDetails.View = System.Windows.Forms.View.Details;
            listDetails.ShowGroups = false;

            listDetails.Columns.Add("Logical Name");
            listDetails.Columns.Add("File Type");
            listDetails.Columns.Add("File Group");
            listDetails.Columns.Add("File Size");
            listDetails.Columns.Add("File Space Used");
            listDetails.Columns.Add("Volume Free Space");
            listDetails.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            foreach (FileGroup fg in db.FileGroups)
            {
                foreach (DataFile df in fg.Files)
                {
                    listDetails.Items.Add(new ListViewItem(new string[] { df.Name, "Data", fg.Name, (df.Size/1024).ToString(), (df.UsedSpace/1024).ToString(), (df.VolumeFreeSpace/1048576).ToString() }));
                }
            }
            foreach (LogFile lf in db.LogFiles)
            {
                listDetails.Items.Add(new ListViewItem(new string[] { lf.Name, "Log", "Not Applicable", (lf.Size/1024).ToString(), (lf.UsedSpace/1024).ToString(), (lf.VolumeFreeSpace / 1048576).ToString() }));
            }
                

            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void loadOptions()
        {
            listDetails.Clear();
            listDetails.View = System.Windows.Forms.View.Details;
            listDetails.ShowGroups = true;

            listDetails.Columns.Add("Name");
            listDetails.Columns.Add("Value");
            listDetails.HeaderStyle = ColumnHeaderStyle.None;

            ListViewGroup vgAutomatic = new ListViewGroup("Automatic");
            ListViewGroup vgCursor = new ListViewGroup("Cursor");
            ListViewGroup vgMiscellaneous = new ListViewGroup("Miscellaneous");
            ListViewGroup vgRecovery = new ListViewGroup("Recovery");
            ListViewGroup vgServiceBroker = new ListViewGroup("Service Broker");
            ListViewGroup vgState = new ListViewGroup("State");

            listDetails.Groups.Add(vgAutomatic);
            listDetails.Groups.Add(vgCursor);
            listDetails.Groups.Add(vgMiscellaneous);
            listDetails.Groups.Add(vgRecovery);
            listDetails.Groups.Add(vgServiceBroker);
            listDetails.Groups.Add(vgState);

            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Close", db.DatabaseOptions.AutoClose.ToString() }, vgAutomatic));
            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Create Incremental Statistics", db.DatabaseOptions.AutoCreateStatisticsIncremental.ToString() }, vgAutomatic));
            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Create Statistics", db.DatabaseOptions.AutoCreateStatistics.ToString() }, vgAutomatic));
            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Shink", db.DatabaseOptions.AutoShrink.ToString() }, vgAutomatic));
            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Update Statistics", db.DatabaseOptions.AutoUpdateStatistics.ToString() }, vgAutomatic));
            listDetails.Items.Add(new ListViewItem(new string[] { "Auto Update Statistics Asyncronously", db.DatabaseOptions.AutoUpdateStatisticsAsync.ToString() }, vgAutomatic));

            listDetails.Items.Add(new ListViewItem(new string[] { "Close Cursor on Commit Enabled", db.DatabaseOptions.CloseCursorsOnCommitEnabled.ToString() }, vgCursor));
            listDetails.Items.Add(new ListViewItem(new string[] { "Local Cursors are Default", db.LocalCursorsDefault.ToString() }, vgCursor));

            listDetails.Items.Add(new ListViewItem(new string[] { "Allow Snapshot Isolation", db.SnapshotIsolationState.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "ANSI NULL Default", db.AnsiNullDefault.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "ANSI NULLS Enabled", db.AnsiNullsEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "ANSI Padding Enabled", db.AnsiPaddingEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "ANSI Warnings Enabled", db.AnsiWarningsEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Arithmetic Abort Enabled", db.ArithmeticAbortEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Concatenate Null Yields Null", db.ConcatenateNullYieldsNull.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Date Correlation Optimization Enabled", db.DateCorrelationOptimization.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Is Read Committed snapshot On", db.IsReadCommittedSnapshotOn.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Numeric Round About", db.NumericRoundAbortEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Is Parameterization Forced", db.IsParameterizationForced.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Quoted identifiers Enabled", db.QuotedIdentifiersEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Recursive Triggers Enabled", db.RecursiveTriggersEnabled.ToString() }, vgMiscellaneous));
            listDetails.Items.Add(new ListViewItem(new string[] { "Trustworthy", db.Trustworthy.ToString() }, vgMiscellaneous));

            listDetails.Items.Add(new ListViewItem(new string[] { "Page Verify", db.PageVerify.ToString() }, vgRecovery));

            listDetails.Items.Add(new ListViewItem(new string[] { "Broker Enabled", db.BrokerEnabled.ToString() }, vgServiceBroker));

            listDetails.Items.Add(new ListViewItem(new string[] { "Database Read-Only", db.ReadOnly.ToString() }, vgState));
            listDetails.Items.Add(new ListViewItem(new string[] { "Database State", db.State.ToString() }, vgState));
            listDetails.Items.Add(new ListViewItem(new string[] { "User Access", db.UserAccess.ToString() }, vgState));


            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
