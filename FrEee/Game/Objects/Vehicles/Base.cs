using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Base : AutonomousSpaceVehicle
	{
		public override WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Base; }
		}

		/// <summary>
		/// Bases have infinite supplies.
		/// </summary>
		public override bool HasInfiniteSupplies
		{
			get
			{
				return true;
			}
		}
	}
}
