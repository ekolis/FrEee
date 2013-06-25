using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class WeaponPlatform : GroundUnit
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public bool CanTarget(ICombatObject target)
		{
			// TODO - alliances
			return target.Owner != Owner && Components.Any(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null && c.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType));
		}
	}
}
