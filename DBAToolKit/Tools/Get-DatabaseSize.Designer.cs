namespace DBAToolKit.Tools
{
    partial class Get_DatabaseSize
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 25D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 25D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.listDatabses = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listSizes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "SQL Databse Sizes";
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.chart2);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Controls.Add(this.listSizes);
            this.panel1.Location = new System.Drawing.Point(198, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 329);
            this.panel1.TabIndex = 31;
            this.panel1.Visible = false;
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(401, 77);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Sizes";
            dataPoint1.Label = "Used";
            dataPoint2.Label = "Unused";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(365, 249);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            title1.Name = "Title1";
            title1.Text = "Transaction Log Space Usage";
            this.chart2.Titles.Add(title1);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(4, 77);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Sizes";
            dataPoint3.IsValueShownAsLabel = true;
            dataPoint3.Label = "Data";
            dataPoint4.IsValueShownAsLabel = true;
            dataPoint4.Label = "Index";
            dataPoint5.Color = System.Drawing.Color.Green;
            dataPoint5.IsValueShownAsLabel = true;
            dataPoint5.Label = "Free";
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(365, 249);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Data Files Space Usage";
            this.chart1.Titles.Add(title2);
            // 
            // listSizes
            // 
            this.listSizes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listSizes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listSizes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSizes.GridLines = true;
            this.listSizes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listSizes.Location = new System.Drawing.Point(4, 4);
            this.listSizes.Name = "listSizes";
            this.listSizes.Scrollable = false;
            this.listSizes.ShowGroups = false;
            this.listSizes.Size = new System.Drawing.Size(315, 66);
            this.listSizes.TabIndex = 3;
            this.listSizes.UseCompatibleStateImageBehavior = false;
            this.listSizes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Area";
            this.columnHeader1.Width = 211;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 100;
            // 
            // Get_DatabaseSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listDatabses);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label3);
            this.Name = "Get_DatabaseSize";
            this.Size = new System.Drawing.Size(1000, 400);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.ListBox listDatabses;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listSizes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}
