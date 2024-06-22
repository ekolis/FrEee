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
	// TODO: make this a PartAbility so it can be reused for components (just have a string for the type of part)

	/// <summary>
	/// Allows an entity to be held by a <see cref="HoldFacilitiesAbility"/>.
	/// </summary>
	public class FacilityAbility
		// TODO: should inherit from a BuildableAbility
		: Ability
	{
		public FacilityAbility
		(
			IEntity container,
			AbilityRule rule,
			Formula<string>? description,
			params IFormula[] values
		) : base(container, rule, description, values)
		{
			Size = values[0].ToFormula<int>();
		}

		public FacilityAbility
		(
			IEntity container,
			IFormula<int> size
		) : this(container, AbilityRule.Find("Facility"), null, size)
		{ }

		public const string SizeStatName = "Facility Size";

		/// <summary>
		/// The number of slots or amount of space consumed by this facility.
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
