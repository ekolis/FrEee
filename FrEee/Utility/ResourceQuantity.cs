using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Utility.Extensions;

namespace FrEee.Utility
{
	/// <summary>
	/// Quantities of resources.
	/// </summary>
	[Serializable]
	public class ResourceQuantity : IDictionary<Resource, int>, IComparable<ResourceQuantity>, IComparable
	{
		public ResourceQuantity()
		{
			resources = new SafeDictionary<string, int>();
		}

		private SafeDictionary<string, int> resources { get; set; }

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
		public void Add(KeyValuePair<Resource, int> item)
		{
			Add(item.Key, item.Value);
		}

		/// <summary>
		/// Adds resources. Does not overwrite the existing value, but adds it to the existing value instead.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(Resource key, int value)
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

		public bool ContainsKey(Resource key)
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.ContainsKey(key.Name);
		}

		public ICollection<Resource> Keys
		{
			get
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				return resources.Select(kvp => Resource.Find(kvp.Key)).ToList();
			}
		}

		public bool Remove(Resource key)
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.Remove(key.Name);
		}

		public bool TryGetValue(Resource key, out int value)
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.TryGetValue(key.Name, out value);
		}

		public ICollection<int> Values
		{
			get
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				return resources.Values;
			}
		}

		public int this[Resource key]
		{
			get
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				if (key == null || key.Name == null)
					return 0;
				return resources[key.Name];
			}
			set
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				resources[key.Name] = value;
			}
		}


		public void Clear()
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			resources.Clear();
		}

		public bool Contains(KeyValuePair<Resource, int> item)
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.Contains(Convert(item));
		}

		private IDictionary<Resource, int> ToDictionary()
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.Select(kvp => Convert(kvp)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}

		private static KeyValuePair<Resource, int> Convert(KeyValuePair<string, int> kvp)
		{
			return new KeyValuePair<Resource, int>(Resource.Find(kvp.Key), kvp.Value);
		}

		private static KeyValuePair<string, int> Convert(KeyValuePair<Resource, int> kvp)
		{
			return new KeyValuePair<string, int>(kvp.Key.Name, kvp.Value);
		}

		public void CopyTo(KeyValuePair<Resource, int>[] array, int arrayIndex)
		{
			ToDictionary().CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				return resources.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (resources == null)
					resources = new SafeDictionary<string, int>();
				return resources.IsReadOnly;
			}
		}

		public bool Remove(KeyValuePair<Resource, int> item)
		{
			if (resources == null)
				resources = new SafeDictionary<string, int>();
			return resources.Remove(Convert(item));
		}

		public IEnumerator<KeyValuePair<Resource, int>> GetEnumerator()
		{
			return ToDictionary().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
