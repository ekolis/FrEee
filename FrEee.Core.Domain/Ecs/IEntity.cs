using FrEee.Ecs.Abilities;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Objects.GameState;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Ecs;

/// <summary>
/// An entity which can have abilities.
/// </summary>
public interface IEntity
	: IEntity<Ability>
{
	/// <summary>
	/// The type of ability target that this entity represents.
	/// </summary>
	[Obsolete("Ability targets will be irrelevant once all game objects are converted to IEntity.")]
	AbilityTargets AbilityTarget { get; }

	/// <summary>
	/// Child entities that can pass up abilities to this object.
	/// </summary>
	[Obsolete("Abilities should not be inherited directly between entities. Entities should instead be linked using linking ability pairs, such as HoldFacilitiesAbility and FacilityAbility.")]
	IEnumerable<IEntity> Children { get; }

	/// <summary>
	/// Parent entities from which this object can inherit abilities.
	/// </summary>
	[Obsolete("Abilities should not be inherited directly between entities. Entities should instead be linked using linking ability pairs, such as HoldFacilitiesAbility and FacilityAbility.")]
	IEnumerable<IEntity> Parents { get; }

	/// <summary>
	/// Any abilities belonging intrinsically to this entity.
	/// </summary>
	// TODO: rename to Abilities?
	IEnumerable<Ability> IntrinsicAbilities { get; }
}

public static class EntityExtensions
{
	/// <summary>
	/// Performs an <see cref="IInteraction"> on this entity via its abilities.
	/// </summary>
	/// <param name="interaction"></param>
	public static void Interact(this IEntity entity, IInteraction interaction)
	{
		foreach (var ability in entity.Abilities)
		{
			if (entity.GetSemanticScopes().Contains(ability.Rule.SemanticScope))
			{
				// if the held entity is has the same scope as the ability,
				// the ability is not inherited past that entity
				continue;
			}
			ability.Interact(interaction);
		}
	}
}