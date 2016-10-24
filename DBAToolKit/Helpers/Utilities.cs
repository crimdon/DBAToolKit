using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace DBAToolKit.Helpers
{
    public class Utilities
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            byte[] bytearray = (byte[])obj;
            return bytearray;
        }

        public static SecureString MakeSecureString(string inputstring)
        {
            var secure = new SecureString();
            foreach (char c in inputstring)
            {
                secure.AppendChar(c);
            }
            return secure;
        }

        public static bool CheckForSmo()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Microsoft SQL Server\\SharedManagementObjects\\CurrentVersion"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("Version");
                        if (o != null)
                        {
                            Version version = new Version(o as String);  
                            if (version.Major > 10)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            catch
            {
                return false;
            }

            return false;
        }

        public static String GetPercentage(Int32 value, Int32 total, Int32 places)
        {
            Decimal percent = 0;
            String retval = string.Empty;
            String strplaces = new String('0', places);

            if (value == 0 || total == 0)
            {
                percent = 0;
            }

            else
            {
                percent = Decimal.Divide(value, total) * 100;

                if (places > 0)
                {
                    strplaces = "." + strplaces;
                }
            }

            retval = percent.ToString("#" + strplaces);

            return retval;
        }
    }
}
