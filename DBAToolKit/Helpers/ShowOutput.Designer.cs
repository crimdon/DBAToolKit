namespace DBAToolKit.Helpers
{
    partial class ShowOutput
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
            this.rtOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtOutput
            // 
            this.rtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtOutput.Location = new System.Drawing.Point(0, 0);
            this.rtOutput.Name = "rtOutput";
            this.rtOutput.ReadOnly = true;
            this.rtOutput.Size = new System.Drawing.Size(969, 276);
            this.rtOutput.TabIndex = 0;
            this.rtOutput.Text = "";
            // 
            // ShowOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtOutput);
            this.Name = "ShowOutput";
            this.Size = new System.Drawing.Size(969, 276);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtOutput;
    }
}
