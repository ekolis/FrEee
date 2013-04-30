using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee
{
	public static class Extensions
	{
		private static Random rand = new Random();

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

		/// <summary>
		/// Picks a random element from a sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickRandom<T>(this IEnumerable<T> src)
		{
			return src.ElementAt(rand.Next(src.Count()));
		}

		public static T MinOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Min();
		}

		public static TProp MinOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MinOrDefault();
		}

		public static T MaxOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Max();
		}

		public static TProp MaxOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MaxOrDefault();
		}
	}
}
