using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// A safe dictionary keyed with transparent refrences.
	/// </summary>
	public class ReferenceKeyedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IPromotable
		where TKey : IReferrable
	{
		public ReferenceKeyedDictionary()
		{
			dict = new SafeDictionary<Reference<TKey>, TValue>();
		}

		private SafeDictionary<Reference<TKey>, TValue> dict { get; set; }

		public void Add(TKey key, TValue value)
		{
			dict.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get { return dict.Keys.Cast<TKey>().ToList(); }
		}

		public bool Remove(TKey key)
		{
			return dict.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dict.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values
		{
			get { return dict.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				return dict[key];
			}
			set
			{
				dict[key] = value;
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			dict.Add(new KeyValuePair<Reference<TKey>,TValue>(item.Key, item.Value));
		}

		public void Clear()
		{
			dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dict.Contains(new KeyValuePair<Reference<TKey>, TValue>(item.Key, item.Value));
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			var d2 = (ICollection<KeyValuePair<TKey, TValue>>)dict.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key.Value, kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			d2.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return dict.Count; }
		}

		public bool IsReadOnly
		{
			get { return dict.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return dict.Remove(new KeyValuePair<Reference<TKey>, TValue>(item.Key, item.Value));
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dict.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key.Value, kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			foreach (var r in dict.Keys)
				r.ReplaceClientIDs(idmap);
		}
	}
}
