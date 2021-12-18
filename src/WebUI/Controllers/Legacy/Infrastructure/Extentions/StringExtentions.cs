using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCore2.WebApp.Infrastructure
{
    public static class StringExtentions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToUpperInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }
    }
}
