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
	public class DynamicDictionary : DynamicObject
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
	}
}
