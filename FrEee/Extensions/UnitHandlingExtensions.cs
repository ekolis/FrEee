using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Extensions;

public static class UnitHandlingExtensions
{
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

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this long? value, string undefinedValue = "Undefined")
	{
		if (value == null)
			return undefinedValue;
		return value.Value.Kilotons();
	}

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this long value)
	{
		if (value < 10000)
			return value + "kT";
		return (value * 1000).ToUnitString() + "T";
	}

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this int? value, string nullText = "Undefined")
	{
		if (value == null)
			return nullText;
		return ((long?)value).Kilotons();
	}

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this int value)
	{
		return ((long)value).Kilotons();
	}

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this double? value, string undefinedValue = "Undefined")
	{
		if (value == null)
			return undefinedValue;
		return value.Value.Kilotons();
	}

	/// <summary>
	/// Displays a number in kT, MT, etc.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Kilotons(this double value)
	{
		return (value * 1000).ToUnitString() + "T";
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
}