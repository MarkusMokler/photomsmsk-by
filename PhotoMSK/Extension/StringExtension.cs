using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public static class StringExtension
    {
        public static string ToFilePath(this string str, int depth = 3, int letter = 2)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < depth; i++)
            {
                builder.Append(str.Substring(i * letter, letter));
                builder.Append(@"/");
            }

            return builder.ToString();
        }

        public static DateTime ToDateTime(this string str)
        {
            var intex =  str.IndexOf("GMT", StringComparison.Ordinal);
         
            return DateTime.ParseExact(str.Substring(0,intex-1),
                                  "ddd MMM d yyyy HH:mm:ss",
                                  CultureInfo.InvariantCulture);
        }
    }
}
