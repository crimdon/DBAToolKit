namespace DBAToolKit.Tools
{
    partial class Get_DatabaseBackupHistory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.listDatabses = new System.Windows.Forms.ListBox();
            this.listBackupHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(291, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "SQL Databse Backup History";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Source Server";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(103, 33);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(150, 20);
            this.txtSource.TabIndex = 27;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(282, 36);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 23);
            this.btnDisplay.TabIndex = 29;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // listDatabses
            // 
            this.listDatabses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listDatabses.FormattingEnabled = true;
            this.listDatabses.Location = new System.Drawing.Point(4, 62);
            this.listDatabses.Name = "listDatabses";
            this.listDatabses.Size = new System.Drawing.Size(188, 329);
            this.listDatabses.TabIndex = 30;
            this.listDatabses.SelectedValueChanged += new System.EventHandler(this.listDatabses_SelectedValueChanged);
            // 
            // listBackupHistory
            // 
            this.listBackupHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBackupHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listBackupHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listBackupHistory.Location = new System.Drawing.Point(199, 62);
            this.listBackupHistory.MultiSelect = false;
            this.listBackupHistory.Name = "listBackupHistory";
            this.listBackupHistory.Size = new System.Drawing.Size(798, 329);
            this.listBackupHistory.TabIndex = 31;
            this.listBackupHistory.UseCompatibleStateImageBehavior = false;
            this.listBackupHistory.View = System.Windows.Forms.View.Details;
            this.listBackupHistory.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Start Time";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "End Time";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Duration";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Expiration Date";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Size";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Logical Device Name";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Physical Device Name";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Backup Set Name";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Description";
            // 
            // Get_DatabaseBackupHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.listBackupHistory);
            this.Controls.Add(this.listDatabses);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label3);
            this.Name = "Get_DatabaseBackupHistory";
            this.Size = new System.Drawing.Size(1000, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.ListBox listDatabses;
        private System.Windows.Forms.ListView listBackupHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}
