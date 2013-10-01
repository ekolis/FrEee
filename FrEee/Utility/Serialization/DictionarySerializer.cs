using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrEee.Utility.Extensions;

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
			var mostIndent = moreIndent + 1;
			var mostTabs = new string('\t', mostIndent);
			foreach (var kvp in dict)
			{
				sb.AppendLine(kvp.Key.Serialize(moreIndent, known) + ":");
				sb.AppendLine(kvp.Value.Serialize(mostIndent, known) + ",");
			}
			sb.Append(tabs);
			sb.Append("}");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type t)
		{
			if (text.Trim() == "null")
				return default(TDictionary);

			string anyWhitespaceRE = "( *)";
			string openBraceRE = "\\{)";
			string keyRE = "(<?key>.*)" + anyWhitespaceRE + ":";
			string valueRE = "(<?value>.*)" + anyWhitespaceRE + ",?";
			string kvpsRE = "(<?kvps>(" + keyRE + valueRE + ")*,?)";
			string closeBraceRE = "(\\})";
			var re = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + kvpsRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE);
			var match = re.Match(text);
			if (!match.Success)
				throw new Exception("Could not parse " + text + " using the dictionary deserialization regular expression.");
			var keys = match.Groups["key"].Captures.Cast<Capture>().Select(c => c.Value.Deserialize(known)).ToArray();
			var values = match.Groups["value"].Captures.Cast<Capture>().Select(c => c.Value.Deserialize(known)).ToArray();
			var dict = Activator.CreateInstance<TDictionary>();
			for (int i = 0; i < keys.Count(); i++)
				dict.Add((TKey)keys[i], (TValue)values[i]);
			return dict;
		}
	}
}
