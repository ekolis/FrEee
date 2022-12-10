using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FrEee.Utility.Extensions
{
	/// <summary>
	/// Extension methods to copy objects.
	/// </summary>
	public static class CopyingExtensions
	{
		/// <summary>
		/// Checks for "do not copy" attribute, even on interface properties.
		/// Returns true if there is no such attribute.
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool CanCopyFully(this PropertyInfo p)
		{
			if (p.HasAttribute<DoNotCopyAttribute>() || p.PropertyType.HasAttribute<DoNotCopyAttribute>())
				return false;
			foreach (var i in p.DeclaringType.GetInterfaces())
			{
				var ip = i.GetProperty(p.Name);
				if (ip != null && ip.GetCustomAttributes(true).OfType<DoNotCopyAttribute>().Any())
					return false;
			}
			return true;
		}

		/// <summary>
		/// Checks for "do not copy" attribute, even on interface properties.
		/// Returns true if there is no such attribute, or the attribute is present but the safe-copy flag is set.
		/// Safe copying means copying a reference but not deep copying.
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool CanCopySafely(this PropertyInfo p)
		{
			if (p.GetCustomAttributes(true).OfType<DoNotCopyAttribute>().Union(p.PropertyType.GetCustomAttributes(true).OfType<DoNotCopyAttribute>()).Any(a => !a.AllowSafeCopy))
				return false;
			foreach (var i in p.DeclaringType.GetInterfaces())
			{
				var ip = i.GetProperty(p.Name);
				if (ip != null && ip.GetCustomAttributes(true).OfType<DoNotCopyAttribute>().Any(a => !a.AllowSafeCopy))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Copies an object.
		/// </summary>
		/// <typeparam name="T">The type of object to copy.</typeparam>
		/// <param name="obj">The object to copy.</param>
		/// <returns>The copy.</returns>
		public static T Copy<T>(this T obj)
		{
			if (obj == null)
				return default(T);
			var dest = obj.GetType().Instantiate();
			var copier = new OnlySafePropertiesCopier(obj, true, IDCopyBehavior.PreserveSource, IDCopyBehavior.PreserveSource);
			copier.Copy(obj, dest);
			return (T)dest;
		}

		/// <summary>
		/// Copies an object and assigns the copy a new ID.
		/// Subordinate objects are assigned new IDs too.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static T CopyAndAssignNewID<T>(this T obj)
		{
			if (obj == null)
				return default(T);
			var dest = obj.GetType().Instantiate();
			var copier = new OnlySafePropertiesCopier(obj, true, IDCopyBehavior.Regenerate, IDCopyBehavior.Regenerate);
			copier.Copy(obj, dest);
			return (T)dest;
		}

		/// <summary>
		/// Copies an object's data to another object.
		/// </summary>
		/// <typeparam name="T">The type of object to copy.</typeparam>
		/// <param name="src">The object to copy.</param>
		/// <param name="dest">The object to copy the source object's data to.</param>
		public static void CopyTo(this object src, object dest, IDCopyBehavior rootBehavior = IDCopyBehavior.PreserveSource, IDCopyBehavior subordinateBehavior = IDCopyBehavior.PreserveSource)
		{
			if (src.GetType() != dest.GetType())
				throw new Exception("Can only copy objects onto objects of the same type.");
			var copier = new OnlySafePropertiesCopier(src, true, rootBehavior, subordinateBehavior);
			copier.Copy(src, dest);
		}

		/// <summary>
		/// Copies an object's data to another object. Skips the ID property.
		/// </summary>
		/// <typeparam name="T">The type of object to copy.</typeparam>
		/// <param name="src">The object to copy.</param>
		/// <param name="dest">The object to copy the source object's data to.</param>
		public static void CopyToExceptID(this IReferrable src, IReferrable dest, IDCopyBehavior subordinateBehavior)
		{
			src.CopyTo(dest, IDCopyBehavior.PreserveDestination, subordinateBehavior);
		}

		private class OnlySafePropertiesCopier
		{
			public OnlySafePropertiesCopier(object root, bool deep, IDCopyBehavior rootBehavior, IDCopyBehavior subordinateBehavior, IDictionary<object, object> known = null)
			{
				Root = root;
				DeepCopy = deep;
				RootBehavior = rootBehavior;
				SubordinateBehavior = subordinateBehavior;

				if (known != null)
				{
					foreach (var kvp in known)
						knownObjects.Add(kvp);
				}
			}

			public bool DeepCopy { get; private set; }
			public object Root { get; private set; }
			public IDCopyBehavior RootBehavior { get; private set; }
			public IDCopyBehavior SubordinateBehavior { get; private set; }
			private SafeDictionary<object, object> knownObjects = new SafeDictionary<object, object>();

			public void Copy(object source, object target)
			{
				if (!knownObjects.ContainsKey(source))
					knownObjects.Add(source, target);
				foreach (var sp in source.GetType().GetProperties(
					BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
					.Where(p => p.GetGetMethod(true) != null && p.GetIndexParameters().Count() == 0)
					.OrderBy(p => p.HasAttribute<SerializationPriorityAttribute>() ? p.GetCustomAttribute<SerializationPriorityAttribute>().Priority : int.MaxValue))
				{
					var tp = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.GetSetMethod(true) != null && p.GetIndexParameters().Count() == 0 && p.Name == sp.Name).SingleOrDefault();
					if (tp != null)
					{
						if (Match(sp, tp))
						{
							bool doit = true;
							bool regen = false;
							if ( source is Component && ((Component)source).Hitpoints == 0) { }
							if (source is IReferrable && sp.Name == "ID")
							{
								// do special things for IDs
								var behavior = source == Root ? RootBehavior : SubordinateBehavior;
								if (behavior == IDCopyBehavior.PreserveSource)
									doit = true;
								else if (behavior == IDCopyBehavior.PreserveDestination)
									doit = false;
								else if (behavior == IDCopyBehavior.Regenerate)
								{
									doit = false;
									regen = true;
								}
							}

							object sv = null;

							if (doit && CanCopyFully(sp) && DeepCopy)
							{
								sv = sp.GetValue(source, null);
								if (sv == null)
									sp.SetValue(target, null, null); // it's null, very simple
								else if (!knownObjects.ContainsKey(sv))
								{
									// copy object and use the copy
									var tv = CopyObject(source, sv);
									sp.SetValue(target, tv, null);
								}
								else
									sp.SetValue(target, knownObjects[sv], null); // known object, don't bother copying again
							}
							else if (doit && CanCopySafely(sp))
							{
								sv = sp.GetValue(source, null);
								if (knownObjects.ContainsKey(sv))
								{
									sp.SetValue(target, knownObjects[sv]); // use known copy
								}
								else
									sp.SetValue(target, sv, null); // use original object
							}

							if (regen)
							{
								// reassign ID
								var r = target as IReferrable;
								if (r.HasValidID())
									r.ReassignID();
							}
						}
					}
				}
				if (target is ICleanable)
					(target as ICleanable).Clean();
			}

			// TODO: determine if all this logic is necessary
			private bool Match(PropertyInfo source, PropertyInfo target)
				=>
				source.Name == target.Name &&
				source.DeclaringType!.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(p => PropertyMatches(p, target.Name)) &&
				target.DeclaringType!.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(p => PropertyMatches(p, target.Name));

			private object CopyObject(object parent, object sv)
			{
				if (sv == null)
					return null;
				if (knownObjects.ContainsKey(sv))
					return knownObjects[sv];
				var type = sv.GetType();

				if (sv.GetType().IsValueType || sv is string)
					return sv;
				else if (sv.GetType().IsArray)
				{
					// do sub object mapping
					var sa = (Array)sv;
					Array ta = (Array)sa.Clone();
					Array.Clear(ta, 0, ta.Length); // no references to original objects!
					for (var i = 0; i < ta.Length; i++)
					{
						var sitem = sa.Cast<object>().ElementAt(i);
						if (sitem != null)
						{
							var titem = CopyObject(sv, sitem);
							if (ta.Rank == 1)
								ta.SetValue(titem, i);
							else if (ta.Rank == 2)
							{
								var width = ta.GetLength(0);
								ta.SetValue(titem, i / width, i % width);
							}
							else
								throw new InvalidOperationException("Arrays with more than 2 dimensions are not supported.");
						}
					}
					return ta;
				}
				else if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					var sc = (IEnumerable)sv;
					var tc = sv.GetType().Instantiate();
					if (type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1).Any())
					{
						// collection
						var adder = type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 1).Single();
						foreach (var si in sc)
						{
							// copy object and add to collection
							var ti = CopyObject(sv, si);
							adder.Invoke(tc, new object[] { ti });
						}
					}
					else if (type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 2).Any())
					{
						// dictionary
						var adder = type.GetMethods().Where(m => m.Name == "Add" && m.GetParameters().Length == 2).Single();
						foreach (var skvp in sc)
						{
							// copy key-value pair and add to collection
							var sk = skvp.GetPropertyValue("Key");
							var skv = skvp.GetPropertyValue("Value");
							var tk = CopyObject(sv, sk);
							var tkv = CopyObject(sv, skv);
							adder.Invoke(tc, new object[] { tk, tkv });
						}
					}
					else
					{
						throw new ArgumentException("Unknown enumerable type " + type + "; must be string/array/collection/dictionary.");
					}
					return tc;
				}
				else
				{
					// do sub object mapping
					var tv = sv.GetType().Instantiate();
					if (!knownObjects.ContainsKey(sv))
						knownObjects.Add(sv, tv);
					// XXX: what does the Map method do in ValueInjecter and how can I replace it?
					Map(sv, tv);
					//knownObjects.Remove(parent);
					return tv;
				}
			}

			private bool PropertyMatches(PropertyInfo p, string name)
			{
				return
				   p.Name == name // it's the right property
					   && p.GetSetMethod(true) != null // has a getter, whether public or private
					   && p.GetSetMethod(true) != null // has a setter, whether public or private
					   && p.GetIndexParameters().Length == 0; // lacks index parameters
			}
		}
	}
}
