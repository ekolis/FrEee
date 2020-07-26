using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Interfaces;

namespace FrEee.Utility.Extensions
{
	public static class CommonExtensions
	{
		private static SafeDictionary<Type, object> defaultValueCache = new SafeDictionary<Type, object>();

		private static List<Type> mappedTypes = new List<Type>();

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

		public static Formula<TValue> BuildMultiConditionalLessThanOrEqual<TKey, TValue>(this IDictionary<TKey, TValue> thresholds, object context, string variableName, TValue defaultValue)
					where TValue : IConvertible, IComparable, IComparable<TValue>
		{
			var sorted = new SortedDictionary<TKey, TValue>(thresholds);
			var formula = "***";
			foreach (var kvp in sorted)
				formula = formula.Replace("***", kvp.Value.ToStringInvariant() + " if " + variableName + " <= " + kvp.Key + " else (***)");
			formula = formula.Replace("***", defaultValue.ToStringInvariant());
			return new ComputedFormula<TValue>(formula, context, true);
		}

		/// <summary>
		/// Consumes supplies if possible.
		/// </summary>
		/// <param name="supplies">The supplies to consume.</param>
		/// <returns>true if successful or unnecessary, otherwise false</returns>
		public static bool BurnSupplies(this IMobileSpaceObject sobj, int supplies)
		{
			if (sobj.HasInfiniteSupplies)
				return true; // no need to burn
			else if (sobj.SupplyRemaining < supplies)
				return false; // not enough
			else
			{
				sobj.SupplyRemaining -= supplies;
				return true;
			}
		}

		/// <summary>
		/// Consumes supplies if possible.
		/// </summary>
		/// <param name="comp">The component consuming supplies.</param>
		/// <returns>true if successful or unnecessary, otherwise false</returns>
		public static bool BurnSupplies(this Component comp)
		{
			if (comp.Container is IMobileSpaceObject)
				return (comp.Container as IMobileSpaceObject).BurnSupplies(comp.Template.SupplyUsage);
			else
				return true; // other component containers don't use supplies
		}

		public static int CargoStorageFree(this ICargoContainer cc)
		{
			return cc.CargoStorage - (cc.Cargo?.Size ?? 0);
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

		public static PictorialLogMessage<T> CreateLogMessage<T>(this T context, string text,LogMessageType logMessageType, int? turnNumber = null)
		{
			if (turnNumber == null)
				return new PictorialLogMessage<T>(text, context, logMessageType);
			else
				return new PictorialLogMessage<T>(text, turnNumber.Value, context, logMessageType);
		}

		public static void DealWithMines(this ISpaceObject sobj)
		{
			if (sobj is IDamageable && sobj is IOwnable)
			{
				var owner = sobj.Owner;
				var d = (IDamageable)sobj;
				var sector = sobj.Sector;
				if (sector == null)
					return;

				// shuffle up the mines so they hit in a random order
				var mines = sector.SpaceObjects.OfType<Mine>().Concat(sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.LeafVehicles.OfType<Mine>())).Where(m => m.IsHostileTo(sobj.Owner)).Shuffle().ToList();

				// for log messages
				var totalDamage = 0;
				var minesSwept = new SafeDictionary<Empire, int>();
				var minesDetonated = new SafeDictionary<Empire, int>();
				var minesAttacking = new SafeDictionary<Empire, int>();

				// can we sweep any?
				int sweeping;
				if (sobj is Fleet f2)
					sweeping = f2.LeafVehicles.Sum(v => v.GetAbilityValue("Mine Sweeping").ToInt());
				else
					sweeping = sobj.GetAbilityValue("Mine Sweeping").ToInt();

				// go through the minefield!
				while (mines.Any() && !d.IsDestroyed)
				{
					var mine = mines.First();
					if (sweeping > 0)
					{
						// sweep a mine
						sweeping--;
						minesSwept[mine.Owner]++;
						mine.Dispose();
					}
					else
					{
						// bang/boom!
						bool detonate = false;
						foreach (var weapon in mine.Weapons)
						{
							var shot = new Shot(mine, weapon, d, 0);
							var damage = weapon.Template.GetWeaponDamage(1);
							var hit = new Hit(shot, d, damage);
							var leftoverDamage = d.TakeDamage(hit);
							totalDamage += damage - leftoverDamage;
							if (weapon.Template.ComponentTemplate.WeaponInfo.IsWarhead)
								detonate = true; // warheads go boom, other weapons don't
						}
						if (detonate)
						{
							minesDetonated[mine.Owner]++;
							mine.Dispose();
						}
						else
							minesAttacking[mine.Owner]++;
					}

					// each mine can only activate or be swept once
					mines.Remove(mine);
				}

				if (sobj is Fleet ff)
					ff.Validate();


				// logging!
				if (minesDetonated.Any() || minesSwept.Any() || minesAttacking.Any())
					owner.Log.Add(sobj.CreateLogMessage(sobj + " encountered a mine field at " + sector + " and took " + totalDamage + " points of damage, sweeping " + minesSwept.Sum(kvp => kvp.Value) + " mines.", LogMessageType.Generic));
				foreach (var emp in minesSwept.Keys.Union(minesDetonated.Keys).Union(minesAttacking.Keys))
					emp.Log.Add(sobj.CreateLogMessage(sobj + " encountered our mine field at " + sector + ". " + minesDetonated[emp] + " of our mines detonated, " + minesAttacking[emp] + " others fired weapons, and " + minesSwept[emp] + " were swept. " + sector.SpaceObjects.OfType<Mine>().Where(m => m.Owner == emp).Count() + " mines remain in the sector.", LogMessageType.Generic));
			}
		}

