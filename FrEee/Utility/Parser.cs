using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Parses various things from strings.
	/// </summary>
	public class Parser
	{
		/// <summary>
		/// The inverse of ToUnitString. Parses a number with units.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>The parsed number, or null if the string could not be parsed.</returns>
		/// <param name="allowMilli">Should lowercase m be treated as milli or mega?</param>
		public static double? Units(string s, bool allowMilli = false)
		{
			var last = s.Last();
			if (char.IsNumber(last))
				return double.Parse(s); // no unit
			s = s.Substring(0, s.Length - 1);
			double num;
			if (!double.TryParse(s, out num))
				return null; // can't parse the number
			if (last == 'k' || last == 'K')
				return num * 1e3;
			if (last == 'M')
				return num * 1e6;
			if (last == 'G' || last == 'B' || last == 'g' || last == 'b') // giga or billions
				return num * 1e9;
			if (last == 'T' || last == 't')
				return num * 1e12;
			if (last == 'm')
				return num * (allowMilli ? 1e-3 : 1e6); // treat as mega if milli isn't allowed
			return null; // can't parse the units
		}

		public static object NullableEnum(Type t, string s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return null;
			return Enum.Parse(t, s);
		}
	}
}
