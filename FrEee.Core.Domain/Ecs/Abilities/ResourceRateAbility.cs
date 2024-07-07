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
		IFormula<string> resource,
		IFormula<int> rate
	) : Ability(entity, rule, null, [])
	{
		public ResourceRateAbility(
			IEntity entity,
			AbilityRule rule,
			Formula<string>? description,
			IFormula[] values
		) : this(entity, rule, values[0].ToStringFormula(), values[1].ToFormula<int>())
		{
		}

		public abstract StatType GetStatType(Resource resource);

		public IFormula<string> ResourceFormula => resource;

		public IFormula<int> RateFormula => rate;

		public Resource Resource => Resource.Find(ResourceFormula.Value);

		public int Rate => RateFormula.Value;

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			if (interaction is GetStatNamesInteraction getStatNames)
			{
				getStatNames.StatNames.Add(GetStatType(Resource).Name);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.StatType == GetStatType(Resource))
			{
				// TODO: support other operations
				getStatValue.Stat.Modifiers.Add(new Modifier(Entity, Operation.Add, Rate));
			}
		}
	}
}
