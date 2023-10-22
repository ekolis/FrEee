using FrEee.Extensions;
using FrEee.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FrEee.Serialization
{
	/// <summary>
	/// Prevents an property or class's value from being copied when the containing object is copied.
	/// Instead, the original value will be used, or the known copy if the value has already been copied.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class DoNotCopyAttribute : Attribute
	{
		public DoNotCopyAttribute(bool allowSafeCopy = true)
		{
			AllowSafeCopy = allowSafeCopy;
		}

		/// <summary>
		/// Is "safe" copying (using the original property value) allowed?
		/// If false, even this will not be attempted, and the property will be completely ignored.
		/// Setting to false is useful for properties whose setters throw NotSupportedException.
		/// </summary>
		public bool AllowSafeCopy { get; private set; }
	}

	/// <summary>
	/// Context for object graph operations.
	/// </summary>
	[Serializable]
	public class ObjectGraphContext
	{
		static ObjectGraphContext()
		{
			KnownTypes = new SafeDictionary<string, Type>();
			KnownPropertiesIncludingDoNotSerialize = new SafeDictionary<Type, IDictionary<string, PropertyInfo>>();
			KnownPropertiesExcludingDoNotSerialize = new SafeDictionary<Type, IDictionary<string, PropertyInfo>>();
			PropertyGetters = new SafeDictionary<PropertyInfo, Delegate>();
			PropertySetters = new SafeDictionary<PropertyInfo, Delegate>();
			CollectionAdders = new SafeDictionary<Type, Delegate>();
		}

		public ObjectGraphContext()
		{
			KnownObjects = new SafeDictionary<Type, IList<object>>();
			KnownIDs = new SafeDictionary<Type, IDictionary<object, int>>();
			objectStack = new Stack<object>();
			propertyStack = new Stack<string>();
			ObjectQueue = new Queue<object>();
		}

		/// <summary>
		/// Adder methods for collection/dictionary types.
		/// </summary>
		public static SafeDictionary<Type, Delegate> CollectionAdders { get; private set; }

		/// <summary>
		/// Known data types.
		/// </summary>
		public static IDictionary<string, Type> KnownTypes { get; private set; }

		/// <summary>
		/// Getters for properties.
		/// </summary>
		public static SafeDictionary<PropertyInfo, Delegate> PropertyGetters { get; private set; }

		/// <summary>
		/// Setters for properties.
		/// </summary>
		public static SafeDictionary<PropertyInfo, Delegate> PropertySetters { get; private set; }

		/// <summary>
		/// The known IDs of objects.
		/// </summary>
		public SafeDictionary<Type, IDictionary<object, int>> KnownIDs { get; private set; }

		/// <summary>
		/// The known objects, grouped by type. Their IDs are their indices in the lists.
		/// </summary>
		[field: NonSerialized]
		public SafeDictionary<Type, IList<object>> KnownObjects { get; private set; }

		/// <summary>
		/// Stack/path of objects to the current object being parsed.
		/// </summary>
		public IEnumerable<object> ObjectStack { get { return objectStack; } }

		/// <summary>
		/// Stack/path of properties to the current object being parsed.
		/// </summary>
		public IEnumerable<string> PropertyStack { get { return propertyStack; } }

		[field: NonSerialized]
		internal Queue<object> ObjectQueue { get; set; }

		[NonSerialized]
		internal Stack<object> objectStack;

		internal Stack<string> propertyStack;

		/// <summary>
		/// Known properties for each object type.
		/// </summary>
		private static SafeDictionary<Type, IDictionary<string, PropertyInfo>> KnownPropertiesIncludingDoNotSerialize { get; set; }

		/// <summary>
		/// Known properties for each object type.
		/// </summary>
		private static SafeDictionary<Type, IDictionary<string, PropertyInfo>> KnownPropertiesExcludingDoNotSerialize { get; set; }

		/// <summary>
		/// Adds the properties for a type.
		/// </summary>
		/// <param name="type"></param>
		public static void AddProperties(Type type)
		{
			if (!KnownPropertiesIncludingDoNotSerialize.ContainsKey(type))
			{
				// list out the object's properties that aren't marked nonserializable
				var props = new Dictionary<PropertyInfo, int>();
				var t = type;
				var i = 0; // how far removed in the type hierarchy?
				while (t != null)
				{
					var newprops = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f =>
						type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)
						||

							// hopefully put "quicker to check" and "easier to fail" conditions up top
							f.GetIndexParameters().Length == 0 // we don't support indexed properties
							&& f.GetGetMethod(true) != null && f.GetSetMethod(true) != null

					);
					foreach (var prop in newprops)
						props.Add(prop, i);
					t = t.BaseType;
					i++;
				}
				// Mono seems to place inherited properties on the derived type too so we need a consistent ordering
				var props2 = props.Distinct().GroupBy(p => p.Key.Name).Select(g => g.Single(p2 => p2.Value == g.Max(p3 => p3.Value))).Select(kvp => kvp.Key).OrderBy(p => p.Name);
				KnownPropertiesIncludingDoNotSerialize.Add(type, props2.ToDictionary(p => p.Name));
				KnownPropertiesExcludingDoNotSerialize.Add(type, props2.Where(x => !x.HasAttribute<DoNotSerializeAttribute>()).ToDictionary(p => p.Name, p => p));
				foreach (var prop in props2)
				{
					var objParm = Expression.Parameter(prop.DeclaringType);
					var valParm = Expression.Parameter(prop.PropertyType, "val");
					var getMethod = prop.GetGetMethod(true);
					var setMethod = prop.GetSetMethod(true);
					if (getMethod != null)
					{
						var getter = Expression.Call(objParm, getMethod);
						PropertyGetters[prop] = Expression.Lambda(getter, objParm).Compile();
					}
					if (setMethod != null)
					{
						var setter = Expression.Call(objParm, setMethod, valParm);
						PropertySetters[prop] = Expression.Lambda(setter, objParm, valParm).Compile();
					}
				}
			}
		}

		public static IDictionary<string, PropertyInfo> GetKnownProperties(Type t, bool includeDoNotSerializeProperties = false)
		{
			var kp = includeDoNotSerializeProperties ? KnownPropertiesIncludingDoNotSerialize : KnownPropertiesExcludingDoNotSerialize;
			if (kp[t] == null)
				AddProperties(t);
			return kp[t];
		}

		/// <summary>
		/// Adds an object.
		/// </summary>
		/// <param name="o"></param>
		/// <returns>The object's ID. IDs are unique within any any given object type but not across types.</returns>
		public int Add(object o)
		{
			if (o == null)
				return -1;
			var type = o.GetType();
			if (!KnownTypes.ContainsKey(type.AssemblyQualifiedName))
				KnownTypes.Add(type.AssemblyQualifiedName, type);
			if (!KnownObjects.ContainsKey(type))
				KnownObjects.Add(type, new List<object>());
			//			if (!KnownObjects[type].Contains(o))
			KnownObjects[type].Add(o);
			if (!KnownIDs.ContainsKey(type))
				KnownIDs.Add(type, new SafeDictionary<object, int>());
			var id = KnownObjects[type].Count - 1;
			KnownIDs[type].Add(o, id);
			AddProperties(type);
			return id;
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
			if (KnownIDs.ContainsKey(type) && KnownIDs[type].ContainsKey(o))
				return KnownIDs[type][o];
			return null;
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

		public IDictionary<string, PropertyInfo> GetProperties(Type type, bool includeDoNotSerialize = false)
		{
			AddProperties(type);
			return includeDoNotSerialize ? KnownPropertiesIncludingDoNotSerialize[type] : KnownPropertiesExcludingDoNotSerialize[type];
		}

		public void SetObjectProperty(object obj, PropertyInfo prop, object val)
		{
			if (obj is Type)
				throw new InvalidOperationException("Cannot set properties on an object of type System.Type.");
			if (obj == null)
				throw new ArgumentNullException(nameof(obj), "Can't set properties on a null object.");
			// lambda expressions don't seem to work on structs
			try
			{
				if (obj.GetType().IsValueType)
					prop.SetValue(obj, val, new object[] { });
				else
					PropertySetters[prop].DynamicInvoke(obj, val);
			}
			catch (NullReferenceException ex)
			{
				throw new InvalidOperationException($"Could not set property {prop} on object {obj} of type {obj.GetType()}. Does the type actually have this property?", ex);
			}
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
				return ReferenceEquals(x, y);
			}

			public int GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	}

	/// <summary>
	/// Parses object graphs and generates events when objects are detected.
	/// </summary>
	public class ObjectGraphParser
	{
		public IDictionary<Type, IList<object>> Parse(object o, ObjectGraphContext context = null)
		{
			// set up our context if we haven't already
			if (context == null)
				context = new ObjectGraphContext();

			// fire up the queue
			context.ObjectQueue.Enqueue(o);
			while (context.ObjectQueue.Any())
			{
				o = context.ObjectQueue.Dequeue();
				context.objectStack.Push(o);

				// deal with nulls
				if (o == null)
				{
					if (Null != null)
						Null(null);
					context.objectStack.Pop();
					continue;
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

				if (!type.IsValueType && type != typeof(string) && !typeof(Array).IsAssignableFrom(type))
				{
					if (id == null)
					{
						// add to context
						context.Add(o);
					}
					else
					{
						if (KnownObject != null)
							KnownObject(o);

						// done
						context.objectStack.Pop();
						continue;
					}
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
				context.objectStack.Pop();
			}

			return context.KnownObjects;
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
				context.propertyStack.Push("(Array Item)");
				if (Item != null)
					Item(item);
				context.ObjectQueue.Enqueue(item);
				context.propertyStack.Pop();
			}
		}

		private void ParseCollection(IEnumerable list, ObjectGraphContext context)
		{
			foreach (var item in list.Cast<object>().ToArray())
			{
				context.propertyStack.Push("(Collection Item)");
				if (Item != null)
					Item(item);
				context.ObjectQueue.Enqueue(item);
				context.propertyStack.Pop();
			}
		}

		private void ParseColor(Color c, ObjectGraphContext context)
		{
			// HACK - Mono's implmentation of Color is different from .NET's so we need to save just the ARGB values in a consistent format
			ParseProperty("A", c, c.A, context);
			ParseProperty("R", c, c.R, context);
			ParseProperty("G", c, c.G, context);
			ParseProperty("B", c, c.B, context);
		}

		private void ParseObject(object o, ObjectGraphContext context)
		{
			var type = o.GetType();
			var props = ObjectGraphContext.GetKnownProperties(type);
			foreach (var p in props.Values)
			{
				var val = p.GetValue(o, new object[] { });
				ParseProperty(p.Name, o, val, context);
			}
		}

		private void ParseProperty(string propertyName, object o, object val, ObjectGraphContext context)
		{
			context.propertyStack.Push(propertyName);
			var recurse = true; // if no event handler, assume we are parsing recursively (really adding subproperties to queue)
			if (Property != null)
				recurse = Property(propertyName, o, val);
			if (recurse)
			{
				context.ObjectQueue.Enqueue(val);
			}
			context.propertyStack.Pop();
		}

		/// <summary>
		/// Raised when a new array is encountered.
		/// </summary>
		public event ArrayDimensionsDelegate ArrayDimensions;

		/// <summary>
		/// Raised when a new object is finished being parsed.
		/// </summary>
		public event ObjectDelegate EndObject;

		/// <summary>
		/// Raised when an item in a collection is encountered.
		/// </summary>
		public event ObjectDelegate Item;

		/// <summary>
		/// Raised when a previously encountered object is encountered again.
		/// </summary>
		public event ObjectDelegate KnownObject;

		/// <summary>
		/// Raised when a null reference is encountered.
		/// </summary>
		public event ObjectDelegate Null;

		/// <summary>
		/// Raised when an object's property is encountered.
		/// </summary>
		public event PropertyDelegate Property;

		/// <summary>
		/// Raised when a new object is encountered.
		/// </summary>
		public event ObjectDelegate StartObject;

		/// <summary>
		/// Delegate for when an array's dimensions are encountered.
		/// </summary>
		/// <param name="dimensions">The array dimensions.</param>
		public delegate void ArrayDimensionsDelegate(IEnumerable<int> dimensions);

		/// <summary>
		/// Delegate for when an object is encountered.
		/// </summary>
		/// <param name="o">The object encountered.</param>
		public delegate void ObjectDelegate(object o);

		/// <summary>
		/// Delegate for when an object's property is encountered.
		/// </summary>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="o">The object possessing the property.</param>
		/// <param name="val">The value of the property.</param>
		/// <returns>true to recursively parse the property value, false to skip it</returns>
		public delegate bool PropertyDelegate(string propertyName, object o, object val);
	}
}