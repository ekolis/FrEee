using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Defines the scope of an entity within the game universe.
	/// </summary>
	public class SemanticScopeAbility(
		IEntity entity,
		AbilityRule rule,
		IFormula<string> scope,
		IFormula<int> size
	) : Ability(entity, rule, [scope, size])
	{
		public SemanticScopeAbility(
			IEntity entity,
			AbilityRule rule,
			params IFormula[] values
		) : this
		(
			entity,
			rule,
			values[0].ToFormula<string>(),
			values[1].ToFormula<int>()
		)
		{
		}

		public string SizeStatName => $"{Scope} Size";

		/// <summary>
		/// Scope of the entity in the game universe.
		/// An entity can have multiple scopes, perhaps corresponding to formerly inherited object types.
		/// For instance, Space Object, Vehicle, and Unit for a fighter.
		/// </summary>
		public IFormula<string> ScopeFormula => Value1;

		public SemanticScope Scope => new(ScopeFormula.Value);

		/// <summary>
		/// The number of slots or amount of space consumed by this entity
		/// when it is held by another entity via <see cref="HolderAbility"/>.
		/// </summary>
		public IFormula<int> Size => size;

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			if (interaction is GetStatNamesInteraction getStatNames)
			{
				getStatNames.StatNames.Add(SizeStatName);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.Name == SizeStatName)
			{
				getStatValue.Stat.Modifiers.Add(new Modifier(Container, Operation.Add, Size.Value));
			}
		}
	}
}
