using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Drawing;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// Serializes and deserializes objects.
	/// </summary>
	public static class Serializer
	{
		public static void Serialize(object o, TextWriter w)
		{
			LegacySerializer.Serialize(o, w);
		}

		public static void Serialize(object o, Stream s)
		{
			var sw = new StreamWriter(s);
			LegacySerializer.Serialize(o, sw);
			sw.Flush();
		}

		public static string SerializeToString(object o)
		{
			var sw = new StringWriter();
			Serialize(o, sw);
			sw.Flush();
			return sw.ToString();
		}

		public static T Deserialize<T>(Stream s)
		{
			return LegacySerializer.Deserialize<T>(s);
		}

		public static object Deserialize(Stream s)
		{
			return LegacySerializer.Deserialize<object>(s);
		}

		public static T DeserializeFromString<T>(string s)
		{
			var sr = new StringReader(s);
			return LegacySerializer.Deserialize<T>(sr);
		}

		public static object DeserializeFromString(string s)
		{
			var sr = new StringReader(s);
			return LegacySerializer.Deserialize<object>(sr);
		}
	}

	/// <summary>
	/// Prevents a property from being serialized, or copied when the containing object is copied.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class DoNotSerializeAttribute : DoNotCopyAttribute
	{
		public DoNotSerializeAttribute(bool allowSafeCopy = true)
			: base(allowSafeCopy)
		{

		}
	}

	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class SerializationPriorityAttribute : Attribute
	{
		public int Priority { get; private set; }

		public SerializationPriorityAttribute(int priority)
		{
			Priority = priority;
		}
	}
}
