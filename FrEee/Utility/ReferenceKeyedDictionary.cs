using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Modding;
using FrEee.Modding.Interfaces;

namespace FrEee.Utility
{
	/// <summary>
	/// A safe dictionary keyed with transparent references.
	/// </summary>
	public class ReferenceKeyedDictionary<TRef, TKey, TValue> : IDictionary<TKey, TValue>, IPromotable, IReferenceEnumerable
		where TRef : IReference<TKey>
	{
		public ReferenceKeyedDictionary()
		{
			dict = new SafeDictionary<TRef, TValue>();
		}

		private static TRef MakeReference(TKey item)
		{
			return (TRef)typeof(TRef).Instantiate(item);
		}

		private SafeDictionary<TRef, TValue> dict { get; set; }

		public void Add(TKey key, TValue value)
		{
			dict.Add(MakeReference(key), value);
		}

		public bool ContainsKey(TKey key)
		{
			return dict.ContainsKey(MakeReference(key));
		}

		public ICollection<TKey> Keys
		{
			get { return dict.Keys.Cast<TKey>().ToList(); }
		}

		public bool Remove(TKey key)
		{
			return dict.Remove(MakeReference(key));
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dict.TryGetValue(MakeReference(key), out value);
		}

		public ICollection<TValue> Values
		{
			get { return dict.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				return dict[MakeReference(key)];
			}
			set
			{
				dict[MakeReference(key)] = value;
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			dict.Add(MakeReference(item.Key), item.Value);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dict.Contains(new KeyValuePair<TRef, TValue>(MakeReference(item.Key), item.Value));
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
			return dict.Remove(new KeyValuePair<TRef, TValue>(MakeReference(item.Key), item.Value));
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dict.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key.Value, kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				foreach (var r in dict.Keys)
				{
					if (r is IPromotable)
						(r as IPromotable).ReplaceClientIDs(idmap, done);
				}
			}
		}
	}
	
	public class GalaxyReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<GalaxyReference<TKey>, TKey, TValue>
	{

	}

	public class ModReferenceKeyedDictionary<TKey, TValue> :  ReferenceKeyedDictionary<ModReference<TKey>, TKey, TValue>
		where TKey : IModObject
	{

	}
}
