using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Ship : SpaceVehicle
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return true; }
		}

		public override WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Ship; }
		}

		public override bool CanWarp
		{
			get { return true; }
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}
	}
}
