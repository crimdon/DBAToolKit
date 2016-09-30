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
    }
}
