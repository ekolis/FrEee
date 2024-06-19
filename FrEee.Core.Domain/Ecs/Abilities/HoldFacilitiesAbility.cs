using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Allows an entity to hold facilities.
	/// </summary>
	class HoldFacilitiesAbility
		: Ability
	{
		public HoldFacilitiesAbility
		(
			IAbilityObject container,
			AbilityRule rule,
			Formula<string>? description,
			params IFormula[] values
		) : base(container, rule, description, values)
		{
			Capacity = values[0].ToFormula<int>();
		}

		/// <summary>
		/// The maximum number of facilities which can be held.
		/// </summary>
		public IFormula<int> Capacity { get; private set; }

		/// <summary>
		/// The currently held facilities.
		/// </summary>
		public IList<Facility> Facilities { get; private set; } = new List<Facility>();

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data["Facilities"] = Facilities;
				return data;
			}
			set
			{
				base.Data = value;
				Capacity = Value1.ToFormula<int>();
				Facilities = (IList<Facility>)value["Facilities"];
			}
		}
	}
}
