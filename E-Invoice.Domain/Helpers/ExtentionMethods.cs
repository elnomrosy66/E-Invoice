using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain
{
    public static class ExtentionMethods
    {
        public static string TooStringStarChar(this string x)
        {
            if (x.StartsWith("."))
            {
                x = "0" + x;
            }
            return x;
        }

        public static int ToInt(this string x)
        {
            if (x.Contains(".")) x = Convert.ToString(System.Math.Floor(double.Parse(x)));
            return int.Parse(x);
        }
        public static int ToInt(this object x) => Convert.ToInt32(Convert.ToString(x));

        public static short ToShort(this string x) => Convert.ToInt16(x);

        public static short ToShort(this object x) => Convert.ToInt16(Convert.ToString(x));

        public static long ToLong(this object x) => Convert.ToInt64(Convert.ToString(x));


        public static long ToLong(this string x) => Convert.ToInt64(x);





        public static string ToCurreny(this object x)
        {
            if (x == null || x.ToString() == "") return "0";

            var f = FormatNumber();
            if (f == 0) return x.ToString();

            return Math.Round(double.Parse(x.ToString()), f).ToString();

        }

        public static string ToDateFormat(this DateTime x)
        {
            if (x == null || x.ToString() == "") return DateTime.Now.ToString("yyyy/MM/dd");
            return x.ToString("yyyy/MM/dd");

        }

        static int FormatNumber()
        {

            //if (Info.Setting.FormatNumbers == FormatNumbers.Two) return 2;
            //if (Info.Setting.FormatNumbers == FormatNumbers.Four) return 4;
            return 2;

        }


        //public static string ToCurreny4(this object x, int mathNumber = 3)
        //{
        //    //  string str = new string('e', 3); repate string

        //    if (x == null || x.ToString() == "") return "0";
        //    return Math.Round(double.Parse(x.ToString()), mathNumber).ToString();
        //}

        public static bool ToBool(this object x)
        {

            if (x == null || x.ToString() == "") return false;
            return Convert.ToBoolean(x);

        }
        public static bool ToBool(this string x)
        {

            if (x == null || x.ToString() == "") return false;
            return Convert.ToBoolean(x);

        }


        public static string ToStringIsNull(this object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }


        public static double ToDouble(this object x)
        {

            if (x == null || x.ToString() == "") return 0;

            double str = Convert.ToDouble(x);

            //if (str.StartsWith(".")) 
            //{ 
            //    return ("0" + str).ToDouble();
            //}

            var f = FormatNumber();
            if (f == 0) return str;

            return Math.Round(str, f);

        }

        public static decimal ToDecimal(this object x)
        {

            if (x == null || x.ToString() == "") return 0;

            decimal str = Convert.ToDecimal(x);

            //if (str.StartsWith(".")) 
            //{ 
            //    return ("0" + str).ToDouble();
            //}

            var f = FormatNumber();
            if (f == 0) return str;

            return Math.Round(str, f);

        }
        public static string ToDatetTimeString(this object x)
        {

            if (x == null || x.ToString() == "") return "";


            return Convert.ToDateTime(x.ToString()).ToString("yyyy/MM/dd");

        }
        public static DateTime ToDate(this object x)
        {

            if (x == null || x.ToString() == "") return DateTime.Now;


            return Convert.ToDateTime(x.ToString());

        }
    }
}
