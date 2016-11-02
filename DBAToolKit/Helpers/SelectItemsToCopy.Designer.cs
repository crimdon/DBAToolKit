namespace DBAToolKit.Helpers
{
    partial class SelectItemsToCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectItemsToCopy));
            this.checkedListItemsToCopy = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkedListItemsToCopy
            // 
            this.checkedListItemsToCopy.FormattingEnabled = true;
            this.checkedListItemsToCopy.Location = new System.Drawing.Point(13, 13);
            this.checkedListItemsToCopy.Name = "checkedListItemsToCopy";
            this.checkedListItemsToCopy.Size = new System.Drawing.Size(220, 304);
            this.checkedListItemsToCopy.TabIndex = 0;
            this.checkedListItemsToCopy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListItemsToCopy_ItemCheck);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(157, 321);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(13, 321);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // SelectItemsToCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 356);
            this.ControlBox = false;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.checkedListItemsToCopy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectItemsToCopy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Items To Copy";
            this.Load += new System.EventHandler(this.SelectItemsToCopy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListItemsToCopy;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkSelectAll;
    }
}