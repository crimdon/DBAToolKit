using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAToolKit.Helpers
{
    public partial class ShowOutput : UserControl
    {
        public ShowOutput()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            rtOutput.Clear();
        }

        public void displayOutput(string message, bool errormessage = false)
        {
            if (errormessage)
                AppendText(message, Color.Red);
            else
                AppendText(message, Color.Black);
        }

        private void AppendText(string text, Color color)
        {
            rtOutput.SelectionStart = rtOutput.TextLength;
            rtOutput.SelectionLength = 0;

            rtOutput.SelectionColor = color;
            if (!string.IsNullOrWhiteSpace(rtOutput.Text))
                rtOutput.AppendText("\r\n" + text);
            else
                rtOutput.AppendText(text);
            rtOutput.SelectionColor = rtOutput.ForeColor;
            rtOutput.ScrollToCaret();
        }
    }
}
