using System;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// Custom math operations.
/// </summary>
public static class MathX
{
	/// <summary>
	/// Finds the ceiling of the specified number to the specified number of decimal places.
	/// </summary>
	/// <param name="num">The number.</param>
	/// <param name="decimalPlaces">The number of decimal places.</param>
	/// <returns></returns>
	public static double Ceiling(double num, int decimalPlaces = 0)
	{
		var dec = Math.Pow(0.1, decimalPlaces);
		return dec * Math.Ceiling(num / dec);
	}
}
