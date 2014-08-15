using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Satellite : SpaceVehicle, IUnit
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			get { return Enumerations.WeaponTargets.Satellite; }
		}

		public override bool CanWarp
		{
			get { return false; }
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}

		public override void Place(ISpaceObject target)
		{
			Utility.Extensions.CommonExtensions.Place(this, target);
		}

		ICargoContainer IContainable<ICargoContainer>.Container
		{
			get { return Utility.Extensions.CommonExtensions.FindContainer(this); }
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			var vis = base.CheckVisibility(emp);
			var sobj = Container as ISpaceObject;
			if (vis < Visibility.Scanned && sobj != null && sobj.CheckVisibility(emp) >= Visibility.Scanned)
				vis = Visibility.Scanned;
			return vis;
		}

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Satellite; }
		}

		public override IMobileSpaceObject RecycleContainer
		{
			get { return (this as IUnit).Container as IMobileSpaceObject; }
		}
	}
}
