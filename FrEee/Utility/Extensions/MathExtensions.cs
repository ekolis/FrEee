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
		public static string CeilingString(this double num, int decimalPlaces = 0)
		{
			return MathX.Ceiling(num, decimalPlaces).ToString("f" + decimalPlaces);
		}
	}
}
