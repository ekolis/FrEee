using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	public class Resources : SafeDictionary<string, int>
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
	}
}
