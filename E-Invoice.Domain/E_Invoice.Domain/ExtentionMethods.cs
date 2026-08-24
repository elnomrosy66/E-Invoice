using System;

namespace E_Invoice.Domain;

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
		if (x.Contains("."))
		{
			x = Convert.ToString(Math.Floor(double.Parse(x)));
		}
		return int.Parse(x);
	}

	public static int ToInt(this object x)
	{
		return Convert.ToInt32(Convert.ToString(x));
	}

	public static short ToShort(this string x)
	{
		return Convert.ToInt16(x);
	}

	public static short ToShort(this object x)
	{
		return Convert.ToInt16(Convert.ToString(x));
	}

	public static long ToLong(this object x)
	{
		return Convert.ToInt64(Convert.ToString(x));
	}

	public static long ToLong(this string x)
	{
		return Convert.ToInt64(x);
	}

	public static string ToCurreny(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return "0";
		}
		int num = FormatNumber();
		if (num == 0)
		{
			return x.ToString();
		}
		return Math.Round(double.Parse(x.ToString()), num).ToString();
	}

	public static string ToDateFormat(this DateTime x)
	{
		if (x.ToString() == "")
		{
			return DateTime.Now.ToString("yyyy/MM/dd");
		}
		return x.ToString("yyyy/MM/dd");
	}

	private static int FormatNumber()
	{
		return 2;
	}

	public static bool ToBool(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return false;
		}
		return Convert.ToBoolean(x);
	}

	public static bool ToBool(this string x)
	{
		if (x == null || x.ToString() == "")
		{
			return false;
		}
		return Convert.ToBoolean(x);
	}

	public static string ToStringIsNull(this object value)
	{
		if (value == null)
		{
			return "";
		}
		return value.ToString();
	}

	public static double ToDouble(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return 0.0;
		}
		double num = Convert.ToDouble(x);
		int num2 = FormatNumber();
		if (num2 == 0)
		{
			return num;
		}
		return Math.Round(num, num2);
	}

	public static decimal ToDecimal(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return 0m;
		}
		decimal num = Convert.ToDecimal(x);
		int num2 = FormatNumber();
		if (num2 == 0)
		{
			return num;
		}
		return Math.Round(num, num2);
	}

	public static string ToDatetTimeString(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return "";
		}
		return Convert.ToDateTime(x.ToString()).ToString("yyyy/MM/dd");
	}

	public static DateTime ToDate(this object x)
	{
		if (x == null || x.ToString() == "")
		{
			return DateTime.Now;
		}
		return Convert.ToDateTime(x.ToString());
	}
}
