using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Utility.Serialization
{
	public class PrimitiveSerializer : Serializer<object>
	{
		public override object Parse(IDictionary<int, object> known, string text, Type t, SafeDictionary<object, SafeDictionary<object, int>> references)
		{
			if (text.Trim() == "null")
				return null;
			return Convert.ChangeType(text, t.GetNonNullableType(), CultureInfo.InvariantCulture);
		}

		public object Parse(string text, Type desiredType = null)
		{
			if (desiredType != null)
			{
				if (text.Trim() == "null")
					return null;
				else if (desiredType.IsEnumOrNullableEnum())
					return Parser.NullableEnum(desiredType.GetNonNullableType(), text);
				else
					return Convert.ChangeType(text, desiredType.GetNonNullableType(), CultureInfo.InvariantCulture);
			}

			// try some primitive types
			bool b;
			byte by;
			short sh;
			int i;
			long l;
			float f;
			double d;
			decimal de;

			text = text.Trim().TrimEnd(',');

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

			foreach (var name in Assembly.GetEntryAssembly().GetReferencedAssemblies())
			{
				var assembly = Assembly.Load(name);
				foreach (var type in assembly.GetTypes())
				{
					if (type.IsEnumOrNullableEnum())
					{
						if (Enum.IsDefined(type, text))
							return Parser.NullableEnum(type, text.UnDoubleQuote());
					}
				}
			}

			throw new Exception("Could not parse " + text + " as a primitive type or enum.");
		}

		public override string Stringify(IList<object> known, object obj, int indent = 0)
		{
			var t = obj.SafeGetType();
			if (t == null)
				throw new Exception("Primitive serializer can only handle primitive types and enums. Nulls are never primitive types.");
			if (!t.IsPrimitive && !t.IsEnumOrNullableEnum())
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");
			var tabs = new string('\t', indent);
			if (obj is Enum)
				return tabs + obj.ToString().DoubleQuote(); // don't break parsing with unquoted commas in flags enums
			return tabs + (string)Convert.ChangeType(obj, typeof(string), CultureInfo.InvariantCulture);
		}

		public override string SerializeTyped(object obj, int indent = 0, IList<object> known = null)
		{
			var t = obj.SafeGetType();
			if (t == null)
				throw new Exception("Primitive serializer can only handle primitive types and enums. Nulls are never primitive types.");
			if (!t.IsPrimitive && !t.IsEnumOrNullableEnum())
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");

			var tabs = new string('\t', indent);
			if (obj == null)
				return tabs + "null";

			if (known == null)
				known = new List<object>();

			var sb = new StringBuilder();

			// Don't create IDs for primitives; it's just confusing.
			if (obj == null)
			{
				// write a null
				sb.Append("null");
			}
			else
			{
				// just write the value; primitives don't have interfaces or abstract types
				sb.Append(Stringify(known, obj, indent + 1));
			}
			return sb.ToString();
		}
	}
}
