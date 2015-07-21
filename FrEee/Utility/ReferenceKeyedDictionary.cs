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
			InitDict();
		}

		private static TRef MakeReference(TKey item)
		{
			return (TRef)typeof(TRef).Instantiate(item);
		}

		private SafeDictionary<TRef, TValue> dict { get; set; }

		/// <summary>
		/// Somehow we can't guarantee that dict will be initialized on freshly instantiated objects otherwise...
		/// </summary>
		private void InitDict()
		{
			if (dict == null)
				dict = new SafeDictionary<TRef, TValue>();
		}

		public void Add(TKey key, TValue value)
		{
			InitDict();
			var r = MakeReference(key);
			if (!r.HasValue)
				throw new Exception("Can't make reference for " + key);
			dict.Add(r, value);
		}

		public bool ContainsKey(TKey key)
		{
			InitDict();
			return dict.ContainsKey(MakeReference(key));
		}

		public ICollection<TKey> Keys
		{
			get { InitDict(); return dict.Keys.Select(k => k.Value).ToList(); }
		}

		public bool Remove(TKey key)
		{
			InitDict();
			return dict.Remove(MakeReference(key));
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			InitDict();
			return dict.TryGetValue(MakeReference(key), out value);
		}

		public ICollection<TValue> Values
		{
			get { InitDict(); return dict.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				InitDict();
				return dict[MakeReference(key)];
			}
			set
			{
				InitDict();
				var r = MakeReference(key);
				if (!r.HasValue)
					throw new Exception("Can't make reference for " + key);
				dict[r] = value;
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			InitDict();
			var r = MakeReference(item.Key);
			if (!r.HasValue)
				throw new Exception("Can't make reference for " + item.Key);
			dict.Add(r, item.Value);
		}

		public void Clear()
		{
			InitDict();
			dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			InitDict();
			return dict.Contains(new KeyValuePair<TRef, TValue>(MakeReference(item.Key), item.Value));
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			InitDict();
			var d2 = (ICollection<KeyValuePair<TKey, TValue>>)dict.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key.Value, kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			d2.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { InitDict(); return dict.Count; }
		}

		public bool IsReadOnly
		{
			get { InitDict(); return dict.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			InitDict();
			return dict.Remove(new KeyValuePair<TRef, TValue>(MakeReference(item.Key), item.Value));
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			InitDict();
			var objs = dict.Select(kvp => new {Key = kvp.Key, KeyValue = kvp.Key.Value, Value = kvp.Value});
			foreach (var obj in objs)
			{
				if (obj.KeyValue == null)
					throw new Exception("Key {0} is an invalid reference.".F(obj.Key));
			}
			return objs.Select(obj => new KeyValuePair<TKey, TValue>(obj.KeyValue, obj.Value)).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			InitDict();
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