		/// <summary>
		/// Returns a custom value if the specified value is null or the wrong type.
		/// Otherwise returns the value itself.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name=""></param>
		/// <returns></returns>
		public static T Default<T>(this object value, T def = default(T), bool throwIfWrongType = false)
		{
			if (throwIfWrongType && !(value is T))
				throw new InvalidCastException($"Cannot convert {value} to type {typeof(T)}.");
			return value == null || !(value is T) ? def : (T)value;
		}

		/// <summary>
		/// Returns a custom value if the specified value is equal to the default value for its type.
		/// Otherwise returns the value itself.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name=""></param>
		/// <returns></returns>
		public static T DefaultTo<T>(this T value, T def)
		{
			return value.Equals(default(T)) ? def : value;
		}

		/// <summary>
		/// Programmatic equivalent of the default operator.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object DefaultValue(this Type t)
		{
			if (defaultValueCache[t] == null)
				defaultValueCache[t] = typeof(CommonExtensions).GetMethod("GetDefaultGeneric", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(t).Invoke(null, null);
			return defaultValueCache[t];
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

		public static void DisposeAndLog(this IFoggable obj, string message = null, params Empire[] empiresToSkipMessage)
		{
			if (Empire.Current == null)
			{
				foreach (var emp in Galaxy.Current.Empires)
				{
					if (obj.CheckVisibility(emp) >= Visibility.Visible)
					{
						if (message != null && !empiresToSkipMessage.Contains(emp))
							emp.RecordLog(obj, message, LogMessageType.Generic);
					}
				}
			}
			obj.Dispose();
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

		public static bool ExecuteMobileSpaceObjectOrders<T>(this T o)
					where T : IMobileSpaceObject<T>
		{
			bool didStuff = false;

			if (o.AreOrdersOnHold)
				return didStuff;

			if (o is Fleet f && !f.Vehicles.ExceptSingle(null).Any())
				o.Dispose();
			var runOrders = new List<IOrder>();
			while (!o.IsDisposed && o.Orders.Any() && (o.TimeToNextMove <= 1e-15 || !o.Orders.First().ConsumesMovement))
			{
				var order = o.Orders.First();
				order.Execute(o);
				runOrders.Add(order);
				if (order.IsComplete && o.Orders.Contains(order))
				{
					o.Orders.RemoveAt(0);
					if (o.AreRepeatOrdersEnabled)
					{
						order.IsComplete = false;
						o.Orders.Add(order);
						if (runOrders.Count == o.Orders.Count)
							break; // don't get in an infinite loop of repeating orders
					}
				}
				didStuff = true;
			}
			if (Galaxy.Current.NextTickSize == double.PositiveInfinity)
				o.TimeToNextMove = 0;
			else
				o.TimeToNextMove -= Galaxy.Current.NextTickSize;
			return didStuff;
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

		/// <summary>
		/// Finds the cargo container which contains this unit.
		/// </summary>
		/// <returns></returns>
		public static ICargoContainer FindContainer(this IUnit unit)
		{
			var containers = Galaxy.Current.FindSpaceObjects<ICargoContainer>().Where(cc => !(cc is Fleet) && cc.Cargo != null && cc.Cargo.Units.Contains(unit));
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

		/// <summary>
		/// Finds the coordinates of a space object within its star system.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Point FindCoordinates(this ISpaceObject sobj)
		{
			return sobj.FindStarSystem().FindCoordinates(sobj);
		}

		public static T FindMemory<T>(this T f, Empire emp) where T : IFoggable, IReferrable
		{
			if (f == null)
				return default;
			if (emp == null)
				return f; // host can see everything
			return (T)emp.Memory[f.ID];
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
				return p.DeclaringType?.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			var b = type.BaseType;
			if (b != null)
			{
				var bp = b.FindProperty(propName);
				if (bp != null)
					return bp.DeclaringType?.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			}
			foreach (var i in type.GetInterfaces())
			{
				var ip = i.FindProperty(propName);
				if (ip != null)
					return ip.DeclaringType?.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			}
			return null;
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
			return new Sector(sys, sys.SpaceObjectLocations.Single(l => l.Item == sobj).Location);
		}

		/// <summary>
		/// Finds the star system containing a space object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static StarSystem FindStarSystem(this ISpaceObject sobj)
		{
			var loc = Galaxy.Current.StarSystemLocations.SingleOrDefault(l => l.Item.Contains(sobj));
			/*if (loc == null)
			{
				// search memories too
				// TODO - this might be kind of slow; might want a reverse memory lookup
				loc = Galaxy.Current.StarSystemLocations.SingleOrDefault(l => l.Item.FindSpaceObjects<ISpaceObject>().Any(s => Galaxy.Current.Empires.ExceptSingle(null).Any(e => e.Memory[s.ID] == sobj)));
			}*/
			if (loc == null)
				return null;
			return loc.Item;
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
		/// Gets the canonical name for a property, class, etc.
		/// This is taken from the [CanonicalName] attribute if present, otherwise the name of the item itself.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public static string GetCanonicalName(this MemberInfo m)
		{
			// TODO - use most derived class's attribute?
			var name = m.GetAttributes<CanonicalNameAttribute>().Select(a => a.Name).SingleOrDefault();
			if (name == null)
				return m.Name;
			return name;
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

		public static SafeDictionary<string, object> GetData(this object o, ObjectGraphContext context)
		{
			// serialize object type and field count
			if (o is IDataObject)
			{
				// use data object code! :D
				var dobj = (IDataObject)o;
				return dobj.Data;
			}
			else if (o != null)
			{
				// use reflection :(
				var dict = new SafeDictionary<string, object>();
				var props = ObjectGraphContext.GetKnownProperties(o.GetType()).Values.Where(p => !p.GetValue(o, null).SafeEquals(p.PropertyType.DefaultValue()));
				foreach (var p in props)
					dict[p.Name] = p.GetValue(o);
				return dict;
			}
			else
				return new SafeDictionary<string, object>();
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
		/// Gets all names for a property, class, etc. including custom names and the actual item name.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public static IEnumerable<string> GetNames(this MemberInfo m)
		{
			return m.GetAttributes<NameAttribute>().Select(a => a.Name).UnionSingle(m.Name);
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
			if (o == null)
				return null;
			var prop = o.GetType().GetProperty(propertyName);
			if (prop == null)
				return null;
			return prop.GetValue(o, new object[0]);
		}

		/// <summary>
		/// Returns an object's hash code, or 0 for null.
		/// </summary>
		/// <param name="o"></param>
		public static int GetSafeHashCode(this object o)
		{
			return o == null ? 0 : o.GetHashCode();
		}

		public static Type GetVehicleType(this VehicleTypes vt) =>
			vt switch
			{
				VehicleTypes.Ship => typeof(Ship),
				VehicleTypes.Base => typeof(Base),
				VehicleTypes.Fighter => typeof(Fighter),
				VehicleTypes.Troop => typeof(Troop),
				VehicleTypes.Mine => typeof(Mine),
				VehicleTypes.Satellite => typeof(Satellite),
				VehicleTypes.Drone => typeof(Drone),
				VehicleTypes.WeaponPlatform => typeof(WeaponPlatform),
				_ => throw new Exception("No type is available for vehicle type " + vt)
			};

		/// <summary>
		/// All income provided by an object.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static ResourceQuantity GrossIncome(this IIncomeProducer o) => o.StandardIncome() + o.RemoteMiningIncome() + o.RawResourceIncome();

		public static object Instantiate(this Type type, params object[] args)
		{
			if (type.Name == "Battle")
				return typeof(SpaceBattle).Instantiate(); // HACK - old savegame compatibility
			if (type.GetConstructors().Where(c => c.GetParameters().Length == (args == null ? 0 : args.Length)).Any())
				return Activator.CreateInstance(type, args);
			else
				return FormatterServices.GetSafeUninitializedObject(type);
		}

		public static T Instantiate<T>(params object[] args) => (T)typeof(T).Instantiate(args);

		public static bool IsUnlocked(this IUnlockable u) => u.UnlockRequirements.All(r => r.IsMetBy(Empire.Current));

		/// <summary>
		/// Limits a value to a range.
		/// Throws an exception if min is bigger than max.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int LimitToRange(this int value, int min, int max)
		{
			if (min > max)
				throw new ArgumentOutOfRangeException($"Min is {min} and can't be larger than max which is {max}!");
			if (value > max)
				value = max;
			if (value < min)
				value = min;
			return value;
		}

		/// <summary>
		/// Limits a value to a range.
		/// Throws an exception if min is bigger than max.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static double LimitToRange(this double value, double min, double max)
		{
			if (min > max)
				throw new ArgumentOutOfRangeException($"Min is {min} and can't be larger than max which is {max}!");
			if (value > max)
				value = max;
			if (value < min)
				value = min;
			return value;
		}

		/// <summary>
		/// Logs an exception in fatalerrorlog.txt. Overwrites the old fatalerrorlog.txt.
		/// </summary>
		/// <param name="ex"></param>
		public static void LogFatal(this Exception ex)
		{
			var sw = new StreamWriter("fatalerrorlog.txt");
			sw.WriteLine(ex.GetType().Name + " occurred at " + DateTime.Now + ":");
			sw.WriteLine(ex.ToString());
			sw.Close();
		}

		/// <summary>
		/// Appends the exception to the end of errorlog.txt. 
		/// </summary>
		/// <param name="ex"></param>
		public static void Log(this Exception ex)
		{
			var sw = new StreamWriter("errorlog.txt", true);
			sw.WriteLine(ex.GetType().Name + " occurred at " + DateTime.Now + ":");
			sw.WriteLine(ex.ToString());
			sw.WriteLine(); 
			sw.Close();
		}

		/// <summary>
		/// Logs an error in the AI of the given empire to disk. 
		/// </summary>
		/// <param name="empire"></param>
		/// <param name="error"></param>
		public static void LogAIMessage(this Empire empire, string message)
		{
			var sw = new StreamWriter($"{empire.AI.Name}.log", true);
			sw.WriteLine($"{DateTime.UtcNow} ({Galaxy.Current.Name}-{empire.ID}):{message}");
			sw.Close();
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
		/// Who does a memory belong to?
		/// </summary>
		/// <param name="f">The memory.</param>
		/// <returns>Empire to which the memory belongs (null if not memory).</returns>
		public static Empire MemoryOwner(this IFoggable f)
		{
			if (!f.IsMemory)
				return null;
			return Galaxy.Current.Empires.ExceptSingle(null).SingleOrDefault(x => x.Memory.Values.Contains(f));
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
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		public static string NameFor(this IBattle b, Empire emp)
		{
			return b.ResultFor(emp).Capitalize() + " at " + b.Sector;
		}

		/// <summary>
		/// Makes sure there aren't more supplies than we can store, or fewer than zero
		/// </summary>
		/// <returns>Leftover supplies (or a negative number if somehow we got negative supplies in this vehicle)</returns>
		public static int NormalizeSupplies(this IMobileSpaceObject sobj)
		{
			if (sobj.SupplyRemaining > sobj.SupplyStorage)
			{
				var leftover = sobj.SupplyRemaining - sobj.SupplyStorage;
				sobj.SupplyRemaining = sobj.SupplyStorage;
				return leftover;
			}
			if (sobj.SupplyRemaining < 0)
			{
				var deficit = sobj.SupplyRemaining;
				sobj.SupplyRemaining = 0;
				return deficit;
			}
			return 0;
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
			foreach (var container in target.Sector.SpaceObjects.OfType<ICargoTransferrer>().Where(cc => cc.Owner == unit.Owner))
			{
				var cargo = container.Cargo;
				if (cargo.Size + unit.Design.Hull.Size <= container.CargoStorage)
				{
					cargo.Units.Add(unit);
					return;
				}
			}
			unit.Owner.Log.Add(unit.CreateLogMessage(unit + " was lost due to insufficient cargo space at " + target + ".", LogMessageType.Warning));
		}

		/// <summary>
		/// Raises an event, but doesn't do anything if the event handler is null.
		/// </summary>
		/// <typeparam name="TArgs"></typeparam>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public static void Raise<TArgs>(this EventHandler<TArgs> evt, object sender, TArgs e) where TArgs : EventArgs
		{
			if (evt != null)
				evt(sender, e);
		}

		/// <summary>
		/// Raw resource income which is not affected by any modifiers.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static ResourceQuantity RawResourceIncome(this IIncomeProducer o)
		{
			var rawResourceIncome = new ResourceQuantity();
			foreach (var resource in Resource.All)
			{
				var rule = Mod.Current.AbilityRules.SingleOrDefault(r => r.Matches("Generate Points " + resource));
				if (rule != null)
				{
					var amount = o.GetAbilityValue(rule.Name).ToInt();
					rawResourceIncome += resource * amount;
				}
			}
			return rawResourceIncome;
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
				if (data <= 0)
					break; // end of stream
				else if (escaping)
				{
					// in an escape sequence
					sb.Append((char)data);
					if (log != null)
						log.Append((char)data);
					escaping = false;
				}
				/*else if (quoting)
				{
					// in quotes
					sb.Append((char)data);
					if (log != null)
						log.Append((char)data);
					if (data == '"')
						quoting = false;
				}*/
				else if (data == c)
					break; // found match
				else if (data == '\\')
					escaping = true; // begin escape sequence
									 /*else if (data == '"')
									 {
										 sb.Append((char)data);
										 quoting = true; // begin quoted string
									 }*/
				else
				{
					// regular data
					sb.Append((char)data);
					if (log != null)
						log.Append((char)data);
				}
			} while (true);
			if (data == c && log != null)
				log.Append(c);
			return sb.ToString();
		}

		/// <summary>
		/// Reads characters until the specified character is found at the end of the line or end of stream.
		/// Returns all characters read except the specified character.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string ReadToEndOfLine(this TextReader r, char c, StringBuilder log)
		{
			var sb = new StringBuilder();
			string data = "";
			do
			{
				data = r.ReadLine();
				log?.Append(data);
				sb.Append(data);
				if (data.EndsWith(c.ToString()))
					break;
			} while (true);
			return sb.ToString().Substring(0, Math.Max(0, sb.Length - 1)); // trim off the semicolon
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

		/// <summary>
		/// Reassigns the ID of an object, overwriting any existing ID.
		/// </summary>
		/// <param name="r"></param>
		public static void ReassignID(this IReferrable r)
		{
			r.ID = 0;
			Galaxy.Current.AssignID(r);
		}

		public static TRef Refer<TRef, T>(this T t) where TRef : IReference<T>
		{
			return (TRef)typeof(TRef).Instantiate(t);
		}

		public static GalaxyReference<T> ReferViaGalaxy<T>(this T t)
			where T : IReferrable
		{
			if (t == null)
				return null;
			return new GalaxyReference<T>(t);
		}

		public static ModReference<T> ReferViaMod<T>(this T t) where T : IModObject
		{
			if (t == null)
				return null;
			return new ModReference<T>(t);
		}

		/// <summary>
		/// Refills the space object's movement points.
		/// </summary>
		public static void RefillMovement(this IMobileSpaceObject sobj)
		{
			sobj.MovementRemaining = sobj.StrategicSpeed;
			sobj.TimeToNextMove = sobj.TimePerMove;
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
				if (order is IMovementOrder)
				{
					var o = (IMovementOrder)order;
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
		/// Income produced by this object's remote mining abilities.
		/// Modified by racial aptitudes.
		/// Not affected by lack of spaceports.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static ResourceQuantity RemoteMiningIncome(this IIncomeProducer o)
		{
			return o.Owner.RemoteMiners.Where(m => m.Key.Item1 == o).Sum(m => m.Value);
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
		public static RemoveOrderCommand RemoveOrderClientSide(this IOrderable obj, IOrder order)
		{
			if (Empire.Current == null)
				throw new InvalidOperationException("RemoveOrderClientSide is intended for client side use.");
			var addCmd = Empire.Current.Commands.OfType<AddOrderCommand>().SingleOrDefault(c => c.Order == order);
			if (addCmd == null)
			{
				// not a newly added order, so create a remove command to take it off the server
				var remCmd = new RemoveOrderCommand(obj, order);
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
		/// Resizes an image. The image should be square.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Image Resize(this Image image, int size)
		{
			if (image == null)
				return null;
			if (size == 0)
				return null;
			var result = new Bitmap(size, size, PixelFormat.Format32bppArgb);
			var g = Graphics.FromImage(result);
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			g.DrawImage(image, 0, 0, size, size);
			return result;
		}

		/// <summary>
		/// The result (victory/defeat/stalemate) for a given empire.
		/// If empire or its allies are not involved or no empire specified, just say "battle".
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static string ResultFor(this IBattle b, Empire emp)
		{
			if (emp == null)
				return "battle"; // no empire specified
			if (!b.Empires.Contains(emp) && !b.Empires.Any(e => e.IsAllyOf(emp, b.StarSystem)))
				return "battle"; // empire/allies not involved
			var survivors = b.Combatants.Where(c => c.IsAlive);
			var ourSurvivors = survivors.Where(c => c.Owner == emp);
			var allySurvivors = survivors.Where(c => c.Owner.IsAllyOf(emp, b.StarSystem));
			var friendlySurvivors = ourSurvivors.Concat(allySurvivors);
			var enemySurvivors = survivors.Where(c => c.Owner.IsEnemyOf(emp, b.StarSystem));
			if (friendlySurvivors.Any() && enemySurvivors.Any())
				return "stalemate";
			if (friendlySurvivors.Any())
				return "victory";
			if (enemySurvivors.Any())
				return "defeat";
			return "Pyrrhic victory"; // mutual annihilation!
		}

		public static IEnumerable<TOut> RunTasks<TOut>(this IEnumerable<Func<TOut>> ops)
		{
			return ops.SpawnTasksAsync().Result;
		}

		public static IEnumerable<TOut> RunTasks<TIn, TOut>(this IEnumerable<TIn> objs, Func<TIn, TOut> op)
		{
			return objs.SpawnTasksAsync(op).Result;
		}

		public static void SafeDispose(this IDisposable d)
		{
			if (d != null)
				d.Dispose();
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

		public static bool SafeSequenceEqual<T>(this IEnumerable<T> e1, IEnumerable<T> e2)
		{
			if (e1.SafeEquals(null) && e2.SafeEquals(null))
				return true;
			if (e1.SafeEquals(null) || e2.SafeEquals(null))
				return false;
			return e1.SequenceEqual(e2);
		}

		public static void SetData(this object o, SafeDictionary<string, object> dict, ObjectGraphContext context)
		{
			if (context == null)
				context = new ObjectGraphContext();
			if (o is IDataObject)
			{
				// use data object code! :D
				var dobj = (IDataObject)o;
				dobj.Data = dict;
			}
			else if (o != null)
			{
				// use reflection :(
				foreach (var kvp in dict)
				{
					var pname = kvp.Key;
					var val = kvp.Value;
					var prop = ObjectGraphContext.GetKnownProperties(o.GetType())[pname];
					if (prop != null)
					{
						try
						{
							context.SetObjectProperty(o, prop, val);
						}
						catch (NullReferenceException)
						{
							if (o == null && prop == null)
								Console.Error.WriteLine($"Attempted to set unknown property {pname} on a null object.");
							else if (o == null)
								Console.Error.WriteLine($"Attempted to set property {pname} on a null object.");
							else if (prop == null)
								Console.Error.WriteLine($"Attempted to set unknown property {pname} on {o}.");
							else
								throw;
						}
						catch (InvalidCastException)
						{
							Console.Error.WriteLine($"Could not set property {pname} of object {o} of type {o.GetType()} to value {val} of type {val.GetType()}.");
							throw;
						}
					}
					else
						Console.Error.WriteLine($"Found unknown property {pname} in serialized data for object type {o.GetType()}.");
				}
			}
			else
				throw new NullReferenceException("Can't set data on a null object.");
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
		/// Spawns multiple tasks to return an enumeration of items.
		/// </summary>
		/// <typeparam name="TOut"></typeparam>
		/// <param name="ops"></param>
		/// <param name="process"></param>
		/// <returns></returns>
		public static async Task<IEnumerable<TOut>> SpawnTasksAsync<TOut>(this IEnumerable<Func<TOut>> ops)
		{
			// Enumerate the tasks we need to do and start them
			var tasks = ops.Select(op => Task<TOut>.Factory.StartNew(op));

			// Wait for them to complete
			return await Task.WhenAll(tasks);
		}

		/// <summary>
		/// Spawns multiple tasks to return an enumeration of items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objs"></param>
		/// <param name="op"></param>
		/// <returns></returns>
		public static async Task<IEnumerable<TOut>> SpawnTasksAsync<TIn, TOut>(this IEnumerable<TIn> objs, Func<TIn, TOut> op)
		{
			return await objs.Select(obj => new Func<TOut>(() => op(obj))).SpawnTasksAsync();
		}

		/// <summary>
		/// Spawns multiple tasks to perform a bunch of actions.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ops"></param>
		/// <param name="process"></param>
		/// <returns></returns>
		public static async Task SpawnTasksAsync(this IEnumerable<Action> ops)
		{
			// Enumerate the tasks we need to do and start them
			var tasks = ops.Select(op => Task.Factory.StartNew(op));

			// Wait for them to complete
			await Task.WhenAll(tasks);
		}

		/// <summary>
		/// Spawns multiple tasks to perform a bunch of actions.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objs"></param>
		/// <param name="op"></param>
		/// <returns></returns>
		public static async Task SpawnTasksAsync<TIn>(this IEnumerable<TIn> objs, Action<TIn> op)
		{
			await objs.Select(obj => new Action(() => op(obj))).SpawnTasksAsync();
		}

		/// <summary>
		/// Standard income provided by mining, research, and intelligence.
		/// Affected by racial aptitudes, happiness, planet value, lack of spaceport, that sort of thing.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static ResourceQuantity StandardIncome(this IIncomeProducer o)
		{
			var income = new ResourceQuantity();
			var ratio = 1.0;
			if (!o.StarSystem.HasAbility(o.Owner, "Spaceport"))
				ratio = o.MerchantsRatio;
			var prefix = "Resource Generation - ";
			var pcts = o.StandardIncomePercentages;
			foreach (var abil in o.Abilities().Where(abil => abil.Rule.Name.StartsWith(prefix)))
			{
				var resource = Resource.Find(abil.Rule.Name.Substring(prefix.Length));
				var amount = abil.Value1.ToInt();

				if (resource.HasValue)
					amount = Galaxy.Current.StandardMiningModel.GetRate(amount, o.ResourceValue[resource], pcts[resource] / 100d);

				income.Add(resource, amount);
			}
			prefix = "Point Generation - ";
			foreach (var abil in o.Abilities().Where(abil => abil.Rule.Name.StartsWith(prefix)))
			{
				var resource = Resource.Find(abil.Rule.Name.Substring(prefix.Length));
				var amount = abil.Value1.ToInt() * pcts[resource] / 100;

				income.Add(resource, amount);
			}

			return income * ratio;
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

		/// <summary>
		/// Inflicts damage on an object.
		/// </summary>
		/// <returns>Leftover damage.</returns>
		public static int TakeDamage(this IDamageable d, DamageType dt, int damage, PRNG dice = null)
		{
			return d.TakeDamage(new Hit(dt, damage, d), dice);
		}

		/// <summary>
		/// Inflicts damage on an object.
		/// </summary>
		/// <returns>Leftover damage.</returns>
		public static int TakeDamage(this IDamageable d, string damageTypeName, int damage, PRNG dice = null)
		{
			return d.TakeDamage(Mod.Current.DamageTypes.FindByName(damageTypeName), damage, dice);
		}

		/// <summary>
		/// Inflicts normal damage on an object out of the blue.
		/// </summary>
		/// <param name="d">The object which should take damage.</param>
		/// <param name="dmg">The amount of normal damage to inflict.</param>
		/// <returns>Leftover damage.</returns>
		public static int TakeNormalDamage(this IDamageable d, int dmg, PRNG dice = null)
		{
			return d.TakeDamage("Normal", dmg, dice);
		}

		public static int TakeShieldDamage(this IDamageable d, Hit hit, int damage, PRNG dice = null)
		{
			// TODO - make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop
			int shieldDmg = 0;
			var dt = hit.Shot?.DamageType ?? DamageType.Normal;
			int normalShieldPiercing = dt.NormalShieldPiercing.Evaluate(hit);
			int phasedShieldPiercing = dt.PhasedShieldPiercing.Evaluate(hit);
			double normalSDF = dt.NormalShieldDamage.Evaluate(hit).Percent();
			double phasedSDF = dt.PhasedShieldDamage.Evaluate(hit).Percent();

			// how much damage pierced the shields?
			double piercedShields = 0;

			if (d.NormalShields > 0)
			{
				var dmg = (int)Math.Min(damage * normalSDF, d.NormalShields);
				piercedShields += damage * normalShieldPiercing.Percent();
				d.NormalShields -= dmg;
				if (normalSDF != 0)
					damage -= (int)Math.Ceiling(dmg / normalSDF);
				shieldDmg += dmg;
			}
			if (d.PhasedShields > 0)
			{
				var dmg = (int)Math.Min(damage * phasedSDF, d.PhasedShields);
				piercedShields += damage * phasedShieldPiercing.Percent();
				d.PhasedShields -= dmg;
				if (phasedSDF != 0)
					damage -= (int)Math.Ceiling(dmg / phasedSDF);
				shieldDmg += dmg;
			}

			return damage;
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
		/// Transfers items from this cargo container to another cargo container.
		/// </summary>
		public static void TransferCargo(this ICargoContainer src, CargoDelta delta, ICargoContainer dest, Empire emp, bool overrideFreeSpace = false)
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
				if (!overrideFreeSpace)
				{
					// limit by amount of free space
					amount = Math.Min(amount, dest.PopulationStorageFree + (long)((dest.CargoStorage - dest.Cargo.Size) / Mod.Current.Settings.PopulationSize));
				}

				amount -= src.RemovePopulation(kvp.Key, amount);
				dest.AddPopulation(kvp.Key, amount);

				if (amount < kvp.Value)
					emp.Log.Add(src.CreateLogMessage(src + " could transfer only " + amount.ToUnitString(true) + " of the desired " + kvp.Value.ToUnitString(true) + " " + kvp.Key + " population to " + dest + " due to lack of population available or lack of storage space.", LogMessageType.Warning));
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
					emp.Log.Add(src.CreateLogMessage(src + " could transfer only " + amount.ToUnitString(true) + " of the desired " + kvp.Value.ToUnitString(true) + " general population to " + dest + " due to lack of population available or lack of storage space.", LogMessageType.Warning));

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
						// if it's not a "transfer all" order, we can log the lack of available units
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
						// if it's not a "transfer all" order, we can log the lack of available units
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
						// if it's not a "transfer all" order, we can log the lack of available units
						if (kvp.Value != null)
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

		/// <summary>
		/// Updates the memory sight cache of any empires that can see this object.
		/// Only makes sense on the host view, so if this is called elsewhere, nothing happens.
		/// </summary>
		/// <param name="obj">The object whose cache to update.</param>
		/// <param name="message">A message to display to any empire that can see this event happen.</param>
		/// <param name="empiresToSkipMessage">Empires to which we don't need to send a message.</param>
		/// <param name="stillExists"></param>
		public static void UpdateEmpireMemories<T>(this T obj, string message = null, params Empire[] empiresToSkipMessage)
			where T : IFoggable, IReferrable, IOwnable
		{
			if (Empire.Current == null)
			{
				foreach (var emp in Galaxy.Current.Empires)
				{
					var sys = (obj as ILocated)?.StarSystem;
					if (obj.CheckVisibility(emp) >= Visibility.Visible)
					{
						emp.UpdateMemory(obj);
						if (message != null && !empiresToSkipMessage.Contains(emp))
							emp.RecordLog(obj, message, LogMessageType.Generic);
					}
				}
			}
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

		private static T GetDefaultGeneric<T>()
		{
			return default(T);
		}

		private static void LogUnitTransferFailed(IDesign<IUnit> design, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + design + " class " + design.VehicleTypeName + "s could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full.", LogMessageType.Warning));
		}

		private static void LogUnitTransferFailed(string role, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + role + " units could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full.", LogMessageType.Warning));
		}

		private static void LogUnitTransferFailed(VehicleTypes vt, ICargoContainer src, ICargoContainer dest, int actualTonnage, int desiredTonnage, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage("Only " + actualTonnage.Kilotons() + " of " + desiredTonnage.Kilotons() + " worth of " + vt.ToSpacedString().ToLower() + "s could be transferred from " + src + " to " + dest + " because there are not enough in " + src + "'s cargo or " + dest + "'s cargo is full.", LogMessageType.Warning));
		}

		private static void LogUnitTransferFailedHostile(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage(unit + " could not be transferred from " + src + " to " + dest + " because " + unit + " is hostile.", LogMessageType.Warning));
		}

		private static void LogUnitTransferFailedNoStorage(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage(unit + " could not be transferred from " + src + " to " + dest + " because " + dest + "'s cargo is full.", LogMessageType.Warning));
		}

		private static void LogUnitTransferFailedNotPresent(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			emp.Log.Add(src.CreateLogMessage(unit + " could not be transferred from " + src + " to " + dest + " because it is not in " + src + "'s cargo.", LogMessageType.Warning));
		}

		private static void TryTransferUnit(IUnit unit, ICargoContainer src, ICargoContainer dest, Empire emp)
		{
			if (unit.IsHostileTo(emp))
				LogUnitTransferFailedHostile(unit, src, dest, emp);
			if (dest.CargoStorageFree() >= unit.Design.Hull.Size)
			{
				src.RemoveUnit(unit);
				dest.AddUnit(unit);
			}
			else
				LogUnitTransferFailedNoStorage(unit, src, dest, emp);
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
		/*/// <summary>
		/// XXX don't use this function, it seems to skip some of the tasks
		/// </summary>
		/// <param name="ops">The ops.</param>
		public static void RunTasks(this IEnumerable<Action> ops)
		{
			// http://stackoverflow.com/a/19193473/1159763
			// for some reason we can't just say ops.SpawnTasksAsync().Wait() as this causes a hang
			var runSync = Task.Factory.StartNew(new Func<Task>(async () =>
			{
				await ops.SpawnTasksAsync();
			})).Unwrap();
			runSync.Wait();
		}

		/// <summary>
		/// XXX don't use this function, it seems to skip some of the tasks
		/// </summary>
		/// <typeparam name="TIn">The type of the in.</typeparam>
		/// <param name="objs">The objs.</param>
		/// <param name="op">The op.</param>
		public static void RunTasks<TIn>(this IEnumerable<TIn> objs, Action<TIn> op)
		{
			// http://stackoverflow.com/a/19193473/1159763
			// for some reason we can't just say objs.SpawnTasksAsync(op).Wait() as this causes a hang
			var runSync = Task.Factory.StartNew(new Func<Task>(async () =>
			{
				await objs.SpawnTasksAsync(op);
			})).Unwrap();
			runSync.Wait();
		}*/

		public static void RecordLog<T>(this T t, string text, LogMessageType logMessageType) where T : IOwnable
		{
			t.Owner.RecordLog(t, text, logMessageType);
		}
	}

	public enum IDCopyBehavior
	{
		PreserveSource,
		PreserveDestination,
		Regenerate
	}
}
