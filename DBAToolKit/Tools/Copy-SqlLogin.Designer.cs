namespace DBAToolKit.Tools
{
    partial class Copy_SqlLogin
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.chksyncOnly = new System.Windows.Forms.CheckBox();
            this.chkForce = new System.Windows.Forms.CheckBox();
            this.chkSyncSaName = new System.Windows.Forms.CheckBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(111, 36);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(150, 20);
            this.txtSource.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Desination Server";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(434, 36);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(150, 20);
            this.txtDestination.TabIndex = 3;
            // 
            // chksyncOnly
            // 
            this.chksyncOnly.AutoSize = true;
            this.chksyncOnly.Location = new System.Drawing.Point(30, 73);
            this.chksyncOnly.Name = "chksyncOnly";
            this.chksyncOnly.Size = new System.Drawing.Size(74, 17);
            this.chksyncOnly.TabIndex = 5;
            this.chksyncOnly.Text = "Sync Only";
            this.chksyncOnly.UseVisualStyleBackColor = true;
            // 
            // chkForce
            // 
            this.chkForce.AutoSize = true;
            this.chkForce.Location = new System.Drawing.Point(187, 73);
            this.chkForce.Name = "chkForce";
            this.chkForce.Size = new System.Drawing.Size(53, 17);
            this.chkForce.TabIndex = 6;
            this.chkForce.Text = "Force";
            this.chkForce.UseVisualStyleBackColor = true;
            // 
            // chkSyncSaName
            // 
            this.chkSyncSaName.AutoSize = true;
            this.chkSyncSaName.Location = new System.Drawing.Point(308, 73);
            this.chkSyncSaName.Name = "chkSyncSaName";
            this.chkSyncSaName.Size = new System.Drawing.Size(106, 17);
            this.chkSyncSaName.TabIndex = 7;
            this.chkSyncSaName.Text = "Sync sa account";
            this.chkSyncSaName.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(16, 107);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(670, 246);
            this.txtOutput.TabIndex = 8;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(509, 73);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 9;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(234, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Copy SQL Logins";
            // 
            // Copy_SqlLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.chkSyncSaName);
            this.Controls.Add(this.chkForce);
            this.Controls.Add(this.chksyncOnly);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Name = "Copy_SqlLogin";
            this.Size = new System.Drawing.Size(701, 370);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.CheckBox chksyncOnly;
        private System.Windows.Forms.CheckBox chkForce;
        private System.Windows.Forms.CheckBox chkSyncSaName;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label3;
    }
}
