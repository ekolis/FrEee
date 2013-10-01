using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Utility.Serialization
{
	public class PrimitiveSerializer<T> : Serializer<T>
	{
		public override object Parse(IList<object> known, string s, Type t)
		{
			if (!typeof(T).IsAssignableFrom(t))
				throw new Exception("This primitive serializer is configured to handle " + typeof(T) + "s, not " + t + "s.");
			if (!t.IsPrimitive && !t.IsEnum)
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");
			return (T)Convert.ChangeType(s, typeof(T), CultureInfo.InvariantCulture);
		}

		public override string Stringify(IList<object> known, object o, int indent = 0)
		{
			var obj = (T)o;
			var t = obj.SafeGetType();
			if (t == null)
				throw new Exception("Primitive serializer can only handle primitive types and enums. Nulls are never primitive types.");
			if (!t.IsPrimitive && !t.IsEnum)
				throw new Exception("Primitive serializer can only handle primitive types and enums. " + t + " is not a primitive type.");
			var tabs = new string('\t', indent);
			return tabs + (string)Convert.ChangeType(obj, typeof(string), CultureInfo.InvariantCulture);
		}

		public override string Serialize(T obj, int indent = 0, IList<object> known = null)
		{
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
				sb.AppendLine("null");
			}
			else
			{
				// just write the value; primitives don't have interfaces or abstract types
				sb.AppendLine(Stringify(known, obj, indent + 1));
			}
			return sb.ToString();
		}
	}
}
