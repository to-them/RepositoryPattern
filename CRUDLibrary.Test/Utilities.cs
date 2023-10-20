using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestCRUDLibrary.Test
{
    public class Utilities
    {
        public enum LogType
        {
            Info,
            Warning,
            Error
        }

        public static string GenerateTempPwd(int PasswordLength)
        {
            // add any more characters that you wish! 
            string strChars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
            Random r = new Random();
            string strNewPassword = string.Empty;
            for (int i = 0; i < PasswordLength; i++)
            {
                int intRandom = r.Next(0, strChars.Length);
                strNewPassword += strChars[intRandom];
            }
            return strNewPassword;

        }
    }
}
