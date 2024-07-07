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
    /// Damages vehicles that warp through a warp point.
    /// </summary>
	
    public class WarpDamageAbility : StatModifierAbility
	{
		public WarpDamageAbility(
			IEntity entity,
			AbilityRule rule,
			Formula<string>? description,
			IFormula[] values
		) : this(entity, rule, damage: values[0].ToFormula<int>())
		{
		}

		public WarpDamageAbility(IEntity entity, AbilityRule rule, IFormula<int> damage)
			 : base(entity, rule, statName: StatType.WarpDamage.Name, damage.ToFormula<decimal>())
		{
			StatName = "Warp Damage";
		}

		public Formula<int> Damage { get; private set; }

		public override void Interact(IInteraction interaction)
		{
			base.Interact(interaction);

			if (interaction is WarpInteraction warp)
			{
				// TODO: move this to WarpInteraction.Execute and have the WarpInteraction just get the damage amount here
				var sobj = warp.WarpingVehicle;
				if (sobj is not null)
				{
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
			}
		}
	}
}
