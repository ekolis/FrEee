using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;

namespace FrEee.Extensions;

/// <summary>
/// Extensions relating to abilities.
/// </summary>
public static class AbilityExtensions
{
	/// <summary>
	/// All abilities belonging to an object.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> Abilities(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		if (obj == null)
			return Enumerable.Empty<Ability>();

		if (sourceFilter is null && Galaxy.Current is not null && Galaxy.Current.IsAbilityCacheEnabled)
		{
			// use the ability cache
			if (Galaxy.Current.AbilityCache[obj] is null)
				Galaxy.Current.AbilityCache[obj] = obj.UnstackedAbilities(true, sourceFilter).Stack(obj).ToArray();
			return Galaxy.Current.AbilityCache[obj];
		}

		return obj.UnstackedAbilities(true, sourceFilter).Stack(obj);
	}

	public static ILookup<Ability, Ability> AbilityTree(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		return obj.UnstackedAbilities(true, sourceFilter).StackToTree(obj);
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

	public static Ability AddAbility(this IAbilityContainer obj, string abilityName, params object[] vals)
	{
		return obj.AddAbility(Mod.Current.AbilityRules.Single(r => r.Name == abilityName || r.Aliases.Contains(abilityName)), vals);
	}

	public static Ability AddAbility(this IAbilityContainer obj, AbilityRule rule, params object[] vals)
	{
		var a = new Ability(obj, rule, null, vals);
		obj.Abilities.Add(a);
		return a;
	}

	/// <summary>
	/// Abilities inherited from ancestor objects.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="includeShared"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> AncestorAbilities(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		var abils = new List<Ability>();
		foreach (var p in obj.Ancestors(sourceFilter).ExceptSingle(null))
			abils.AddRange(p.IntrinsicAbilities);
		return abils.Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
	}

	public static IEnumerable<IAbilityObject> Ancestors(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		if (obj == null)
			yield break;
		// TODO - filter out duplicate ancestors
		foreach (var p in obj.Parents.ExceptSingle(null))
		{
			if (p != null && (sourceFilter == null || sourceFilter(p)))
			{
				yield return p;
				foreach (var x in p.Ancestors(sourceFilter))
					yield return x;
			}
		}
	}

	/// <summary>
	/// Consumes supplies if possible.
	/// </summary>
	/// <param name="a">The ability consuming supplies.</param>
	/// <returns>true if successful or unnecessary, otherwise false</returns>
	public static bool BurnSupplies(this Ability a)
	{
		if (a.Container is Component comp)
			return comp.BurnSupplies();
		else
			return true; // other ability containers don't use supplies
	}

	public static void ClearAbilityCache(this IAbilityObject o)
	{
		Galaxy.Current.AbilityCache.Remove(o);
	}

	/// <summary>
	/// Abilities passed up from descendant objects.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="includeShared"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> DescendantAbilities(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		var abils = new List<Ability>();
		foreach (var c in obj.Descendants(sourceFilter))
			abils.AddRange(c.IntrinsicAbilities);
		return abils.Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
	}

	public static IEnumerable<IAbilityObject> Descendants(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		// we can't use a HashSet here because e.g. ships can have multiples of the same engine template installed
		List<IAbilityObject> result = new();
		if (obj != null)
		{
			foreach (var c in obj.Children)
			{
				if (c is not null && (sourceFilter is null || sourceFilter(c)))
				{
					result.Add(c);
					foreach (var x in c.Descendants(sourceFilter))
						result.Add(x);
				}
			}
		}
		return result;
	}

	public static IEnumerable<Ability> EmpireAbilities(this ICommonAbilityObject obj, Empire emp, Func<IAbilityObject, bool> sourceFilter = null)
	{
		if (obj == null)
			return Enumerable.Empty<Ability>();

		if (sourceFilter == null)
		{
			var subobjs = obj.GetContainedAbilityObjects(emp);
			Func<IEnumerable<Ability>> getabils = () =>
				subobjs.SelectMany(o => o.Abilities()).Where(a => a.Rule.CanTarget(obj.AbilityTarget));
			if (Galaxy.Current.IsAbilityCacheEnabled)
			{
				var tuple = Tuple.Create(obj, emp);
				if (Galaxy.Current.CommonAbilityCache[tuple] == null)
					Galaxy.Current.CommonAbilityCache[tuple] = getabils();
				return Galaxy.Current.CommonAbilityCache[tuple];
			}
			else
				return getabils();
		}
		else
			return obj.GetContainedAbilityObjects(emp).Where(o => sourceFilter(o)).SelectMany(o => o.Abilities()).Where(a => a.Rule.CanTarget(obj.AbilityTarget));
	}

	/// <summary>
	/// Finds empire-common abilities inherited by an object (e.g. empire abilities of a sector in which a ship resides).
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="sourceFilter"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> EmpireCommonAbilities(this IAbilityObject obj, Func<IAbilityObject, bool> sourceFilter = null)
	{
		// Unowned objects cannot empire common abilities.
		var ownable = obj as IOwnableAbilityObject;
		if (ownable == null || ownable.Owner == null)
			yield break;

		// Where are these abilities coming from?
		// Right now they can only come from ancestors, since sectors and star systems are the only common ability objects.
		// TODO - Would it make sense for them to come from descendants? What kind of common ability object could be used as a descendant of an owned object?
		var ancestors = obj.Ancestors(sourceFilter).OfType<ICommonAbilityObject>();

		// What abilities do we have?
		foreach (var ancestor in ancestors)
		{
			foreach (var abil in ancestor.EmpireAbilities(ownable.Owner, sourceFilter))
				yield return abil;
		}
	}

	private static IEnumerable<Ability> FindSharedAbilities(this IOwnableAbilityObject obj, ShareAbilityClause clause)
	{
		if (obj == null)
			yield break;

		var rule = clause.AbilityRule;
		if (rule.CanTarget(obj.AbilityTarget))
		{
			if (rule.CanTarget(AbilityTargets.Sector) && obj is ILocated locObj)
			{
				var sector = locObj.Sector;
				foreach (var emp in Galaxy.Current.Empires.Where(emp => emp != null))
				{
					foreach (var abil in sector.EmpireAbilities(emp))
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
					foreach (var abil in sys.EmpireAbilities(emp))
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
					foreach (var abil in Galaxy.Current.EmpireAbilities(emp))
					{
						if (rule == abil.Rule)
							yield return abil;
					}
				}
			}
		}
	}

	/// <summary>
	/// Aggregates abilities for an empire's space objects.
	/// </summary>
	/// <param name="emp"></param>
	/// <param name="name"></param>
	/// <param name="index"></param>
	/// <param name="filter"></param>
	/// <returns></returns>
	public static string? GetEmpireAbilityValue(this ICommonAbilityObject obj, Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
	{
		if (obj == null)
			return null;

		if (filter == null && Galaxy.Current.IsAbilityCacheEnabled)
		{
			// use the cache
			var cached = Galaxy.Current.CommonAbilityCache[Tuple.Create(obj, emp)];
			if (cached != null)
			{
				if (cached.Any())
					return cached.Where(x => x.Rule.Matches(name)).Stack(obj).FirstOrDefault()?.Values[index - 1];
				else
					return null;
			}
		}

		IEnumerable<Ability> abils;
		var subabils = obj.GetContainedAbilityObjects(emp).SelectMany(o => o.UnstackedAbilities(true).Where(a => a.Rule.Name == name));
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
	public static string GetAbilityValue(this IAbilityObject obj, string name, int index = 1, bool includeShared = true, bool includeEmpireCommon = true, Func<Ability, bool> filter = null)
	{
		if (obj == null)
			return null;

		var abils = obj.Abilities();
		if (includeShared)
			abils = abils.Union(obj.SharedAbilities());
		if (includeEmpireCommon)
			abils = abils.Union(obj.EmpireCommonAbilities());

		abils = abils.Where(a => a.Rule != null && a.Rule.Matches(name) && a.Rule.CanTarget(obj.AbilityTarget) && (filter == null || filter(a)));
		abils = abils.Stack(obj);
		if (!abils.Any())
			return null;
		return abils.First().Values[index - 1];
	}

	public static string GetAbilityValue(this IEnumerable<IAbilityObject> objs, string name, IAbilityObject stackTo, int index = 1, bool includeShared = true, bool includeEmpireCommon = true, Func<Ability, bool> filter = null)
	{
		var tuples = objs.Squash(o => o.Abilities());
		if (includeShared)
			tuples = tuples.Union(objs.Squash(o => o.SharedAbilities()));
		if (includeEmpireCommon)
			tuples = tuples.Union(objs.Squash(o => o.EmpireCommonAbilities()));
		var abils = tuples.GroupBy(t => new { Rule = t.Item2.Rule, Object = t.Item1 }).Where(g => g.Key.Rule.Matches(name) && g.Key.Rule.CanTarget(g.Key.Object.AbilityTarget)).SelectMany(x => x).Select(t => t.Item2).Where(a => filter == null || filter(a)).Stack(stackTo);
		if (!abils.Any())
			return null;
		return abils.First().Values[index - 1];
	}

	/// <summary>
	/// Gets abilities that have been shared to an object.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> SharedAbilities(this IAbilityObject obj, Func<IAbilityObject, bool>? sourceFilter = null)
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

	/// <summary>
	/// Gets abilities that have been shared to an object.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> SharedAbilities(this ICommonAbilityObject obj, Empire empire, Func<IAbilityObject, bool> sourceFilter = null)
	{
		if (obj == null)
			yield break;
		if (empire == null)
			yield break;

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
						foreach (var abil in sector.EmpireAbilities(emp, sourceFilter))
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
						foreach (var abil in sys.EmpireAbilities(emp, sourceFilter))
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
						foreach (var abil in Galaxy.Current.EmpireAbilities(emp, sourceFilter))
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
	/// All abilities belonging to an object, before stacking.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="includeShared"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> UnstackedAbilities(this IAbilityObject obj, bool includeShared, Func<IAbilityObject, bool>? sourceFilter = null)
	{
		if (obj == null)
			return Enumerable.Empty<Ability>();

		IEnumerable<Ability> result;
		var descendantAbilities = obj.DescendantAbilities(sourceFilter);
		var ancestorAbilities = obj.AncestorAbilities(sourceFilter);
		var sharedAbilities = obj.SharedAbilities(sourceFilter);
		if (sourceFilter == null || sourceFilter(obj))
			result = obj.IntrinsicAbilities.Concat(descendantAbilities).Concat(ancestorAbilities);
		else
			result = descendantAbilities.Concat(ancestorAbilities);

		if (includeShared)
			result = result.Concat(sharedAbilities);

		return result;
	}
}
