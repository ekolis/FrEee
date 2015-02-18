using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Omu.ValueInjecter;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding;
using FrEee.Game.Objects.Vehicles;
using System.Text;
using System.IO;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Civilization;
using System.Reflection;
using System.Collections;
using FrEee.Game.Objects.Commands;
using System.Drawing.Imaging;
using FrEee.Game.Enumerations;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Dynamic;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Enumerations;
using System.Text.RegularExpressions;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using NewtMath.f16;
using FrEee.Game.Objects.Combat2; // TODO -remove this, just for testing

namespace FrEee.Utility.Extensions
{
	public static class CommonExtensions
	{
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
			dest.InjectFrom(new OnlySafePropertiesInjection(obj, true, IDCopyBehavior.PreserveSource, IDCopyBehavior.PreserveSource), obj);
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
			where T : IReferrable
		{
			if (obj == null)
				return default(T);
			var dest = obj.GetType().Instantiate();
			dest.InjectFrom(new OnlySafePropertiesInjection(obj, true, IDCopyBehavior.Regenerate, IDCopyBehavior.Regenerate), obj);
			return (T)dest;
		}

		/// <summary>
		/// Finds a property on a type, base type, or interface.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="propName"></param>
		/// <returns></returns>
		public static PropertyInfo FindProperty(this Type type, string propName)
		{
			var p = type.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (p != null)
				return p.DeclaringType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			var b = type.BaseType;
			if (b != null)
			{
				var bp = b.FindProperty(propName);
				if (bp != null)
					return bp.DeclaringType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			}
			foreach (var i in type.GetInterfaces())
			{
				var ip = i.FindProperty(propName);
				if (ip != null)
					return ip.DeclaringType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			}
			return null;
		}

