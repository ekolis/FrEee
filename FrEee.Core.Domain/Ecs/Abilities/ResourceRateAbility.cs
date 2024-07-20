using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Processes.Combat;
using FrEee.Utility;
using Microsoft.Scripting.Utils;

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Allows resources to be produced or consumed at a specific rate.
	/// </summary>
	public abstract class ResourceRateAbility(
		IEntity entity,
		AbilityRule rule,
		StatType statType,
		Operation operation,
		IFormula<string> resource,
		IFormula<int> rate
	) : StatModifierAbility(entity, rule, statType, operation, rate.ToFormula<decimal>())
	{
		public ResourceRateAbility(
			IEntity entity,
			AbilityRule rule,
			IFormula[] values
		) : this(entity, rule, null,Operation.Add, values[0].ToStringFormula(), values[1].ToFormula<int>())
		{
		}

		public abstract StatType GetStatType(Resource resource);

		public IFormula<string> ResourceFormula { get; protected set; } = resource;

		public IFormula<int> RateFormula { get; protected set; } = rate;

		public Resource Resource => Resource.Find(ResourceFormula.Value);

		public int Rate => RateFormula.Value;

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data[nameof(ResourceFormula)] = ResourceFormula;
				data[nameof(RateFormula)] = RateFormula;
				return data;
			}
			set
			{
				base.Data = value;
				ResourceFormula = (IFormula<string>)value[nameof(ResourceFormula)];
				RateFormula = (IFormula<int>)value[nameof(RateFormula)];
			}
		}

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
		}
	}
}
