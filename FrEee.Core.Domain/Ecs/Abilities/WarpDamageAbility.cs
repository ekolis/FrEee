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

namespace FrEee.Ecs.Abilities
{
    /// <summary>
    /// Damages vehicles that warp through a warp point.
    /// </summary>
    public class WarpDamageAbility(IAbilityObject container, int damage)
        : Ability(container, AbilityRule.Find("Warp Point - Turbulence"), null, damage)
    {
        public int Damage { get; private set; } = damage;

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
        }
    }
}
