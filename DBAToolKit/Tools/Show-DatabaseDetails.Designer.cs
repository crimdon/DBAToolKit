namespace DBAToolKit.Tools
{
    partial class Show_DatabaseDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Show_DatabaseDetails));
            this.listPages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listDetails = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listPages
            // 
            this.listPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listPages.FormattingEnabled = true;
            this.listPages.Location = new System.Drawing.Point(13, 26);
            this.listPages.Name = "listPages";
            this.listPages.Size = new System.Drawing.Size(118, 498);
            this.listPages.TabIndex = 0;
            this.listPages.SelectedValueChanged += new System.EventHandler(this.listPages_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label1.Size = new System.Drawing.Size(118, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select a page        ";
            // 
            // listDetails
            // 
            this.listDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDetails.GridLines = true;
            this.listDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listDetails.HideSelection = false;
            this.listDetails.Location = new System.Drawing.Point(151, 26);
            this.listDetails.Name = "listDetails";
            this.listDetails.Size = new System.Drawing.Size(644, 498);
            this.listDetails.TabIndex = 3;
            this.listDetails.UseCompatibleStateImageBehavior = false;
            // 
            // Show_DatabaseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 542);
            this.Controls.Add(this.listDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listPages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Show_DatabaseDetails";
            this.Text = "Database Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listPages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listDetails;
    }
}