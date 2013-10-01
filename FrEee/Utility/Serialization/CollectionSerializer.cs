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
				sb.AppendLine(item.Serialize(moreIndent, known));
			sb.Append(tabs);
			sb.Append("]");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type t)
		{
			if (text.Trim() == "null")
				return default(TCollection);

			string anyWhitespaceRE = "( *)";
			string openBraceRE = "\\[)";
			string valuesRE = "(<?value>.*,?)";
			string closeBraceRE = "(\\])";
			var re = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + valuesRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE);
			var match = re.Match(text);
			if (!match.Success)
				throw new Exception("Could not parse " + text + " using the collection deserialization regular expression.");
			var valueCaptures = match.Groups["value"].Captures;
			var values = valueCaptures.Cast<Capture>().Select(c => (TItem)c.Value.TrimEnd(',').Deserialize());
			var coll = Activator.CreateInstance<TCollection>();
			foreach (var item in values)
				coll.Add(item);
			return coll;
		}
	}
}
