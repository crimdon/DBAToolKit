﻿namespace DBAToolKit.Tools
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 25D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 25D);
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50D);
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.listDatabses = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listSizes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
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
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(4, 77);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.Legend = "Legend1";
            series6.Name = "Sizes";
            dataPoint13.IsValueShownAsLabel = true;
            dataPoint13.Label = "Data";
            dataPoint14.IsValueShownAsLabel = true;
            dataPoint14.Label = "Index";
            dataPoint15.Color = System.Drawing.Color.Green;
            dataPoint15.IsValueShownAsLabel = true;
            dataPoint15.Label = "Free";
            series6.Points.Add(dataPoint13);
            series6.Points.Add(dataPoint14);
            series6.Points.Add(dataPoint15);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(365, 249);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            title6.Name = "Title1";
            title6.Text = "Data Files Space Usage";
            this.chart1.Titles.Add(title6);
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
            // chart2
            // 
            chartArea5.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart2.Legends.Add(legend5);
            this.chart2.Location = new System.Drawing.Point(401, 77);
            this.chart2.Name = "chart2";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series5.Legend = "Legend1";
            series5.Name = "Sizes";
            dataPoint11.Label = "Used";
            dataPoint12.Label = "Unused";
            series5.Points.Add(dataPoint11);
            series5.Points.Add(dataPoint12);
            this.chart2.Series.Add(series5);
            this.chart2.Size = new System.Drawing.Size(365, 249);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            title5.Name = "Title1";
            title5.Text = "Transaction Log Space Usage";
            this.chart2.Titles.Add(title5);
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
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
