using FrEee.Modding.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace FrEee.Utility.Extensions
{
	public static class ConversionExtensions
	{
		/// <summary>
		/// Computes the angle from one point to the other.
		/// Zero degrees is east, and positive is counterclockwise.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static double AngleTo(this Point p, Point target)
		{
			return Math.Atan2(target.Y - p.Y, target.X - p.X) * 180d / Math.PI;
		}

		/// <summary>
		/// Computes the angle from one point to the other.
		/// Zero degrees is north, and positive is clockwise.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static double AngleTo(this PointF p, PointF target)
		{
			return Math.Atan2(target.Y - p.Y, target.X - p.X) * 180d / Math.PI;
		}

		/// <summary>
		/// Casts an object to a type. Throws an exception if  the type is wrong.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <returns></returns>
		public static T CastTo<T>(this object o, T defaultValue = default(T))
		{
			return (T)((o ?? defaultValue) ?? default(T));
		}

		/// <summary>
		/// Parses a string as a boolean. Returns false if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool ToBool(this string s)
		{
			bool b;
			bool.TryParse(s, out b);
			return b;
		}

		/// <summary>
		/// Parses a string as a boolean. Returns false if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool ToBool(this IFormula f)
		{
			return f.Value.ToString().ToBool();
		}

		/// <summary>
		/// Parses a string as a double. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
		/// <returns></returns>
		public static double ToDouble(this string s, int cultureCode = 127)
		{
			double d;
			double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo(cultureCode), out d);
			return d;
		}

		/// <summary>
		/// Parses a string as a double. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
		/// <returns></returns>
		public static double ToDouble(this IFormula f, int cultureCode = 127)
		{
			// TODO - object.ToString() doesn't seem to take a culture code...
			return f.Value.ToString().ToDouble(cultureCode);
		}

		/// <summary>
		/// Parses a string as an integer. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static int ToInt(this string s)
		{
			int i;
			int.TryParse(s, out i);
			return i;
		}

		/// <summary>
		/// Parses a string as an integer. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static int ToInt(this IFormula f)
		{
			return f.Value.ToString().ToInt();
		}

		/// <summary>
		/// Gets a capital letter from the English alphabet.
		/// </summary>
		/// <param name="i">1 to 26</param>
		/// <returns>A to Z</returns>
		/// <exception cref="ArgumentException">if i is not from 1 to 26</exception>
		public static char ToLetter(this int i)
		{
			if (i < 1 || i > 26)
				throw new ArgumentException("Only 26 letters in the alphabet, can't get letter #" + i + ".", "i");
			return (char)('A' + i - 1);
		}

		public static int? ToNullableInt(this long? l)
		{
			if (l == null)
				return null;
			return (int)l.Value;
		}

		public static long? ToNullableLong(this int? i)
		{
			if (i == null)
				return null;
			return (long)i.Value;
		}

		public static Point ToPoint(this Vector2<int> v)
		{
			return new Point(v.X, v.Y);
		}

		public static string ToSafeString(this object o)
		{
			if (o == null)
				return null;
			return o.ToString();
		}

		/// <summary>
		/// Converts an object to a string with spaces between camelCased words.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static string ToSpacedString(this object o)
		{
			var sb = new StringBuilder();
			bool wasSpace = true;
			foreach (var c in o.ToString())
			{
				if (!wasSpace && (char.IsUpper(c) || char.IsNumber(c)))
					sb.Append(" ");
				sb.Append(c);
				wasSpace = char.IsWhiteSpace(c);
			}
			return sb.ToString();
		}

		public static string ToString(this double? d, string fmt)
		{
			if (d == null)
				return "";
			return d.Value.ToString(fmt);
		}

		/// <summary>
		/// Converts to a string using the invariant culture.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string ToStringInvariant(this IConvertible c)
		{
			return (string)Convert.ChangeType(c, typeof(string), CultureInfo.InvariantCulture);
		}
	}
}