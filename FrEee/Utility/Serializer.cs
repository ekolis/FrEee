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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FrEee.Utility
{
	/// <summary>
	/// Serializes and deserializes objects.
	/// </summary>
	public static class Serializer
	{
		public static void Serialize<T>(T o, Stream s, ObjectGraphContext context = null, int tabLevel = 0)
		{
			var sw = new StreamWriter(s);
			Serialize(o, sw, context);
			sw.Flush();
		}

		public static void Serialize<T>(T o, TextWriter w, ObjectGraphContext context = null, int tabLevel = 0)
		{
			var js = new JsonSerializer();
			js.PreserveReferencesHandling = PreserveReferencesHandling.All;
			js.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			js.Formatting = Formatting.Indented;
			js.TypeNameHandling = TypeNameHandling.All;
			js.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
			var resolver = new CustomContractResolver();
			js.ContractResolver = resolver;
			js.Serialize(w, o);
		}

		public static string SerializeToString(object o)
		{
			var sw = new StringWriter();
			Serialize(o, sw);
			return sw.ToString();
		}
				
		public static T Deserialize<T>(Stream s, ObjectGraphContext context = null)
		{
			var sr = new StreamReader(s);
			var result = Deserialize<T>(sr, context);
			return result;
		}

		public static T Deserialize<T>(TextReader r, ObjectGraphContext context = null)
		{
			return DeserializeFromString<T>(r.ReadToEnd());
		}

		public static T DeserializeFromString<T>(string s)
		{
			var settings = new JsonSerializerSettings();
			settings.TypeNameHandling = TypeNameHandling.Auto;
			var resolver = new CustomContractResolver();
			settings.ContractResolver = resolver;
			return JsonConvert.DeserializeObject<T>(s, settings);
		}

		public static object DeserializeFromString(string s)
		{
			var settings = new JsonSerializerSettings();
			settings.TypeNameHandling = TypeNameHandling.Auto;
			var resolver = new CustomContractResolver();
			settings.ContractResolver = resolver;
			return JsonConvert.DeserializeObject(s, settings);
		}

		/// <summary>
		/// Ignores fields, readonly properties, and properties marked [DoNotSerialize].
		/// Includes non-public properties.
		/// </summary>
		private class CustomContractResolver : DefaultContractResolver
		{
			public CustomContractResolver()
				: base()
			{
				DefaultMembersSearchFlags |= BindingFlags.NonPublic; // include non-public members

				// fix hashsets
				IgnoreSerializableAttribute = true;
				IgnoreSerializableInterface = true;
			}

			public CustomContractResolver(bool shareCache)
				: base(shareCache)
			{
				DefaultMembersSearchFlags |= BindingFlags.NonPublic; // include non-public members
			}

			protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
			{
				var props = base.CreateProperties(type, memberSerialization);
				return props.Where(p =>
				{
					if (!p.Writable)
						return false;
					var prop = p.DeclaringType.GetProperty(p.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (prop == null)
						return false; // field
					if (prop.HasAttribute(typeof(DoNotSerializeAttribute)))
						return false;
					return true;
				}).ToList();
			}

		}
	}

	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class DoNotSerializeAttribute : DoNotCopyAttribute
	{

	}
}
