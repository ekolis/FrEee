using FrEee.Utility.Serialization;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Serializes and deserializes objects.
	/// </summary>
	public static class Serializer
	{
		private const bool EnableJsonSerializer = false;

		public static bool IsDeserializing { get; private set; }

		public static T Deserialize<T>(string s)
		{
			IsDeserializing = true;
			T t;
			using (MemoryStream stream = new MemoryStream())
			{
				var sw = new StreamWriter(stream);
				sw.Write(s);
				sw.Flush();
				stream.Seek(0, SeekOrigin.Begin);
				t = Deserialize<T>(stream);
			}
			IsDeserializing = false;
			return t;
		}

		public static T Deserialize<T>(Stream str)
		{
			return (T)Deserialize(str);
		}

		public static object Deserialize(Stream str)
		{
			try
			{
				// TODO - enable JSON serializer
				IsDeserializing = true;
				var result = LegacySerializer.Deserialize<object>(str);
				IsDeserializing = false;
				return result;
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
				// warning disabled because code waits for the TODO - enable JSON serializer
#pragma warning disable CS0162 // Unreachable code detected
				var js = new FrEee.Utility.Serialization.JsonSerializer();
				return js.DeserializeFromString(s);
#pragma warning restore CS0162 // Unreachable code detected
			}
			else
			{
				IsDeserializing = true;
				var sr = new StringReader(s);
				var result = LegacySerializer.Deserialize<object>(sr);
				IsDeserializing = false;
				return result;
			}
		}

		public static void Serialize(object o, TextWriter w)
		{
			if (EnableJsonSerializer)
			{
				// TODO - enable JSON serializer
				throw new Exception("JSON serializer isn't configured");
			}
			else
				LegacySerializer.Serialize(o, w, typeof(object));
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
				// warning disabled because code waits for the TODO - enable JSON serializer
#pragma warning disable CS0162 // Unreachable code detected
				var js = new FrEee.Utility.Serialization.JsonSerializer();
				return js.SerializeToString(o);
#pragma warning restore CS0162 // Unreachable code detected
			}
			else
			{
				var sw = new StringWriter();
				LegacySerializer.Serialize(o, sw, typeof(object));
				return sw.ToString();
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
		public SerializationPriorityAttribute(int priority)
		{
			Priority = priority;
		}

		public int Priority { get; private set; }
	}

	/// <summary>
	/// Forces serialization of a property even when it has a default value, e.g. null or zero.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class ForceSerializationWhenDefaultValueAttribute : Attribute
	{
		public ForceSerializationWhenDefaultValueAttribute()
		{
		}
	}
}
