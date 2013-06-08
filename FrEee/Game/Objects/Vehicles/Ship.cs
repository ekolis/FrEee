using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Ship : AutonomousSpaceVehicle
	{
		public override WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Ship; }
		}
	}
}
