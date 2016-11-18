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
    public partial class Change_Autogrowth : Form
    {
        public Server dbServer { get; set; }
        public string dbName { get; set; }
        public string dbLogicalFile { get; set; }
        public string dbFileType { get; set; }
        private DataFile dataFileChanged;
        private LogFile logFilechanged;
        public Change_Autogrowth()
        {
            InitializeComponent();
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkEnable.Checked)
            {
                this.numericInPercent.Enabled = false;
                this.numericInMegabytes.Enabled = false;
                this.numericLimitedTo.Enabled = false;
                this.radioInPercent.Enabled = false;
                this.radioInMegabytes.Enabled = false;
                this.radioLimitedTo.Enabled = false;
                this.radioUnlimited.Enabled = false;
            }
            else
            {
                this.numericInPercent.Enabled = true;
                this.numericInMegabytes.Enabled = true;
                this.numericLimitedTo.Enabled = true;
                this.radioInPercent.Enabled = true;
                this.radioInMegabytes.Enabled = true;
                this.radioLimitedTo.Enabled = true;
                this.radioUnlimited.Enabled = true;
            }
        }

        private void radioInPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (radioInPercent.Checked && this.Enabled)
            {
                numericInPercent.Enabled = true;
                numericInMegabytes.Enabled = false;
            }
        }

        private void radioInMegabytes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioInMegabytes.Checked && this.Enabled)
            {
                numericInPercent.Enabled = false;
                numericInMegabytes.Enabled = true;
            }

        }

        private void radioLimitedTo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLimitedTo.Checked && this.Enabled)
            {
                numericLimitedTo.Enabled = true;
            }
        }

        private void radioUnlimited_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUnlimited.Checked && this.Enabled)
            {
                numericLimitedTo.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (dbFileType)
            {
                case "Data":
                    dataFileChanged.Alter();
                    break;
                case "Log":
                    logFilechanged.Alter();
                    break;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Change_Autogrowth_Shown(object sender, EventArgs e)
        {
            Database db = dbServer.Databases[dbName];

            if (dbFileType == "Data")
            {
                bool matched = false;
                foreach (FileGroup fileGroup in db.FileGroups)
                {
                    foreach (DataFile dbFile in fileGroup.Files)
                    {
                        if (dbFile.Name == dbLogicalFile)
                        {
                            switch (dbFile.GrowthType)
                            {
                                case FileGrowthType.None:
                                    this.radioInPercent.Enabled = false;
                                    this.radioInMegabytes.Enabled = false;
                                    this.radioLimitedTo.Enabled = false;
                                    this.radioUnlimited.Enabled = false;
                                    this.chkEnable.Checked = false;
                                    this.radioInPercent.Checked = true;
                                    this.numericInPercent.Value = 10;
                                    this.numericInMegabytes.Value = 10;
                                    this.numericInPercent.Enabled = false;
                                    this.numericInMegabytes.Enabled = false;
                                    break;
                                case FileGrowthType.Percent:
                                    this.chkEnable.Checked = true;
                                    this.radioInPercent.Checked = true;
                                    this.numericInPercent.Value = (decimal)dbFile.Growth;
                                    break;
                                case FileGrowthType.KB:
                                    this.chkEnable.Checked = true;
                                    this.radioInMegabytes.Checked = true;
                                    this.numericInMegabytes.Value = (decimal)dbFile.Growth / 1024;
                                    break;
                            }

                            switch (dbFile.MaxSize.ToString())
                            {
                                case "-1":
                                    this.radioUnlimited.Checked = true;
                                    this.numericLimitedTo.Value = 100;
                                    this.numericLimitedTo.Enabled = false;
                                    break;
                                default:
                                    this.radioLimitedTo.Checked = true;
                                    this.numericLimitedTo.Value = (decimal)dbFile.MaxSize / 1024;
                                    break;
                            }

                            dataFileChanged = dbFile;
                            matched = true;
                            break;
                        }
                        if (matched) break;
                    }
                }
            }

            if (dbFileType == "Log")
            {
                foreach (LogFile dbFile in db.LogFiles)
                {
                    if (dbFile.Name == dbLogicalFile)
                    {
                        switch (dbFile.GrowthType)
                        {
                            case FileGrowthType.None:
                                this.radioInPercent.Enabled = false;
                                this.radioInMegabytes.Enabled = false;
                                this.radioLimitedTo.Enabled = false;
                                this.radioUnlimited.Enabled = false;
                                this.chkEnable.Checked = false;
                                this.radioInPercent.Checked = true;
                                this.numericInPercent.Value = 10;
                                this.numericInMegabytes.Value = 10;
                                this.numericInPercent.Enabled = false;
                                this.numericInMegabytes.Enabled = false;
                                break;
                            case FileGrowthType.Percent:
                                this.chkEnable.Checked = true;
                                this.radioInPercent.Checked = true;
                                this.numericInPercent.Value = (decimal)dbFile.Growth;
                                break;
                            case FileGrowthType.KB:
                                this.chkEnable.Checked = true;
                                this.radioInMegabytes.Checked = true;
                                this.numericInMegabytes.Value = (decimal)dbFile.Growth / 1024;
                                break;
                        }

                        switch ((dbFile.MaxSize / 1024).ToString())
                        {
                            case "2097152":
                                this.radioUnlimited.Checked = true;
                                this.numericLimitedTo.Value = 100;
                                this.numericLimitedTo.Enabled = false;
                                break;
                            default:
                                this.radioLimitedTo.Checked = true;
                                this.numericLimitedTo.Value = (decimal)dbFile.MaxSize / 1024;
                                break;
                        }

                        logFilechanged = dbFile;
                        break;
                    }
                }
            }
        }
    }
}

