using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// Extensions for checking features of entities based on their abilities.
/// </summary>
public static class FeatureExtensions
{
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

	/// <summary>
	/// Gets the semantic scopes of an entity.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static IEnumerable<SemanticScope> GetSemanticScopes(this IEntity entity) =>
		entity.GetAbilities<SemanticScopeAbility>().Select(q => q.Scope).Distinct().Append(SemanticScope.Entity);

	/// <summary>
	/// Determines if an entity has a particular semantic scope.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="scope"></param>
	/// <returns></returns>
	public static bool HasSemanticScope(this IEntity entity, SemanticScope scope) =>
		entity.GetSemanticScopes().Contains(scope);
}
