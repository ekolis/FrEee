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
using FrEee.Extensions;
using FrEee.Ecs;
using System.Collections.Immutable;
using FrEee.Ecs.Abilities;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using System.Numerics;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Utility;

namespace FrEee.Ecs;

/// <summary>
/// Extensions relating to abilities.
/// </summary>
public static class Extensions
{
	#region old stuff
	/// <summary>
	/// All abilities belonging to an object.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> Abilities(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
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

	public static ILookup<Ability, Ability> AbilityTree(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
	{
		return obj.UnstackedAbilities(true, sourceFilter).StackToTree(obj);
	}

	/// <summary>
	/// Gets any abilities that can be activated.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IDictionary<Ability, IEntity> ActivatableAbilities(this Vehicle v)
	{
		var dict = new Dictionary<Ability, IEntity>();
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
	public static IDictionary<Ability, IEntity> ActivatableAbilities(this Planet p)
	{
		var dict = new Dictionary<Ability, IEntity>();
		if (p.Colony == null)
			return dict;
		foreach (var f in p.Colony.FacilityAbilities.Where(f => !f.IsDestroyed))
		{
			foreach (var a in f.Container.Abilities())
			{
				if (a.Rule.IsActivatable)
					dict.Add(a, f.Container);
			}
		}
		return dict;
	}

	/// <summary>
	/// Gets any abilities that can be activated.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IDictionary<Ability, IEntity> ActivatableAbilities(this IEntity o)
	{
		if (o is Vehicle)
			return ((Vehicle)o).ActivatableAbilities();
		if (o is Planet)
			return ((Planet)o).ActivatableAbilities();

		var dict = new Dictionary<Ability, IEntity>();
		foreach (var a in o.Abilities())
		{
			if (a.Rule.IsActivatable)
				dict.Add(a, o);
		}
		return dict;
	}

	/// <summary>
	/// Abilities inherited from ancestor objects.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="includeShared"></param>
	/// <returns></returns>
	[Obsolete("Use ECS entity repository abilities, once they're added.")]
	public static IEnumerable<Ability> AncestorAbilities(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
	{
		var abils = new List<Ability>();
		foreach (var p in obj.Ancestors(sourceFilter).ExceptSingle(null))
			abils.AddRange(p.IntrinsicAbilities);
		return abils.Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
	}

	[Obsolete("Use ECS entity repository abilities, once they're added.")]
	public static IEnumerable<IEntity> Ancestors(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
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

	[Obsolete("Use ECS abilities instead.")]
	public static void ClearAbilityCache(this IEntity o)
	{
		Galaxy.Current.AbilityCache.Remove(o);
	}

	/// <summary>
	/// Abilities passed up from descendant objects.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="includeShared"></param>
	/// <returns></returns>
	[Obsolete("Use ECS entity repository abilities, once they're added.")]
	public static IEnumerable<Ability> DescendantAbilities(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
	{
		var abils = new List<Ability>();
		foreach (var c in obj.Descendants(sourceFilter))
			abils.AddRange(c.IntrinsicAbilities);
		return abils.Where(a => a.Rule == null || a.Rule.CanTarget(obj.AbilityTarget));
	}

	[Obsolete("Use ECS entity repository abilities, once they're added.")]
	public static IEnumerable<IEntity> Descendants(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
	{
		// we can't use a HashSet here because e.g. ships can have multiples of the same engine template installed
		List<IEntity> result = new();
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

	[Obsolete("Use ECS scoped abilities instead, once they're added.")]
	public static IEnumerable<Ability> EmpireAbilities(this ICommonEntity obj, Empire emp, Func<IEntity, bool> sourceFilter = null)
	{
		if (obj == null)
			return Enumerable.Empty<Ability>();

		if (sourceFilter == null)
		{
			var subobjs = obj.GetContainedEntities(emp);
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
			return obj.GetContainedEntities(emp).Where(o => sourceFilter(o)).SelectMany(o => o.Abilities()).Where(a => a.Rule.CanTarget(obj.AbilityTarget));
	}

	/// <summary>
	/// Finds empire-common abilities inherited by an object (e.g. empire abilities of a sector in which a ship resides).
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="sourceFilter"></param>
	/// <returns></returns>
	[Obsolete("Use ECS scoped abilities instead, once they're added.")]
	public static IEnumerable<Ability> EmpireCommonAbilities(this IEntity obj, Func<IEntity, bool> sourceFilter = null)
	{
		// Unowned objects cannot empire common abilities.
		var ownable = obj as IOwnableEntity;
		if (ownable == null || ownable.Owner == null)
			yield break;

		// Where are these abilities coming from?
		// Right now they can only come from ancestors, since sectors and star systems are the only common ability objects.
		// TODO - Would it make sense for them to come from descendants? What kind of common ability object could be used as a descendant of an owned object?
		var ancestors = obj.Ancestors(sourceFilter).OfType<ICommonEntity>();

		// What abilities do we have?
		foreach (var ancestor in ancestors)
		{
			foreach (var abil in ancestor.EmpireAbilities(ownable.Owner, sourceFilter))
				yield return abil;
		}
	}

	[Obsolete("Use ECS scoped abilities instead, once they're added.")]
	private static IEnumerable<Ability> FindSharedAbilities(this IOwnableEntity obj, ShareAbilityClause clause)
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
	[Obsolete("Use ECS scoped abilities instead, once they're added.")]
	public static string? GetEmpireAbilityValue(this ICommonEntity obj, Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
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
		var subabils = obj.GetContainedEntities(emp).SelectMany(o => o.UnstackedAbilities(true).Where(a => a.Rule.Name == name));
		if (obj is IEntity)
			abils = ((IEntity)obj).Abilities().Where(a => a.Rule != null && a.Rule.Name == name).Concat(subabils).Stack(obj);
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
	[Obsolete("Use ECS abilities instead.")]
	public static string GetAbilityValue(this IEntity obj, string name, int index = 1, bool includeShared = true, bool includeEmpireCommon = true, Func<Ability, bool> filter = null)
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

	[Obsolete("Use ECS abilities instead.")]
	public static string GetAbilityValue(this IEnumerable<IEntity> objs, string name, IEntity stackTo, int index = 1, bool includeShared = true, bool includeEmpireCommon = true, Func<Ability, bool> filter = null)
	{
		var tuples = objs.Squash(o => o.Abilities());
		if (includeShared)
			tuples = tuples.Union(objs.Squash(o => o.SharedAbilities()));
		if (includeEmpireCommon)
			tuples = tuples.Union(objs.Squash(o => o.EmpireCommonAbilities()));
		var abils = tuples.GroupBy(t => new { t.Item2.Rule, Object = t.Item1 }).Where(g => g.Key.Rule.Matches(name) && g.Key.Rule.CanTarget(g.Key.Object.AbilityTarget)).SelectMany(x => x).Select(t => t.Item2).Where(a => filter == null || filter(a)).Stack(stackTo);
		if (!abils.Any())
			return null;
		return abils.First().Values[index - 1];
	}

	/// <summary>
	/// Gets abilities that have been shared to an object.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<Ability> SharedAbilities(this IEntity obj, Func<IEntity, bool>? sourceFilter = null)
	{
		// Unowned objects cannot have abilities shared to them.
		var ownable = obj as IOwnableEntity;
		if (ownable == null || ownable.Owner == null)
			yield break;

		// update cache if necessary
		foreach (var clause in ownable.Owner.ReceivedTreatyClauses.Flatten().OfType<ShareAbilityClause>())
		{
			var tuple = Tuple.Create(ownable, clause.Owner);
			if (Empire.Current == null || !Galaxy.Current.SharedAbilityCache.ContainsKey(tuple))
				Galaxy.Current.SharedAbilityCache[tuple] = ownable.FindSharedAbilities(clause).ToArray();
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
	public static IEnumerable<Ability> SharedAbilities(this ICommonEntity obj, Empire empire, Func<IEntity, bool> sourceFilter = null)
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
	public static IEnumerable<Ability> UnstackedAbilities(this IEntity obj, bool includeShared, Func<IEntity, bool>? sourceFilter = null)
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
	#endregion

	#region General ability checks
	/// <summary>
	/// Checks if an entity has any abilities of a particular type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static bool HasAbility<T>(this IEntity entity)
		where T : IAbility
		=> entity.Abilities.OfType<T>().Any();

	/// <summary>
	/// Gets the single ability of a particular type belonging to an entity.
	/// Will throw if there is not exactly one ability of that type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static T GetAbility<T>(this IEntity entity)
		where T : IAbility
		=> entity.GetAbilities<T>().Single();

	/// <summary>
	/// Gets the single ability of a particular type belonging to an entity.
	/// Will return null if there is no ability of that type
	/// and throw if there is more than one.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static T? GetAbilityOrNull<T>(this IEntity entity)
		where T : IAbility
		=> entity.GetAbilities<T>().SingleOrDefault();

	/// <summary>
	/// Gets the abilities of a particular type belonging to an entity.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static IEnumerable<T> GetAbilities<T>(this IEntity entity)
		where T : IAbility
		=> entity.Abilities.OfType<T>();
	#endregion

	#region Ability/feature checks
	/// <summary>
	/// Determines if an entity has a space yard.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static bool HasSpaceYard(this IEntity entity)
	{
		return
			entity.HasStat(StatType.SpaceYardRateMinerals)
			|| entity.HasStat(StatType.SpaceYardRateOrganics)
			|| entity.HasStat(StatType.SpaceYardRateRadioactives);
	}

	/// <summary>
	/// Determines if the entity is ownable.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static bool IsOwnable(this IEntity entity) =>
		entity.HasAbility<OwnableAbility>();

	public static IEnumerable<SemanticScope> GetSemanticScopes(this IEntity entity) =>
		entity.GetAbilities<SemanticScopeAbility>().Select(q => q.Scope).Distinct().Append(SemanticScope.Entity);
	#endregion

	#region Get/set ability values
	/// <summary>
	/// Gets the owner of an entity.
	/// If it is not ownable, returns null.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static Empire? GetOwner(this IEntity entity) =>
		entity.GetAbilityOrNull<OwnableAbility>()?.Owner;

	/// <summary>
	/// Sets the owner of an entity.
	/// If it is not ownable, throws an exception.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="owner"></param>
	public static void SetOwner(this IEntity entity, Empire? owner) =>
		entity.GetAbility<OwnableAbility>().Owner = owner;

	public static ResourceQuantity GetColonyResourceExtraction(this IEntity entity)
	{
		var extractResources = new ExtractResourcesFromColoniesInteraction(entity.GetOwner());
		entity.Interact(extractResources);
		return extractResources.Resources.Sum(q => q.Value);
	}
	#endregion

	#region Semantic scope
	/// <summary>
	/// Determines if an entity has a particular semantic scope.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="scope"></param>
	/// <returns></returns>
	public static bool HasSemanticScope(this IEntity entity, SemanticScope scope)
	{
		var abils = entity.GetAbilities<SemanticScopeAbility>();
		return abils.Any(q => q.Scope == scope);
	}
	#endregion
}
