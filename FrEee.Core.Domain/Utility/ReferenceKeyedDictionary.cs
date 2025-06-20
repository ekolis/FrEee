﻿using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Utility;

/// <summary>
/// A safe dictionary keyed with transparent references.
/// </summary>
public abstract class ReferenceKeyedDictionary<TID, TRef, TKey, TValue> : IDictionary<TKey, TValue>, IPromotable, IReferenceEnumerable
	where TRef : IReference<TKey>
{
	public ReferenceKeyedDictionary()
	{
		InitDict();
	}

	public int Count
	{
		get { InitDict(); return dict.Count; }
	}

	public bool IsReadOnly
	{
		get { InitDict(); return dict.IsReadOnly; }
	}

	public ICollection<TKey> Keys
	{
		get { InitDict(); return dict.Keys.Select(k => LookUp(k)).ToList(); }
	}

	public ICollection<TValue> Values
	{
		get { InitDict(); return dict.Values; }
	}

	private SafeDictionary<TID, TValue> dict { get; set; }

	public TValue this[TKey key]
	{
		get
		{
			InitDict();
			return dict[ExtractID(key)];
		}
		set
		{
			InitDict();
			var id = ExtractID(key);
			dict[id] = value;
		}
	}

	public void Add(TKey key, TValue value)
	{
		InitDict();
		var id = ExtractID(key);
		dict.Add(id, value);
	}

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		InitDict();
		var id = ExtractID(item.Key);
		dict.Add(id, item.Value);
	}

	public void Clear()
	{
		InitDict();
		dict.Clear();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		InitDict();
		return dict.Contains(new KeyValuePair<TID, TValue>(ExtractID(item.Key), item.Value));
	}

	public bool ContainsKey(TKey key)
	{
		InitDict();
		return dict.ContainsKey(ExtractID(key));
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		InitDict();
		var d2 = (ICollection<KeyValuePair<TKey, TValue>>)dict.Select(kvp => new KeyValuePair<TKey, TValue>(LookUp(kvp.Key), kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		d2.CopyTo(array, arrayIndex);
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		InitDict();
		var objs = dict.Select(kvp => new { kvp.Key, KeyValue = LookUp(kvp.Key), kvp.Value });
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

	public bool Remove(TKey key)
	{
		InitDict();
		return dict.Remove(ExtractID(key));
	}

	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		InitDict();
		return dict.Remove(ExtractID(item.Key));
	}

	public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
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
		return this;
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		InitDict();
		return dict.TryGetValue(ExtractID(key), out value);
	}

	protected abstract TID ExtractID(TKey key);

	protected abstract TKey LookUp(TID id);

	/// <summary>
	/// Somehow we can't guarantee that dict will be initialized on freshly instantiated objects otherwise...
	/// </summary>
	private void InitDict()
	{
		if (dict == null)
			dict = new SafeDictionary<TID, TValue>();
	}
}