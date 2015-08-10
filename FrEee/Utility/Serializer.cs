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
using Newtonsoft.Json;

namespace FrEee.Utility
{
	/// <summary>
	/// Serializes and deserializes objects.
	/// </summary>
	public static class Serializer
	{
		private const bool EnableJsonSerializer = false;

		public static void Serialize(object o, TextWriter w)
		{
			var s = SerializeToString(o);
			w.Write(s);
			w.Flush();
		}

		public static void Serialize(object o, Stream s)
		{
			var sw = new StreamWriter(s);
			Serialize(o, sw);
			sw.Flush();
		}

		public static string SerializeToString(object o)
		{
			if (EnableJsonSerializer)
			{
				var js = new JsonSerializer();
				return js.SerializeToString(o);
			}
			else
			{
				var sw = new StringWriter();
				LegacySerializer.Serialize(o, sw, typeof(object));
				return sw.ToString();
			}
		}

		public static T Deserialize<T>(Stream str)
		{
			return (T)Deserialize(str);
		}

		public static object Deserialize(Stream str)
		{
			try
			{
				var sr = new StreamReader(str);
				var s = sr.ReadToEnd();
				return DeserializeFromString(s);
			}
			catch (JsonException ex)
			{
				Console.Error.WriteLine("Could not deserialize using JSON serializer. Attempting to use legacy serializer. Error dump follows:");
				Console.Error.WriteLine(ex);
				try
				{
					return LegacySerializer.Deserialize<object>(str);
				}
				catch (SerializationException ex2)
				{
					Console.Error.WriteLine("Could not deserialize using legacy serializer. Error dump follows:");
					Console.Error.WriteLine(ex2);
				}
				throw new Exception("Unable to deserialize. Please check stderr.txt for details.");
			}
		}

		public static T DeserializeFromString<T>(string s)
		{
			return (T)DeserializeFromString(s);
		}

		public static object DeserializeFromString(string s)
		{
			if (EnableJsonSerializer)
			{
				var js = new JsonSerializer();
				return js.DeserializeFromString(s);
			}
			else
			{
				var sr = new StringReader(s);
				return LegacySerializer.Deserialize<object>(sr);
			}
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
