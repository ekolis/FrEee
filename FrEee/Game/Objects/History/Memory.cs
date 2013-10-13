using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// A memory of some object that has been seen by an empire.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Memory
	{
		public Memory()
		{
		}

		public void Load(object value, IDictionary<object, Memory> seen = null)
		{
			if (value != null)
			{
				if (seen == null)
					seen = new Dictionary<object, Memory>();
				DataType = value.GetType();
				if (DataType.IsPrimitive || typeof(Enum).IsAssignableFrom(DataType) || DataType.Name == "Nullable`1")
					ScalarData = value;
				else if (DataType == typeof(string))
					ScalarData = value;
				else if (DataType == typeof(Color))
				{
					// HACK - mono and .net store colors differently
					var color = (Color)value;
					PropertyData = new SafeDictionary<string, Memory>();
					PropertyData["R"] = new Memory(color.R);
					PropertyData["G"] = new Memory(color.G);
					PropertyData["B"] = new Memory(color.B);
					PropertyData["A"] = new Memory(color.A);
				}
				else if (typeof(Array).IsAssignableFrom(DataType))
				{
					var array = (Array)value;
					ListData = new List<Memory>();
					ArrayDimensionsData = new List<int>();
					if (array.Rank == 1)
					{
						ArrayDimensionsData.Add(array.GetLength(0));
						foreach (var item in array)
						{
							if (item != null && seen.ContainsKey(item))
								ListData.Add(seen[item]);
							else
							{
								var m = new Memory();
								if (item != null)
									seen[item] = m;
								m.Load(item, seen);
								ListData.Add(m);
							}
						}
					}
					else if (array.Rank == 2)
					{
						ArrayDimensionsData.Add(array.GetLength(0));
						ArrayDimensionsData.Add(array.GetLength(1));
						for (int x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
						{
							for (int y = array.GetLowerBound(1); y <= array.GetUpperBound(1); y++)
							{
								var item = array.GetValue(x, y);
								if (item != null && seen.ContainsKey(item))
									ListData.Add(seen[item]);
								else
								{
									var m = new Memory();
									if (item != null)
										seen[item] = m;
									m.Load(item, seen);
									ListData.Add(m);
								}
							}
						}
					}
					else
						throw new Exception("Arrays with more than 2 dimensions are not supported.");
				}
				else if (typeof(IEnumerable).IsAssignableFrom(DataType) && DataType.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1 || m.GetParameters().Length == 2).Any())
					ListData = ((IEnumerable)value).Cast<object>().Select(o =>
					{
						if (o != null && seen.ContainsKey(o))
							return seen[o];
						else
						{
							var m = new Memory();
							if (o != null)
								seen[o] = m;
							m.Load(o, seen);
							return m;
						}
					}).ToList();
				else
				{
					var props = DataType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f => !f.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any() && f.GetGetMethod(true) != null && f.GetSetMethod(true) != null);
					PropertyData = new SafeDictionary<string, Memory>();
					foreach (var prop in props)
					{
						var propval = prop.GetValue(value, new object[0]);
						if (propval != null && seen.ContainsKey(propval))
							PropertyData[prop.Name] = seen[propval];
						else
						{
							var m = new Memory();
							if (propval != null)
								seen[propval] = m;
							m.Load(propval, seen);
							PropertyData[prop.Name] = m;
						}
					}
				}
			}
		}

		public Memory(object value)
		{
			Load(value);
		}

		/// <summary>
		/// The type of data which is being remembered.
		/// </summary>
		public Type DataType { get; private set; }

		/// <summary>
		/// The data that is known about this memory.
		/// </summary>
		public IList<Memory> ListData { get; private set; }

		/// <summary>
		/// The array dimensions.
		/// </summary>
		public IList<int> ArrayDimensionsData { get; private set; }

		/// <summary>
		/// The data that is known about this memory.
		/// </summary>
		public SafeDictionary<string, Memory> PropertyData { get; private set; }

		/// <summary>
		/// The data that is known about this memory.
		/// </summary>
		public object ScalarData { get; private set; }

		/// <summary>
		/// Reconstitutes an object from the known data.
		/// </summary>
		/// <returns></returns>
		public object Remember()
		{
			if (DataType == null)
				return null;
			if (DataType.IsPrimitive || typeof(Enum).IsAssignableFrom(DataType) || DataType.Name == "Nullable`1")
				return ScalarData;
			else if (DataType == typeof(string))
				return ScalarData;
			var obj = DataType.Instantiate();
			RememberTo(obj);
			return obj;
		}

		/// <summary>
		/// Reconstitutes an object from the known data.
		/// </summary>
		/// <typeparam name="T">Must be a concrete type.</typeparam>
		/// <returns></returns>
		public T Remember<T>()
		{
			return (T)Remember();
		}

		/// <summary>
		/// Reconstitutes an object onto an existing object, overwriting its data.
		/// </summary>
		/// <param name="obj"></param>
		public void RememberTo(object obj)
		{
			if (DataType == null || ListData == null && PropertyData == null && ScalarData == null)
				throw new Exception("Cannot remember a memory of null onto an object.");
			else if (DataType.IsValueType)
				throw new Exception("Cannot remember a memory onto a value type.");
			else if (typeof(Array).IsAssignableFrom(DataType))
			{
				var array = (Array)obj;
				if (array.Rank == 1)
				{
					for (int x = 0; x < ArrayDimensionsData[0]; x++)
						array.SetValue(ListData[x], x);
				}
				else if (array.Rank == 2)
				{
					for (int x = 0; x < ArrayDimensionsData[0]; x++)
					{
						for (int y = 0; y <= ArrayDimensionsData[1]; y++)
							array.SetValue(ListData[x * ArrayDimensionsData[1] + y], x, y);
					}
				}
				else
					throw new Exception("Arrays with more than 2 dimensions are not supported.");
			}
			else if (typeof(IEnumerable).IsAssignableFrom(DataType) && DataType.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1 || m.GetParameters().Length == 2).Any())
			{
				var list = (IEnumerable)obj;
				var adder = DataType.GetMethods().Single(m => m.Name == "Add" && m.GetParameters().Length == 1);
				foreach (var item in ListData)
					adder.Invoke(list, new object[] { item });
			}
			else
			{
				var props = DataType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f => !f.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any() && f.GetGetMethod(true) != null && f.GetSetMethod(true) != null);
				foreach (var prop in props)
					prop.SetValue(obj, PropertyData[prop.Name], new object[0]);
			}
		}
	}
}
