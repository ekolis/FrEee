using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FrEee.Utility.Extensions
{
    public static class ConversionExtensions
    {
        #region Private Fields

        private static IDictionary<int, string> RomanNumeralCache = new Dictionary<int, string>();

        private static Tuple<int, string>[] RomanNumeralParts = new Tuple<int, string>[]
                        {
            Tuple.Create(1000, "M"),
            Tuple.Create(900, "CM"),
            Tuple.Create(500, "D"),
            Tuple.Create(400, "CD"),
            Tuple.Create(100, "C"),
            Tuple.Create(90, "XC"),
            Tuple.Create(50, "L"),
            Tuple.Create(40, "XL"),
            Tuple.Create(10, "X"),
            Tuple.Create(9, "IX"),
            Tuple.Create(5, "V"),
            Tuple.Create(4, "IV"),
            Tuple.Create(1, "I"),
                        };

        #endregion Private Fields

        #region Public Methods

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
        public static T CastTo<T>(this object o)
        {
            return (T)o;
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

        /// <summary>
        /// Gets a roman numeral.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToRomanNumeral(this int i)
        {
            // do we already know this?
            if (!RomanNumeralCache.ContainsKey(i))
            {
                // get silly negative numbers and zeroes out of the way
                if (i < 0)
                    RomanNumeralCache.Add(i, "-" + ToRomanNumeral(-i));
                else if (i == 0)
                    RomanNumeralCache.Add(i, "");
                else
                {
                    // scan the roman numeral parts list recursively
                    foreach (var part in RomanNumeralParts.OrderByDescending(part => part.Item1))
                    {
                        if (i >= part.Item1)
                        {
                            RomanNumeralCache.Add(i, part.Item2 + (i - part.Item1).ToRomanNumeral());
                            break;
                        }
                    }
                }
            }

            return RomanNumeralCache[i];
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

        /// <summary>
        /// Converts a turn number to a stardate.
        /// </summary>
        /// <param name="turnNumber"></param>
        /// <returns></returns>
        public static string ToStardate(this int turnNumber)
        {
            // TODO - moddable starting stardate?
            return ((turnNumber + 23999) / 10.0).ToString("0.0");
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

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this long? value, bool bForBillions = false, int sigfigs = 4, string undefinedValue = "Undefined")
        {
            if (value == null)
                return undefinedValue;
            return value.Value.ToUnitString(bForBillions, sigfigs);
        }

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this long value, bool bForBillions = false, int sigfigs = 4)
        {
            if (Math.Abs(value) >= 1e12 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e12));
                var decimals = sigfigs - 1 - log;
                return (value / 1e12).ToString("f" + decimals) + "T";
            }
            if (Math.Abs(value) >= 1e9 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e9));
                var decimals = sigfigs - 1 - log;
                return (value / 1e9).ToString("f" + decimals) + (bForBillions ? "B" : "G");
            }
            if (Math.Abs(value) >= 1e6 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e6));
                var decimals = sigfigs - 1 - log;
                return (value / 1e6).ToString("f" + decimals) + "M";
            }
            if (Math.Abs(value) >= 1e3 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e3));
                var decimals = sigfigs - 1 - log;
                return (value / 1e3).ToString("f" + decimals) + "k";
            }
            return value.ToString();
        }

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this int? value, bool bForBillions = false, int sigfigs = 4)
        {
            return ((long?)value).ToUnitString(bForBillions, sigfigs);
        }

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this int value, bool bForBillions = false, int sigfigs = 4)
        {
            return ((long)value).ToUnitString(bForBillions, sigfigs);
        }

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this double value, bool bForBillions = false, int sigfigs = 4)
        {
            if (Math.Abs(value) >= 1e12 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e12));
                var decimals = sigfigs - 1 - log;
                return (value / 1e12).ToString("f" + decimals) + "T";
            }
            if (Math.Abs(value) >= 1e9 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e9));
                var decimals = sigfigs - 1 - log;
                return (value / 1e9).ToString("f" + decimals) + (bForBillions ? "B" : "G");
            }
            if (Math.Abs(value) >= 1e6 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e6));
                var decimals = sigfigs - 1 - log;
                return (value / 1e6).ToString("f" + decimals) + "M";
            }
            if (Math.Abs(value) >= 1e3 * Math.Pow(10, sigfigs - 3))
            {
                var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e3));
                var decimals = sigfigs - 1 - log;
                return (value / 1e3).ToString("f" + decimals) + "k";
            }
            return value.ToString();
        }

        /// <summary>
        /// Adds SI prefixes to a value and rounds it off.
        /// e.g. 25000 becomes 25.00k
        /// </summary>
        /// <param name="value"></param>
        public static string ToUnitString(this double? value, bool bForBillions = false, int sigfigs = 4, string undefinedValue = "Undefined")
        {
            if (value == null)
                return undefinedValue;
            return value.Value.ToUnitString(bForBillions, sigfigs);
        }

        #endregion Public Methods
    }
}
