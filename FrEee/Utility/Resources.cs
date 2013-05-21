using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Utility
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	 [Serializable] public class Resources : SafeDictionary<string, int>
	{
		public static Resources operator +(Resources r1, Resources r2)
		{
			var result = new Resources();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] + r2[key]);
			return result;
		}

		public static Resources operator -(Resources r1, Resources r2)
		{
			var result = new Resources();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] - r2[key]);
			return result;
		}

		public static Resources operator *(Resources r, double scalar)
		{
			var result = new Resources();
			foreach (var key in r.Keys)
				result.Add(key, (int)Math.Round(r[key] * scalar));
			return result;
		}

		public static Resources operator /(Resources r, double scalar)
		{
			var result = new Resources();
			foreach (var key in r.Keys)
				result.Add(key, (int)Math.Round(r[key] / scalar));
			return result;
		}

		public override string ToString()
		{
			return string.Join(", ", this.Select(kvp => kvp.Value + " " + kvp.Key));
		}

		/// <summary>
		/// Adds resources. Does not overwrite the existing value, but adds it to the existing value instead.
		/// </summary>
		/// <param name="item"></param>
		public override void Add(KeyValuePair<string, int> item)
		{
			Add(item.Key, item.Value);
		}

		/// <summary>
		/// Adds resources. Does not overwrite the existing value, but adds it to the existing value instead.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public override void Add(string key, int value)
		{
			this[key] += value;
		}
	}
}
