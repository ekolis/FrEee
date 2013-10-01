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
				for (int x = 0; x < array.GetLength(0); x++)
				{
					sb.Append(moreTabs);
					sb.AppendLine("[");
					for (int y = 0; y < array.GetLength(1); y++)
						sb.AppendLine(array.GetValue(x, y).Serialize(moreIndent, known) + ",");
					sb.Append(moreTabs);
					sb.AppendLine("]");
				}
			}
			else
				throw new Exception("Only 1D and 2D arrays can be serialized at this time.");
			sb.Append(tabs);
			sb.Append("],");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type t)
		{
			if (text.Trim() == "null")
				return null;

			if (known == null)
				known = new List<object>();

			var inside = text.BetweenBraces('[', ']');
			if (!inside.Any())
				throw new Exception("Arrays are delimited with square braces. No square braces were found in " + text + ".");
			if (inside.Count() > 1)
				throw new Exception("Arrays cannot contain more than one set of square braces.");
			var arrayText = inside.First();
			
			// see if we have a 2D array
			var inside2 = arrayText.BetweenBraces('[', ']');
			if (inside2.Any())
			{
				// 2D array, contains 1D arrays
				var biglist = new List<List<TItem>>();
				foreach (var arrayText2 in inside2)
				{
					// sub array is 1D
					var sublist = new List<TItem>();
					biglist.Add(sublist);
					var inside21 = arrayText2.BetweenBraces('[', ']');
					if (!inside21.Any())
						throw new Exception("Arrays are delimited with square braces. No square braces were found in " + text + ".");
					if (inside21.Count() > 1)
						throw new Exception("Arrays cannot contain more than one set of square braces.");
					var arrayText21 = inside21.First();
					var split = arrayText21.SplitCsv();
					var values = split.Select(s => (TItem)s.Deserialize(known));
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
				var list = new List<TItem>();
				var split = arrayText.SplitCsv();
				var values = split.Select(s => (TItem)s.Deserialize(known));
				foreach (var item in values)
					list.Add(item);
				return list.ToArray();
			}
		}
	}
}
