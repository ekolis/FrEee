using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using System;
using System.Linq;

namespace FrEee.Objects.Vehicles
{
	[Serializable]
	public class Ship : MajorSpaceVehicle
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Ship; }
		}

		public override bool CanWarp
		{
			get { return !Owner?.IsMinorEmpire ?? true; }
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}

		public override bool RequiresSpaceYardQueue
		{
			get { return true; }
		}

		public override WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Ship; }
		}
	}
}
