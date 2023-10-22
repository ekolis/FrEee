using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;

namespace FrEee.Extensions
{
	/// <summary>
	/// All extensions dealing strictly with game-specific enumerables.
	/// </summary>
	public static class GameEnumerableExtensions
	{
		/// <summary>
		/// Filters a list to objects belonging to a specific empire.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static IEnumerable<T> BelongingTo<T>(this IEnumerable<T> list, Empire emp) where T : IOwnable
		{
			return list.Where(t => t.Owner == emp);
		}

		/// <summary>
		/// Removes points within a certain eight way distance of a certain point.
		/// </summary>
		/// <param name="points">The points to start with.</param>
		/// <param name="center">The point to block out.</param>
		/// <param name="distance">The distance to block out from the center.</param>
		/// <returns>The points that are left.</returns>
		public static IEnumerable<Point> BlockOut(this IEnumerable<Point> points, Point center, int distance)
		{
			foreach (var p in points)
			{
				if (center.EightWayDistance(p) > distance)
					yield return p;
			}
		}

		public static IEnumerable<T> FindAllByName<T>(this IEnumerable<T> stuff, string name) where T : INamed
		{
			return stuff.Where(item => item.Name == name);
		}

		public static T FindByModID<T>(this IEnumerable<T> items, string modID)
							where T : IModObject
		{
			if (modID == null)
				return default;
			// should be SingleOrDefault for these three checks but FirstOrDefault is faster and we shouldn't really have duplicates to begin with
			var result = items.FirstOrDefault(item => item.ModID == modID);
			if (result == null)
				result = items.FirstOrDefault(item => item.ModID.Substring(item.ModID.IndexOf(" ") + 1) == modID);
			if (result == null)
				result = items.FirstOrDefault(item => item.ModID == modID.Substring(modID.IndexOf(" ") + 1));
			return result;
		}

		public static T FindByName<T>(this IEnumerable<T> stuff, string name) where T : INamed
		{
			return stuff.FirstOrDefault(item => item.Name == name);
		}

		public static TValue FindByName<TKey, TValue>(this IDictionary<TKey, TValue> dict, string name)
							where TKey : INamed
		{
			return dict[dict.Keys.FindByName(name)];
		}

		public static T FindByTypeNameIndex<T>(this IEnumerable<T> items, Type type, string name, int index)
							where T : IModObject
		{
			return items.Where(item => item.GetType() == type && item.Name == name).ElementAtOrDefault(index);
		}

		public static T FindMatch<T>(this IEnumerable<T> items, T nu, IEnumerable<T> nuItems)
							where T : class, IModObject
		{
			return items.FindByModID(nu.ModID) ?? items.FindByTypeNameIndex(nu.GetType(), nu.Name, nuItems.GetIndex(nu));
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
				return default;
			return groups.First().Key;
		}

		/// <summary>
		/// Computes the max of each resource in a group of resource quantities.
		/// </summary>
		/// <param name="qs"></param>
		/// <returns></returns>
		public static ResourceQuantity MaxOfAllResources(this IEnumerable<ResourceQuantity> qs)
		{
			var result = new ResourceQuantity();
			foreach (var q in qs)
			{
				foreach (var kvp in q)
				{
					if (kvp.Value > result[kvp.Key])
						result[kvp.Key] = kvp.Value;
				}
			}
			return result;
		}

		public static ResourceQuantity MaxOfAllResources<T>(this IEnumerable<T> stuff, Func<T, ResourceQuantity> selector)
		{
			return stuff.Select(t => selector(t)).MaxOfAllResources();
		}

		public static IEnumerable<T> NewerVersions<T>(this IEnumerable<T> stuff, T me, Func<T, string> familySelector)
					where T : class
		{
			var foundme = false;
			foreach (var t in stuff)
			{
				if (familySelector(t) == familySelector(me))
				{
					// same family
					if (t == me)
						foundme = true;
					else if (foundme)
						yield return t; // it's later
				}
			}
		}

		public static IEnumerable<T> OlderVersions<T>(this IEnumerable<T> stuff, T me, Func<T, string> familySelector)
					where T : class
		{
			var foundme = false;
			foreach (var t in stuff)
			{
				if (familySelector(t) == familySelector(me))
				{
					// same family
					if (t == me)
						foundme = true;
					else if (!foundme)
						yield return t; // it's earlier
				}
			}
		}

		public static IEnumerable<T> OnlyLatestVersions<T>(this IEnumerable<T> stuff, Func<T, string> familySelector)
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
		/// Filters a list to objects that are owned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static IEnumerable<T> Owned<T>(this IEnumerable<T> list) where T : IOwnable
		{
			return list.Where(t => t.Owner != null);
		}

		/// <summary>
		/// Filters a list to objects belonging to a specific empire.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static IEnumerable<T> OwnedBy<T>(this IEnumerable<T> list, Empire emp) where T : IOwnable
		{
			return list.BelongingTo(emp);
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
		/// Stacks any abilities of the same type according to the current mod's stacking rules.
		/// Keeps the original abilities in a handy tree format under the stacked abilities
		/// so you can tell which abilities contributed to which stacked abilities.
		/// </summary>
		/// <param name="abilities"></param>
		/// <param name="stackTo">The object which should own the stacked abilities.</param>
		/// <returns></returns>
		public static ILookup<Ability, Ability> StackToTree(this IEnumerable<Ability> abilities, IAbilityObject stackTo)
		{
			// create result list
			var stacked = new List<Tuple<Ability, Ability>>();

			// group abilities by rule
			var grouped = abilities.GroupBy(a => a.Rule);

			foreach (var group in grouped)
			{
				if (group.Key == null)
					continue; // invalid ability rule

				// stack this ability group
				var lookup = group.Key.GroupAndStack(group, stackTo);

				foreach (var lgroup in lookup)
				{
					// create a merged ability with a generated description (since ability values are stacked when merged)
					var mergedAbility = new Ability(stackTo, lgroup.Key.Rule, description: null, values: lgroup.Key.Values);

					foreach (var abil in group)
					{
						// add this ability to the current group in the result list, using the merged ability as the key
						stacked.Add(Tuple.Create(mergedAbility, abil));
					}
				}
			}

			// create a lookup from the result list
			return stacked.ToLookup(t => t.Item1, t => t.Item2);
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

		/// <summary>
		/// Filters a list to objects that are unowned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static IEnumerable<T> Unowned<T>(this IEnumerable<T> list) where T : IOwnable
		{
			return list.BelongingTo(null);
		}
	}
}
