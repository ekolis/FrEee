using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System;
using FrEee.Serialization;

namespace FrEee.Objects.Vehicles
{
	[Serializable]
	public class Fighter : SpaceVehicle, IUnit
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Fighter; }
		}

		public override bool CanWarp
		{
			get { return false; }
		}

		ICargoContainer IContainable<ICargoContainer>.Container
		{
			get { return CommonExtensions.FindContainer(this); }
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
			get { return Enumerations.WeaponTargets.Fighter; }
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			var vis = base.CheckVisibility(emp);
			var sobj = Container as ISpaceObject;
			if (sobj != null && sobj.HasVisibility(emp, Visibility.Scanned))
				vis = Visibility.Scanned;
			return vis;
		}

		public override void Place(ISpaceObject target)
		{
			CommonExtensions.Place(this, target);
		}

		// HACK - until we end our game and this can be purged
		[DoNotSerialize]
		private Cargo Cargo { get; set;}

		public override bool FillsCombatTile => false;
	}
}
