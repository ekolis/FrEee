using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using System.IO;

namespace FrEee.Utility.Serialization
{
	public abstract class Serializer<T> : ISerializer
	{
		public abstract string Stringify(IList<object> known, object obj, int indent = 0);

		public virtual string Serialize(T obj, int indent = 0, IList<object> known = null)
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

		public abstract object Parse(IList<object> known, string text, Type t);

		public string Serialize(object obj, int indent = 0, IList<object> known = null)
		{
			if (!(obj is T))
				throw new Exception(this + " is not capable of serializing a " + obj.GetType() + ".");
			return Serialize((T)obj, indent, known);
		}
	}

	public static class Serializer
	{
		public static string Serialize(object obj, int indent = 0, IList<object> known = null)
		{
			if (obj == null)
			{
				var tabs = new string('\t', indent);
				return tabs + "null";
			}

			if (known == null)
				known = new List<object>();

			var t = obj.GetType();
			if (t.IsPrimitive || t.IsEnum)
			{
				var serType = typeof(PrimitiveSerializer<>).MakeGenericType(t);
				var ser = (ISerializer)Activator.CreateInstance(serType);
				return ser.Serialize(obj, indent, known);
			}
			if (t == typeof(string))
				return new StringSerializer().Serialize(obj.ToString(), indent, known);
			if (typeof(Array).IsAssignableFrom(t))
			{
				var serType = typeof(ArraySerializer<>).MakeGenericType(t.GetElementType());
				var ser = (ISerializer)Activator.CreateInstance(serType);
				return ser.Serialize(obj, indent, known);
			}
			if (t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>)) && !typeof(IReferenceEnumerable).IsAssignableFrom(t))
			{
				var dictType = t.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
				var keyType = dictType.GetGenericArguments()[0];
				var valType = dictType.GetGenericArguments()[1];
				var serType = typeof(DictionarySerializer<,,>).MakeGenericType(dictType, keyType, valType);
				var ser = (ISerializer)Activator.CreateInstance(serType);
				return ser.Serialize(obj, indent, known);
			}
			if (t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>))&& !typeof(IReferenceEnumerable).IsAssignableFrom(t))
			{
				var collType = t.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
				var itemType = collType.GetGenericArguments()[0];
				var serType = typeof(CollectionSerializer<,>).MakeGenericType(collType, itemType);
				var ser = (ISerializer)Activator.CreateInstance(serType);
				return ser.Serialize(obj, indent, known);
			}
			var objSerType = typeof(ObjectSerializer<>).MakeGenericType(t);
			var objSer = (ISerializer)Activator.CreateInstance(objSerType);
			return objSer.Serialize(obj, indent, known);
		}

		public static void Serialize(object o, Stream s)
		{
			var sw = new StreamWriter(s);
			sw.Write(Serialize(o));
			sw.Close();
		}

		public static object Deserialize(string text, IList<object> known = null)
		{
			if (text == "null")
				return null;

			if (known == null)
				known = new List<object>();

			// build some regular expressions - thanks txt2re.com!
			string anyWhitespaceRE = "(\\s*?)";
			string openBraceRE = @"[{|\[]";
			string idRE = "(?<id>#[0-9]+)";
			string dataTypeRE = "(?<dataType>\".*?\")";
			string commaRE = "(,)";
			string valueRE = "(?<val>.*)";
			string closeBraceRE = @"[}|\]]";

			var re = new Regex(anyWhitespaceRE + openBraceRE + anyWhitespaceRE + idRE + anyWhitespaceRE + commaRE + anyWhitespaceRE + dataTypeRE + anyWhitespaceRE + commaRE + anyWhitespaceRE + valueRE + anyWhitespaceRE + closeBraceRE + anyWhitespaceRE, RegexOptions.Singleline);
			var match = re.Match(text);
			if (match.Success)
			{
				// new object
				var dataTypeString = match.Groups["dataType"].Value.UnDoubleQuote();
				var dataType = Type.GetType(dataTypeString);
				if (dataType == null)
					throw new Exception("Cannot find data type " + dataType + ".");
				var valueString = match.Groups["val"].Value;
				Type serType;
				if (dataType.IsPrimitive || dataType.IsEnum)
					serType = typeof(PrimitiveSerializer<>).MakeGenericType(dataType);
				else if (dataType == typeof(string))
					serType = typeof(StringSerializer);
				else if (typeof(Array).IsAssignableFrom(dataType))
				{
					serType = typeof(ArraySerializer<>).MakeGenericType(dataType.GetElementType());
				}
				else if (dataType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
				{
					var dictType = dataType.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
					var keyType = dictType.GetGenericArguments()[0];
					var valType = dictType.GetGenericArguments()[1];
					serType = typeof(DictionarySerializer<,,>).MakeGenericType(dictType, keyType, valType);
				}
				else if (dataType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>)))
				{
					var collType = dataType.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
					var itemType = collType.GetGenericArguments()[0];
					serType = typeof(CollectionSerializer<,>).MakeGenericType(collType, itemType);
				}
				else
					serType = typeof(ObjectSerializer<>).MakeGenericType(dataType);
				var ser = (ISerializer)Activator.CreateInstance(serType);
				var val = match.Groups["val"].Value;
				return ser.Parse(known, val, dataType);
			}
			else
			{
				re = new Regex(anyWhitespaceRE + idRE + anyWhitespaceRE);
				match = re.Match(text);
				if (match.Success)
				{
					// object reference by ID
					var id = int.Parse(match.Groups["id"].Value.TrimStart('#'));
					if (id < 0)
						throw new Exception("Negative serialization IDs are not allowed.");
					if (id >= known.Count)
						throw new Exception("Reference to unknown serialization ID " + id + " found.");
					return known[id];
				}
				else
				{
					// try some primitive types
					bool b;
					byte by;
					short sh;
					int i;
					long l;
					float f;
					double d;
					decimal de;

					text = text.TrimEnd().TrimEnd(',');

					if (bool.TryParse(text, out b))
						return b;
					if (byte.TryParse(text, out by))
						return by;
					if (short.TryParse(text, out sh))
						return sh;
					if (int.TryParse(text, out i))
						return i;
					if (long.TryParse(text, out l))
						return l;
					if (float.TryParse(text, out f))
						return f;
					if (double.TryParse(text, out d))
						return d;
					if (decimal.TryParse(text, out de))
						return de;
					if (text.StartsWith("'") && text.EndsWith("'"))
						return text.Trim('\'')[0];
					if (text.StartsWith("\"") && text.EndsWith("\""))
						return text.Trim('"');
					throw new Exception("Could not parse " + text + " as an object or object reference.");
				}
			}
		}

		public static object Deserialize(Stream s)
		{
			var sr = new StreamReader(s);
			var text = sr.ReadToEnd();
			sr.Close();
			return Deserialize(text);
		}
	}
}
