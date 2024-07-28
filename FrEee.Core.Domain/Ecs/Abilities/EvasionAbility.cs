using System;
using System.Collections.Generic;
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
    /// Decreases the accuracy of enemy direct fire weapons fired at this combatant.
    /// </summary>
    public class EvasionAbility(
		IEntity entity,
		AbilityRule rule,
		IFormula<int> evasion,
		IFormula<string>? group = null
	) : StatModifierAbility(entity, rule, StatType.Evasion, Operation.Add, evasion.ToFormula<decimal>(), group)
	{
		public EvasionAbility(
			IEntity entity,
			AbilityRule rule,
			IFormula[] values
		) : this(entity, rule, evasion: values[0].ToFormula<int>(), values.Length > 1 ? values[1]?.ToStringFormula() : null)
		{
		}

		public IFormula<int> Evasion { get; private set; } = evasion;

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);

			if (interaction is FireWeaponInteraction fire && (Container == fire.Target || Container.Ancestors().Contains(fire.Target)))
			{
				fire.Evasion = Operation.Aggregate(fire.Evasion, [Evasion.Value]).RoundTo<int>();
			}
		}
	}
}
