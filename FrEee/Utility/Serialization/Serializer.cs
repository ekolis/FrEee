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

		public virtual string SerializeTyped(T obj, int indent = 0, IList<object> known = null)
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
				sb.Append("]");
			}
			return sb.ToString();
		}

		public abstract object Parse(IList<object> known, string text, Type t);

		public string Serialize(object obj, int indent = 0, IList<object> known = null)
		{
			if (!(obj is T))
				throw new Exception(this + " is not capable of serializing a " + obj.GetType() + ".");
			return SerializeTyped((T)obj, indent, known);
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
				return new PrimitiveSerializer().Serialize(obj, indent, known);
			if (t == typeof(string))
				return new StringSerializer().SerializeTyped(obj.ToString(), indent, known);
			if (typeof(Array).IsAssignableFrom(t))
			{
				var serType = typeof(ArraySerializer<>).MakeGenericType(t.GetElementType());
				var ser = (ISerializer)serType.Instantiate();
				return ser.Serialize(obj, indent, known);
			}
			if (t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>)) && !typeof(IReferenceEnumerable).IsAssignableFrom(t))
			{
				var dictType = t.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
				var keyType = dictType.GetGenericArguments()[0];
				var valType = dictType.GetGenericArguments()[1];
				var serType = typeof(DictionarySerializer<,,>).MakeGenericType(dictType, keyType, valType);
				var ser = (ISerializer)serType.Instantiate();
				return ser.Serialize(obj, indent, known);
			}
			if (t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>)) && !typeof(IReferenceEnumerable).IsAssignableFrom(t))
			{
				var collType = t.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
				var itemType = collType.GetGenericArguments()[0];
				var serType = typeof(CollectionSerializer<,>).MakeGenericType(collType, itemType);
				var ser = (ISerializer)serType.Instantiate();
				return ser.Serialize(obj, indent, known);
			}
			var objSerType = typeof(ObjectSerializer<>).MakeGenericType(t);
			var objSer = (ISerializer)objSerType.Instantiate();
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
			text = text.Trim();

			if (text == "null")
				return null;

			if (known == null)
				known = new List<object>();

			if (text.StartsWith("#"))
			{
				// id ref
				var id = int.Parse(text.Substring(1));
				return known[id];
			}

			if (text.StartsWith("["))
			{
				var split = text.BetweenBraces('[', ']');
				if (split.Count() != 1)
					throw new Exception("Reference objects must contain exactly one set of outer square braces.");
				var csv = split.First().SplitCsv();
				if (csv.Count() != 3)
					throw new Exception("Reference objects must contain exactly 3 comma delimited items: an ID, a type, and a value.");
				var id = int.Parse(csv.ElementAt(0).Trim().TrimStart('#'));
				var type = Type.GetType(csv.ElementAt(1).Trim().UnDoubleQuote());
				var valtext = csv.ElementAt(2).Trim();

				object result = null;
				if (valtext.StartsWith("["))
				{
					// array or collection
					var inside = text.BetweenBraces('[', ']');
					if (inside.Count() == 1)
					{
						if (typeof(Array).IsAssignableFrom(type))
						{
							var serType = typeof(ArraySerializer<>).MakeGenericType(type.GetElementType());
							var ser = (ISerializer)serType.Instantiate();
							result = ser.Parse(known, valtext, type);
						}
						else if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>)) && !typeof(IReferenceEnumerable).IsAssignableFrom(type))
						{
							var collType = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
							var itemType = collType.GetGenericArguments()[0];
							var serType = typeof(CollectionSerializer<,>).MakeGenericType(type, itemType);
							var ser = (ISerializer)serType.Instantiate();
							result = ser.Parse(known, valtext, type);
						}
					}
					else
						throw new Exception("Arrays must contain exactly one set of outer square braces.");
				}
				else if (valtext.StartsWith("{"))
				{
					// dictionary or object
					if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>)) && !typeof(IReferenceEnumerable).IsAssignableFrom(type))
					{
						var dictType = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
						var keyType = dictType.GetGenericArguments()[0];
						var valType = dictType.GetGenericArguments()[1];
						var serType = typeof(DictionarySerializer<,,>).MakeGenericType(type, keyType, valType);
						var ser = (ISerializer)serType.Instantiate();
						result = ser.Parse(known, valtext, type);
					}
					else
					{
						var serType = typeof(ObjectSerializer<>).MakeGenericType(type);
						var ser = (ISerializer)Activator.CreateInstance(serType);
						result = ser.Parse(known, valtext, type);
					}
				}
				else
					throw new Exception("Reference values must start with { (for dictionaries or objects) or [ (for collections or arrays).");

				known.Add(result);
				return result;
			}

			if (text.IsDoubleQuoted())
				return new StringSerializer().Parse(known, text, typeof(string));
			
			var ps = new PrimitiveSerializer();
			return ps.Parse(text);
		}

		public static T Deserialize<T>(Stream s)
		{
			var sr = new StreamReader(s);
			var text = sr.ReadToEnd();
			sr.Close();
			return (T)Deserialize(text, null);
		}
	}
}
