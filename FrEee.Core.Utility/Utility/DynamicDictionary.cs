using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// A dynamic dictionary that's dynamic dictionaries all the way down.
/// About as close as you can get to a Perl hash in C#.
/// </summary>
public class DynamicDictionary : DynamicObject, IDictionary<object, object>
{
	public DynamicDictionary()
	{
		dict = new SafeDictionary<object, object>();
	}

	public int Count
	{
		get { return dict.Count; }
	}

	public bool IsEmpty
	{
		get { return dict.Count == 0; }
	}

	public bool IsReadOnly
	{
		get { return dict.IsReadOnly; }
	}

	public ICollection<object> Keys
	{
		get { return dict.Keys; }
	}

	public ICollection<object> Values
	{
		get { return dict.Values; }
	}

	private SafeDictionary<object, object> dict { get; set; }

	public object this[object key]
	{
		get
		{
			if (!dict.ContainsKey(key))
				dict[key] = new DynamicDictionary();
			return dict[key];
		}
		set
		{
			dict[key] = value;
		}
	}

	public void Add(object key, object value)
	{
		dict.Add(key, value);
	}

	public void Add(KeyValuePair<object, object> item)
	{
		dict.Add(item);
	}

	public void Clear()
	{
		dict.Clear();
	}

	public bool Contains(KeyValuePair<object, object> item)
	{
		return dict.Contains(item);
	}

	public bool ContainsKey(object key)
	{
		return dict.ContainsKey(key);
	}

	public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
	{
		dict.CopyTo(array, arrayIndex);
	}

	public override IEnumerable<string> GetDynamicMemberNames()
	{
		return dict.Keys.OfType<string>();
	}

	public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
	{
		return dict.GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public bool HasProperty(string prop)
	{
		return dict.ContainsKey(prop);
	}

	public bool Remove(object key)
	{
		return dict.Remove(key);
	}

	public bool Remove(KeyValuePair<object, object> item)
	{
		return dict.Remove(item);
	}

	public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
	{
		var key = string.Join(",", indexes.Select(o => o.ToString()).ToArray());
		if (!dict.ContainsKey(key))
			dict[key] = new DynamicDictionary();
		result = dict[key];
		return true;
	}

	public override bool TryGetMember(GetMemberBinder binder, out object result)
	{
		if (!dict.ContainsKey(binder.Name))
			dict[binder.Name] = new DynamicDictionary();
		result = dict[binder.Name];
		return true;
	}

	public bool TryGetValue(object key, out object value)
	{
		if (!dict.ContainsKey(key))
			dict[key] = new DynamicDictionary();
		value = dict[key];
		return true;
	}

	public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
	{
		var key = string.Join(",", indexes.Select(o => o.ToString()).ToArray());
		dict[key] = value;
		return true;
	}

	public override bool TrySetMember(SetMemberBinder binder, object? value)
	{
		dict[binder.Name] = value;
		return true;
	}
}
