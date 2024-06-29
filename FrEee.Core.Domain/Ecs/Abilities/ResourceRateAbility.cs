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
		Formula<string>? description,
		params IFormula[] values
	) : Ability(entity, rule, description, values)
	{
		public ResourceRateAbility(IEntity entity, AbilityRule rule, Formula<string> resource, Formula<int> rate)
			 : this(entity, rule, null, resource, rate)
		{
		}

		public abstract StatType GetStatType(Resource resource);

		public Formula<string> ResourceFormula => Values[0].ToStringFormula();

		public Formula<int> RateFormula => (Formula<int>)Values[1].ToFormula<int>();

		public Resource Resource => Resource.Find(ResourceFormula);

		public int Rate => RateFormula;

		public override void Interact(IInteraction interaction)
		{
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
