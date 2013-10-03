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
				throw new Exception("Arrays are delimited with square braces. No square braces were found in " + text + ".");
			if (inside.Count() > 1)
				throw new Exception("Arrays cannot contain more than one set of square braces.");
			var arrayText = inside.First();

			// see if we have a 2D array
			var inside2 = arrayText.BetweenBraces('[', ']');
			if (inside2.Any())
			{
				// 2D array, contains 1D arrays
				var tempArray = new TItem[0,0];
				var biglist = new List<List<TItem>>();
				int x = 0, y = 0;
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
					foreach (var s in split)
					{
						object item;
						if (typeof(TItem).IsEnumOrNullableEnum())
							item = Parser.NullableEnum(typeof(TItem), s.Deserialize(tempArray, new int[] { x, y }, known, references, typeof(TItem).GetNonNullableType()).ToString().UnDoubleQuote());
						else
							item = s.Deserialize(tempArray, new int[]{x, y}, known, references, typeof(TItem));
						sublist.Add((TItem)item);
						y++;
					}
					x++;
				}
				var width = biglist.Count;
				var height = biglist.MaxOrDefault(l => l.Count);
				var array = new TItem[width, height];

				for (x = 0; x < width; x++)
				{
					for (y = 0; y < height; y++)
					{
						if (biglist[x].Count > y)
							array[x, y] = biglist[x][y];
					}
				}

				// replace references with references to the real array now that we know its size
				var rs = references[tempArray];
				references.Remove(tempArray);
				references.Add(array, rs);

				return array;
			}
			else
			{
				// 1D array
				var tempArray = new TItem[0];
				var list = new List<TItem>();
				var split = arrayText.SplitCsv();
				int x = 0;
				foreach (var s in split.Where(s => !string.IsNullOrWhiteSpace(s)))
				{
					object item;
					if (typeof(TItem).IsEnumOrNullableEnum())
						item = Parser.NullableEnum(typeof(TItem), s.Deserialize(tempArray, new int[] { x }, known, references, typeof(TItem).GetNonNullableType()).ToString().UnDoubleQuote());
					else
						item = s.Deserialize(tempArray, new int[] { x }, known, references, typeof(TItem));
					list.Add((TItem)item);
					x++;
				}

				var array = list.ToArray();

				// replace references with references to the real array now that we know its size
				var rs = references[tempArray];
				references.Remove(tempArray);
				references.Add(array, rs);

				return list.ToArray();
			}
		}
	}
}
