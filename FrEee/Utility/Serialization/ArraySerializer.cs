using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrEee.Utility.Extensions;

namespace FrEee.Utility.Serialization
{
	public class ArraySerializer<TItem> : Serializer<Array>
	{
		public override string Stringify(IList<object> known, object obj, int indent = 0)
		{
			var array = (Array)obj;
			if (array == null)
				return "null";
			var tabs = new string('\t', indent);
			var sb = new StringBuilder();
			sb.Append(tabs);
			sb.AppendLine("[");
			var moreIndent = indent + 1;
			var moreTabs = new string('\t', moreIndent);
			// TODO - recursive algorithm for 3D+ arrays
			if (array.Rank == 1)
			{
				foreach (var item in array)
					sb.AppendLine(item.Serialize(moreIndent, known) + ",");
			}
			else if (array.Rank == 2)
			{
				var mostIndent = moreIndent + 1;
				var mostTabs = new string('\t', mostIndent);
				for (int x = 0; x < array.GetLength(0); x++)
				{
					sb.Append(moreTabs);
					sb.AppendLine("[");
					for (int y = 0; y < array.GetLength(1); y++)
						sb.AppendLine(array.GetValue(x, y).Serialize(mostIndent, known) + ",");
					sb.Append(moreTabs);
					sb.AppendLine("]");
				}
			}
			else
				throw new Exception("Only 1D and 2D arrays can be serialized at this time.");
			sb.Append(tabs);
			sb.Append("]");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type t)
		{
			if (text.Trim() == "null")
				return null;

			string anyWhitespaceRE = "( *)";
			string openBraceRE = "\\[)";
			string valuesRE = "((<?value>.*),)?";
			string closeBraceRE = "(\\])";
			string values2DRE = "((?<values>" + anyWhitespaceRE + openBraceRE + anyWhitespaceRE + valuesRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE + ")," + anyWhitespaceRE + ")*";
			// try as 2D array
			var re2D = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + values2DRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE);
			var re1D = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + valuesRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE);
			var match2D = re2D.Match(text);
			if (match2D.Success)
			{
				// 2D array, contains 1D arrays
				var biglist = new List<List<TItem>>();
				foreach (var sublistCapture in match2D.Groups["values"].Captures.Cast<Capture>())
				{
					// sub array is 1D
					var sublist = new List<TItem>();
					biglist.Add(sublist);
					var match1D = re1D.Match(sublistCapture.Value);
					if (!match1D.Success)
						throw new Exception("Could not parse " + text + " using the 1D array deserialization regular expression.");
					var valueCaptures = match1D.Groups["value"].Captures;
					var values = valueCaptures.Cast<Capture>().Select(c => (TItem)c.Value.Deserialize());
					foreach (var item in values)
						sublist.Add(item);
				}
				var width = biglist.Count;
				var height = biglist.MaxOrDefault(l => l.Count);
				var array = new TItem[width, height];
				for (var x = 0; x < width; x++)
				{
					for (var y = 0; y < height; y++)
					{
						if (biglist[x].Count > y)
							array[x, y] = biglist[x][y];
					}
				}
				return array;
			}
			else
			{
				// 1D array
				var match1D = re1D.Match(text);
				if (!match1D.Success)
					throw new Exception("Could not parse " + text + " using the 1D or 2D array deserialization regular expressions.");
				var valueCaptures = match1D.Groups["value"].Captures;
				var values = valueCaptures.Cast<Capture>().Select(c => (TItem)c.Value.Deserialize());
				var list = new List<TItem>();
				foreach (var item in values)
					list.Add(item);
				return (TItem[])list.ToArray();
			}
		}
	}
}
