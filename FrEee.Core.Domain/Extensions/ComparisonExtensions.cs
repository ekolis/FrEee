﻿using System;
using System.Globalization;
using FrEee.Objects.GameState;
using FrEee.Modding;

namespace FrEee.Extensions;

public static class ComparisonExtensions
{
	/// <summary>
	/// Determines if a string can be parsed as an boolean.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static bool IsBool(this string s)
	{
		bool b;
		return bool.TryParse(s, out b);
	}

	/// <summary>
	/// Determines if a string can be parsed as an boolean.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static bool IsBool(this IFormula f)
	{
		return f.Value.ToString().IsBool();
	}

	/// <summary>
	/// Determines if a string can be parsed as a double.
	/// </summary>
	/// <param name="s"></param>
	/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
	/// <returns></returns>
	public static bool IsDouble(this string s, int cultureCode = 127)
	{
		double d;
		return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo(cultureCode), out d);
	}

	/// <summary>
	/// Determines if a string can be parsed as a double.
	/// </summary>
	/// <param name="s"></param>
	/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
	/// <returns></returns>
	public static bool IsDouble(this IFormula f, int cultureCode = 127)
	{
		// TODO - object.ToString() doesn't seem to take a culture code...
		return f.Value.ToString().IsDouble(cultureCode);
	}

	/// <summary>
	/// Determines if a string can be parsed as an integer.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static bool IsInt(this string s)
	{
		int i;
		return int.TryParse(s, out i);
	}

	/// <summary>
	/// Determines if a formula can be parsed as an integer.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static bool IsInt(this IFormula f)
	{
		return f.Value.ToString().IsInt();
	}

	/// <summary>
	/// Gets a description of an object's timestamp as a memory age.
	/// </summary>
	/// <param name="timestamp">The timestamp.</param>
	/// <returns>The description.</returns>
	public static string GetMemoryAgeDescription(this double timestamp)
	{
		if (timestamp == Game.Current.Timestamp)
			return "Current";
		else if (Game.Current.Timestamp - timestamp <= 1)
			return "Memory from last turn";
		else
			return $"Memory from {Math.Ceiling(Game.Current.Timestamp - timestamp)} turns ago";
	}
}