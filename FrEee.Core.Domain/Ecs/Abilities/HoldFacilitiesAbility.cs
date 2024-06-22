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
	// TODO: make this a HoldPartsAbility so it can be reused for components (just have a string for the type of part)

    /// <summary>
    /// Holds entities that have a <see cref="FacilityAbility"/>.
    /// </summary>
    class HoldFacilitiesAbility
		: Ability
	{
		public HoldFacilitiesAbility
		(
			IEntity container,
			AbilityRule rule,
			Formula<string>? description,
			params IFormula[] values
		) : base(container, rule, description, values)
		{
			Capacity = values[0].ToFormula<int>();
		}

		public HoldFacilitiesAbility
		(
			IEntity container,
			AbilityRule rule,
			IFormula<int> capacity
		) : this(container, rule, null, capacity)
		{ }

		/// <summary>
		/// The maximum number of facilities which can be held.
		/// </summary>
		public IFormula<int> Capacity { get; private set; }

		/// <summary>
		/// The currently held facilities (abilities).
		/// </summary>
		public IList<FacilityAbility> FacilityAbilities { get; private set; } = new List<FacilityAbility>();

		/// <summary>
		/// The currently held facilities (entities).
		/// </summary>
		public IEnumerable<IEntity> Facilities => FacilityAbilities.Select(q => q.Container);

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data["FacilityAbilities"] = FacilityAbilities;
				return data;
			}
			set
			{
				base.Data = value;
				Capacity = Value1.ToFormula<int>();
				FacilityAbilities = (IList<FacilityAbility>)value["FacilityAbilities"];
			}
		}

		public override void Interact(IInteraction interaction)
		{
			foreach (var facility in FacilityAbilities)
			{
				facility.Interact(interaction);
			}
		}
	}
}
