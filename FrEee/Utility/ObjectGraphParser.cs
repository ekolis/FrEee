using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Parses object graphs and generates events when objects are detected.
	/// </summary>
	public class ObjectGraphParser
	{
		public delegate void ObjectDelegate(object o);
		public delegate void ArrayDimensionsDelegate(IEnumerable<int> dimensions);
		public delegate void PropertyDelegate(string propertyName, object o, object val);

		/// <summary>
		/// Raised when a null reference is encountered.
		/// </summary>
		public event ObjectDelegate Null;
		/// <summary>
		/// Raised when a previously encountered object is encountered again.
		/// </summary>
		public event ObjectDelegate KnownObject;
		/// <summary>
		/// Raised when a new object is encountered.
		/// </summary>
		public event ObjectDelegate StartObject;
		/// <summary>
		/// Raised when a new object is finished being parsed.
		/// </summary>
		public event ObjectDelegate EndObject;
		/// <summary>
		/// Raised when a new array is encountered.
		/// </summary>
		public event ArrayDimensionsDelegate ArrayDimensions;
		/// <summary>
		/// Raised when an object's property is encountered.
		/// </summary>
		public event PropertyDelegate Property;
		/// <summary>
		/// Raised when an item in a collection is encountered.
		/// </summary>
		public event ObjectDelegate Item;
		
		public void Parse(object o, ObjectGraphContext context = null)
		{
			// set up our context if we haven't already
			if (context == null)
				context = new ObjectGraphContext();

			// deal with nulls
			if (o == null)
			{
				if (Null != null)
					Null(null);
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
				context.AddProperties(type);
			}

			if (id == null && !type.IsValueType && type != typeof(string) && !typeof(Array).IsAssignableFrom(type))
			{
				// add to context
				context.Add(o);
			}

			// deal with refs
			if (id != null)
			{
				if (KnownObject != null)
					KnownObject(o);

				// done
				return;
			}

			// object was found
			if (StartObject != null)
				StartObject(o);

			// parse sub objects
			if (type.IsPrimitive || typeof(Enum).IsAssignableFrom(type) || type == typeof(string))
			{
				// nothing to do, no sub objects
			}
			else if (type == typeof(Color))
				ParseColor((Color)o, context);
			else if (typeof(Array).IsAssignableFrom(type))
				ParseArray((Array)o, context);
			else if (typeof(IEnumerable).IsAssignableFrom(type) && type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1 || m.GetParameters().Length == 2).Any())
				ParseCollection((IEnumerable)o, context);
			else
				ParseObject(o, context);

			// done parsing object
			if (EndObject != null)
				EndObject(o);
		}

		private void ParseProperty(string propertyName, object o, object val, ObjectGraphContext context)
		{
			if (Property != null)
				Property(propertyName, o, val);
			Parse(val, context);
		}

		private void ParseColor(Color c, ObjectGraphContext context)
		{
			// HACK - Mono's implmentation of Color is different from .NET's so we need to save just the ARGB values in a consistent format
			ParseProperty("A", c, c.A, context);
			ParseProperty("R", c, c.R, context);
			ParseProperty("G", c, c.G, context);
			ParseProperty("B", c, c.B, context);
		}

		private void ParseArray(Array array, ObjectGraphContext context)
		{
			if (ArrayDimensions != null)
			{
				var bounds = new List<int>();
				for (var rank = 0; rank < array.Rank; rank++)
					bounds.Add(array.GetLength(rank));
				ArrayDimensions(bounds);
			}
			foreach (var item in array)
			{
				if (Item != null)
					Item(item);
				Parse(item, context);
			}
		}

		private void ParseCollection(IEnumerable list, ObjectGraphContext context)
		{
			foreach (var item in list.Cast<object>().ToArray())
			{
				if (Item != null)
					Item(item);
				Parse(item, context);
			}
		}

		private void ParseObject(object o, ObjectGraphContext context)
		{
			var type = o.GetType();
			var props = ObjectGraphContext.KnownProperties[type];
			foreach (var p in props)
			{
				// serialize field value
				var val = p.GetValue(o, new object[] { });
				if (Property != null)
					Property(p.Name, o, val);
				Parse(val, context);
			}
		}
	}

	/// <summary>
	/// Context for object graph operations.
	/// </summary>
	public class ObjectGraphContext
	{
		public ObjectGraphContext()
		{
			KnownObjects = new SafeDictionary<Type, IList<object>>();
		}

		static ObjectGraphContext()
		{
			KnownTypes = new SafeDictionary<string, Type>();
			KnownProperties = new SafeDictionary<Type, IEnumerable<PropertyInfo>>();
			PropertyGetters = new SafeDictionary<PropertyInfo, Delegate>();
			PropertySetters = new SafeDictionary<PropertyInfo, Delegate>();
		}

		/// <summary>
		/// Known data types.
		/// </summary>
		public static IDictionary<string, Type> KnownTypes { get; private set; }

		/// <summary>
		/// Known properties for each object type.
		/// </summary>
		public static SafeDictionary<Type, IEnumerable<PropertyInfo>> KnownProperties { get; private set; }

		/// <summary>
		/// Getters for properties.
		/// </summary>
		public static SafeDictionary<PropertyInfo, Delegate> PropertyGetters { get; private set; }

		/// <summary>
		/// Setters for properties.
		/// </summary>
		public static SafeDictionary<PropertyInfo, Delegate> PropertySetters { get; private set; }

		/// <summary>
		/// The known objects, grouped by type. Their IDs are their indices in the lists.
		/// </summary>
		public SafeDictionary<Type, IList<object>> KnownObjects { get; private set; }

		/// <summary>
		/// Adds an object.
		/// </summary>
		/// <param name="o"></param>
		/// <returns>The object's ID. IDs are unique within any any given object type but not across types.</returns>
		public int Add(object o)
		{
			var type = o.GetType();
			if (!KnownTypes.ContainsKey(type.AssemblyQualifiedName))
				KnownTypes.Add(type.AssemblyQualifiedName, type);
			if (!KnownObjects.ContainsKey(type))
				KnownObjects.Add(type, new List<object>());
			KnownObjects[type].Add(o);
			AddProperties(type);
			return KnownObjects[type].Count - 1;
		}

		/// <summary>
		/// Adds the fields for a type.
		/// </summary>
		/// <param name="type"></param>
		public void AddProperties(Type type)
		{
			if (!KnownProperties.ContainsKey(type))
			{
				// list out the object's properties that aren't marked nonserializable
				var props = new Dictionary<PropertyInfo, int>();
				var t = type;
				int i = 0; // how far removed in the type hierarchy?
				while (t != null)
				{
					var newprops = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f => !f.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any() && f.GetGetMethod(true) != null && f.GetSetMethod(true) != null);
					foreach (var prop in newprops.Where(prop => prop.GetIndexParameters().Length == 0))
						props.Add(prop, i);
					t = t.BaseType;
					i++;
				}
				// Mono seems to place inherited properties on the derived type too so we need a consistent ordering
				var props2 = props.Distinct().GroupBy(p => p.Key.Name).Select(g => g.Single(p2 => p2.Value == g.Max(p3 => p3.Value))).Select(kvp => kvp.Key).OrderBy(p => p.Name);
				KnownProperties.Add(type, props2.ToArray());
				foreach (var prop in props2)
				{
					var objParm = Expression.Parameter(prop.DeclaringType);
					var valParm = Expression.Parameter(prop.PropertyType, "val");
					var getter = Expression.Call(objParm, prop.GetGetMethod(true));
					var setter = Expression.Call(objParm, prop.GetSetMethod(true), valParm);
					if (!PropertyGetters.ContainsKey(prop))
						PropertyGetters.Add(prop, Expression.Lambda(getter, objParm).Compile());
					if (!PropertySetters.ContainsKey(prop))
						PropertySetters.Add(prop, Expression.Lambda(setter, objParm, valParm).Compile());
				}
			}
		}

		public object GetObjectProperty(object obj, PropertyInfo prop)
		{
			AddProperties(obj.GetType());
			// lambda expressions don't seem to work on structs
			if (obj.GetType().IsValueType)
				return prop.GetValue(obj, new object[] { });
			else
				return PropertyGetters[prop].DynamicInvoke(obj);
		}

		public void SetObjectProperty(object obj, PropertyInfo prop, object val)
		{
			AddProperties(obj.GetType());
			// lambda expressions don't seem to work on structs
			if (obj.GetType().IsValueType)
				prop.SetValue(obj, val, new object[] { });
			else
				PropertySetters[prop].DynamicInvoke(obj, val);
		}

		/// <summary>
		/// Gets the ID for an object, or null if the object is unknown.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public int? GetID(object o)
		{
			if (o == null)
				return null;
			var type = o.GetType();
			if (KnownObjects.ContainsKey(type) && KnownObjects[type].Contains(o, ReferenceEqualityComparer.Instance))
				return KnownObjects[type].IndexOf(o);
			return null;
		}

		private class ReferenceEqualityComparer : IEqualityComparer<object>
		{
			static ReferenceEqualityComparer()
			{
				Instance = new ReferenceEqualityComparer();
			}

			private ReferenceEqualityComparer()
			{
			}

			public static ReferenceEqualityComparer Instance { get; private set; }

			public new bool Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			public int GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	}

	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class DoNotCopyAttribute : Attribute
	{

	}
}
