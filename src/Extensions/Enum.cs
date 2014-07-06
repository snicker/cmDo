using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s7.cmDo.Extensions
{
    public static class EnumExtensions
    {
        public static bool TryParse<T>(this Enum theEnum, string strType, out T result)
        {
            string strTypeFixed = strType.Replace(' ', '_');
            if (Enum.IsDefined(typeof(T), strTypeFixed))
            {
                result = (T)Enum.Parse(typeof(T), strTypeFixed, true);
                return true;
            }
            long numTypeFixed = default(long);
            if (long.TryParse(strType, out numTypeFixed))
            {
                result = (T)Enum.Parse(typeof(T),numTypeFixed.ToString());
                return true;
            }
            foreach (string value in Enum.GetNames(typeof(T)))
            {
                if (value.Equals(strTypeFixed, StringComparison.OrdinalIgnoreCase))
                {
                    result = (T)Enum.Parse(typeof(T), value);
                    return true;
                }
            }
            result = default(T);
            return false;
        }
    }
}
