namespace DBAToolKit.Tools
{
    partial class Copy_DatabaseMail
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
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.chkDropDest = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.registeredServersSource = new DBAToolKit.Helpers.RegisteredServers();
            this.showOutput = new DBAToolKit.Helpers.ShowOutput();
            this.registeredServersDestination = new DBAToolKit.Helpers.RegisteredServers();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Copy Database Mail";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(780, 69);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 28;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Desination Server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Source Server";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Action";
            // 
            // cboAction
            // 
            this.cboAction.FormattingEnabled = true;
            this.cboAction.Location = new System.Drawing.Point(112, 60);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(150, 21);
            this.cboAction.TabIndex = 33;
            this.cboAction.SelectedIndexChanged += new System.EventHandler(this.cboAction_SelectedIndexChanged);
            // 
            // chkDropDest
            // 
            this.chkDropDest.AutoSize = true;
            this.chkDropDest.Location = new System.Drawing.Point(320, 68);
            this.chkDropDest.Name = "chkDropDest";
            this.chkDropDest.Size = new System.Drawing.Size(148, 17);
            this.chkDropDest.TabIndex = 34;
            this.chkDropDest.Text = "Drop Destination (if exists)";
            this.chkDropDest.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(687, 25);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(121, 23);
            this.btnSelect.TabIndex = 35;
            this.btnSelect.Text = "Select Items To Copy";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // registeredServersSource
            // 
            this.registeredServersSource.Location = new System.Drawing.Point(112, 28);
            this.registeredServersSource.Name = "registeredServersSource";
            this.registeredServersSource.SelectedServer = null;
            this.registeredServersSource.Size = new System.Drawing.Size(150, 20);
            this.registeredServersSource.TabIndex = 37;
            this.registeredServersSource.SelectedServerChanged += new System.EventHandler(this.registeredServersSource_SelectedServerChanged);
            // 
            // showOutput
            // 
            this.showOutput.Location = new System.Drawing.Point(8, 101);
            this.showOutput.Name = "showOutput";
            this.showOutput.Size = new System.Drawing.Size(969, 276);
            this.showOutput.TabIndex = 36;
            // 
            // registeredServersDestination
            // 
            this.registeredServersDestination.Location = new System.Drawing.Point(433, 27);
            this.registeredServersDestination.Name = "registeredServersDestination";
            this.registeredServersDestination.SelectedServer = null;
            this.registeredServersDestination.Size = new System.Drawing.Size(150, 20);
            this.registeredServersDestination.TabIndex = 38;
            // 
            // Copy_DatabaseMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.registeredServersDestination);
            this.Controls.Add(this.registeredServersSource);
            this.Controls.Add(this.showOutput);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkDropDest);
            this.Controls.Add(this.cboAction);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "Copy_DatabaseMail";
            this.Size = new System.Drawing.Size(1000, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.CheckBox chkDropDest;
        private System.Windows.Forms.Button btnSelect;
        private Helpers.ShowOutput showOutput;
        private Helpers.RegisteredServers registeredServersSource;
        private Helpers.RegisteredServers registeredServersDestination;
    }
}
