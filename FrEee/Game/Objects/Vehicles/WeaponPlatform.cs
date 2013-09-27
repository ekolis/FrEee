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

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			// weapon platforms cannot be targeted in space combat
			get { return Enumerations.WeaponTargets.Invalid; }
		}
	}
}
