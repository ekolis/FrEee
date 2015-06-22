using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Globalization;

namespace FrEee.Utility
{
	/// <summary>
	/// A dynamic dictionary that's dynamic dictionaries all the way down.
	/// About as close as you can get to a Perl hash in C#.
	/// </summary>
	public class DynamicDictionary : DynamicObject, IDictionary<string, object>
	{
		public DynamicDictionary()
		{
			dict = new SafeDictionary<string, object>();
		}

		private SafeDictionary<string, object> dict { get; set; }

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return dict.Keys;
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

		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			var key = string.Join(",", indexes.Select(o => o.ToString()).ToArray());
			dict[key] = value;
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			dict[binder.Name] = value;
			return true;
		}

		public bool IsEmpty
		{
			get { return dict.Count == 0; }
		}

		public bool HasProperty(string prop)
		{
			return dict.ContainsKey(prop);
		}

		public void Add(string key, object value)
		{
			dict.Add(key, value);
		}

		public bool ContainsKey(string key)
		{
			return dict.ContainsKey(key);
		}

		public ICollection<string> Keys
		{
			get { return dict.Keys; }
		}

		public bool Remove(string key)
		{
			return dict.Remove(key);
		}

		public bool TryGetValue(string key, out object value)
		{
			if (!dict.ContainsKey(key))
				dict[key] = new DynamicDictionary();
			value = dict[key];
			return true;
		}

		public ICollection<object> Values
		{
			get { return dict.Values; }
		}

		public object this[string key]
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

		public void Add(KeyValuePair<string, object> item)
		{
			dict.Add(item);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public bool Contains(KeyValuePair<string, object> item)
		{
			return dict.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			dict.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return dict.Count; }
		}

		public bool IsReadOnly
		{
			get { return dict.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<string, object> item)
		{
			return dict.Remove(item);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