		/// <summary>
		/// Checks for attributes in a class or its interfaces.
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="attributeType"></param>
		/// <returns></returns>
		public static bool HasAttribute(this MemberInfo mi, Type attributeType)
		{
			if (Attribute.GetCustomAttributes(mi, attributeType).Any())
				return true;
			foreach (var i in mi.DeclaringType.GetInterfaces())
			{
				if (i.GetMember(mi.Name, mi.MemberType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(mi2 => mi2.HasAttribute(attributeType)))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Checks for attributes in a class or its interfaces.
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="attributeType"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetAttributes<T>(this MemberInfo mi) where T : Attribute
		{
			var atts = Attribute.GetCustomAttributes(mi, typeof(T));
			foreach (var att in atts)
				yield return (T)att;
			foreach (var i in mi.DeclaringType.GetInterfaces())
			{
				var mis = i.GetMember(mi.Name, mi.MemberType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (var mi2 in mis)
				{
					foreach (var att2 in Attribute.GetCustomAttributes(mi2, typeof(T)))
						yield return (T)att2;
				}
			}
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
			dest.InjectFrom(new OnlySafePropertiesInjection(src, true, rootBehavior, subordinateBehavior), src);
		}

		private class OnlySafePropertiesInjection : ConventionInjection
		{
			public OnlySafePropertiesInjection(object root, bool deep, IDCopyBehavior rootBehavior, IDCopyBehavior subordinateBehavior, IDictionary<object, object> known = null)
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

			public object Root { get; private set; }

			public bool DeepCopy { get; private set; }

			public IDCopyBehavior RootBehavior { get; private set; }

			public IDCopyBehavior SubordinateBehavior { get; private set; }

			private SafeDictionary<object, object> knownObjects = new SafeDictionary<object, object>();

			protected override bool Match(ConventionInfo c)
			{
				return
					c.SourceProp.Name == c.TargetProp.Name &&
					c.Source.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(p => PropertyMatches(p, c.TargetProp.Name)) &&
					c.Target.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(p => PropertyMatches(p, c.TargetProp.Name));
			}

			private bool PropertyMatches(PropertyInfo p, string name)
			{
				return
				   p.Name == name // it's the right property
					   && p.GetSetMethod(true) != null // has a getter, whether public or private
					   && p.GetSetMethod(true) != null // has a setter, whether public or private
					   && p.GetIndexParameters().Length == 0; // lacks index parameters
			}

			protected override void Inject(object source, object target)
			{
				if (source is ConstructionQueue && (source as ConstructionQueue).Name.StartsWith("Munjumb I"))
				{

				}
				foreach (var sp in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.GetGetMethod(true) != null && p.GetIndexParameters().Count() == 0))
				{
					var tp = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.GetSetMethod(true) != null && p.GetIndexParameters().Count() == 0 && p.Name == sp.Name).SingleOrDefault();
					if (tp != null)
					{
						var c = new ConventionInfo
						{
							Source = new ConventionInfo.TypeInfo { Type = sp.DeclaringType },
							SourceProp = new ConventionInfo.PropInfo { Name = sp.Name },
							Target = new ConventionInfo.TypeInfo { Type = tp.DeclaringType },
							TargetProp = new ConventionInfo.PropInfo { Name = tp.Name },
						};
						if (Match(c))
						{
							bool doit = true;
							bool regen = false;
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

							if (doit && CanCopyFully(sp) && DeepCopy)
							{
								var sv = sp.GetValue(source, null);
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
								var sv = sp.GetValue(source, null);
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
				if (!knownObjects.ContainsKey(source))
					knownObjects.Add(source, target);
			}

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
					knownObjects.Add(sv, tv);
					Map(sv, tv);
					//knownObjects.Remove(parent);
					return tv;
				}
			}
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

		/// <summary>
		/// Reassigns the ID of an object, overwriting any existing ID.
		/// </summary>
		/// <param name="r"></param>
		public static void ReassignID(this IReferrable r)
		{
			r.ID = 0;
			Galaxy.Current.AssignID(r);
		}

		/// <summary>
		/// Generates new IDs for this object (unless skipRoot is true) and all subordinate objects.
		/// TODO - take into account DoNotAssignIDAttribute
		/// </summary>
		/// <param name="obj"></param>
		public static void ReassignAllIDs(this IReferrable obj, bool skipRoot = false)
		{
			var parser = new ObjectGraphParser();
			var canCopy = new System.Collections.Generic.Stack<bool>(); // stack of bools indicating which objects in the current hierarchy path we can copy
			canCopy.Push(true);
			parser.Property += (pname, o, val) =>
			{
				var prop = o.GetType().FindProperty(pname);
				var shouldRecurse = !prop.CanCopyFully();
				if (shouldRecurse)
					canCopy.Push(shouldRecurse);
				return shouldRecurse;
			};
			parser.Item += (o) =>
			{
				// can always serialize collection items
				canCopy.Push(true);
			};
			parser.StartObject += (o) =>
			{
				var doit = canCopy.All(b => b) && (!skipRoot || o != obj);
				if (doit && o is IReferrable)
				{
					var r = (IReferrable)o;
					r.ReassignID();
				}
			};
			parser.EndObject += (o) =>
			{
				canCopy.Pop();
			};
			parser.Null += (o) =>
			{
				canCopy.Pop();
			};
			parser.KnownObject += (o) =>
			{
				canCopy.Pop();
			};
			parser.Parse(obj);
		}

		/*// based on http://cangencer.wordpress.com/2011/06/08/auto-ignore-non-existing-properties-with-automapper/
		private static IMappingExpression<T, T> IgnoreReadOnlyAndNonSerializableProperties<T>(this IMappingExpression<T, T> expression)
		{
			var type = typeof(T);
			var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.IsAssignableFrom(type)
				&& x.DestinationType.IsAssignableFrom(type));
			foreach (var property in existingMaps.GetPropertyMaps().Where(pm =>
				{
					var prop = (PropertyInfo)pm.DestinationProperty.MemberInfo;
					var realprop = prop.DeclaringType.GetProperty(prop.Name);
					return realprop.GetSetMethod(true) == null || realprop.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any();
				}))
				expression.ForMember(property.DestinationProperty.Name, opt => opt.Ignore());
			return expression;
		}

		private static IMappingExpression<T, T> IgnoreIDProperty<T>(this IMappingExpression<T, T> expression)
			where T : IReferrable
		{
			var type = typeof(T);
			var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(type)
				&& x.DestinationType.Equals(type));
			foreach (var property in existingMaps.GetPropertyMaps().Where(pm => ((PropertyInfo)pm.DestinationProperty.MemberInfo).Name == "ID"))
				expression.ForMember(property.DestinationProperty.Name, opt => opt.Ignore());
			return expression;
		}*/

		private static List<Type> mappedTypes = new List<Type>();

		/// <summary>
		/// Finds the largest space object out of a group of space objects.
		/// Stars are the largest space objects, followed by planets, asteroid fields, storms, fleets, ships/bases, and finally unit groups.
		/// Within a category, space objects are sorted by stellar size or tonnage as appropriate.
		/// </summary>
		/// <param name="objects">The group of space objects.</param>
		/// <returns>The largest space object.</returns>
		public static ISpaceObject Largest(this IEnumerable<ISpaceObject> objects)
		{
			if (objects.OfType<Star>().Any())
			{
				return objects.OfType<Star>().OrderByDescending(obj => obj.StellarSize).First();
			}
			if (objects.OfType<Planet>().Any())
			{
				return objects.OfType<Planet>().OrderByDescending(obj => obj.StellarSize).First();
			}
			if (objects.OfType<AsteroidField>().Any())
			{
				return objects.OfType<AsteroidField>().OrderByDescending(obj => obj.StellarSize).First();
			}
			if (objects.OfType<Storm>().Any())
			{
				return objects.OfType<Storm>().OrderByDescending(obj => obj.StellarSize).First();
			}
			if (objects.OfType<WarpPoint>().Any())
			{
				return objects.OfType<WarpPoint>().OrderByDescending(obj => obj.StellarSize).First();
			}
			if (objects.OfType<IMobileSpaceObject>().Any())
			{
				return objects.OfType<IMobileSpaceObject>().OrderByDescending(obj => obj.Size).First();
			}
			return null;
		}

		/// <summary>
		/// Determines if an object has a specified ability.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="abilityName"></param>
		/// <returns></returns>
		public static bool HasAbility(this IAbilityObject obj, string abilityName, bool includeShared = true)
		{
			IEnumerable<Ability> abils;
			if (includeShared && obj is IOwnableAbilityObject)
				abils = obj.Abilities().Union(obj.SharedAbilities());
			else
				abils = obj.Abilities();
			return abils.Any(abil => abil.Rule != null && abil.Rule.Matches(abilityName));
		}

		/// <summary>
		/// Determines if a common ability object has a specified ability for an empire.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="abilityName"></param>
		/// <returns></returns>
		public static bool HasAbility(this ICommonAbilityObject obj, string abilityName, Empire emp, bool includeShared = true)
		{
			IEnumerable<Ability> abils;
			if (includeShared)
				abils = obj.Abilities(emp).Union(obj.SharedAbilities(emp));
			else
				abils = obj.Abilities(emp);
			return abils.Any(abil => abil.Rule != null && abil.Rule.Matches(abilityName));
		}

		/// <summary>
		/// Stacks any abilities of the same type according to the current mod's stacking rules.
		/// Keeps the original abilities in a handy tree format under the stacked abilities
		/// so you can tell which abilities contributed to which stacked abilities.
		/// </summary>
		/// <param name="abilities"></param>
		/// <param name="stackTo">The object which should own the stacked abilities.</param>
		/// <returns></returns>
		public static ILookup<Ability, Ability> StackToTree(this IEnumerable<Ability> abilities, IAbilityObject stackTo)
		{
			var stacked = new List<Tuple<Ability, Ability>>();
			if (abilities.Any())
			{
				foreach (var rule in Mod.Current.AbilityRules)
				{
					var lookup = rule.GroupAndStack(abilities, stackTo);
					foreach (var group in lookup)
					{
						foreach (var abil in group)
							stacked.Add(Tuple.Create(group.Key, abil));
					}
				}
			}
			foreach (var abil in abilities.Where(a => !Mod.Current.AbilityRules.Any(r => r == a.Rule)))
				stacked.Add(Tuple.Create(abil, abil));
			return stacked.ToLookup(t => t.Item1, t => t.Item2);
		}

		public static IEnumerable<Ability> Stack(this IEnumerable<Ability> abilities, IAbilityObject stackTo)
		{
			return abilities.StackToTree(stackTo).Select(g => g.Key);
		}

		public static IEnumerable<Ability> StackAbilities(this IEnumerable<IAbilityObject> objs, IAbilityObject stackTo)
		{
			return objs.SelectMany(obj => obj.Abilities()).Stack(stackTo);
		}

		public static ILookup<Ability, Ability> StackAbilitiesToTree(this IEnumerable<IAbilityObject> objs, IAbilityObject stackTo)
		{
			return objs.SelectMany(obj => obj.Abilities()).StackToTree(stackTo);
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this long? value, bool bForBillions = false, int sigfigs = 4, string undefinedValue = "Undefined")
		{
			if (value == null)
				return undefinedValue;
			return value.Value.ToUnitString(bForBillions, sigfigs);
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this long value, bool bForBillions = false, int sigfigs = 4)
		{
			if (Math.Abs(value) >= 1e12 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e12));
				var decimals = sigfigs - 1 - log;
				return (value / 1e12).ToString("f" + decimals) + "T";
			}
			if (Math.Abs(value) >= 1e9 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e9));
				var decimals = sigfigs - 1 - log;
				return (value / 1e9).ToString("f" + decimals) + (bForBillions ? "B" : "G");
			}
			if (Math.Abs(value) >= 1e6 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e6));
				var decimals = sigfigs - 1 - log;
				return (value / 1e6).ToString("f" + decimals) + "M";
			}
			if (Math.Abs(value) >= 1e3 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e3));
				var decimals = sigfigs - 1 - log;
				return (value / 1e3).ToString("f" + decimals) + "k";
			}
			return value.ToString();
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this int? value, bool bForBillions = false, int sigfigs = 4)
		{
			return ((long?)value).ToUnitString(bForBillions, sigfigs);
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this int value, bool bForBillions = false, int sigfigs = 4)
		{
			return ((long)value).ToUnitString(bForBillions, sigfigs);
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this double value, bool bForBillions = false, int sigfigs = 4)
		{
			if (Math.Abs(value) >= 1e12 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e12));
				var decimals = sigfigs - 1 - log;
				return (value / 1e12).ToString("f" + decimals) + "T";
			}
			if (Math.Abs(value) >= 1e9 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e9));
				var decimals = sigfigs - 1 - log;
				return (value / 1e9).ToString("f" + decimals) + (bForBillions ? "B" : "G");
			}
			if (Math.Abs(value) >= 1e6 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e6));
				var decimals = sigfigs - 1 - log;
				return (value / 1e6).ToString("f" + decimals) + "M";
			}
			if (Math.Abs(value) >= 1e3 * Math.Pow(10, sigfigs - 3))
			{
				var log = (int)Math.Floor(Math.Log10(Math.Abs(value) / 1e3));
				var decimals = sigfigs - 1 - log;
				return (value / 1e3).ToString("f" + decimals) + "k";
			}
			return value.ToString();
		}

		/// <summary>
		/// Adds SI prefixes to a value and rounds it off.
		/// e.g. 25000 becomes 25.00k
		/// </summary>
		/// <param name="value"></param>
		public static string ToUnitString(this double? value, bool bForBillions = false, int sigfigs = 4, string undefinedValue = "Undefined")
		{
			if (value == null)
				return undefinedValue;
			return value.Value.ToUnitString(bForBillions, sigfigs);
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this long? value, string undefinedValue = "Undefined")
		{
			if (value == null)
				return undefinedValue;
			return value.Value.Kilotons();
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this long value)
		{
			if (value < 10000)
				return value + "kT";
			return (value * 1000).ToUnitString() + "T";
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this int? value, string nullText = "Undefined")
		{
			if (value == null)
				return nullText;
			return ((long?)value).Kilotons();
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this int value)
		{
			return ((long)value).Kilotons();
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this double? value, string undefinedValue = "Undefined")
		{
			if (value == null)
				return undefinedValue;
			return value.Value.Kilotons();
		}

		/// <summary>
		/// Displays a number in kT, MT, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Kilotons(this double value)
		{
			return (value * 1000).ToUnitString() + "T";
		}

		/// <summary>
		/// Converts a turn number to a stardate.
		/// </summary>
		/// <param name="turnNumber"></param>
		/// <returns></returns>
		public static string ToStardate(this int turnNumber)
		{
			// TODO - moddable starting stardate?
			return ((turnNumber + 23999) / 10.0).ToString("0.0");
		}

		/// <summary>
		/// Picks a random element from a sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickRandom<T>(this IEnumerable<T> src)
		{
			if (!src.Any())
				return default(T);
			return src.ElementAt(RandomHelper.Next(src.Count()));
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, int> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			int num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);

			int sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, long> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			long num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);
			long sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, double> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			double num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);
			double sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		/// <summary>
		/// Orders elements randomly.
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src, PRNG prng = null)
		{
			return src.OrderBy(t => RandomHelper.Next(int.MaxValue, prng));
		}

		public static T MinOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Min();
		}

		public static TProp MinOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MinOrDefault();
		}

		public static T MaxOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Max();
		}

		public static TProp MaxOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MaxOrDefault();
		}

		public static T FindByName<T>(this IEnumerable<T> stuff, string name) where T : INamed
		{
			return stuff.SingleOrDefault(item => item.Name == name);
		}

		public static IEnumerable<T> FindAllByName<T>(this IEnumerable<T> stuff, string name) where T : INamed
		{
			return stuff.Where(item => item.Name == name);
		}

		/// <summary>
		/// Gets the points on the border of a rectangle.
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static IEnumerable<Point> GetBorderPoints(this Rectangle r)
		{
			for (var x = r.Left; x <= r.Right; x++)
			{
				if (x == r.Left || x == r.Right)
				{
					// get left and right sides
					for (var y = r.Top; y <= r.Bottom; y++)
						yield return new Point(x, y);
				}
				else
				{
					// just get top and bottom
					yield return new Point(x, r.Top);
					if (r.Top != r.Bottom)
						yield return new Point(x, r.Bottom);
				}
			}
		}

		/// <summary>
		/// Gets points in the interior of a rectangle.
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static IEnumerable<Point> GetInteriorPoints(this Rectangle r)
		{
			for (var x = r.Left + 1; x < r.Right; x++)
			{
				for (var y = r.Top + 1; y < r.Bottom; y++)
					yield return new Point(x, y);
			}
		}

		/// <summary>
		/// Gets points both on the border and in the interior of a rectangle.
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static IEnumerable<Point> GetAllPoints(this Rectangle r)
		{
			for (var x = r.Left; x <= r.Right; x++)
			{
				for (var y = r.Top; y <= r.Bottom; y++)
					yield return new Point(x, y);
			}
		}

		/// <summary>
		/// Computes the Manhattan (4-way grid) distance between two points.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int ManhattanDistance(this Point p, Point target)
		{
			return Math.Abs(target.X - p.X) + Math.Abs(target.Y - p.Y);
		}

		/// <summary>
		/// Computes the distance between two points along a grid with eight-way movement.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int EightWayDistance(this Point p, Point target)
		{
			var dx = Math.Abs(target.X - p.X);
			var dy = Math.Abs(target.Y - p.Y);
			return Math.Max(dx, dy);
		}

		/// <summary>
		/// Computes the angle from one point to the other.
		/// Zero degrees is east, and positive is counterclockwise.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static double AngleTo(this Point p, Point target)
		{
			return Math.Atan2(target.Y - p.Y, target.X - p.X) * 180d / Math.PI;
		}

		/// <summary>
		/// Computes the angle from one point to the other.
		/// Zero degrees is north, and positive is clockwise.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static double AngleTo(this PointF p, PointF target)
		{
			return Math.Atan2(target.Y - p.Y, target.X - p.X) * 180d / Math.PI;
		}

		/// <summary>
		/// Removes points within a certain Manhattan distance of a certain point.
		/// </summary>
		/// <param name="points">The points to start with.</param>
		/// <param name="center">The point to block out.</param>
		/// <param name="distance">The distance to block out from the center.</param>
		/// <returns>The points that are left.</returns>
		public static IEnumerable<Point> BlockOut(this IEnumerable<Point> points, Point center, int distance)
		{
			foreach (var p in points)
			{
				if (center.ManhattanDistance(p) > distance)
					yield return p;
			}
		}

		/// <summary>
		/// Flattens groupings into a single sequence.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="lookup"></param>
		/// <returns></returns>
		public static IEnumerable<TValue> Flatten<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> lookup)
		{
			return lookup.SelectMany(g => g);
		}

		/// <summary>
		/// "Squashes" a nested lookup into a collection of tuples.
		/// </summary>
		/// <typeparam name="TKey1"></typeparam>
		/// <typeparam name="TKey2"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="lookup"></param>
		/// <returns></returns>
		public static IEnumerable<Tuple<TKey1, TKey2, TValue>> Squash<TKey1, TKey2, TValue>(this ILookup<TKey1, ILookup<TKey2, TValue>> lookup)
		{
			foreach (var group1 in lookup)
			{
				foreach (var sublookup in group1)
				{
					foreach (var group2 in sublookup)
					{
						foreach (var item in group2)
							yield return Tuple.Create(group1.Key, group2.Key, item);
					}
				}
			}
		}

		/// <summary>
		/// "Squashes" a nested collection into a collection of tuples.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <returns></returns>
		public static IEnumerable<Tuple<TParent, TChild>> Squash<TParent, TChild>(this IEnumerable<TParent> parents, Func<TParent, IEnumerable<TChild>> childSelector)
		{
			foreach (var parent in parents)
			{
				foreach (var child in childSelector(parent))
					yield return Tuple.Create(parent, child);
			}
		}

		/// <summary>
		/// Gets a capital letter from the English alphabet.
		/// </summary>
		/// <param name="i">1 to 26</param>
		/// <returns>A to Z</returns>
		/// <exception cref="ArgumentException">if i is not from 1 to 26</exception>
		public static char ToLetter(this int i)
		{
			if (i < 1 || i > 26)
				throw new ArgumentException("Only 26 letters in the alphabet, can't get letter #" + i + ".", "i");
			return (char)('A' + i - 1);
		}

		/// <summary>
		/// Gets a roman numeral.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static string ToRomanNumeral(this int i)
		{
			// do we already know this?
			if (!RomanNumeralCache.ContainsKey(i))
			{
				// get silly negative numbers and zeroes out of the way
				if (i < 0)
					RomanNumeralCache.Add(i, "-" + ToRomanNumeral(-i));
				else if (i == 0)
					RomanNumeralCache.Add(i, "");
				else
				{
					// scan the roman numeral parts list recursively
					foreach (var part in RomanNumeralParts.OrderByDescending(part => part.Item1))
					{
						if (i >= part.Item1)
						{
							RomanNumeralCache.Add(i, part.Item2 + (i - part.Item1).ToRomanNumeral());
							break;
						}
					}
				}
			}

			return RomanNumeralCache[i];
		}

		private static Tuple<int, string>[] RomanNumeralParts = new Tuple<int, string>[]
		{
			Tuple.Create(1000, "M"),
			Tuple.Create(900, "CM"),
			Tuple.Create(500, "D"),
			Tuple.Create(400, "CD"),
			Tuple.Create(100, "C"),
			Tuple.Create(90, "XC"),
			Tuple.Create(50, "L"),
			Tuple.Create(40, "XL"),
			Tuple.Create(10, "X"),
			Tuple.Create(9, "IX"),
			Tuple.Create(5, "V"),
			Tuple.Create(4, "IV"),
			Tuple.Create(1, "I"),
		};

		private static IDictionary<int, string> RomanNumeralCache = new Dictionary<int, string>();

		/// <summary>
		/// Determines if a string can be parsed as an integer.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsInt(this string s)
		{
			int i;
			return int.TryParse(s, out i);
		}

		/// <summary>
		/// Determines if a string can be parsed as a double.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
		/// <returns></returns>
		public static bool IsDouble(this string s, int cultureCode = 127)
		{
			double d;
			return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo(cultureCode), out d);
		}

		/// <summary>
		/// Determines if a string can be parsed as an boolean.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsBool(this string s)
		{
			bool b;
			return bool.TryParse(s, out b);
		}

		/// <summary>
		/// Parses a string as an integer. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static int ToInt(this string s)
		{
			int i;
			int.TryParse(s, out i);
			return i;
		}

		/// <summary>
		/// Parses a string as a double. Returns 0 if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cultureCode">The LCID of the culture used to parse. Defaults to 127, which represents the invariant culture.</param>
		/// <returns></returns>
		public static double ToDouble(this string s, int cultureCode = 127)
		{
			double d;
			double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo(cultureCode), out d);
			return d;
		}

		/// <summary>
		/// Parses a string as a boolean. Returns false if it could not be parsed.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool ToBool(this string s)
		{
			bool b;
			bool.TryParse(s, out b);
			return b;
		}

		/// <summary>
		/// Gets an ability value.
		/// If the stacking rule in the mod is DoNotStack, an arbitrary matching ability will be chosen.
		/// If there are no values, null will be returned.
		/// </summary>
		/// <param name="name">The name of the ability.</param>
		/// <param name="obj">The object from which to get the value.</param>
		/// <param name="index">The ability value index (usually 1 or 2).</param>
		/// <param name="filter">A filter for the abilities. For instance, you might want to filter by the ability grouping rule's value.</param>
		/// <returns>The ability value.</returns>
		public static string GetAbilityValue(this IAbilityObject obj, string name, int index = 1, bool includeShared = true, Func<Ability, bool> filter = null)
		{
			var abils = obj.Abilities();
			if (includeShared)
				abils = abils.Union(obj.SharedAbilities());
			abils = abils.Where(a => a.Rule != null && a.Rule.Matches(name) && a.Rule.CanTarget(obj.AbilityTarget) && (filter == null || filter(a)));
			abils = abils.Stack(obj);
			if (!abils.Any())
				return null;
			return abils.First().Values[index - 1];
		}

		public static string GetAbilityValue(this IEnumerable<IAbilityObject> objs, string name, IAbilityObject stackTo, int index = 1, bool includeShared = true, Func<Ability, bool> filter = null)
		{
			var tuples = objs.Squash(o => o.Abilities()).ToArray();
			if (includeShared)
				tuples = tuples.Union(objs.Squash(o => o.SharedAbilities())).ToArray();
			var abils = tuples.GroupBy(t => new { Rule = t.Item2.Rule, Object = t.Item1 }).Where(g => g.Key.Rule.Matches(name) && g.Key.Rule.CanTarget(g.Key.Object.AbilityTarget)).SelectMany(x => x).Select(t => t.Item2).Where(a => filter == null || filter(a)).Stack(stackTo);
			if (!abils.Any())
				return null;
			return abils.First().Values[index - 1];
		}

		/// <summary>
		/// Aggregates abilities for an empire's space objects.
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="name"></param>
		/// <param name="index"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static string GetAbilityValue(this ICommonAbilityObject obj, Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
		{
			if (filter == null && Galaxy.Current.IsAbilityCacheEnabled)
			{
				// use the cache
				var cached = Galaxy.Current.CommonAbilityCache[Tuple.Create(obj, emp)];
				if (cached != null)
				{
					if (cached.Any())
						return cached.First().Values[index - 1];
					else
						return null;
				}
			}

			IEnumerable<Ability> abils;
			var subabils = obj.GetContainedAbilityObjects(emp).SelectMany(o => o.UnstackedAbilities().Where(a => a.Rule.Name == name));
			if (obj is IAbilityObject)
				abils = ((IAbilityObject)obj).Abilities().Where(a => a.Rule != null && a.Rule.Name == name).Concat(subabils).Stack(obj);
			else
				abils = subabils;
			abils = abils.Where(a => a.Rule != null && a.Rule.Matches(name) && a.Rule.CanTarget(obj.AbilityTarget) && (filter == null || filter(a)));
			string result;
			if (!abils.Any())
				result = null;
			else
				result = abils.First().Values[index - 1];

			// cache abilities if we can
			if (filter == null && Galaxy.Current.IsAbilityCacheEnabled)
				Galaxy.Current.CommonAbilityCache[Tuple.Create(obj, emp)] = abils.ToArray();

			return result;
		}

		public static IEnumerable<Ability> Abilities(this ICommonAbilityObject obj, Empire emp, Func<IAbilityObject, bool> sourceFilter = null)
		{
			if (sourceFilter == null)
				return obj.GetContainedAbilityObjects(emp).SelectMany(o => o.Abilities()).Where(a => a.Rule.CanTarget(obj.AbilityTarget)).ToArray();
			else
				return obj.GetContainedAbilityObjects(emp).Where(o => sourceFilter(o)).SelectMany(o => o.Abilities()).Where(a => a.Rule.CanTarget(obj.AbilityTarget));
		}

		/// <summary>
		/// Gets abilities that have been shared to an object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> SharedAbilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			// Unowned objects cannot have abilities shared to them.
			var ownable = obj as IOwnableAbilityObject;
			if (ownable == null || ownable.Owner == null)
				yield break;

			// update cache if necessary
			foreach (var clause in ownable.Owner.ReceivedTreatyClauses.Flatten().OfType<ShareAbilityClause>())
			{
				var tuple = Tuple.Create(ownable, clause.Owner);
				if (Empire.Current == null || !Galaxy.Current.SharedAbilityCache.ContainsKey(tuple))
					Galaxy.Current.SharedAbilityCache[tuple] = FindSharedAbilities(ownable, clause).ToArray();
			}

			// get cached abilities
			foreach (var keyTuple in Galaxy.Current.SharedAbilityCache.Keys.Where(k => k.Item1 == ownable && (sourceFilter == null || sourceFilter(k.Item2))))
			{
				foreach (var abil in Galaxy.Current.SharedAbilityCache[keyTuple])
					yield return abil;
			}
		}

		private static IEnumerable<Ability> FindSharedAbilities(this IOwnableAbilityObject obj, ShareAbilityClause clause)
		{
			var rule = clause.AbilityRule;
			if (rule.CanTarget(obj.AbilityTarget))
			{
				if (rule.CanTarget(AbilityTargets.Sector) && obj is ILocated)
				{
					var sector = ((ILocated)obj).Sector;
					foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
					{
						foreach (var abil in sector.Abilities(emp))
						{
							if (rule == abil.Rule)
								yield return abil;
						}
					}
				}
				else if (rule.CanTarget(AbilityTargets.StarSystem) && obj is ILocated)
				{
					var sys = ((ILocated)obj).StarSystem;
					foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
					{
						foreach (var abil in sys.Abilities(emp))
						{
							if (rule == abil.Rule)
								yield return abil;
						}
					}
				}
				else if (rule.CanTarget(AbilityTargets.Galaxy))
				{
					foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
					{
						foreach (var abil in Galaxy.Current.Abilities(emp))
						{
							if (rule == abil.Rule)
								yield return abil;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets abilities that have been shared to an object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> SharedAbilities(this ICommonAbilityObject obj, Empire empire, Func<IAbilityObject, bool> sourceFilter = null)
		{
			foreach (var clause in empire.ReceivedTreatyClauses.Flatten().OfType<ShareAbilityClause>())
			{
				var rule = clause.AbilityRule;
				if (clause.AbilityRule.CanTarget(obj.AbilityTarget))
				{
					if (rule.CanTarget(AbilityTargets.Sector) && obj is ILocated)
					{
						var sector = ((ILocated)obj).Sector;
						foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
						{
							foreach (var abil in sector.Abilities(emp, sourceFilter))
							{
								if (clause.AbilityRule == abil.Rule)
									yield return abil;
							}
						}
					}
					else if (rule.CanTarget(AbilityTargets.StarSystem) && (obj is StarSystem || obj is ILocated))
					{
						var sys = ((ILocated)obj).StarSystem;
						foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
						{
							foreach (var abil in sys.Abilities(emp, sourceFilter))
							{
								if (clause.AbilityRule == abil.Rule)
									yield return abil;
							}
						}
					}
					else if (rule.CanTarget(AbilityTargets.Galaxy))
					{
						foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
						{
							foreach (var abil in Galaxy.Current.Abilities(emp, sourceFilter))
							{
								if (clause.AbilityRule == abil.Rule)
									yield return abil;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Copies an image and draws planet population bars on it.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="planet">The planet whose population bars should be drawn.</param>
		/// <returns>The copied image with the population bars.</returns>
		public static Image DrawPopulationBars(this Image image, Planet planet)
		{
			var img2 = (Image)image.Clone();
			planet.DrawPopulationBars(img2);
			return img2;
		}

		/// <summary>
		/// Resizes an image. The image should be square.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Image Resize(this Image image, int size)
		{
			if (image == null)
				return null;
			var result = new Bitmap(size, size, PixelFormat.Format32bppArgb);
			var g = Graphics.FromImage(result);
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			g.DrawImage(image, 0, 0, size, size);
			return result;
		}

		/// <summary>
		/// Adds up a bunch of resources.
		/// </summary>
		/// <param name="resources"></param>
		/// <returns></returns>
		public static ResourceQuantity Sum(this IEnumerable<ResourceQuantity> resources)
		{
			if (!resources.Any())
				return new ResourceQuantity();
			return resources.Aggregate((r1, r2) => r1 + r2);
		}

		/// <summary>
		/// Adds up a bunch of resources.
		/// </summary>
		/// <param name="resources"></param>
		/// <returns></returns>
		public static ResourceQuantity Sum<T>(this IEnumerable<T> stuff, Func<T, ResourceQuantity> selector)
		{
			return stuff.Select(item => selector(item)).Sum();
		}

		/// <summary>
		/// Adds up a bunch of cargo.
		/// </summary>
		/// <param name="resources"></param>
		/// <returns></returns>
		public static Cargo Sum(this IEnumerable<Cargo> cargo)
		{
			if (!cargo.Any())
				return new Cargo();
			return cargo.Aggregate((r1, r2) => r1 + r2);
		}

		/// <summary>
		/// Adds up a bunch of cargo.
		/// </summary>
		/// <param name="resources"></param>
		/// <returns></returns>
		public static Cargo Sum<T>(this IEnumerable<T> stuff, Func<T, Cargo> selector)
		{
			return stuff.Select(item => selector(item)).Sum();
		}

		public static IEnumerable<T> OnlyLatest<T>(this IEnumerable<T> stuff, Func<T, string> familySelector)
			where T : class
		{
			string family = null;
			T latest = null;
			foreach (var t in stuff)
			{
				if (family == null)
				{
					// first item
					latest = t;
					family = familySelector(t);
				}
				else if (family == familySelector(t))
				{
					// same family
					latest = t;
				}
				else
				{
					// different family
					yield return latest;
					latest = t;
					family = familySelector(t);
				}
			}
			if (stuff.Any())
				yield return stuff.Last();
		}

		/// <summary>
		/// Finds the sector containing a space object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Sector FindSector(this ISpaceObject sobj)
		{
			var sys = sobj.FindStarSystem();
			if (sys == null)
				return null;
			// TODO - this might be kind of slow; might want a reverse memory lookup
			return new Sector(sys, sys.SpaceObjectLocations.Single(l => l.Item == sobj || Galaxy.Current.Empires.Except(null).Any(e => e.Memory[l.Item.ID] == sobj)).Location);
		}

		/// <summary>
		/// Finds the star system containing a space object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static StarSystem FindStarSystem(this ISpaceObject sobj)
		{
			var loc = Galaxy.Current.StarSystemLocations.SingleOrDefault(l => l.Item.Contains(sobj));
			if (loc == null)
			{
				// search memories too
				// TODO - this might be kind of slow; might want a reverse memory lookup
				loc = Galaxy.Current.StarSystemLocations.SingleOrDefault(l => l.Item.FindSpaceObjects<ISpaceObject>().Any(s => Galaxy.Current.Empires.Except(null).Any(e => e.Memory[s.ID] == sobj)));
			}
			if (loc == null)
				return null;
			return loc.Item;
		}

		/// <summary>
		/// Finds the coordinates of a space object within its star system.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Point FindCoordinates(this ISpaceObject sobj)
		{
			return sobj.FindStarSystem().FindCoordinates(sobj);
		}

		/// <summary>
		/// Reads characters until the specified character is found or end of stream.
		/// Returns all characters read except the specified character.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string ReadTo(this TextReader r, char c, StringBuilder log)
		{
			var sb = new StringBuilder();
			int data = 0;
			bool escaping = false;
			do
			{
				data = r.Read();
				if (data > 0 && data != (int)c && data != (int)'\\' && !escaping)
				{
					sb.Append((char)data);
					if (log != null)
						log.Append((char)data);
				}
				else if (escaping)
				{
					sb.Append((char)data);
					if (log != null)
						log.Append((char)data);
					escaping = false;
				}
				else if (data == (int)'\\')
					escaping = true;

			} while (data > 0 && data != (int)c);
			if (data == c && log != null)
				log.Append(c);
			return sb.ToString();
		}

		public static IEnumerable<T> Except<T>(this IEnumerable<T> src, T badguy)
		{
			return src.Except(new T[] { badguy });
		}

		public static Reference<T> Reference<T>(this T t)
		{
			return new Reference<T>(t);
		}

		public static PictorialLogMessage<T> CreateLogMessage<T>(this T context, string text, int? turnNumber = null)
			where T : IPictorial
		{
			if (turnNumber == null)
				return new PictorialLogMessage<T>(text, context);
			else
				return new PictorialLogMessage<T>(text, turnNumber.Value, context);
		}

		/// <summary>
		/// Returns the elements of a sequence that have the maximum of some selected value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TCompared"></typeparam>
		/// <param name="src"></param>
		/// <param name="getter"></param>
		/// <returns></returns>
		public static IEnumerable<T> WithMax<T, TCompared>(this IEnumerable<T> src, Func<T, TCompared> selector)
		{
			var list = src.Select(item => new { Item = item, Value = selector(item) });
			if (!list.Any())
				return Enumerable.Empty<T>();
			var max = list.Max(x => x.Value);
			return list.Where(x => x.Value.SafeEquals(max)).Select(x => x.Item);
		}

		/// <summary>
		/// Returns the elements of a sequence that have the minimum of some selected value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TCompared"></typeparam>
		/// <param name="src"></param>
		/// <param name="getter"></param>
		/// <returns></returns>
		public static IEnumerable<T> WithMin<T, TCompared>(this IEnumerable<T> src, Func<T, TCompared> selector)
		{
			var list = src.Select(item => new { Item = item, Value = selector(item) });
			if (!list.Any())
				return Enumerable.Empty<T>();
			var min = list.Min(x => x.Value);
			return list.Where(x => x.Value.SafeEquals(min)).Select(x => x.Item);
		}

		/// <summary>
		/// Finds the majority value of some attribute. If there is no clear majority, the first tied value is selected arbitrarily.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TCompared"></typeparam>
		/// <param name="src"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		public static TCompared Majority<T, TCompared>(this IEnumerable<T> src, Func<T, TCompared> selector)
		{
			var groups = src.GroupBy(x => selector(x));
			groups = groups.WithMax(g => g.Count());
			if (!groups.Any())
				return default(TCompared);
			return groups.First().Key;
		}

		/// <summary>
		/// Is this type safe to pass from the client to the server?
		/// Primitives, strings, points and colors are client safe.
		/// So are types implementing IPromotable.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static bool IsClientSafe(this Type t)
		{
			return
				t.IsPrimitive ||
				t == typeof(string) ||
				t == typeof(Point) ||
				t == typeof(Color) ||
				typeof(IEnumerable<object>).IsAssignableFrom(t) ||
				typeof(IEnumerable).IsAssignableFrom(t) ||
				typeof(IPromotable).IsAssignableFrom(t) ||
				t.BaseType != null && t.BaseType.IsClientSafe() ||
				t.GetInterfaces().Any(i => i.IsClientSafe());
		}

		public static int IndexOf<T>(this IEnumerable<T> haystack, T needle)
		{
			int i = 0;
			foreach (var item in haystack)
			{
				if (item.Equals(needle))
					return i;
				i++;
			}
			return -1;
		}

		/// <summary>
		/// Checks a command to make sure it doesn't contain any objects that are not client safe.
		/// </summary>
		/// <param name="cmd"></param>
		public static void CheckForClientSafety(this ICommand cmd)
		{
			var vals = cmd.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(f => !f.GetCustomAttributes(true).OfType<DoNotSerializeAttribute>().Any() && f.GetGetMethod(true) != null && f.GetSetMethod(true) != null).Select(prop => new { Name = prop.Name, Value = prop.GetValue(cmd, new object[0]) });
			var badVals = vals.Where(val => val.Value != null && !val.Value.GetType().IsClientSafe());
			if (badVals.Any())
				throw new Exception(cmd + " contained a non-client-safe type " + badVals.First().Value.GetType() + " in property " + badVals.First().Name);
		}

		/// <summary>
		/// Logs an exception in errorlog.txt. Overwrites the old errorlog.txt.
		/// </summary>
		/// <param name="ex"></param>
		public static void Log(this Exception ex)
		{
			var sw = new StreamWriter("errorlog.txt");
			sw.WriteLine(ex.GetType().Name + " occurred at " + DateTime.Now + ":");
			sw.WriteLine(ex.ToString());
			sw.Close();
		}

		/// <summary>
		/// Is this order a new order added this turn, or one the server already knows about?
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public static bool IsNew<T>(this IOrder<T> order) where T : IOrderable
		{
			return Galaxy.Current.Referrables.OfType<AddOrderCommand<T>>().Where(cmd => cmd.Order == order).Any();
		}

		/// <summary>
		/// Equals method that doesn't throw an exception when objects are null.
		/// Null is not equal to anything else, except other nulls.
		/// </summary>
		/// <param name="o1"></param>
		/// <param name="o2"></param>
		/// <returns></returns>
		public static bool SafeEquals(this object o1, object o2)
		{
			if (o1 == null && o2 == null)
				return true;
			if (o1 == null || o2 == null)
				return false;
			return o1.Equals(o2);
		}

		/// <summary>
		/// Gets a property value from an object using reflection.
		/// If the property does not exist, returns null.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static object GetPropertyValue(this object o, string propertyName)
		{
			var prop = o.GetType().GetProperty(propertyName);
			if (prop == null)
				return null;
			return prop.GetValue(o, new object[0]);
		}

		/// <summary>
		/// Gets a property value from an object using reflection.
		/// If the property does not exist or the property value is not IComparable, returns an empty string.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static IComparable GetComparablePropertyValue(this object o, string propertyName)
		{
			var pval = GetPropertyValue(o, propertyName);
			if (pval == null || !(pval is IComparable))
				return "";
			return (IComparable)pval;
		}

		/// <summary>
		/// Sets a property value on an object using reflection.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static void SetPropertyValue(this object o, string propertyName, object value)
		{
			o.GetType().GetProperty(propertyName).SetValue(o, value, new object[0]);
		}

		/// <summary>
		/// Tests if an object is null.
		/// Useful for writing == operators that don't infinitely recurse.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNull(this object o)
		{
			return o == null;
		}

		public static void IssueOrder<T>(this T obj, IOrder<T> order) where T : IOrderable
		{
			if (obj.Owner != Empire.Current)
				throw new Exception("Cannot issue orders to another empire's objects.");
			Empire.Current.IssueOrder(obj, order);
		}

		public static int CargoStorageFree(this ICargoContainer cc)
		{
			return cc.CargoStorage - cc.Cargo.Size;
		}

		/// <summary>
		/// Transfers items from this cargo container to another cargo container.
		/// </summary>
		public static void TransferCargo(this ICargoContainer src, CargoDelta delta, ICargoContainer dest, Empire emp)
		{
			// if destination is null, we are transferring to/from space
			if (dest == null)
				dest = src.Sector;
			if (src == null)
				src = dest.Sector;

			// transfer per-race population
			foreach (var kvp in delta.RacePopulation)
			{
				var amount = long.MaxValue;

				// limit by desired amount to transfer
				if (kvp.Value != null)
					amount = Math.Min(amount, kvp.Value.Value);
				// limit by amount available
				amount = Math.Min(amount, src.AllPopulation[kvp.Key]);
				// limit by amount of free space
				amount = Math.Min(amount, dest.PopulationStorageFree + (long)((dest.CargoStorage - dest.Cargo.Size) / Mod.Current.Settings.PopulationSize));

				amount -= src.RemovePopulation(kvp.Key, amount);
				dest.AddPopulation(kvp.Key, amount);

				if (amount < kvp.Value)
					emp.Log.Add(src.CreateLogMessage(src + " could transfer only " + amount.ToUnitString(true) + " of the desired " + kvp.Value.ToUnitString(true) + " " + kvp.Key + " population to " + dest + " due to lack of population available or lack of storage space."));
			}

			// transfer any-population
			var anyPopLeft = delta.AnyPopulation;
			foreach (var kvp in src.AllPopulation.ToArray())
			{
				var amount = long.MaxValue;

				// limit by desired amount to transfer
				if (!delta.AllPopulation)
					amount = Math.Min(amount, anyPopLeft);
				// limit by amount available
				amount = Math.Min(amount, kvp.Value);
				// limit by amount of free space
				amount = Math.Min(amount, dest.PopulationStorageFree + (long)((dest.CargoStorage - dest.Cargo.Size) / Mod.Current.Settings.PopulationSize));

				amount -= src.RemovePopulation(kvp.Key, amount);
				dest.AddPopulation(kvp.Key, amount);

				if (amount < anyPopLeft)
					emp.Log.Add(src.CreateLogMessage(src + " could transfer only " + amount.ToUnitString(true) + " of the desired " + kvp.Value.ToUnitString(true) + " general population to " + dest + " due to lack of population available or lack of storage space."));

				if (amount == 0)
					continue;
			}

			// clear population that was emptied out
			foreach (var race in src.Cargo.Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
				src.Cargo.Population.Remove(race);
			if (src is Planet)
			{
				var p = (Planet)src;
				if (p.Colony != null)
				{
					foreach (var race in p.Colony.Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
						p.Colony.Population.Remove(race);
				}
			}

			// transfer specific units
			foreach (var unit in delta.Units)
			{
				if (src.Cargo.Units.Contains(unit))
					TryTransferUnit(unit, src, dest, emp);
				else
					LogUnitTransferFailedNotPresent(unit, src, dest, emp);
			}

			// transfer unit tonnage by design
			foreach (var kvp in delta.UnitDesignTonnage)
			{
				int transferred = 0;
				while (kvp.Value == null || transferred <= kvp.Value - kvp.Key.Hull.Size)
				{
					var unit = src.AllUnits.FirstOrDefault(u => u.Design == kvp.Key);
					if (unit == null && kvp.Value != null)
					{
						if (kvp.Value != null)
							LogUnitTransferFailed(kvp.Key, src, dest, transferred, kvp.Value.Value, emp);
						break;
					}
					if (dest.CargoStorageFree() < kvp.Key.Hull.Size)
					{
						LogUnitTransferFailedNoStorage(unit, src, dest, emp);
						break;
					}
					if (transferred + kvp.Key.Hull.Size > kvp.Value)
						break; // next unit would be too much
					if (unit != null)
					{
						src.RemoveUnit(unit);
						dest.AddUnit(unit);
						transferred += kvp.Key.Hull.Size;
					}
					else
						break;
				}
			}

			// transfer unit tonnage by role
			foreach (var kvp in delta.UnitRoleTonnage)
			{
				int transferred = 0;
				var available = src.AllUnits.Where(u => u.Design.Role == kvp.Key);
				while (kvp.Value == null || transferred <= kvp.Value - available.MinOrDefault(u => u.Design.Hull.Size))
				{
					if (!available.Any())
					{
						if (kvp.Value != null)
							LogUnitTransferFailed(kvp.Key, src, dest, transferred, kvp.Value.Value, emp);
						break;
					}
					var unit = available.FirstOrDefault(u => u.Design.Hull.Size <= dest.CargoStorageFree() && kvp.Value == null || u.Design.Hull.Size <= kvp.Value - transferred);
					if (unit != null)
					{
						src.RemoveUnit(unit);
						dest.AddUnit(unit);
						available = src.AllUnits.Where(u => u.Design.Role == kvp.Key);
						transferred += unit.Design.Hull.Size;
					}
					else
						break;
				}
			}

			// transfer unit tonnage by hull type
			foreach (var kvp in delta.UnitTypeTonnage)
			{
				int transferred = 0;
				var available = src.AllUnits.Where(u => u.Design.VehicleType == kvp.Key);
				while (kvp.Value == null || transferred <= kvp.Value - available.MinOrDefault(u => u.Design.Hull.Size))
				{
					if (!available.Any())
					{
						LogUnitTransferFailed(kvp.Key, src, dest, transferred, kvp.Value.Value, emp);
						break;
					}
					var unit = available.FirstOrDefault(u => u.Design.Hull.Size <= dest.CargoStorageFree() && kvp.Value == null || u.Design.Hull.Size <= kvp.Value - transferred);
					if (unit != null)
					{
						src.RemoveUnit(unit);
						dest.AddUnit(unit);
						available = src.AllUnits.Where(u => u.Design.VehicleType == kvp.Key);
						transferred += unit.Design.Hull.Size;
					}
					else
						break;
				}
			}
		}

		private static void LogUnitTransferFailedNotPresent(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage(unit + " could not be transferred from " + src + " to " + dest + " because it is not in " + src + "'s cargo."));
		}

		private static void LogUnitTransferFailedNoStorage(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage(unit + " could not be transferred from " + src + " to " + dest + " because " + dest + "'s cargo is full."));
		}

		private static void LogUnitTransferFailed(IDesign<IUnit> design, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + design + " class " + design.VehicleTypeName + "s could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full."));
		}

		private static void LogUnitTransferFailed(string role, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + role + " units could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full."));
		}

		private static void LogUnitTransferFailed(VehicleTypes vt, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + vt.ToSpacedString().ToLower() + "s could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full."));
		}

		private static void TryTransferUnit(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			if (dest.CargoStorageFree() >= unit.Design.Hull.Size)
			{
				src.RemoveUnit(unit);
				dest.AddUnit(unit);
			}
			else
				LogUnitTransferFailedNoStorage(unit, src, dest, emp);
		}

		/// <summary>
		/// Converts an object to a string with spaces between camelCased words.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static string ToSpacedString(this object o)
		{
			var sb = new StringBuilder();
			bool wasSpace = true;
			foreach (var c in o.ToString())
			{
				if (!wasSpace && (char.IsUpper(c) || char.IsNumber(c)))
					sb.Append(" ");
				sb.Append(c);
				wasSpace = char.IsWhiteSpace(c);
			}
			return sb.ToString();
		}

		public static Type GetVehicleType(this VehicleTypes vt)
		{
			switch (vt)
			{
				case VehicleTypes.Ship:
					return typeof(Ship);
				case VehicleTypes.Base:
					return typeof(Base);
				case VehicleTypes.Fighter:
					return typeof(Fighter);
				case VehicleTypes.Troop:
					return typeof(Troop);
				case VehicleTypes.Mine:
					return typeof(Mine);
				case VehicleTypes.Satellite:
					return typeof(Satellite);
				case VehicleTypes.Drone:
					return typeof(Drone);
				case VehicleTypes.WeaponPlatform:
					return typeof(WeaponPlatform);
				default:
					throw new Exception("No type is available for vehicle type " + vt);
			}
		}

		public static bool IsDirectFire(this WeaponTypes wt)
		{
			return wt == WeaponTypes.DirectFire || wt == WeaponTypes.DirectFirePointDefense;
		}

		public static bool IsSeeking(this WeaponTypes wt)
		{
			return wt == WeaponTypes.Seeking || wt == WeaponTypes.SeekingPointDefense;
		}

		public static bool IsWarhead(this WeaponTypes wt)
		{
			return wt == WeaponTypes.Warhead || wt == WeaponTypes.WarheadPointDefense;
		}

		public static bool IsPointDefense(this WeaponTypes wt)
		{
			return wt == WeaponTypes.DirectFirePointDefense || wt == WeaponTypes.SeekingPointDefense || wt == WeaponTypes.WarheadPointDefense;
		}

		/// <summary>
		/// Finds the last sector in a space object's path, or if it has no movement-type orders, its current sector.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Sector FinalSector<T>(this T sobj)
			where T : IMobileSpaceObject
		{
			var path = sobj.Path();
			if (path == null || !path.Any())
				return sobj.Sector;
			return path.Last();
		}

		public static string ToString(this double? d, string fmt)
		{
			if (d == null)
				return "";
			return d.Value.ToString(fmt);
		}

		/// <summary>
		/// Refills the space object's movement points.
		/// </summary>
		public static void RefillMovement(this IMobileSpaceObject sobj)
		{
			sobj.MovementRemaining = sobj.Speed;
			sobj.TimeToNextMove = sobj.TimePerMove;
		}

		/// <summary>
		/// Computes the path that this space object is ordered to follow.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static IEnumerable<Sector> Path(this IMobileSpaceObject sobj)
		{
			// TODO - cache paths and only recalculate them when the orders change
			var last = sobj.Sector;
			foreach (var order in sobj.Orders)
			{
				if (order is IMovementOrder)
				{
					var o = (IMovementOrder)order;
					foreach (var s in o.Pathfind(sobj, last))
						yield return s;
					last = o.Destination;
				}
			}
		}

		public static void RefreshDijkstraMap(this IMobileSpaceObject sobj)
		{
			// create new map if necessary
			if (sobj.DijkstraMap == null)
				sobj.DijkstraMap = new Dictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>>();

			// prune old nodes
			var start = sobj.Sector;
			foreach (var n in sobj.DijkstraMap.Keys.OrderBy(n => n.Cost).ToArray())
			{
				if ((n.PreviousNode == null || !sobj.DijkstraMap.ContainsKey(n.PreviousNode)) && n.Location != start)
				{
					// already went here or it was an aborted path
					// delete the node (and this will mark for deletion all its children that we're not at)
					sobj.DijkstraMap.Remove(n);
					if (n.Location == start)
					{
						foreach (var n2 in sobj.DijkstraMap.Keys)
							n2.Cost -= 1;
					}
				}
			}

			// add new nodes
			int minCost = 0;
			foreach (var order in sobj.Orders)
			{
				var last = start;
				if (order is IMovementOrder<SpaceVehicle>)
				{
					var o = (IMovementOrder<SpaceVehicle>)order;
					foreach (var kvp in o.CreateDijkstraMap(sobj, last))
					{
						kvp.Key.Cost += minCost;
						sobj.DijkstraMap.Add(kvp);
					}
					// account for cost of previous orders
					minCost = sobj.DijkstraMap.Keys.MaxOrDefault(n => n.MinimumCostRemaining);
					last = o.Destination;
				}
			}
		}

		/// <summary>
		/// Finds all subfleets (recursively, including this fleet) that have any child space objects that are not fleets.
		/// </summary>
		/// <param name="rootFleet"></param>
		/// <returns></returns>
		public static IEnumerable<Fleet> SubfleetsWithNonFleetChildren(this Fleet rootFleet)
		{
			if (rootFleet.Vehicles.Any(sobj => !(sobj is Fleet)))
				yield return rootFleet;
			foreach (var subfleet in rootFleet.Vehicles.OfType<Fleet>())
			{
				foreach (var subsub in subfleet.SubfleetsWithNonFleetChildren())
					yield return subsub;
			}
		}

		public static void Place(this IUnit unit, ISpaceObject target)
		{
			if (target is ICargoContainer)
			{
				var container = (ICargoContainer)target;
				var cargo = container.Cargo;
				if (cargo.Size + unit.Design.Hull.Size <= container.CargoStorage)
				{
					cargo.Units.Add(unit);
					return;
				}
			}
			foreach (var container in target.FindSector().SpaceObjects.OfType<ICargoTransferrer>().Where(cc => cc.Owner == unit.Owner))
			{
				var cargo = container.Cargo;
				if (cargo.Size + unit.Design.Hull.Size <= container.CargoStorage)
				{
					cargo.Units.Add(unit);
					return;
				}
			}
			unit.Owner.Log.Add(unit.CreateLogMessage(unit + " was lost due to insufficient cargo space at " + target + "."));
		}

		/// <summary>
		/// Finds the cargo container which contains this unit.
		/// </summary>
		/// <returns></returns>
		public static ICargoContainer FindContainer(this IUnit unit)
		{
			var containers = Galaxy.Current.FindSpaceObjects<ICargoTransferrer>().Where(cc => cc.Cargo != null && cc.Cargo.Units.Contains(unit));
			if (!containers.Any())
			{
				if (unit is IMobileSpaceObject)
				{
					var v = (IMobileSpaceObject)unit;
					return v.Sector;
				}
				else
					return null; // unit is in limbo...
			}
			if (containers.Count() > 1)
				throw new Exception("Unit is in multiple cargo containers?!");
			return containers.Single();
		}

		public static int? ToNullableInt(this long? l)
		{
			if (l == null)
				return null;
			return (int)l.Value;
		}

		public static long? ToNullableLong(this int? i)
		{
			if (i == null)
				return null;
			return (long)i.Value;
		}

		public static string EscapeBackslashes(this string s)
		{
			return s.Replace("\\", "\\\\");
		}

		public static string EscapeQuotes(this string s)
		{
			return s.Replace("'", "\\'").Replace("\"", "\\\"");
		}

		public static string EscapeNewlines(this string s)
		{
			return s.Replace("\r", "\\r").Replace("\n", "\\n");
		}

		/// <summary>
		/// Casts an object to a type. Throws an exception if  the type is wrong.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <returns></returns>
		public static T CastTo<T>(this object o)
		{
			return (T)o;
		}

		/// <summary>
		/// Casts an object to a type. Returns null if the type is wrong.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <returns></returns>
		public static T As<T>(this object o, bool throwExceptionIfWrongType = false)
			where T : class
		{
			return o as T;
		}

		/// <summary>
		/// Builds a delegate to wrap a MethodInfo.
		/// http://stackoverflow.com/questions/13041674/create-func-or-action-for-any-method-using-reflection-in-c
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="method"></param>
		/// <param name="missingParamValues"></param>
		/// <returns></returns>
		public static T BuildDelegate<T>(this MethodInfo method, params object[] missingParamValues)
		{
			var queueMissingParams = new Queue<object>(missingParamValues);

			var dgtMi = typeof(T).GetMethod("Invoke");
			var dgtRet = dgtMi.ReturnType;
			var dgtParams = dgtMi.GetParameters();

			var paramsOfDelegate = dgtParams
				.Select(tp => Expression.Parameter(tp.ParameterType, tp.Name))
				.ToArray();

			var methodParams = method.GetParameters();

			if (method.IsStatic)
			{
				var paramsToPass = methodParams
					.Select((p, i) => CreateParam(paramsOfDelegate, i, p, queueMissingParams))
					.ToArray();

				var expr = Expression.Lambda<T>(
					Expression.Call(method, paramsToPass),
					paramsOfDelegate);

				return expr.Compile();
			}
			else
			{
				var paramThis = Expression.Convert(paramsOfDelegate[0], method.DeclaringType);

				var paramsToPass = methodParams
					.Select((p, i) => CreateParam(paramsOfDelegate, i + 1, p, queueMissingParams))
					.ToArray();

				var expr = Expression.Lambda<T>(
					Expression.Call(paramThis, method, paramsToPass),
					paramsOfDelegate);

				return expr.Compile();
			}
		}

		public static Delegate BuildDelegate(this MethodInfo method, params object[] missingParamValues)
		{
			var parms = method.GetParameters();
			var parmTypes = parms.Select(p => p.ParameterType).ToArray();
			var delegateType = method.ReturnType == typeof(void) ? MakeActionType(parmTypes) : MakeFuncType(parmTypes, method.ReturnType);
			var builder = typeof(CommonExtensions).GetMethods().Single(m => m.Name == "BuildDelegate" && m.GetGenericArguments().Length == 1).MakeGenericMethod(delegateType);
			return (Delegate)builder.Invoke(null, new object[] { method, missingParamValues });
		}

		public static Type MakeActionType(this IEnumerable<Type> parmTypes)
		{
			if (parmTypes.Count() == 0)
				return typeof(Action);
			if (parmTypes.Count() == 1)
				return typeof(Action<>).MakeGenericType(parmTypes.ToArray());
			if (parmTypes.Count() == 2)
				return typeof(Action<,>).MakeGenericType(parmTypes.ToArray());
			if (parmTypes.Count() == 3)
				return typeof(Action<,,>).MakeGenericType(parmTypes.ToArray());
			if (parmTypes.Count() == 4)
				return typeof(Action<,,,>).MakeGenericType(parmTypes.ToArray());
			if (parmTypes.Count() == 5)
				return typeof(Action<,,,,>).MakeGenericType(parmTypes.ToArray());
			if (parmTypes.Count() == 6)
				return typeof(Action<,,,,,>).MakeGenericType(parmTypes.ToArray());
			// TODO - more parms
			throw new Exception("MakeActionType currently supports only 0-6 parameters.");
		}

		public static Type MakeFuncType(this IEnumerable<Type> parmTypes, Type returnType)
		{
			var types = parmTypes.Concat(returnType.SingleItem());
			if (parmTypes.Count() == 0)
				return typeof(Func<>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 1)
				return typeof(Func<,>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 2)
				return typeof(Func<,,>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 3)
				return typeof(Func<,,,>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 4)
				return typeof(Func<,,,,>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 5)
				return typeof(Func<,,,,,>).MakeGenericType(types.ToArray());
			if (parmTypes.Count() == 6)
				return typeof(Func<,,,,,,>).MakeGenericType(types.ToArray());
			// TODO - more parms
			throw new Exception("MakeFuncType currently supports only -16 parameters.");
		}

		private static Expression CreateParam(ParameterExpression[] paramsOfDelegate, int i, ParameterInfo callParamType, Queue<object> queueMissingParams)
		{
			if (i < paramsOfDelegate.Length)
				return Expression.Convert(paramsOfDelegate[i], callParamType.ParameterType);

			if (queueMissingParams.Count > 0)
				return Expression.Constant(queueMissingParams.Dequeue());

			if (callParamType.ParameterType.IsValueType)
				return Expression.Constant(Activator.CreateInstance(callParamType.ParameterType));

			return Expression.Constant(null);
		}

		/// <summary>
		/// Creates an enumerable containing a single item.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private static IEnumerable<T> SingleItem<T>(this T obj)
		{
			return new T[] { obj };
		}

		/// <summary>
		/// Updates the memory sight cache of any empires that can see this object.
		/// Only makes sense on the host view, so if this is called elsewhere, nothing happens.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="stillExists"></param>
		public static void UpdateEmpireMemories(this IFoggable obj)
		{
			if (Empire.Current == null)
			{
				foreach (var emp in Galaxy.Current.Empires)
				{
					if (obj.CheckVisibility(emp) >= Visibility.Visible)
						emp.UpdateMemory(obj);
				}
			}
		}

		public static object Instantiate(this Type type, params object[] args)
		{
			if (type.GetConstructor(new Type[0]) != null)
				return Activator.CreateInstance(type, args);
			else
				return FormatterServices.GetSafeUninitializedObject(type);
		}

		public static bool HasProperty(this ExpandoObject obj, string propertyName)
		{
			return obj.GetType().GetProperty(propertyName) != null;
		}

		/// <summary>
		/// Parses a string using the type's static Parse method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <returns></returns>
		public static T Parse<T>(this string s)
		{
			var parser = typeof(T).GetMethod("Parse", BindingFlags.Static);
			var expr = Expression.Call(parser);
			return (T)expr.Method.Invoke(null, new object[] { s });
		}

		/// <summary>
		/// Converts to a string using the invariant culture.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string ToStringInvariant(this IConvertible c)
		{
			return (string)Convert.ChangeType(c, typeof(string), CultureInfo.InvariantCulture);
		}

		public static string CamelCase(this string s)
		{
			return s[0].ToString().ToLowerInvariant() + s.Substring(1);
		}

		public static Formula<TValue> BuildMultiConditionalLessThanOrEqual<TKey, TValue>(this IDictionary<TKey, TValue> thresholds, object context, string variableName, TValue defaultValue)
			where TValue : IConvertible, IComparable, IComparable<TValue>
		{
			var sorted = new SortedDictionary<TKey, TValue>(thresholds);
			var formula = "***";
			foreach (var kvp in sorted)
				formula = formula.Replace("***", kvp.Value.ToStringInvariant() + " if " + variableName + " <= " + kvp.Key + " else (***)");
			formula = formula.Replace("***", defaultValue.ToStringInvariant());
			return new Formula<TValue>(context, formula, FormulaType.Dynamic);
		}

		/// <summary>
		/// Returns an object's hash code, or 0 for null.
		/// </summary>
		/// <param name="o"></param>
		public static int GetSafeHashCode(this object o)
		{
			return o == null ? 0 : o.GetHashCode();
		}

		/// <summary>
		/// Returns "We" if the empire is the current empire, otherwise "The " followed by the empire name.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static string WeOrName(this Empire emp, bool capitalize = true)
		{
			if (emp == Empire.Current)
				return "We";
			return "The " + emp.Name;
		}

		/// <summary>
		/// Returns "us" if the empire is the current empire, otherwise "the " followed by the empire name.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static string UsOrName(this Empire emp, bool capitalize = false)
		{
			if (emp == Empire.Current)
				return "us";
			return "the " + emp.Name;
		}

		/// <summary>
		/// Gets a possessive form of a noun or pronoun.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="isStart">For "I", is this the first word? For "you" and "she", is this in the subject of the sentence?</param>
		/// <returns></returns>
		public static string Possessive(this string s, bool isStart = false)
		{
			if (s == "I")
				return isStart ? "My" : "my";
			if (s == "we")
				return "our";
			if (s == "We")
				return "Our";
			if (s == "you")
				return isStart ? "your" : "yours";
			if (s == "You")
				return isStart ? "Your" : "Yours";
			if (s == "he" || s == "him")
				return "his";
			if (s == "He")
				return "His";
			if (s == "she" || s == "her")
				return isStart ? "her" : "hers";
			if (s == "She")
				return "Her";
			if (s == "it")
				return "its";
			if (s == "they")
				return "their";
			if (s == "They")
				return "Their";
			if (s == "them")
				return "theirs";

			if (s.EndsWith("s"))
				return s + "'";
			return s + "'s";
		}

		public static string Capitalize(this string s)
		{
			if (s == null)
				return null;
			if (s.Length == 0)
				return s;
			return s[0].ToString().ToUpper() + s.Substring(1);
		}

		public static ILookup<TKey, TValue> MyLookup<TKey, TEnumerable, TValue>(this IEnumerable<KeyValuePair<TKey, TEnumerable>> dict)
			where TEnumerable : IEnumerable<TValue>
		{
			var list = new List<KeyValuePair<TKey, TValue>>();
			foreach (var kvp in dict)
			{
				foreach (var item in kvp.Value)
					list.Add(new KeyValuePair<TKey, TValue>(kvp.Key, item));
			}
			return list.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
		}

		/// <summary>
		/// All abilities belonging to an object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> Abilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			if (sourceFilter == null && Galaxy.Current.IsAbilityCacheEnabled)
			{
				// use the ability cache
				if (Galaxy.Current.AbilityCache[obj] == null)
					Galaxy.Current.AbilityCache[obj] = obj.UnstackedAbilities(sourceFilter).Stack(obj).ToArray();
				return Galaxy.Current.AbilityCache[obj];
			}

			return obj.UnstackedAbilities(sourceFilter).Stack(obj);
		}

		/// <summary>
		/// All abilities belonging to an object, before stacking.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="includeShared"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> UnstackedAbilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			if (sourceFilter == null || sourceFilter(obj))
				return obj.IntrinsicAbilities.Concat(obj.SharedAbilities(sourceFilter)).Concat(obj.DescendantAbilities(sourceFilter)).Concat(obj.AncestorAbilities(sourceFilter));
			else
				return obj.SharedAbilities(sourceFilter).Concat(obj.DescendantAbilities(sourceFilter)).Concat(obj.AncestorAbilities(sourceFilter));
		}

		/// <summary>
		/// Abilities passed up from descendant objects.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="includeShared"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> DescendantAbilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			return obj.Children.SelectMany(c => c.IntrinsicAbilities.Concat(c.DescendantAbilities(sourceFilter))).Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
		}

		/// <summary>
		/// Abilities inherited from ancestor objects.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="includeShared"></param>
		/// <returns></returns>
		public static IEnumerable<Ability> AncestorAbilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			if (obj.Parent == null)
				return Enumerable.Empty<Ability>();
			return obj.Parent.IntrinsicAbilities.Concat(obj.Parent.AncestorAbilities(sourceFilter)).Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
		}

		/// <summary>
		/// Appends an item to a sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static IEnumerable<T> Append<T>(this IEnumerable<T> sequence, T item)
		{
			return sequence.Concat(new T[] { item });
		}

		public static ILookup<Ability, Ability> AbilityTree(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
		{
			return obj.UnstackedAbilities(sourceFilter).StackToTree(obj);
		}

		public static void Patch<T>(this ICollection<T> old, IEnumerable<T> nu)
			where T : class, IModObject
		{
			foreach (var item in old.Where(item => !item.StillExists(old, nu)).ToArray())
			{
				// delete item that was deleted
				old.Remove(item);
				if (item is IReferrable)
					((IReferrable)item).Dispose();
			}
			foreach (var item in nu.ToArray())
			{
				var oldItem = old.FindMatch(item, nu);
				if (oldItem == null)
				{
					// add item that was added
					old.Add(item);
				}
				else
				{
					// patch item and delete the patch
					if (item is IReferrable)
					{
						var r = (IReferrable)item;
						var r2 = (IReferrable)oldItem;
						r.CopyToExceptID(r2, IDCopyBehavior.PreserveDestination);
						r.Dispose();
					}
					else
						item.CopyTo(oldItem, IDCopyBehavior.PreserveDestination);
				}
			}
		}

		public static T FindByModID<T>(this IEnumerable<T> items, string modID)
			where T : IModObject
		{
			if (modID == null)
				return default(T);
			return items.SingleOrDefault(item => item.ModID == modID);
		}

		public static T FindByTypeNameIndex<T>(this IEnumerable<T> items, Type type, string name, int index)
			where T : IModObject
		{
			return items.Where(item => item.GetType() == type && item.Name == name).ElementAtOrDefault(index);
		}

		/// <summary>
		/// Returns the index of an item in a list after the list has been filtered to items with the same name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static int GetIndex<T>(this IEnumerable<T> items, T item)
			where T : INamed
		{
			return items.Where(i => i.Name == item.Name).IndexOf(item);
		}

		public static T FindMatch<T>(this IEnumerable<T> items, T nu, IEnumerable<T> nuItems)
			where T : class, IModObject
		{
			return items.FindByModID(nu.ModID) ?? items.FindByTypeNameIndex(nu.GetType(), nu.Name, nuItems.GetIndex(nu));
		}

		public static bool StillExists<T>(this T old, IEnumerable<T> oldItems, IEnumerable<T> nuItems)
			where T : IModObject
		{
			var match = nuItems.FindByModID(old.ModID);
			if (match != null)
				return true;
			match = nuItems.FindByTypeNameIndex(old.GetType(), old.Name, oldItems.GetIndex(old));
			return match != null;
		}

		/// <summary>
		/// Finds the original object of a memory, if it is known.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static IFoggable FindOriginalObject(this IFoggable f, Empire emp)
		{
			// not a memory? it is its own real object
			if (!(f.IsMemory))
				return f;

			// look for the real object
			if (emp.Memory.Any(kvp => kvp.Value == f))
				return (IFoggable)Galaxy.Current.referrables[emp.Memory.Single(kvp => kvp.Value == f).Key];

			// nothing found?
			return null;
		}

		public static void ClearAbilityCache(this IAbilityObject o)
		{
			Galaxy.Current.AbilityCache.Remove(o);
		}

		public static string ToSafeString(this object o)
		{
			if (o == null)
				return null;
			return o.ToString();
		}

		/// <summary>
		/// Is this space object hidden from view of an empire due to cloaking?
		/// Space objects are hidden from view if they have at least one cloaking ability, and
		/// all cloaking abilities they possess outrank the appropriate sensors possessed by the empire in the system.
		/// However a space object must possess at least one cloaking ability to actually be cloaked.
		/// </summary>
		/// <remarks>
		/// Unlike in SE4, where cloaks must outrank sensors in five specific sight types,
		/// in FrEee, sight types are just custom strings in the data files, so cloaks must only outrank sensors
		/// in sight types that the viewing empire actually possesses in the system.
		/// Thus, a level 2 temporal cloak will hide you from enemy sight on its own, even if you don't have a
		/// level 2 psychic cloak or a level 2 foobar cloak or whatever. However if the enemy has a level 2 temporal
		/// sensor, or a level 1 sensor of any type but temporal, then they can see you.
		/// </remarks>
		/// <param name="sobj"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static bool IsHiddenFrom(this ISpaceObject sobj, Empire emp)
		{
			var sys = sobj.StarSystem;
			var sec = sobj.Sector;
			var sensors = sys.Abilities(emp).Where(a => a.Rule.Name == "Sensor Level");
			var cloaks = sobj.Abilities().Where(a => a.Rule.Name == "Cloak Level");
			var joined = from sensor in sensors
						 join cloak in cloaks on sensor.Value1.Value equals cloak.Value1.Value into gj
						 from subcloak in gj.DefaultIfEmpty()
						 select new
						 {
							 SightType = sensor.Value1.Value,
							 SensorLevel = sensor.Value2.Value.ToInt(),
							 CloakLevel = subcloak == null ? 0 : subcloak.Value2.Value.ToInt(),
						 };
			var obscurationLevel = Math.Max(sys.GetAbilityValue("Sector - Sight Obscuration").ToInt(), sys.GetAbilityValue(sobj.Owner, "Sector - Sight Obscuration").ToInt());
			obscurationLevel = Math.Max(obscurationLevel, sec.GetAbilityValue("Sector - Sight Obscuration").ToInt());
			obscurationLevel = Math.Max(obscurationLevel, sec.GetAbilityValue(sobj.Owner, "Sector - Sight Obscuration").ToInt());
			return (cloaks.Any() || obscurationLevel > 0) && joined.All(j => j.CloakLevel > j.SensorLevel || obscurationLevel > j.SensorLevel);
		}

		/// <summary>
		/// Converts a percentage into a ratio.
		/// </summary>
		/// <param name="i">The percentage, e.g. 50</param>
		/// <returns>The ratio, e.g. 0.5</returns>
		public static double Percent(this int i)
		{
			return (double)i / 100d;
		}

		/// <summary>
		/// Multiplies an integer by a scale factor and rounds it.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static int TimesAndRound(this int i, double d)
		{
			return (int)Math.Round(i * d);
		}

		/// <summary>
		/// Multiplies an integer by a percentage and rounds it.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static int PercentOfRounded(this int p, int i)
		{
			return i.TimesAndRound(p.Percent());
		}

		/// <summary>
		/// Gets any abilities that can be activated.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IDictionary<Ability, IAbilityObject> ActivatableAbilities(this Vehicle v)
		{
			var dict = new Dictionary<Ability, IAbilityObject>();
			foreach (var a in v.Hull.Abilities)
			{
				if (a.Rule.IsActivatable)
					dict.Add(a, v.Hull);
			}
			foreach (var c in v.Components.Where(c => !c.IsDestroyed))
			{
				foreach (var a in c.Abilities)
				{
					if (a.Rule.IsActivatable)
						dict.Add(a, c);
				}
			}
			return dict;
		}

		/// <summary>
		/// Gets any abilities that can be activated.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IDictionary<Ability, IAbilityObject> ActivatableAbilities(this Planet p)
		{
			var dict = new Dictionary<Ability, IAbilityObject>();
			if (p.Colony == null)
				return dict;
			foreach (var f in p.Colony.Facilities.Where(f => !f.IsDestroyed))
			{
				foreach (var a in f.Abilities)
				{
					if (a.Rule.IsActivatable)
						dict.Add(a, f);
				}
			}
			return dict;
		}

		/// <summary>
		/// Gets any abilities that can be activated.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IDictionary<Ability, IAbilityObject> ActivatableAbilities(this IAbilityObject o)
		{
			if (o is Vehicle)
				return ((Vehicle)o).ActivatableAbilities();
			if (o is Planet)
				return ((Planet)o).ActivatableAbilities();

			var dict = new Dictionary<Ability, IAbilityObject>();
			foreach (var a in o.Abilities())
			{
				if (a.Rule.IsActivatable)
					dict.Add(a, o);
			}
			return dict;
		}

		public static bool SafeSequenceEqual<T>(this IEnumerable<T> e1, IEnumerable<T> e2)
		{
			if (e1.SafeEquals(null) && e2.SafeEquals(null))
				return true;
			if (e1.SafeEquals(null) || e2.SafeEquals(null))
				return false;
			return e1.SequenceEqual(e2);
		}

		/// <summary>
		/// Checks for "do not copy" attribute, even on interface properties.
		/// Returns true if there is no such attribute.
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool CanCopyFully(this PropertyInfo p)
		{
			if (p.GetCustomAttributes(true).OfType<DoNotCopyAttribute>().Any())
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
			if (p.GetCustomAttributes(true).OfType<DoNotCopyAttribute>().Any(a => !a.AllowSafeCopy))
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
		/// Does this object's ID match what the galaxy says it is?
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static bool HasValidID(this IReferrable r)
		{
			return Galaxy.Current.referrables.ContainsKey(r.ID) && Galaxy.Current.referrables[r.ID] == r;
		}

		/// <summary>
		/// Disposes of all objects in an enumerated list that meet a specified condition (or all items if condition is null).
		/// Does not clear the list; if the list is a collection, you can do this yourself.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="condition"></param>
		public static void DisposeAll<T>(this IEnumerable<T> list, Func<T, bool> condition = null) where T : IDisposable
		{
			foreach (var d in list.Where(d => condition == null || condition(d)).ToArray())
				d.Dispose();
		}

		public static void SafeDispose(this IDisposable d)
		{
			if (d != null)
				d.Dispose();
		}

		/// <summary>
		/// Programmatic equivalent of the default operator.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object DefaultValue(this Type t)
		{
			return typeof(CommonExtensions).GetMethod("GetDefaultGeneric", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(t).Invoke(null, null);
		}

		private static T GetDefaultGeneric<T>()
		{
			return default(T);
		}

		internal static Visibility CheckSpaceObjectVisibility(this ISpaceObject sobj, Empire emp)
		{
			if (emp == sobj.Owner)
				return Visibility.Owned;

			if (sobj.Sector == null || sobj.StarSystem == null)
				return Visibility.Unknown; // it doesn't really exist...

			// You can always scan space objects you are in combat with.
			// But only their state at the time they were in combat; not for the rest of the turn!
			if (Battle_Space.Current.Union(Battle_Space.Previous).Any(b => (b.StartCombatants.Values.OfType<ISpaceObject>().Contains(sobj)) && b.StartCombatants.Values.Any(c => c.Owner == emp)))
				return Visibility.Scanned;

			// do we have anything that can see it?
			var seers = sobj.FindStarSystem().FindSpaceObjects<ISpaceObject>(s => s.Owner == emp);
			if (!seers.Any() || sobj.IsHiddenFrom(emp))
			{
				if (Galaxy.Current.OmniscientView && sobj.StarSystem.ExploredByEmpires.Contains(emp))
					return Visibility.Visible;
				if (emp.AllSystemsExploredFromStart)
					return Visibility.Fogged;
				var known = emp.Memory[sobj.ID];
				if (known != null && sobj.GetType() == known.GetType())
					return Visibility.Fogged;
				else if (Battle_Space.Current.Union(Battle_Space.Previous).Any(b => b.StartCombatants.Any(kvp => kvp.Key == sobj.ID) && b.StartCombatants.Values.Any(c => c.Owner == emp)))
					return Visibility.Fogged;
				else
					return Visibility.Unknown;
			}
			if (!sobj.HasAbility("Scanner Jammer"))
			{
				var scanners = seers.Where(s =>
					s.HasAbility("Long Range Scanner") && s.GetAbilityValue("Long Range Scanner").ToInt() >= s.FindSector().Coordinates.EightWayDistance(sobj.FindSector().Coordinates)
					|| s.HasAbility("Long Range Scanner - System"));
				if (scanners.Any())
					return Visibility.Scanned;
			}
			return Visibility.Visible;
		}

		/// <summary>
		/// Removes an order from some object.
		/// If the order was just added by the player this turn, simply deletes it.
		/// If not, also creates a RemoveOrderCommand to remove it on the server, and adds that command to the empire's commands.
		/// Intended only for client side use.
		/// </summary>
		/// <typeparam name="T">The type of orderable object.</typeparam>
		/// <param name="obj">The object from which to remove an order.</param>
		/// <param name="order">The order to remove.</param>
		/// <returns>The remove-order command created, if any.</returns>
		public static RemoveOrderCommand<T> RemoveOrderClientSide<T>(this T obj, IOrder<T> order) where T : IOrderable
		{
			if (Empire.Current == null)
				throw new InvalidOperationException("RemoveOrderClientSide is intended for client side use.");
			var addCmd = Empire.Current.Commands.OfType<AddOrderCommand<T>>().SingleOrDefault(c => c.Order == order);
			if (addCmd == null)
			{
				// not a newly added order, so create a remove command to take it off the server
				var remCmd = new RemoveOrderCommand<T>(obj, order);
				Empire.Current.Commands.Add(remCmd);
				obj.RemoveOrder(order);
				return remCmd;
			}
			else
			{
				// a newly added order, so just get rid of the add command
				Empire.Current.Commands.Remove(addCmd);
				obj.RemoveOrder(order);
				return null;
			}
		}

		/// <summary>
		/// Finds the previous item in a list, or null if there is no previous item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static T Previous<T>(this IEnumerable<T> list, T item, bool wrap = false)
		{
			var index = list.IndexOf(item) - 1;
			if (index < 0)
			{
				if (wrap)
					return list.LastOrDefault();
				else
					return default(T);
			}
			else
				return list.ElementAt(index);
		}

		/// <summary>
		/// Finds the next item in a list, or null if there is no next item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static T Next<T>(this IEnumerable<T> list, T item, bool wrap = false)
		{
			var index = list.IndexOf(item) + 1;
			if (index >= list.Count())
			{
				if (wrap)
					return list.FirstOrDefault();
				else
					return default(T);
			}
			else
				return list.ElementAt(index);
		}

		/// <summary>
		/// Converts an enumeration to an array, then does something to each item.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="action"></param>
		public static void SafeForeach<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items != null && action != null)
			{
				foreach (var item in items.ToArray())
					action(item);
			}
		}
	}

	public enum IDCopyBehavior
	{
		PreserveSource,
		PreserveDestination,
		Regenerate
	}
}