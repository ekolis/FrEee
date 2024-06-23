using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Defines the scope of an entity within the game universe.
	/// </summary>
	public class SemanticScopeAbility(
		IEntity container,
		AbilityRule rule,
		Formula<string>? description,
		params IFormula[] values
	) : Ability(container, rule, description, values)
	{
		public SemanticScopeAbility
		(
			IEntity container,
			IFormula<string> scope,
			IFormula<int> size
		) : this(container, AbilityRule.Find(scope.Value), null, scope, size)
		{ }

		public string SizeStatName => $"{Scope} Size";

		/// <summary>
		/// Scope of the entity in the game universe.
		/// An entity can have multiple scopes, perhaps corresponding to formerly inherited object types.
		/// For instance, Space Object, Vehicle, and Unit for a fighter.
		/// </summary>
		public IFormula<string> Scope { get; private set; } = values[0].ToFormula<string>();

		/// <summary>
		/// The number of slots or amount of space consumed by this entity
		/// when it is held by another entity via <see cref="HolderAbility"/>.
		/// </summary>
		public IFormula<int> Size { get; private set; } = values[1].ToFormula<int>();

		public override void Interact(IInteraction interaction)
		{
			if (interaction is GetStatNamesInteraction getStatNames)
			{
				getStatNames.StatNames.Add(SizeStatName);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.Name == SizeStatName)
			{
				getStatValue.Stat.Values.Add(Size.Value);
			}
		}
	}
}
