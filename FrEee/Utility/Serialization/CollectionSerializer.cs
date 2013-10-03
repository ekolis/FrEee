using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;

namespace FrEee.Utility.Serialization
{
	public class CollectionSerializer<TCollection, TItem> : Serializer<TCollection> where TCollection : ICollection<TItem>
	{
		public override string Stringify(IList<object> known, object obj, int indent = 0)
		{
			var coll = (TCollection)obj;
			if (coll == null)
				return "null";
			var tabs = new string('\t', indent);
			var sb = new StringBuilder();
			sb.Append(tabs);
			sb.AppendLine("[");
			var moreIndent = indent + 1;
			var moreTabs = new string('\t', moreIndent);
			foreach (var item in coll)	
				sb.AppendLine(item.Serialize(moreIndent, known) + ",");
			sb.Append(tabs);
			sb.Append("]");
			return sb.ToString();
		}

		public override object Parse(IDictionary<int, object> known, string text, Type t, SafeDictionary<object, SafeDictionary<object, int>> references)
		{
			if (text.Trim() == "null")
				return null;

			if (known == null)
				known = new Dictionary<int, object>();
			if (references == null)
				references = new SafeDictionary<object, SafeDictionary<object, int>>(true);

			var inside = text.BetweenBraces('[', ']');
			if (!inside.Any())
				throw new Exception("Collections are delimited with square braces. No square braces were found in " + text + ".");
			if (inside.Count() > 1)
				throw new Exception("Collection cannot contain more than one set of square braces.");
			var arrayText = inside.First();

			var coll = (TCollection)typeof(TCollection).Instantiate();
			var split = arrayText.SplitCsv().Where(s => s.Trim().Length > 0);
			int idx = 0;
			foreach (var s in split)
			{
				object item;
				if (typeof(TItem).IsEnumOrNullableEnum())
					item = Parser.NullableEnum(typeof(TItem), s.Deserialize(coll, idx, known, references, typeof(TItem).GetNonNullableType()).ToString().UnDoubleQuote());
				else
					item = s.Deserialize(coll, idx, known, references, typeof(TItem));
				coll.Add((TItem)item);
				idx++;
			}
			return coll;
		}
	}
}
