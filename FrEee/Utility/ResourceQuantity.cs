using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;

namespace FrEee.Utility
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	[Serializable]
	public class ResourceQuantity : NamedDictionary<Resource, int>, IComparable<ResourceQuantity>, IComparable
	{
		public static ResourceQuantity operator +(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] + r2[key]);
			return result;
		}

		public static ResourceQuantity operator -(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] - r2[key]);
			return result;
		}

		public static ResourceQuantity operator *(ResourceQuantity r, double scalar)
		{
			var result = new ResourceQuantity();
			foreach (var key in r.Keys)
				result.Add(key, (int)Math.Round(r[key] * scalar));
			return result;
		}

		public static ResourceQuantity operator /(ResourceQuantity r, double scalar)
		{
			var result = new ResourceQuantity();
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
		public override void Add(KeyValuePair<Resource, int> item)
		{
			Add(item.Key, item.Value);
		}

		/// <summary>
		/// Adds resources. Does not overwrite the existing value, but adds it to the existing value instead.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public override void Add(Resource key, int value)
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
		public static ResourceQuantity Max(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
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
		public static ResourceQuantity Min(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
			if (r1 == null || r2 == null)
				return new ResourceQuantity();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, Math.Min(r1[key], r2[key]));
			return result;
		}

		public static bool operator ==(ResourceQuantity r1, ResourceQuantity r2)
		{
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] != r2[key])
					return false;
			}
			return true;
		}

		public static bool operator !=(ResourceQuantity r1, ResourceQuantity r2)
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
			if (obj is ResourceQuantity)
				return (ResourceQuantity)obj == this;
			return false;
		}

		public static bool operator >=(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] < r2[key])
					return false;
			}
			return true;
		}

		public static bool operator <=(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] > r2[key])
					return false;
			}
			return true;
		}

		public static bool operator >(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] < r2[key])
					return false;
			}
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] > r2[key])
					return true;
			}
			return false;
		}

		public static bool operator <(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] > r2[key])
					return false;
			}
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] < r2[key])
					return true;
			}
			return false;
		}

		public int CompareTo(ResourceQuantity other)
		{
			return this.Sum(kvp => kvp.Value).CompareTo(other.Sum(kvp => kvp.Value));
		}

		public int CompareTo(object obj)
		{
			if (obj is ResourceQuantity)
				return CompareTo((ResourceQuantity)obj);
			return this.Sum(kvp => kvp.Value).CompareTo(obj.ToString().ToInt());
		}

		public static ResourceQuantity Parse(string s)
		{
			var q = new ResourceQuantity();
			var resSplit = s.Split(',').Select(sub => sub.Trim());
			foreach (var res in resSplit)
			{
				var pos = res.IndexOf(" ");
				var amount = res.Substring(0, pos);
				var resName = res.Substring(pos + 1);
				q.Add(Resource.Find(resName), int.Parse(amount));
			}
			return q;
		}
	}
}
