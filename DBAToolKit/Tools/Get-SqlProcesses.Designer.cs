namespace DBAToolKit.Tools
{
    partial class Get_SqlProcesses
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.listProcesses = new BrightIdeasSoftware.DataListView();
            this.contextMenuProcesses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredServersSource = new DBAToolKit.Helpers.RegisteredServers();
            ((System.ComponentModel.ISupportInitialize)(this.listProcesses)).BeginInit();
            this.contextMenuProcesses.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "SQL Active Processes";
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
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(895, 36);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 23);
            this.btnDisplay.TabIndex = 29;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // listProcesses
            // 
            this.listProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listProcesses.CellEditUseWholeCell = false;
            this.listProcesses.ContextMenuStrip = this.contextMenuProcesses;
            this.listProcesses.DataSource = null;
            this.listProcesses.EmptyListMsg = "No active processes found";
            this.listProcesses.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listProcesses.FullRowSelect = true;
            this.listProcesses.HasCollapsibleGroups = false;
            this.listProcesses.Location = new System.Drawing.Point(4, 63);
            this.listProcesses.MultiSelect = false;
            this.listProcesses.Name = "listProcesses";
            this.listProcesses.ShowGroups = false;
            this.listProcesses.Size = new System.Drawing.Size(993, 325);
            this.listProcesses.TabIndex = 30;
            this.listProcesses.UseCompatibleStateImageBehavior = false;
            this.listProcesses.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuProcesses
            // 
            this.contextMenuProcesses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killProcessToolStripMenuItem});
            this.contextMenuProcesses.Name = "contextMenuProcesses";
            this.contextMenuProcesses.Size = new System.Drawing.Size(134, 26);
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.killProcessToolStripMenuItem.Text = "Kill Process";
            this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.killProcessToolStripMenuItem_Click);
            // 
            // registeredServersSource
            // 
            this.registeredServersSource.Location = new System.Drawing.Point(87, 36);
            this.registeredServersSource.Name = "registeredServersSource";
            this.registeredServersSource.SelectedServer = null;
            this.registeredServersSource.Size = new System.Drawing.Size(150, 20);
            this.registeredServersSource.TabIndex = 31;
            // 
            // Get_SqlProcesses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.registeredServersSource);
            this.Controls.Add(this.listProcesses);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "Get_SqlProcesses";
            this.Size = new System.Drawing.Size(1000, 400);
            ((System.ComponentModel.ISupportInitialize)(this.listProcesses)).EndInit();
            this.contextMenuProcesses.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisplay;
        private BrightIdeasSoftware.DataListView listProcesses;
        private System.Windows.Forms.ContextMenuStrip contextMenuProcesses;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private Helpers.RegisteredServers registeredServersSource;
    }
}
