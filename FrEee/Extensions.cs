using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee
{
	public static class Extensions
	{
		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this int value)
		{
			if (Math.Abs(value) >= 1e13)
				return Math.Round(value / 1e12, 2) + "T";
			if (Math.Abs(value) >= 1e12)
				return Math.Round(value / 1e12, 3) + "T";
			if (Math.Abs(value) >= 1e10)
				return Math.Round(value / 1e9, 2) + "G";
			if (Math.Abs(value) >= 1e9)
				return Math.Round(value / 1e9, 3) + "G";
			if (Math.Abs(value) >= 1e7)
				return Math.Round(value / 1e6, 2) + "M";
			if (Math.Abs(value) >= 1e6)
				return Math.Round(value / 1e6, 3) + "M";
			if (Math.Abs(value) >= 1e4)
				return Math.Round(value / 1e3, 2) + "k";
			return value.ToString();
		}
	}
}
