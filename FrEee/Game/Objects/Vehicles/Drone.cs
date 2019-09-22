using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Drone : SpaceVehicle, IUnit
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Drone; }
		}

		public override bool CanWarp
		{
			get { return true; }
		}

		ICargoContainer IContainable<ICargoContainer>.Container
		{
			get { return Utility.Extensions.CommonExtensions.FindContainer(this); }
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}

		public override IMobileSpaceObject RecycleContainer
		{
			get { return (this as IUnit).Container as IMobileSpaceObject; }
		}

		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			get { return Enumerations.WeaponTargets.Drone; }
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			var vis = base.CheckVisibility(emp);
			var sobj = Container as ISpaceObject;
			if (sobj != null && sobj.CheckVisibility(emp) >= Visibility.Scanned)
				vis = Visibility.Scanned;
			return vis;
		}

		public override void Place(ISpaceObject target)
		{
			Utility.Extensions.CommonExtensions.Place(this, target);
		}

		public override bool FillsCombatTile => false;
	}
}