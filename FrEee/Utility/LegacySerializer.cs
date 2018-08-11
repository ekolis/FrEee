﻿using FrEee.Utility.Extensions;
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
	internal static class LegacySerializer
	{
		private static void Serialize<T>(T o, Stream s, ObjectGraphContext context = null, int tabLevel = 0)
		{
			var sw = new StreamWriter(new BufferedStream(s));
			Serialize(o, sw, context);
			sw.Flush();
		}

		internal static void Serialize<T>(T o, TextWriter w, ObjectGraphContext context = null, int tabLevel = 0)
		{
			if (o == null)
				Serialize(o, w, typeof(T), context, tabLevel);
			else
				Serialize(o, w, o.GetType(), context, tabLevel);
		}

		internal static void Serialize(object o, TextWriter w, Type desiredType, ObjectGraphContext context = null, int tabLevel = 0)
		{
			var tabs = new string('\t', tabLevel);

			// type checking!
			if (o is Type)
				throw new SerializationException("Cannot serialize objects of type System.Type.");
			if (o != null && !desiredType.IsAssignableFrom(o.GetType()))
				throw new SerializationException("Attempting to serialize " + o.GetType() + " as " + desiredType + ".");

			// set up our serialization context if we haven't already
			if (context == null)
				context = new ObjectGraphContext();

			// write some tabs to improve readability
			for (int i = 0; i < tabLevel; i++)
				w.Write('\t');

			// deal with nulls
			if (o == null)
			{
				if (!ObjectGraphContext.KnownTypes.ContainsKey(desiredType.AssemblyQualifiedName))
				{
					ObjectGraphContext.KnownTypes.Add(desiredType.AssemblyQualifiedName, desiredType);
					ObjectGraphContext.AddProperties(desiredType);
				}
				w.Write(desiredType.AssemblyQualifiedName);
				w.WriteLine(":n;");
				return;
			}

			var type = o.GetType();

			int? id = null;
			if (!type.IsValueType && type != typeof(string))
				id = context.GetID(o);

			if (!ObjectGraphContext.KnownTypes.ContainsKey(type.AssemblyQualifiedName))
			{
				// register type
				ObjectGraphContext.KnownTypes.Add(type.AssemblyQualifiedName, type);
				ObjectGraphContext.AddProperties(type);
			}

			// write the type name if it's not the same as the desired type
			if (type == desiredType)
				w.WriteLine(":");
			else
				w.WriteLine(type.AssemblyQualifiedName + ":");

			if (id == null && !type.IsValueType && type != typeof(string) && !typeof(Array).IsAssignableFrom(type))
			{
				// add to context
				context.Add(o);
			}

			// deal with refs
			if (id != null)
			{
				// already seen this object, just write an ID
				w.Write(tabs);
				w.Write("i");
				w.Write(id);

				// write end object
				w.WriteLine(";");

				// done
				return;
			}

			// write some tabs
			w.Write(tabs);

			// serialize the object
			if (type.IsPrimitive || typeof(Enum).IsAssignableFrom(type) || type.Name == "Nullable`1")
				WritePrimitiveOrEnum(o, w);
			else if (type == typeof(string))
				WriteString((string)o, w);
			else if (type == typeof(Color))
				WriteColor((Color)o, w, tabLevel);
			else if (typeof(Array).IsAssignableFrom(type))
				WriteArray((Array)o, w, context, tabLevel);
			else if (typeof(IEnumerable).IsAssignableFrom(type) && type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1 || m.GetParameters().Length == 2).Any()
				// these types should not be serialized as normal dictionaries/sets!
				&& !typeof(IReferenceEnumerable).IsAssignableFrom(type))
				WriteCollection((IEnumerable)o, w, context, tabLevel);
			else
				WriteObject(o, w, context, tabLevel);

			// flush the stream
			w.Flush();
		}

		private static void WritePrimitiveOrEnum(object o, TextWriter w)
		{
			// just serialize the primitive type
			w.Write(o);

			// write end object
			w.WriteLine(";");
		}

		private static void WriteString(string s, TextWriter w)
		{
			if (s == null)
				w.Write("n");
			else
			{
				// quote strings and escape any backslashes and semicolons inside them
				w.Write('"' + s.Replace("\\", "\\\\").Replace(";", "\\;").Replace("\"", "\\\"") + '"');
			}

			// write end object
			w.WriteLine(";");
		}

		private static void WriteColor(Color c, TextWriter w, int tabLevel)
		{
			// HACK - Mono's implmentation of Color is different from .NET's so we need to save just the ARGB values in a consistent format
			w.Write(c.A);
			w.Write(",");
			w.Write(c.R);
			w.Write(",");
			w.Write(c.G);
			w.Write(",");
			w.Write(c.B);
			w.WriteLine(";");
		}

		private static void WriteArray(Array array, TextWriter w, ObjectGraphContext context, int tabLevel)
		{
			var tabs = new string('\t', tabLevel);

			// arrays get size and elements listed out
			var bounds = new List<string>();
			for (var rank = 0; rank < array.Rank; rank++)
				bounds.Add(array.GetLowerBound(rank) + "_" + array.GetUpperBound(rank));
			w.WriteLine("a" + string.Join(",", bounds.ToArray()) + ":" + tabs);
			var type = array.GetType();
			var itemtype = type.GetElementType();
			if (array.Rank == 1)
			{
				foreach (var item in array)
					Serialize(item, w, itemtype, context, tabLevel + 1);
			}
			else if (array.Rank == 2)
			{
				for (int x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
				{
					for (int y = array.GetLowerBound(1); y <= array.GetUpperBound(1); y++)
						Serialize(array.GetValue(x, y), w, itemtype, context, tabLevel + 1);
				}
			}
			else
				throw new SerializationException("Arrays with more than 2 dimensions are not supported.");

			// write end object
			for (int i = 0; i < tabLevel; i++)
				w.Write('\t');
			w.WriteLine(";");
		}

		private static void WriteCollection(IEnumerable list, TextWriter w, ObjectGraphContext context, int tabLevel)
		{
			var tabs = new string('\t', tabLevel);

			// collections get size and elements listed out
			Type itemType;
			var type = list.GetType();
			bool isDict = false;
			if (type.GetGenericArguments().Length == 2)
			{
				// HACK - assume it's a dictionary, no real way to test
				itemType = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
				w.WriteLine("d" + list.Cast<object>().Count() + ":" + tabs);
				isDict = true;
			}
			else if (type.BaseType.GetGenericArguments().Length == 2)
			{
				// HACK - Resources inherits from a dictionary type
				itemType = typeof(KeyValuePair<,>).MakeGenericType(type.BaseType.GetGenericArguments());
				w.WriteLine("d" + list.Cast<object>().Count() + ":" + tabs);
				isDict = true;
			}
			else if (type.GetGenericArguments().Length == 1)
			{
				// HACK - assume it's a collection, no real way to test
				itemType = type.GetGenericArguments()[0];
				w.WriteLine("c" + list.Cast<object>().Count() + ":" + tabs);
			}
			else
			{
				// no generic type? probably a list of objects?
				itemType = typeof(object);
				w.WriteLine("c" + list.Cast<object>().Count() + ":" + tabs);
			}
			foreach (var item in list)
			{
				if (isDict)
				{
					var keyprop = ObjectGraphContext.GetKnownProperties(itemType)["Key"];
					var valprop = ObjectGraphContext.GetKnownProperties(itemType)["Value"];
					Serialize(context.GetObjectProperty(item, keyprop), w, keyprop.PropertyType, context, tabLevel + 1);
					Serialize(context.GetObjectProperty(item, valprop), w, valprop.PropertyType, context, tabLevel + 1);
				}
				else
					Serialize(item, w, itemType, context, tabLevel + 1);
			}

			// write end object
			for (int i = 0; i < tabLevel; i++)
				w.Write('\t');
			w.WriteLine(";");
		}

		private static int GetSerializationPriority(PropertyInfo p)
		{
			var atts = p.GetCustomAttributes(true).OfType<SerializationPriorityAttribute>();
			if (atts.Any())
				return atts.Min(att => att.Priority);
			return int.MaxValue;
		}

		private static void WriteObject(object o, TextWriter w, ObjectGraphContext context, int tabLevel)
		{
			// serialize object type and field count
			if (o is IDataObject)
			{
				// use data object code! :D
				var type = o.GetType();
				var data = (o as IDataObject).Data;
				w.WriteLine("p" + data.Count + ":");
				foreach (var kvp in data)
				{
					var pname = kvp.Key;
					var val = kvp.Value;
					var prop = ObjectGraphContext.GetKnownProperties(type)[pname];
					if (prop != null)
					{
						var ptype = prop.PropertyType;
						WriteProperty(w, o, ptype, pname, val, context, tabLevel);
					}
					else
					{
						// TODO - if property doesn't exist, log a warning somewhere?
						WriteProperty(w, o, typeof(object), pname, val, context, tabLevel);
					}
				}
			}
			else
			{
				// use reflection :(
				var type = o.GetType();
				var props = ObjectGraphContext.GetKnownProperties(type).Values.Where(p => !p.GetValue(o, null).SafeEquals(p.PropertyType.DefaultValue()));
				w.WriteLine("p" + props.Count() + ":");
				foreach (var p in props.OrderBy(p => GetSerializationPriority(p)))
					WriteProperty(w, o, p.PropertyType, p.Name, context.GetObjectProperty(o, p), context, tabLevel);
			}

			// write end object
			for (int i = 0; i < tabLevel; i++)
				w.Write('\t');
			w.WriteLine(";");
		}

		private static void WriteProperty(TextWriter w, object o, Type ptype, string pname, object val, ObjectGraphContext context, int tabLevel)
		{
			var tabs = new string('\t', tabLevel);
			var moreTabs = new string('\t', tabLevel + 1);

			// serialize property name and value
			try
			{
				w.Write(moreTabs);
				w.Write(pname);
				w.Write(":\n");
				Serialize(val, w, ptype, context, tabLevel + 2);
			}
			catch (Exception ex)
			{
				throw new SerializationException("Could not serialize property " + pname + " of " + o + " because the property accessor threw an exception: " + ex.Message, ex);
			}
		}

		internal static T Deserialize<T>(Stream s, ObjectGraphContext context = null)
		{
			var sr = new StreamReader(new BufferedStream(s));
			var result = Deserialize<T>(sr, context);
			return result;
		}


		private static string DeserializeString(TextReader r, ObjectGraphContext context, StringBuilder log)
		{
			string o;
			bool foundRealSemicolon = false;
			StringBuilder sb = new StringBuilder();
			int quotes = 0;
			while (!foundRealSemicolon)
			{
				var ns = r.ReadTo(';', log);
				quotes += ns.Count(c => c == '"');
				sb.Append(ns);
				if (!ns.EndsWith("\\") && quotes % 2 == 0)
					foundRealSemicolon = true;
			}
			var s = sb.ToString();
			if (s == "n")
				o = null;
			else
				o = s.Trim().Trim('"').Replace("\\\"", "\"").Replace("\\;", ";").Replace("\\\\", "\\");
			return o;
		}

		private static Array DeserializeArray(TextReader r, Type type, ObjectGraphContext context, StringBuilder log)
		{
			// arrays
			Array o;
			// read bounds or id number
			var fin = r.Read();
			while (fin != 0 && char.IsWhiteSpace((char)fin))
			{
				if (log != null)
					log.Append((char)fin);
				fin = r.Read();
			}
			if (fin != 0 && log != null)
				log.Append((char)fin);
			if (fin == 'a')
			{
				var boundsStrs = r.ReadTo(':', log).Split(',');
				if (boundsStrs.Length < 1)
					throw new SerializationException("Arrays cannot have zero dimensions.");

				if (boundsStrs.Length == 1)
				{
					int min, max;
					var bounds1Strs = boundsStrs[0].Split('_');
					if (!int.TryParse(bounds1Strs[0], out min))
						throw new SerializationException("Expected integer, got \"" + bounds1Strs[0] + "\" when parsing array bounds.");
					if (!int.TryParse(bounds1Strs[1], out max))
						throw new SerializationException("Expected integer, got \"" + bounds1Strs[1] + "\" when parsing array bounds.");
					// HACK - figure out how to set min and max bounds, in case it matters (VB?)
					var array = Array.CreateInstance(type.GetElementType(), max - min + 1);
					for (int i = min; i <= max; i++)
						array.SetValue(Deserialize(r, type.GetElementType(), false, context, log), i);
					o = array;
				}
				else if (boundsStrs.Length == 2)
				{
					int min1, max1, min2, max2;
					var bounds1Strs = boundsStrs[0].Split('_');
					var bounds2Strs = boundsStrs[1].Split('_');
					if (!int.TryParse(bounds1Strs[0], out min1))
						throw new SerializationException("Expected integer, got \"" + bounds1Strs[0] + "\" when parsing array bounds.");
					if (!int.TryParse(bounds1Strs[1], out max1))
						throw new SerializationException("Expected integer, got \"" + bounds1Strs[1] + "\" when parsing array bounds.");
					if (!int.TryParse(bounds2Strs[0], out min2))
						throw new SerializationException("Expected integer, got \"" + bounds2Strs[0] + "\" when parsing array bounds.");
					if (!int.TryParse(bounds2Strs[1], out max2))
						throw new SerializationException("Expected integer, got \"" + bounds2Strs[1] + "\" when parsing array bounds.");
					// HACK - figure out how to set min and max bounds, in case it matters (VB?)
					var array = Array.CreateInstance(type.GetElementType(), max1 - min1 + 1, max2 - min2 + 1);
					for (int x = min1; x <= max1; x++)
					{
						for (int y = min2; y <= max2; y++)
							array.SetValue(Deserialize(r, type.GetElementType(), false, context, log), x, y);
					}
					o = array;
					context.Add(o);
				}
				else
					throw new SerializationException("Arrays with more than two dimensions are not supported.");

				// clean up
				ReadSemicolon(r, type, log);
			}
			else if (fin == 'i')
			{
				// ID - need to find known object
				int id;
				string s = r.ReadTo(';', log);
				if (!int.TryParse(s, out id))
					throw new SerializationException("Expected integer, got \"" + s + "\" when parsing object ID.");

				// do we have it?
				if (!context.KnownObjects.ContainsKey(type) || context.KnownObjects[type].Count <= id)
					throw new SerializationException("No known object of type " + type + " has an ID of " + id + ".");

				// found it!
				o = (Array)context.KnownObjects[type][id];
			}
			else if (fin == 'n')
			{
				// null object!
				o = null;

				// clean up
				ReadSemicolon(r, type, log);
			}
			else
				throw new SerializationException("Expected 'a'/'i'/'n', got '" + (char)fin + "' when parsing " + type + ".");
			return o;
		}

		private static IEnumerable DeserializeCollection(TextReader r, Type type, ObjectGraphContext context, StringBuilder log)
		{
			IEnumerable o;
			// collections
			// read size or id number
			var fin = r.Read();
			while (fin != 0 && char.IsWhiteSpace((char)fin))
			{
				if (log != null)
					log.Append((char)fin);
				fin = r.Read();
			}
			if (fin != 0 && log != null)
				log.Append((char)fin);
			if (fin == 'c')
			{
				int size;
				var sizeStr = r.ReadTo(':', log);
				if (!int.TryParse(sizeStr, out size))
					throw new SerializationException("Expected integer, got \"" + sizeStr + "\" when parsing collection size.");
				var coll = Activator.CreateInstance(type);
				context.Add(coll);
				var adder = type.GetMethods().Single(m => m.Name == "Add" && m.GetParameters().Length == 1);
				Type itemType;
				if (typeof(DynamicDictionary).IsAssignableFrom(type))
				{
					itemType = typeof(KeyValuePair<object, object>);
				}
				else if (type.GetGenericArguments().Length == 2)
				{
					// HACK - assume it's a dictionary, no real way to test
					itemType = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
				}
				else if (type.GetGenericArguments().Length == 1)
				{
					// HACK - assume it's a collection, no real way to test
					itemType = type.GetGenericArguments()[0];
				}
				else
				{
					// no generic type? probably a list of objects?
					itemType = typeof(object);
				}
				var collParm = Expression.Parameter(type, "coll");
				var objParm = Expression.Parameter(itemType, "obj");
				Delegate lambdaAdder;
				if (ObjectGraphContext.CollectionAdders[type] == null)
				{
					// lambda has not been created yet, so create it
					try
					{
						ObjectGraphContext.CollectionAdders[type] =
							Expression.Lambda(Expression.Call(
								collParm, // the collection to add to
								adder, // the add method to call
								objParm), // the object to add
								collParm, objParm).Compile();
					}
					catch (Exception ex)
					{
						throw new SerializationException("Could not create lambda to add {0} items to {1}.".F(itemType, type), ex);
					}
				}

				// get lambda
				lambdaAdder = ObjectGraphContext.CollectionAdders[type];

				// load items and add them
				for (int i = 0; i < size; i++)
				{
					var item = Deserialize(r, itemType, false, context, log);
					lambdaAdder.DynamicInvoke(coll, item);
				}
				o = (IEnumerable)coll;

				// clean up
				ReadSemicolon(r, type, log);
			}
			else if (fin == 'd')
			{
				int size;
				var sizeStr = r.ReadTo(':', log);
				if (!int.TryParse(sizeStr, out size))
					throw new SerializationException("Expected integer, got \"" + sizeStr + "\" when parsing collection size.");
				var coll = type.Instantiate();
				context.Add(coll);
				var adder = type.GetMethods().Single(m => m.Name == "Add" && m.GetParameters().Length == 2);
				Type itemType;
				if (type.GetGenericArguments().Count() == 2)
					itemType = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
				else
					// HACK - Resources inherits from a dictionary type
					itemType = typeof(KeyValuePair<,>).MakeGenericType(type.BaseType.GetGenericArguments());

				var collParm = Expression.Parameter(typeof(object), "coll");
				var keyParm = Expression.Parameter(typeof(object), "key");
				var valParm = Expression.Parameter(typeof(object), "val");
				var keyprop = ObjectGraphContext.GetKnownProperties(itemType)["Key"];
				var valprop = ObjectGraphContext.GetKnownProperties(itemType)["Value"];
				Delegate lambdaAdder;
				if (ObjectGraphContext.CollectionAdders[type] == null)
				{
					// lambda has not been created yet, so create it
					ObjectGraphContext.CollectionAdders[type] =
						Expression.Lambda(Expression.Call(
							Expression.Convert(collParm, type),
							adder,
							Expression.Convert(keyParm, keyprop.PropertyType),
							Expression.Convert(valParm, valprop.PropertyType)
							), collParm, keyParm, valParm).Compile();
				}

				// get lambda
				lambdaAdder = ObjectGraphContext.CollectionAdders[type];

				// load items and add them
				for (int i = 0; i < size; i++)
				{
					var key = Deserialize(r, keyprop.PropertyType, false, context, log);
					var val = Deserialize(r, valprop.PropertyType, false, context, log);
					lambdaAdder.DynamicInvoke(coll, key, val);
				}

				o = (IEnumerable)coll;

				// clean up
				ReadSemicolon(r, type, log);
			}
			else if (fin == 'i')
			{
				// ID - need to find known object
				int id;
				string s = r.ReadTo(';', log);
				if (!int.TryParse(s, out id))
					throw new SerializationException("Expected integer, got \"" + s + "\" when parsing object ID.");

				// do we have it?
				if (!context.KnownObjects.ContainsKey(type) || context.KnownObjects[type].Count <= id)
					throw new SerializationException("No known object of type " + type + " has an ID of " + id + ".");

				// found it!
				o = (IEnumerable)context.KnownObjects[type][id];
			}
			else if (fin == 'n')
			{
				// null object!
				o = null;

				// clean up
				ReadSemicolon(r, type, log);
			}
			else
				throw new SerializationException("Expected 'c'/'d'/'i'/'n', got '" + (char)fin + "' when parsing " + type + ".");
			return o;
		}

		private static IList<Task> propertySetterTasks = new List<Task>();

		private static object DeserializeObject(TextReader r, Type type, ObjectGraphContext context, StringBuilder log)
		{
			object o;
			// read property count or id number
			var fin = r.Read();
			while (fin != 0 && char.IsWhiteSpace((char)fin))
			{
				if (log != null)
					log.Append((char)fin);
				fin = r.Read();
			}
			if (fin != 0 && log != null)
				log.Append((char)fin);
			if (fin == 'p')
			{
				// create object and add it to our context
				o = type.Instantiate();
				context.Add(o);

				// properties count - need to create object and populate properties
				int count;
				string s = r.ReadTo(':', log);
				if (!int.TryParse(s, out count))
					throw new SerializationException("Expected integer, got \"" + s + "\" when parsing property count.");

				var dict = new SafeDictionary<string, object>();

				// deserialize the properties
				var props = ObjectGraphContext.GetKnownProperties(type);
				for (int i = 0; i < count; i++)
				{
					var pname = r.ReadTo(':', log).Trim();
					if (props.ContainsKey(pname))
					{
						var prop = props[pname];
						if (prop != null && !prop.HasAttribute<DoNotSerializeAttribute>())
						{
							if (prop.Name == "Sector" && type == typeof(Game.Objects.Civilization.SpaceObjectWaypoint))
							{

							}
							dict[pname] = Deserialize(r, prop.PropertyType, false, context, log);
						}
						else
							r.ReadTo(';', log); // throw away this property, we don't need it
												// if p is null or has do not serialize attribute, it must be data from an old version with different property names, so don't crash
					}
				}

				//propertySetterTasks.Add(Task.Factory.StartNew(() =>
				//{
				o.SetData(dict, context);
				//}));

				// clean up
				ReadSemicolon(r, type, log);
			}
			else if (fin == 'i')
			{
				// ID - need to find known object
				int id;
				string s = r.ReadTo(';', log);
				if (!int.TryParse(s, out id))
					throw new SerializationException("Expected integer, got \"" + s + "\" when parsing object ID.");

				// do we have it?
				if (!context.KnownObjects.ContainsKey(type) || context.KnownObjects[type].Count <= id)
					throw new SerializationException("No known object of type " + type + " has an ID of " + id + ".");

				// found it!
				o = context.KnownObjects[type][id];
			}
			else if (fin == 'n')
			{
				// null object!
				o = null;

				// clean up
				ReadSemicolon(r, type, log);
			}
			else
				throw new SerializationException("Expected 'p'/'i'/'n', got '" + (char)fin + "' when parsing " + type + ".");
			return o;
		}

		public static object Deserialize(Stream s, Type desiredType, ObjectGraphContext context = null, StringBuilder log = null)
		{
			var sr = new StreamReader(s);
			var result = Deserialize(sr, desiredType, true, context, log);
			sr.Close();
			return result;
		}

		internal static T Deserialize<T>(TextReader r, ObjectGraphContext context = null)
		{
			return (T)LegacySerializer.Deserialize(r, typeof(T), true, context);
		}

		private static object Deserialize(TextReader r, Type desiredType, bool isRoot, ObjectGraphContext context = null, StringBuilder log = null)
		{
			// set up our serialization context if we haven't already
			if (context == null)
				context = new ObjectGraphContext();

			if (isRoot)
			{
				// clear out our tasks
				propertySetterTasks = new List<Task>();
			}

			// find data type
			var typename = r.ReadTo(':', log).Trim();
			Type type;
			if (string.IsNullOrWhiteSpace(typename))
				type = desiredType;
			else
			{
				type = ObjectGraphContext.KnownTypes[typename];
				if (type == null)
				{
					try
					{
						type = new SafeType(typename).Type;
					}
					catch (Exception ex)
					{
						throw new SerializationException("Unknown data type '" + typename + "'. Perhaps this data was serialized with an incompatible version of the application?", ex);
					}
				}
				if (type == null)
					throw new SerializationException("Unable to determine object type from type string \"" + typename + "\"");
			}

			if (!ObjectGraphContext.KnownTypes.ContainsKey(type.AssemblyQualifiedName))
			{
				// add to known types
				ObjectGraphContext.KnownTypes.Add(type.AssemblyQualifiedName, type);
			}

			// check type so we don't bother trying to create an object only to find it's the wrong type later
			if (!desiredType.IsAssignableFrom(type))
				throw new SerializationException("Expected " + desiredType + ", got " + type + " when parsing new object.");

			// the object!
			object o;

			if (type.IsPrimitive)
			{
				// parse primitive types
				var val = r.ReadTo(';', log);
				o = Convert.ChangeType(val, type);
			}
			else if (type == typeof(string))
			{
				// parse strings
				o = DeserializeString(r, context, log);
			}
			else if (type == typeof(Color))
			{
				// HACK - Color implmentation varies between .NET and Mono, so treat it as raw ARGB values
				var argb = r.ReadTo(';', log).Split(',');
				if (argb.Length != 4)
					throw new SerializationException("Colors must have 4 ARGB values.");
				byte a, rv, g, b;
				if (!byte.TryParse(argb[0], out a) || !byte.TryParse(argb[1], out rv) || !byte.TryParse(argb[2], out g) || !byte.TryParse(argb[3], out b))
					throw new SerializationException("Could not parse one of the ARGB values in \"" + argb + "\".");
				o = Color.FromArgb(a, rv, g, b);
			}
			else if (typeof(Enum).IsAssignableFrom(type))
			{
				// parse enums
				var val = r.ReadTo(';', log);
				o = val.ParseEnum(type);
			}
			else if (typeof(Array).IsAssignableFrom(type))
			{
				// parse arrays
				o = DeserializeArray(r, type, context, log);
			}
			else if (typeof(IEnumerable).IsAssignableFrom(type) && type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1 || m.GetParameters().Length == 2).Any() && !typeof(IReferenceEnumerable).IsAssignableFrom(type))
			{
				// parse collections
				o = DeserializeCollection(r, type, context, log);
			}
			else
			{
				// parse objects
				o = DeserializeObject(r, type, context, log);
			}

			/*if (isRoot)
			{
				// wait for tasks to complete
				Action<Task> awaitTask = async t => await t;
				propertySetterTasks.RunTasks(awaitTask);
			}*/

			// return our new object
			return o;
		}

		private static void ReadSemicolon(TextReader r, Type type, StringBuilder log)
		{
			// read the semicolon at the end and any whitespace
			int ender;
			do
			{
				ender = r.Read();
				if (log != null)
					log.Append((char)ender);
				if (ender == 0 || ender != ';' && !char.IsWhiteSpace((char)ender))
					throw new SerializationException("Expected ';', got '" + (char)ender + "' at the end of " + type + ".");
			} while (ender != ';');
		}
	}
}
