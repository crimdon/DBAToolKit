namespace DBAToolKit.Helpers
{
    partial class RegisteredServers
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
            this.cboServerName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboServerName
            // 
            this.cboServerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerName.FormattingEnabled = true;
            this.cboServerName.Location = new System.Drawing.Point(0, 0);
            this.cboServerName.Name = "cboServerName";
            this.cboServerName.Size = new System.Drawing.Size(150, 21);
            this.cboServerName.TabIndex = 0;
            this.cboServerName.DropDown += new System.EventHandler(this.cboServerName_DropDown);
            this.cboServerName.SelectedValueChanged += new System.EventHandler(this.cboServerName_SelectedValueChanged);
            // 
            // RegisteredServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboServerName);
            this.Name = "RegisteredServers";
            this.Size = new System.Drawing.Size(150, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboServerName;
    }
}
