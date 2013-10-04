using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Fighter : SpaceVehicle
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			get { return Enumerations.WeaponTargets.Fighter; }
		}

		public override bool CanWarp
		{
			get { return false; }
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}
	}
}
