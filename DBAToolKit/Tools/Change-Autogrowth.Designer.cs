namespace DBAToolKit.Tools
{
    partial class Change_Autogrowth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Change_Autogrowth));
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioInMegabytes = new System.Windows.Forms.RadioButton();
            this.radioInPercent = new System.Windows.Forms.RadioButton();
            this.groupMaxFileSize = new System.Windows.Forms.GroupBox();
            this.radioUnlimited = new System.Windows.Forms.RadioButton();
            this.radioLimitedTo = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numericLimitedTo = new System.Windows.Forms.NumericUpDown();
            this.numericInPercent = new System.Windows.Forms.NumericUpDown();
            this.numericInMegabytes = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupMaxFileSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLimitedTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInMegabytes)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(13, 13);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(116, 17);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "Enable Autogrowth";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioInMegabytes);
            this.groupBox1.Controls.Add(this.radioInPercent);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 73);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Growth";
            // 
            // radioInMegabytes
            // 
            this.radioInMegabytes.AutoSize = true;
            this.radioInMegabytes.Location = new System.Drawing.Point(7, 50);
            this.radioInMegabytes.Name = "radioInMegabytes";
            this.radioInMegabytes.Size = new System.Drawing.Size(89, 17);
            this.radioInMegabytes.TabIndex = 1;
            this.radioInMegabytes.TabStop = true;
            this.radioInMegabytes.Text = "In Megabytes";
            this.radioInMegabytes.UseVisualStyleBackColor = true;
            this.radioInMegabytes.CheckedChanged += new System.EventHandler(this.radioInMegabytes_CheckedChanged);
            // 
            // radioInPercent
            // 
            this.radioInPercent.AutoSize = true;
            this.radioInPercent.Location = new System.Drawing.Point(7, 20);
            this.radioInPercent.Name = "radioInPercent";
            this.radioInPercent.Size = new System.Drawing.Size(74, 17);
            this.radioInPercent.TabIndex = 0;
            this.radioInPercent.TabStop = true;
            this.radioInPercent.Text = "In Percent";
            this.radioInPercent.UseVisualStyleBackColor = true;
            this.radioInPercent.CheckedChanged += new System.EventHandler(this.radioInPercent_CheckedChanged);
            // 
            // groupMaxFileSize
            // 
            this.groupMaxFileSize.Controls.Add(this.radioUnlimited);
            this.groupMaxFileSize.Controls.Add(this.radioLimitedTo);
            this.groupMaxFileSize.Location = new System.Drawing.Point(13, 116);
            this.groupMaxFileSize.Name = "groupMaxFileSize";
            this.groupMaxFileSize.Size = new System.Drawing.Size(200, 73);
            this.groupMaxFileSize.TabIndex = 2;
            this.groupMaxFileSize.TabStop = false;
            this.groupMaxFileSize.Text = "Maxumum File Size";
            // 
            // radioUnlimited
            // 
            this.radioUnlimited.AutoSize = true;
            this.radioUnlimited.Location = new System.Drawing.Point(7, 50);
            this.radioUnlimited.Name = "radioUnlimited";
            this.radioUnlimited.Size = new System.Drawing.Size(68, 17);
            this.radioUnlimited.TabIndex = 1;
            this.radioUnlimited.TabStop = true;
            this.radioUnlimited.Text = "Unlimited";
            this.radioUnlimited.UseVisualStyleBackColor = true;
            this.radioUnlimited.CheckedChanged += new System.EventHandler(this.radioUnlimited_CheckedChanged);
            // 
            // radioLimitedTo
            // 
            this.radioLimitedTo.AutoSize = true;
            this.radioLimitedTo.Location = new System.Drawing.Point(7, 20);
            this.radioLimitedTo.Name = "radioLimitedTo";
            this.radioLimitedTo.Size = new System.Drawing.Size(95, 17);
            this.radioLimitedTo.TabIndex = 0;
            this.radioLimitedTo.TabStop = true;
            this.radioLimitedTo.Text = "Limited to (MB)";
            this.radioLimitedTo.UseVisualStyleBackColor = true;
            this.radioLimitedTo.CheckedChanged += new System.EventHandler(this.radioLimitedTo_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(207, 215);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // numericLimitedTo
            // 
            this.numericLimitedTo.Location = new System.Drawing.Point(287, 136);
            this.numericLimitedTo.Maximum = new decimal(new int[] {
            2097152,
            0,
            0,
            0});
            this.numericLimitedTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericLimitedTo.Name = "numericLimitedTo";
            this.numericLimitedTo.Size = new System.Drawing.Size(120, 20);
            this.numericLimitedTo.TabIndex = 8;
            this.numericLimitedTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericInPercent
            // 
            this.numericInPercent.Location = new System.Drawing.Point(287, 56);
            this.numericInPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericInPercent.Name = "numericInPercent";
            this.numericInPercent.Size = new System.Drawing.Size(120, 20);
            this.numericInPercent.TabIndex = 9;
            this.numericInPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericInMegabytes
            // 
            this.numericInMegabytes.Location = new System.Drawing.Point(287, 88);
            this.numericInMegabytes.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.numericInMegabytes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericInMegabytes.Name = "numericInMegabytes";
            this.numericInMegabytes.Size = new System.Drawing.Size(120, 20);
            this.numericInMegabytes.TabIndex = 10;
            this.numericInMegabytes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Change_Autogrowth
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 262);
            this.Controls.Add(this.numericInMegabytes);
            this.Controls.Add(this.numericInPercent);
            this.Controls.Add(this.numericLimitedTo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupMaxFileSize);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkEnable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Change_Autogrowth";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Auto Growth for";
            this.Shown += new System.EventHandler(this.Change_Autogrowth_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupMaxFileSize.ResumeLayout(false);
            this.groupMaxFileSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLimitedTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInMegabytes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioInMegabytes;
        private System.Windows.Forms.RadioButton radioInPercent;
        private System.Windows.Forms.GroupBox groupMaxFileSize;
        private System.Windows.Forms.RadioButton radioUnlimited;
        private System.Windows.Forms.RadioButton radioLimitedTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numericLimitedTo;
        private System.Windows.Forms.NumericUpDown numericInPercent;
        private System.Windows.Forms.NumericUpDown numericInMegabytes;
    }
}