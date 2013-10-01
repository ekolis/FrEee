using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Utility.Serialization
{
	public class StringSerializer : Serializer<string>
	{
		public override object Parse(IList<object> known, string s, Type t)
		{
			if (s.Trim() == "null")
				return null;
			return s.UnDoubleQuote();
		}

		public override string Stringify(IList<object> known, object o, int indent = 0)
		{
			var s = (string)o;
			if (s == null)
				return "null";
			var tabs = new string('\t', indent);
			return tabs + s.DoubleQuote();
		}

		public override string SerializeTyped(string obj, int indent = 0, IList<object> known = null)
		{
			var tabs = new string('\t', indent);
			if (obj == null)
				return tabs + "null";

			if (known == null)
				known = new List<object>();

			var sb = new StringBuilder();

			// Don't create IDs for strings; it's just confusing.
			if (obj == null)
			{
				// write a null
				sb.AppendLine("null");
			}
			else
			{
				// just write the value; strings don't have interfaces or abstract types
				sb.AppendLine(Stringify(known, obj, indent + 1));
			}
			return sb.ToString();
		}
	}
}
