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
    /// Increases the accuracy of this combatant's direct fire weapons.
    /// </summary>
    public class AccuracyAbility(
		IEntity entity,
		AbilityRule rule,
		IFormula<int> accuracy,
		IFormula<string>? group = null
	) : StatModifierAbility(entity, rule, StatType.Accuracy, Operation.Add, accuracy.ToFormula<decimal>(), group)
	{
		public AccuracyAbility(
			IEntity entity,
			AbilityRule rule,
			IFormula[] values
		) : this(entity, rule, accuracy: values[0].ToFormula<int>(), group: values.Length > 1 ? values[1]?.ToStringFormula() : null)
		{
		}

		public IFormula<int> Accuracy { get; private set; } = accuracy;

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);

			if (interaction is FireWeaponInteraction fire && (Container == fire.Source || Container.Ancestors().Contains(fire.Source)))
			{
				fire.Accuracy = Operation.Aggregate(fire.Accuracy, [Accuracy.Value]).RoundTo<int>();
			}
		}
	}
}
