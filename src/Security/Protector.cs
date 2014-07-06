using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace s7.cmDo.Security
{
    public class Protector
    {
        public static string Protect(string toProtect)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(toProtect);
            byte[] protectedPassword = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedPassword);
        }

        public static string Unprotect(string toUnprotect)
        {
            byte[] bytes = Convert.FromBase64String(toUnprotect);
            byte[] password = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(password);
        }
    }
}
