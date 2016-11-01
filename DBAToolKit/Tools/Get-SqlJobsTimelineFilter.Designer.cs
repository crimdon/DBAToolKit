namespace DBAToolKit.Tools
{
    partial class Get_SqlJobsTimelineFilter
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListCategories = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Show jobs in these categories";
            // 
            // checkedListCategories
            // 
            this.checkedListCategories.FormattingEnabled = true;
            this.checkedListCategories.Location = new System.Drawing.Point(13, 30);
            this.checkedListCategories.Name = "checkedListCategories";
            this.checkedListCategories.Size = new System.Drawing.Size(177, 199);
            this.checkedListCategories.TabIndex = 1;
            this.checkedListCategories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListCategories_ItemCheck_1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(115, 235);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Get_SqlJobsTimelineFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.checkedListCategories);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Get_SqlJobsTimelineFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Job Filters";
            this.Load += new System.EventHandler(this.Get_SqlJobsTimelineFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListCategories;
        private System.Windows.Forms.Button btnClose;
    }
}