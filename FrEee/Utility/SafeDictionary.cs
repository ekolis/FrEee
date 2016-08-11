using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FrEee.Utility.Extensions;

namespace FrEee.Utility
{
	/// <summary>
	/// A dictionary that does not throw exceptions when manipulating nonexistent data or overwriting old data.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[Serializable]
	public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private Dictionary<TKey, TValue> dict;

		public SafeDictionary()
			: this(false)
		{

		}

		public SafeDictionary(bool autoInit)
		{
			InitDict();
			AutoInit = autoInit;
		}

		public SafeDictionary(IDictionary<TKey, TValue> tocopy, bool autoInit = false)
			: this(autoInit)
		{
			foreach (var kvp in tocopy)
				Add(kvp);
		}

		/// <summary>
		/// Somehow we can't guarantee that dict will be initialized on freshly instantiated objects otherwise...
		/// </summary>
		private void InitDict()
		{
			if (dict == null)
				dict = new Dictionary<TKey, TValue>();
		}

		/// <summary>
		/// Should newly referenced values be initialized to new objects or left null?
		/// </summary>
		public bool AutoInit { get; set; }

		/// <summary>
		/// For initializing newly created values.
		/// </summary>
		public object[] AutoInitArgs { get; set; }

		public virtual void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public bool ContainsKey(TKey key)
		{
			if (key == null)
				return false; // dicts can't contain null keys anyway

			if (dict == null)
				return false; // obviously empty, no need to InitDict

			return dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				if (dict == null)
					return new List<TKey>();
				return dict.Keys;
			}
		}

		public bool Remove(TKey key)
		{
			if (dict == null)
				return false;
			return dict.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (dict == null)
				value = default(TValue);
			else
				value = this[key];
			return true;
		}

		public ICollection<TValue> Values
		{
			get
			{
				if (dict == null)
					return new List<TValue>();
				return dict.Values;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (dict == null)
					return default(TValue);
				TValue val;
				if (dict.TryGetValue(key, out val))
					return val;
				else
				{
					if (AutoInit)
					{
						try
						{
							val = (TValue)typeof(TValue).Instantiate(AutoInitArgs);
							this[key] = val;
							return val;
						}
						catch
						{
							// can't instantiate the object
							return default(TValue);
						}
					}
					else
						return default(TValue);
				}
			}
			set
			{
				InitDict();
				if (ContainsKey(key))
					dict[key] = value;
				else
					dict.Add(key, value);
			}
		}

		public virtual void Add(KeyValuePair<TKey, TValue> item)
		{
			this[item.Key] = item.Value;
		}

		public void Clear()
		{
			if (dict != null)
				dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (dict == null)
				return false;
			return dict.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (dict != null)
				((IDictionary<TKey, TValue>)dict).CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				if (dict == null)
					return 0;
				return dict.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (dict == null)
				return false;
			return ((IDictionary<TKey, TValue>)dict).Remove(item);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (dict == null)
				return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
			return dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
