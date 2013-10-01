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
		public override object Parse(IList<object> known, string s, Type t)
		{
			if (!t.IsPrimitive && !t.IsEnum)
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");
			return Convert.ChangeType(s, t, CultureInfo.InvariantCulture);
		}

		public object Parse(string text)
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
					if (type.IsEnum)
					{
						if (Enum.IsDefined(type, text))
							return Enum.Parse(type, text);
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
			if (!t.IsPrimitive && !t.IsEnum)
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");
			var tabs = new string('\t', indent);
			return tabs + (string)Convert.ChangeType(obj, typeof(string), CultureInfo.InvariantCulture);
		}

		public override string SerializeTyped(object obj, int indent = 0, IList<object> known = null)
		{
			var t = obj.SafeGetType();
			if (t == null)
				throw new Exception("Primitive serializer can only handle primitive types and enums. Nulls are never primitive types.");
			if (!t.IsPrimitive && !t.IsEnum)
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
