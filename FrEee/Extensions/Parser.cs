using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrEee.Utility;

namespace FrEee.Extensions
{
	/// <summary>
	/// Parses various things from strings.
	/// </summary>
	public static class Parser
	{
		private static readonly MethodInfo enumParser = typeof(Parser).GetMethods().Single(m => m.Name == "ParseEnum" && m.ContainsGenericParameters);

		private static SafeDictionary<Type, IDictionary<MemberInfo, object>> enumMemberCache = new SafeDictionary<Type, IDictionary<MemberInfo, object>>();

		private static SafeDictionary<Type, SafeDictionary<string, object>> enumValues = new SafeDictionary<Type, SafeDictionary<string, object>>(true);

		/// <summary>
		/// Parses a string as an enum, using custom names.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		public static T ParseEnum<T>(this string s)
		{
			if (typeof(T).HasAttribute<FlagsAttribute>())
				return s.ParseFlagsEnum<T>();
			else
				return s.ParseSingleEnum<T>();
		}

		/// <summary>
		/// Parses a string as an enum, using custom names.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static object ParseEnum(this string s, Type type)
		{
			var v = enumValues[type][s];
			if (v == null)
			{
				var parser = enumParser.MakeGenericMethod(type);
				v = enumValues[type][s] = parser.Invoke(null, new object[] { s.Trim() });
			}
			return v;
		}

		/// <summary>
		/// The inverse of ToUnitString. Parses a number with units.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>The parsed number, or null if the string could not be parsed.</returns>
		/// <param name="allowMilli">Should lowercase m be treated as milli or mega?</param>
		public static double? ParseUnits(this string s, bool allowMilli = false)
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

		private static IDictionary<MemberInfo, object> GetEnumValues<T>()
		{
			var names = Enum.GetNames(typeof(T));
			var mems = names.SelectMany(n => typeof(T).GetMember(n));
			var dict = new Dictionary<MemberInfo, object>();
			foreach (var mem in mems)
				dict.Add(mem, (T)Enum.Parse(typeof(T), mem.Name));
			return dict;
		}

		/// <summary>
		/// Parses a string as a flags enum.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		private static T ParseFlagsEnum<T>(this string s)
		{
			var spl = s.Split(',', '\\', '/');
			return spl.ParseFlagsEnum<T>();
		}

		/// <summary>
		/// Parses some strings as flags enum values.
		/// If the type is not a flags enum, just par
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ss"></param>
		/// <returns></returns>
		private static T ParseFlagsEnum<T>(this IEnumerable<string> ss)
		{
			if (!(typeof(T).IsEnum && typeof(T).HasAttribute<FlagsAttribute>()))
				throw new InvalidCastException("{0} is not a flags enum type.".F(typeof(T)));

			// TODO - non-integer enums
			var result = 0;

			var dict = GetEnumValues<T>();

			foreach (var x in ss.Select(s => s.ParseSingleEnum<T>()))
			{
				// why can't I cast x to int?
				var n = Enum.GetName(typeof(T), x);
				var i = (int)Enum.Parse(typeof(T), n);
				result |= i;
			}

			return (T)(object)result;
		}

		private static T ParseSingleEnum<T>(this string s)
		{
			s = s.Trim();

			if (!typeof(T).IsEnum)
				throw new InvalidCastException("{0} is not an enum type.".F(typeof(T)));

			if (enumMemberCache[typeof(T)] == null)
				enumMemberCache[typeof(T)] = GetEnumValues<T>();
			var dict = enumMemberCache[typeof(T)];
			var mems = dict.Keys;
			var matches = mems.Where(m => m.GetNames().Contains(s)).ToArray();
			MemberInfo match;
			if (matches.Count() == 1)
				match = matches.Single();
			else if (matches.Count() == 0)
				throw new ArgumentException("{0} is not a valid value or alias for enum type {1}.".F(s, typeof(T)));
			else
			{
				// find by canonical name
				var canonicalMatches = matches.Where(m => m.GetCanonicalName() == s);
				if (canonicalMatches.Count() == 1)
					match = canonicalMatches.Single();
				else
					throw new ArgumentException("{0} is an ambiguous match for {1}.".F(s, typeof(T)));
			}
			return (T)dict[match];
		}
	}
}
