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
		StatType statType,
		Operation operation,
		IFormula<decimal> amount
	) : Ability(entity, rule, values: [amount])
	{
		public StatModifierAbility(
			IEntity entity,
			AbilityRule rule,
			IFormula[] values
		) : this(entity, rule, statType: StatType.Unknown, operation: Operation.Add, amount: values[0].ToFormula<decimal>())
		{
		}

		public StatType StatType { get; protected set; } = statType;

		public Operation Operation => Operation.Find(OperationName);

		public string OperationName { get; private set; } = (operation ?? Operation.Add).Name;

		public IFormula<decimal> Modifier => Value1.ToFormula<decimal>();

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);
			if (interaction is GetStatNamesInteraction statNames)
			{
				statNames.StatNames.Add(StatType.Name);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.StatType == StatType)
			{
				getStatValue.Stat.Modifiers.Add(new Modifier(Container, Operation, Modifier.Value));
			}
		}

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data[nameof(StatType)] = StatType;
				data[nameof(OperationName)] = OperationName;
				return data;
			}
			set
			{
				base.Data = value;
				StatType = (StatType)value[nameof(StatType)];
				OperationName = (string)value[nameof(OperationName)];
			}
		}
	}
}
