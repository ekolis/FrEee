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
    /// Allows an entity to construct ships, bases, and units.
    /// </summary>
    public class SpaceYardAbility(
		IEntity entity,
		AbilityRule rule,
		IFormula<string> resource,
		IFormula<int> rate
		// TODO: pass in resource formula to ResourceRateAbility
	) : ResourceRateAbility(entity, rule, StatType.SpaceYardRate(resource.Value), Operation.Highest, resource, rate)
	{
		public SpaceYardAbility(
			IEntity entity,
			AbilityRule rule,
			Formula<string>? description,
			IFormula[] values
		) : this(entity, rule, resource: values[0].ToStringFormula(), values[1].ToFormula<int>())
		{
		}

		public override StatType GetStatType(Resource resource) =>
			StatType.SpaceYardRate(resource);

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			// TODO: build interaction
		}
	}
}
