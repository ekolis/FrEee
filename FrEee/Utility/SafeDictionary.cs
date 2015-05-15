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
		private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

		public SafeDictionary(bool autoInit = false)
		{
			AutoInit = autoInit;
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
			// need to check for nulls when deserializing...
			if (dict == null)
				dict = new Dictionary<TKey, TValue>();

			return dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				// why is this null?
				if (dict == null)
					dict = new Dictionary<TKey, TValue>();
				return dict.Keys;
			}
		}

		public bool Remove(TKey key)
		{
			return dict.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			value = this[key];
			return true;
		}

		public ICollection<TValue> Values
		{
			get
			{
				// why is this null?
				if (dict == null)
					dict = new Dictionary<TKey, TValue>();
				return dict.Values;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (ContainsKey(key))
					return dict[key];
				else
				{
					if (AutoInit)
					{
						try
						{
							return (TValue)typeof(TValue).Instantiate(AutoInitArgs);
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
			dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dict.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)dict).CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return dict.Count; }
		}

		public bool IsReadOnly
		{
			get { return ((IDictionary<TKey, TValue>)dict).IsReadOnly; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
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
