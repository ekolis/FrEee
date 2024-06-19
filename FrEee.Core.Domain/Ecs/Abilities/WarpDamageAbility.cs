using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
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
    /// Damages vehicles that warp through a warp point.
    /// </summary>
    public class WarpDamageAbility(IAbilityObject container, AbilityRule rule, Formula<string>? description, params IFormula[] values)
		// TODO: don't hardcode ability rule names
		: Ability(container, rule, description, values)
	{
		public const string StatName = "Warp Damage";

		public WarpDamageAbility(IAbilityObject container, AbilityRule rule, Formula<int> damage)
			 : this(container, rule, null, damage.ToStringFormula())
		{
			Damage = damage;
		}

		public Formula<int> Damage { get; private set; }

		public override void Interact(IInteraction interaction)
		{
			if (interaction is WarpInteraction warp)
			{
				var sobj = warp.WarpingVehicle;
				sobj.TakeNormalDamage(Damage);
				if (sobj.IsDestroyed)
				{
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " was destroyed by turbulence when traversing " + warp.WarpPoint + ".", LogMessageType.Generic));
				}
				else
				{
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + Damage + " points of damage from turbulence when traversing " + warp.WarpPoint + ".", LogMessageType.Generic));
				}
			}
			if (interaction is GetStatNamesInteraction getStatNames)
			{
				getStatNames.StatNames.Add(StatName);
			}
			if (interaction is GetStatValueInteraction getStatValue && getStatValue.Stat.Name == StatName)
			{
				getStatValue.Stat.Values.Add(Damage);
			}
		}
	}
}
