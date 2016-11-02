namespace DBAToolKit.Tools
{
    partial class Copy_SqlJobs
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
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.chkDisableOnSource = new System.Windows.Forms.CheckBox();
            this.chkDisableOnDest = new System.Windows.Forms.CheckBox();
            this.chkDropDest = new System.Windows.Forms.CheckBox();
            this.showOutput = new DBAToolKit.Helpers.ShowOutput();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Copy SQL Jobs";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(755, 69);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(435, 27);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(150, 20);
            this.txtDestination.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Desination Server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Source Server";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(112, 27);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(150, 20);
            this.txtSource.TabIndex = 0;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            // 
            // chkDisableOnSource
            // 
            this.chkDisableOnSource.AutoSize = true;
            this.chkDisableOnSource.Location = new System.Drawing.Point(85, 69);
            this.chkDisableOnSource.Name = "chkDisableOnSource";
            this.chkDisableOnSource.Size = new System.Drawing.Size(113, 17);
            this.chkDisableOnSource.TabIndex = 20;
            this.chkDisableOnSource.Text = "Disable on Source";
            this.chkDisableOnSource.UseVisualStyleBackColor = true;
            // 
            // chkDisableOnDest
            // 
            this.chkDisableOnDest.AutoSize = true;
            this.chkDisableOnDest.Location = new System.Drawing.Point(256, 69);
            this.chkDisableOnDest.Name = "chkDisableOnDest";
            this.chkDisableOnDest.Size = new System.Drawing.Size(132, 17);
            this.chkDisableOnDest.TabIndex = 21;
            this.chkDisableOnDest.Text = "Disable on Destination";
            this.chkDisableOnDest.UseVisualStyleBackColor = true;
            // 
            // chkDropDest
            // 
            this.chkDropDest.AutoSize = true;
            this.chkDropDest.Location = new System.Drawing.Point(448, 69);
            this.chkDropDest.Name = "chkDropDest";
            this.chkDropDest.Size = new System.Drawing.Size(148, 17);
            this.chkDropDest.TabIndex = 22;
            this.chkDropDest.Text = "Drop Destination (if exists)";
            this.chkDropDest.UseVisualStyleBackColor = true;
            // 
            // showOutput
            // 
            this.showOutput.Location = new System.Drawing.Point(17, 98);
            this.showOutput.Name = "showOutput";
            this.showOutput.Size = new System.Drawing.Size(969, 276);
            this.showOutput.TabIndex = 23;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(675, 23);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(127, 23);
            this.btnSelect.TabIndex = 24;
            this.btnSelect.Text = "Select Jobs To Copy";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // Copy_SqlJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.showOutput);
            this.Controls.Add(this.chkDropDest);
            this.Controls.Add(this.chkDisableOnDest);
            this.Controls.Add(this.chkDisableOnSource);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label3);
            this.Name = "Copy_SqlJobs";
            this.Size = new System.Drawing.Size(1000, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.CheckBox chkDisableOnSource;
        private System.Windows.Forms.CheckBox chkDisableOnDest;
        private System.Windows.Forms.CheckBox chkDropDest;
        private Helpers.ShowOutput showOutput;
        private System.Windows.Forms.Button btnSelect;
    }
}
