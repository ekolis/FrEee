using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Interactions;
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
	public class SpaceYardAbility(IAbilityObject container, AbilityRule rule, Formula<string>? description, params Formula<string>[] values)
		// TODO: don't hardcode ability rule names
		: Ability(container, AbilityRule.Find("Space Yard"), description, values)
	{
		public string GetStatName(Resource resource) =>
			$"Space Yard Rate {resource.Name}";

		public SpaceYardAbility(IAbilityObject container, AbilityRule rule, Resource resource, int rate)
			 : this(container, rule, null, resource.Number.ToString(), rate.ToString())
		{
			Resource = resource;
			Rate = rate;
		}

		// TODO: resource and rate should be formulas
		public Resource Resource { get; private set; } = Resource.Find(values[0]);

		public Formula<int> Rate { get; private set; } = values[1].ToInt();

		public override void Interact(IInteraction interaction)
		{
			if (interaction is GetStatNamesInteraction getStatNames)
			{
				getStatNames.StatNames.Add(GetStatName(Resource));
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.Name == GetStatName(Resource))
			{
				getStatValue.Stat.Values.Add(Rate);
			}
		}
	}
}
