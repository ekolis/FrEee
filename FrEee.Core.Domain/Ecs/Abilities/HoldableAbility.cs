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
	/// Allows an entity to be held by a <see cref="HolderAbility"/> with the appropriate category.
	/// </summary>
	public class HoldableAbility
		: Ability
	{
		public HoldableAbility
		(
			IEntity container,
			AbilityRule rule,
			Formula<string>? description,
			params IFormula[] values
		) : base(container, rule, description, values)
		{
			Category = values[0].ToFormula<string>();
			Size = values[1].ToFormula<int>();
		}

		public HoldableAbility
		(
			IEntity container,
			IFormula<string> category,
			IFormula<int> size
		) : this(container, AbilityRule.Find(category.Value), null, category, size)
		{ }

		public string SizeStatName => $"{Category} Size";

		/// <summary>
		/// Category describing what the entity is considered to be when held.
		/// </summary>
		public IFormula<string> Category { get; private set; }

		/// <summary>
		/// The number of slots or amount of space consumed by this holdable.
		/// </summary>
		public IFormula<int> Size { get; private set; }

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
