using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Utility
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	[Serializable]
	public class ResourceQuantity : SafeDictionary<Resource, int>, IComparable<ResourceQuantity>, IComparable
	{
		/// <summary>
		/// Is this quantity all zeroes?
		/// </summary>
		public bool IsEmpty => this.All(kvp => kvp.Value == 0);

		public ResourceQuantity()
		{

		}

		public ResourceQuantity(ResourceQuantity q)
		{
			if (q != null)
			{
				foreach (var x in q)
					Add(x);
			}
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

		public static ResourceQuantity operator -(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] - r2[key]);
			return result;
		}

		public static bool operator !=(ResourceQuantity? r1, ResourceQuantity? r2)
		{
			return !(r1 == r2);
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

		public static ResourceQuantity operator +(ResourceQuantity r1, ResourceQuantity r2)
		{
			var result = new ResourceQuantity();
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] + r2[key]);
			return result;
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

		public static bool operator <=(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] > r2[key])
					return false;
			}
			return true;
		}

		public static bool operator ==(ResourceQuantity? r1, ResourceQuantity? r2)
		{
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			foreach (var key in r1!.Keys.Union(r2!.Keys))
			{
				if (r1[key] != r2[key])
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

		public static bool operator >=(ResourceQuantity r1, ResourceQuantity r2)
		{
			foreach (var key in r1.Keys.Union(r2.Keys))
			{
				if (r1[key] < r2[key])
					return false;
			}
			return true;
		}

		public static ResourceQuantity Parse(string s)
		{
			var q = new ResourceQuantity();
			if (s.Length > 0)
			{
				var resSplit = s.Split(',').Select(sub => sub.Trim());
				foreach (var res in resSplit)
				{
					var pos = res.IndexOf(" ");
					var amount = res.Substring(0, pos);
					var resName = res.Substring(pos + 1);
					q.Add(Resource.Find(resName), int.Parse(amount));
				}
			}
			return q;
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

		public int CompareTo(ResourceQuantity? other)
		{
			return this.Sum(kvp => kvp.Value).CompareTo(other.Sum(kvp => kvp.Value));
		}

		public int CompareTo(object? obj)
		{
			if (obj is ResourceQuantity)
				return CompareTo((ResourceQuantity)obj);
			return this.Sum(kvp => kvp.Value).CompareTo(obj?.ToString().ToInt());
		}

		public override bool Equals(object? obj)
		{
			if (obj is ResourceQuantity rq)
				return this == rq;
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.MashList(this.Where(kvp => kvp.Value != 0));
		}

		public override string ToString()
		{
			return string.Join(", ", this.Select(kvp => kvp.Value + " " + kvp.Key));
		}
	}
}
