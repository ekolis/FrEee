using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Utility
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	[Serializable]
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

		/// <summary>
		/// Computes the maximum of two resource amounts.
		/// Missing values are treated as zeroes!
		/// </summary>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <returns></returns>
		public static Resources Max(Resources r1, Resources r2)
		{
			var result = new Resources();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, Math.Max(r1[key], r2[key]));
			return result;
		}

		/// <summary>
		/// Computes the minimum of two resource amounts.
		/// Missing values are treated as zeroes!
		/// </summary>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <returns></returns>
		public static Resources Min(Resources r1, Resources r2)
		{
			var result = new Resources();
			if (r1 == null || r2 == null)
				return new Resources();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, Math.Min(r1[key], r2[key]));
			return result;
		}

		public static bool operator ==(Resources r1, Resources r2)
		{
			if (((object)r1 == null) ^ ((object)r2 == null))
				return false;
			if (((object)r1 == null) && ((object)r2 == null))
				return true;
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] != r2[key])
					return false;
			}
			return true;
		}

		public static bool operator !=(Resources r1, Resources r2)
		{
			return !(r1 == r2);
		}

		public override int GetHashCode()
		{
			var hash = 0;
			foreach (var kvp in this)
			{
				if (kvp.Value != 0)
					hash ^= kvp.GetHashCode();
			}
			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj is Resources)
				return (Resources)obj == this;
			return false;
		}

		public static bool operator >=(Resources r1, Resources r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] < r2[key])
					return false;
			}
			return true;
		}

		public static bool operator <=(Resources r1, Resources r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] > r2[key])
					return false;
			}
			return true;
		}

		public static bool operator >(Resources r1, Resources r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] <= r2[key])
					return false;
			}
			return true;
		}

		public static bool operator <(Resources r1, Resources r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] >= r2[key])
					return false;
			}
			return true;
		}
	}
}
