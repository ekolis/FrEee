using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee
{
	/// <summary>
	/// A dictionary that does not throw exceptions when manipulating nonexistent data or overwriting old data.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

		public virtual void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public bool ContainsKey(TKey key)
		{
			return dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get { return dict.Keys; }
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
			get { return dict.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				if (ContainsKey(key))
					return dict[key];
				else
					return default(TValue);
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
			return dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
