using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility.Extensions
{
	public static class MathExtensions
	{
		/// <summary>
		/// Finds the ceiling of the specified number to the specified number of decimal places and returns it as a string.
		/// </summary>
		/// <param name="num">The number.</param>
		/// <param name="decimalPlaces">The decimal places.</param>
		/// <returns></returns>
		public static string CeilingString(this double? num, int decimalPlaces = 0)
		{
			if (num == null)
				return null;
			return num.Value.CeilingString(decimalPlaces);
		}

		/// <summary>
		/// Finds the ceiling of the specified number to the specified number of decimal places and returns it as a string.
		/// </summary>
		/// <param name="num">The number.</param>
		/// <param name="decimalPlaces">The decimal places.</param>
		/// <returns></returns>
		public static string CeilingString(this double num, int decimalPlaces = 0)
		{
			return MathX.Ceiling(num, decimalPlaces).ToString("f" + decimalPlaces);
		}

		/// <summary>
		/// Converts a percentage into a ratio.
		/// </summary>
		/// <param name="i">The percentage, e.g. 50</param>
		/// <returns>The ratio, e.g. 0.5</returns>
		public static double Percent(this int i)
		{
			return (double)i / 100d;
		}

		/// <summary>
		/// Multiplies a number by a percentage.
		/// </summary>
		/// <param name="p">The percentage, e.g. 50</param>
		/// <returns></returns>
		public static double PercentOf(this int p, double d)
		{
			return p * d / 100d;
		}

		/// <summary>
		/// Multiplies a number by a percentage.
		/// </summary>
		/// <param name="p">The percentage, e.g. 50</param>
		/// <returns></returns>
		public static double PercentOf(this double p, double d)
		{
			return p * d / 100d;
		}

		/// <summary>
		/// Multiplies an integer by a percentage and rounds it.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static int PercentOfRounded(this int p, int i)
		{
			if (p * i < 0)
				return -PercentOfRounded(p, -i);
			// we don't want to use Math.Round because it rounds to the nearest even integer when at 0.5 and we want to always round up
			var temp = i * p / 100d;
			var ipart = Math.Floor((double)temp);
			var dpart = temp - ipart;
			if (dpart >= 0.5)
				return (int)(ipart + 1);
			else
				return (int)ipart;
		}
	}
}
