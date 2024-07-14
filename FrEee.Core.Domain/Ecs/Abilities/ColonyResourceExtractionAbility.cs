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
    /// Allows a colony to produce resources based on its value, population, and happiness.
    /// </summary>
    public class ColonyResourceExtractionAbility(
		IEntity entity,
		AbilityRule rule,
		IFormula<string> resource,
		IFormula<int> rate
	) : ResourceRateAbility(entity, rule, resource, rate)
	{
		public ColonyResourceExtractionAbility(
			IEntity entity,
			AbilityRule rule,
			Formula<string>? description,
			IFormula[] values
		) : this(entity, rule, resource: values[0].ToStringFormula(), values[1].ToFormula<int>())
		{
		}

		public override StatType GetStatType(Resource resource) =>
			StatType.ColonyResourceExtraction(resource);

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			
			if (interaction is ProduceResourcesInteraction produceResources)
			{
				produceResources.ColonyResources.TryGetValue(Container, out var resources);
				resources ??= new();
				resources += Rate * Resource;
				produceResources.ColonyResources[Container] = resources;
			}
		}
	}
}
