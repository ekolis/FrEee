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
			var mostIndent = moreIndent + 1;
			foreach (var prop in properties[t])
			{
				sb.Append(moreTabs);
				sb.AppendLine("\"" + prop.Name + "\":");
				var s = obj.GetPropertyValue(prop).Serialize(mostIndent, known).TrimEnd();
				sb.AppendLine(s + ",");
			}
			sb.Append(tabs);
			sb.Append("}");
			return sb.ToString();
		}

		public override object Parse(IList<object> known, string s, Type type)
		{
			if (s.Trim() == "null")
				return default(T);

			string anyWhitespaceRE = "(\\s*)";
			string openBraceRE = "\\{";
			string propNameRE = "\"(?<propName>.*?)\"";
			string colonRE = ":";
			string valueRE = "(?<value>.*?)";
			string propRE = "(?<prop>" + propNameRE + anyWhitespaceRE + colonRE + anyWhitespaceRE + valueRE + "),?";
			string closeBraceRE = "\\}";
			var re = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + "(" + propRE + ")*?" + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE, RegexOptions.Singleline);
			var match = re.Match(s);
			if (!match.Success)
				throw new Exception("Could not parse " + s + " using the object deserialization regular expression.");

			var obj = Activator.CreateInstance<T>();
			foreach (var c in match.Groups["prop"].Captures.Cast<Capture>())
			{
				var name = Regex.Match(c.Value, propNameRE, RegexOptions.Singleline).Value.Trim().UnDoubleQuote();
				var val = c.Value.Substring(c.Value.IndexOf(":") + 1);
				var prop = type.GetProperty(name);
				if (prop != null)
					obj.SetPropertyValue(name, val.Deserialize(known));
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

		public override string Serialize(T obj, int indent = 0, IList<object> known = null)
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
				sb.AppendLine("null");
			}
			else
			{
				// write the ID, the data type, and the value
				var id = known.Count;
				known.Add(obj); // also add object to known list for future reference
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
				sb.AppendLine("]");
			}
			return sb.ToString();
		}
	}
}
