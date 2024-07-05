using System;
using System.Collections.Generic;
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
    /// Modifies a stat of an entity.
    /// </summary>
    public class StatModifierAbility(
		IEntity entity,
		AbilityRule rule,
		Formula<string>? description,
		params IFormula[] values
	) : Ability(entity, rule, description, values)
	{
		public StatModifierAbility(IEntity entity, AbilityRule rule, string statName, IFormula<decimal> modifier)
			 : this(entity, rule, null, values: modifier)
		{
			StatName = statName;
		}

		public string StatName { get; protected init; }

		public IFormula<decimal> Modifier => Value1.ToFormula<decimal>();

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			if (interaction is GetStatNamesInteraction statNames)
			{
				statNames.StatNames.Add(StatName);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.Name == StatName)
			{
				// TODO: support other operations
				getStatValue.Stat.Modifiers.Add(new Modifier(Entity, Operation.Add, Modifier.Value));
			}
		}
	}
}
