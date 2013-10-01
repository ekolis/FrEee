using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FrEee.Utility.Extensions;
using System.Text.RegularExpressions;

namespace FrEee.Utility.Serialization
{
	public class ObjectSerializer<T> : Serializer<T>
	{
		static ObjectSerializer()
		{
			properties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
		}

		public override string Stringify(IList<object> known, object o, int indent = 0)
		{
			var obj = (T)o;
			if (obj == null)
				return "null";
			var t = obj.GetType();
			AddProperties(t);
			var tabs = new string('\t', indent);
			var sb = new StringBuilder();
			sb.Append(tabs);
			sb.AppendLine("{");
			var moreIndent = indent + 1;
			var moreTabs = new string('\t', moreIndent);
			foreach (var prop in properties[t])
			{
				sb.Append(moreTabs);
				sb.AppendLine("\"" + prop.Name + "\":");
				var s = obj.GetPropertyValue(prop).Serialize(moreIndent, known).TrimEnd();
				sb.AppendLine(s + ",");
			}
			sb.Append(tabs);
			sb.Append("}");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string text, Type type)
		{
			if (text.Trim() == "null")
				return null;

			if (known == null)
				known = new List<object>();

			var inside = text.BetweenBraces('{', '}');
			if (!inside.Any())
				throw new Exception("Objects are delimited with curly braces. No curly braces were found in " + text + ".");
			if (inside.Count() > 1)
				throw new Exception("Objects cannot contain more than one set of curly braces.");
			var arrayText = inside.First();

			// use object not var for manual boxing
			// http://stackoverflow.com/questions/6280506/is-there-a-way-to-set-properties-on-struct-instances-using-reflection
			object obj = (T)typeof(T).Instantiate();
			var split = arrayText.SplitCsv();
			foreach (var s in split)
			{
				if (s.Trim().Length > 0)
				{
					var split2 = s.SplitCsv(':');
					if (split2.Count() != 2)
						throw new Exception("Object property/value pairs must be in the format PropertyName: Value.");
					var prop = typeof(T).GetProperty(split2.First().Trim().UnDoubleQuote());
					if (prop != null)
					{
						var val = split2.Last().Trim().Deserialize(known);
						obj.SetPropertyValue(prop, val);
					}
				}
			}
			return obj;
		}


		/// <summary>
		/// Properties of all serialized object types.
		/// </summary>
		private static IDictionary<Type, IEnumerable<PropertyInfo>> properties;

		private static void AddProperties(Type type)
		{
			if (!properties.ContainsKey(type))
			{
				// list out the object's properties that aren't marked nonserializable and also aren't indexed properties (since we have no way to enumerate those)
				var props = new Dictionary<PropertyInfo, int>();
				var t = type;
				int i = 0; // how far removed in the type hierarchy?
				while (t != null)
				{
					var newprops = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f => !f.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any() && f.GetGetMethod(true) != null && f.GetSetMethod(true) != null && f.GetIndexParameters().Length == 0);
					foreach (var prop in newprops.Where(prop => prop.GetIndexParameters().Length == 0))
						props.Add(prop, i);
					t = t.BaseType;
					i++;
				}
				properties.Add(type, props.Keys.ToArray());
			}
		}

		public override string SerializeTyped(T obj, int indent = 0, IList<object> known = null)
		{
			var tabs = new string('\t', indent);
			if (obj == null)
				return tabs + "null";

			if (known == null)
				known = new List<object>();

			var sb = new StringBuilder();
			if (known.Contains(obj))
			{
				// write the ID alone
				sb.Append(tabs);
				sb.AppendLine("#" + known.IndexOf(obj).ToString());
			}
			else if (obj == null)
			{
				// write a null
				sb.Append(tabs);
				sb.AppendLine("null");
			}
			else
			{
				// write the ID, the data type, and the value
				var id = known.Count;
				sb.Append(tabs);
				sb.AppendLine("[");
				var moreIndent = indent + 1;
				var moreTabs = new string('\t', moreIndent);
				sb.Append(moreTabs);
				sb.AppendLine("#" + id + ",");
				sb.Append(moreTabs);
				sb.AppendLine("\"" + obj.GetType().AssemblyQualifiedName + "\",");
				sb.AppendLine(Stringify(known, obj, moreIndent));
				sb.Append(tabs);
				sb.Append("]");
			}
			return sb.ToString();
		}
	}
}
