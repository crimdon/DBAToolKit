using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAToolKit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var reg = Registry.CurrentUser.OpenSubKey("Software\\DBAToolKit", true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\DBAToolKit");
            }
            reg.Close();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
