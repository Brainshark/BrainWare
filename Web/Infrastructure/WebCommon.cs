using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Infrastructure.Util
{
    //basic shared helper class (ideally a separate library with many other utility functions shared among projects...maybe through on-premise Nuget server)
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            str = str?.Trim();
            return string.IsNullOrEmpty(str);
        }

        public static int ToInt(this string str)
        {
            int number;
            Int32.TryParse(str, out number);
            return number;
        }

        public static int ToInt(this object o)
        {
            if (o != null)
            {
                return o.ToString().ToInt();
            }
            return 0;
        }

        public static decimal ToDec(this object o)
        {
            return o.ToString().ToDec();
        }

        public static decimal ToDec(this string str)
        {
            decimal number;
            var retDec = decimal.TryParse(str, out number);
            return number;
        }

        public static T SafeDBNull<T>(this object value, T defaultValue)
        {
            if (value == null)
                return default(T);

            if (value is string)
                return (T)Convert.ChangeType(value, typeof(T));

            return (value == DBNull.Value) ? defaultValue : (T)value;
        }

        public static T SafeDBNull<T>(this object value)
        {
            return value.SafeDBNull(default(T));
        }

        public static bool IsNumeric(this string val)
        {
            try
            {
                decimal outval;
                String s = val.TrimStart(new Char[] { '0' });
                return !s.IsNullOrEmpty() && (decimal.TryParse(s, out outval));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}