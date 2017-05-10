using DBAToolKit.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DBAToolKit.Tools
{
    public partial class NewConnectionForm : Form
    {
        public NewConnectionForm()
        {
            InitializeComponent();
        }

        private void NewConnectionForm_Load(object sender, EventArgs e)
        {
            cboAuthentication.SelectedIndex = 0;
        }

        private void cboAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboAuthentication.Text)
            {
                case "Windows Authentication":
                    txtUserName.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    break;
                case "SQL Server Authentication":
                    txtUserName.ReadOnly = false;
                    txtPassword.ReadOnly = false;
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                if (this.ValidateChildren())
                {
                    ConnectionManager sqlCmd = new ConnectionManager();
                    string strConn = sqlCmd.makeConnectionString(txtServerName.Text, cboAuthentication.SelectedIndex, txtUserName.Text, txtPassword.Text, "Master");
                    bool connectionOK = sqlCmd.testConnection(strConn);
                    if (!connectionOK)
                    {
                        errorProvider1.SetError(txtServerName, "Connection to SQL Server failed");
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        sqlCmd.saveConnectionString(txtServerName.Text, strConn);
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txtServerName, ex.Message);
                this.DialogResult = DialogResult.None;
            }
        }

        private void txtServerName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtServerName.Text))
            {
                errorProvider1.SetError(txtServerName, "Please ener a server name");
            }
            else
            {
                errorProvider1.SetError(txtServerName, null);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 1 && String.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "Please enter a user name");
            }
            else
            {
                errorProvider1.SetError(txtServerName, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 1 && String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Please enter a password");
            }
            else
            {
                errorProvider1.SetError(txtServerName, null);
            }
        }
    }
}
