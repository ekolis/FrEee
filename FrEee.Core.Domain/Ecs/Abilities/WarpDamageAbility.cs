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

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Damages vehicles that warp through a warp point.
	/// </summary>
	public class WarpDamageAbility(IAbilityObject container, AbilityRule rule, string? description, params string[] values)
		: Ability(container, AbilityRule.Find("Warp Point - Turbulence"), description, values)
	{
		public WarpDamageAbility(IAbilityObject container, AbilityRule rule, int damage)
			 : this(container, rule, null, damage.ToString())
		{
			Damage = damage;
		}

		public int Damage { get; private set; }

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
			if (interaction is GetStatsInteraction getStats)
			{
				getStats.AddValue("Warp Damage", Damage);
				// TODO: load stacking rule from mod somehow
				getStats.SetStackingRule("Warp Damage", new AdditionStackingRule());
			}
		}
	}
}
