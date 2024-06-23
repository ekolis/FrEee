using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities
{
    /// <summary>
    /// Holds entities that have a <see cref="HoldableAbility"/> with the appopriate category.
    /// </summary>
    class HolderAbility
		: Ability
	{
		public HolderAbility
		(
			IEntity container,
			AbilityRule rule,
			Formula<string>? description,
			params IFormula[] values
		) : base(container, rule, description, values)
		{
			Category = values[0].ToFormula<string>();
			Capacity = values[1].ToFormula<int>();
		}

		public HolderAbility
		(
			IEntity container,
			AbilityRule rule,
			IFormula<string> category,
			IFormula<int> capacity
		) : this(container, rule, null, category, capacity)
		{ }

		/// <summary>
		/// Category describing what can be held.
		/// </summary>
		public IFormula<string> Category { get; private set; }

		/// <summary>
		/// The maximum number or size capacity of items which can be held.
		/// </summary>
		public IFormula<int> Capacity { get; private set; }

		/// <summary>
		/// The currently held abilities.
		/// </summary>
		// TODO: validate that held abilities have the right category
		public IList<HoldableAbility> HeldAbilities { get; private set; } = new List<HoldableAbility>();

		/// <summary>
		/// The currently held entities.
		/// </summary>
		public IEnumerable<IEntity> HeldEntities => HeldAbilities.Select(q => q.Container);

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data["HeldAbilities"] = HeldAbilities;
				return data;
			}
			set
			{
				base.Data = value;
				Capacity = Value1.ToFormula<int>();
				HeldAbilities = (IList<HoldableAbility>)value["HeldAbilities"];
			}
		}

		public override void Interact(IInteraction interaction)
		{
			foreach (var held in HeldAbilities)
			{
				held.Interact(interaction);
			}
		}
	}
}
