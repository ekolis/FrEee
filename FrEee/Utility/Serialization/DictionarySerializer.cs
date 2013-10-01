using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrEee.Utility.Extensions;
using System.Globalization;

namespace FrEee.Utility.Serialization
{
	public class DictionarySerializer<TDictionary, TKey, TValue> : Serializer<TDictionary> where TDictionary : IDictionary<TKey, TValue>
	{
		public override string Stringify(IList<object> known, object obj, int indent = 0)
		{
			var dict = (TDictionary)obj;
			if (dict == null)
				return "null";
			var tabs = new string('\t', indent);
			var sb = new StringBuilder();
			sb.Append(tabs);
			sb.AppendLine("{");
			var moreIndent = indent + 1;
			var moreTabs = new string('\t', moreIndent);
			foreach (var kvp in dict)
			{
				sb.AppendLine(kvp.Key.Serialize(indent, known) + ":");
				sb.AppendLine(kvp.Value.Serialize(moreIndent, known) + ",");
			}
			sb.Append(tabs);
			sb.Append("}");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type t)
		{
			if (text.Trim() == "null")
				return null;

			if (known == null)
				known = new List<object>();

			var inside = text.BetweenBraces('{', '}');
			if (!inside.Any())
				throw new Exception("Dictionaries are delimited with curly braces. No curly braces were found in " + text + ".");
			if (inside.Count() > 1)
				throw new Exception("Dictionaries cannot contain more than one set of curly braces.");
			var arrayText = inside.First();

			var dict = (TDictionary)typeof(TDictionary).Instantiate();
			var split = arrayText.SplitCsv();
			foreach (var s in split)
			{
				if (s.Trim().Length > 0)
				{
					var split2 = s.SplitCsv(':');
					if (split2.Count() != 2)
						throw new Exception("Dictionary key/value pairs must be in the format Key: Value.");
					dict.Add((TKey)Convert.ChangeType(split2.First().Deserialize(known), typeof(TKey), CultureInfo.InvariantCulture), (TValue)Convert.ChangeType(split2.Last().Deserialize(known), typeof(TValue), CultureInfo.InvariantCulture));
				}
			}
			return dict;
		}
	}
}
