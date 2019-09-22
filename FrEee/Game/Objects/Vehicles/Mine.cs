using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Mine : SpaceVehicle, IUnit
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Mine; }
		}

		public override bool CanWarp
		{
			get { return false; }
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
			// mines cannot be targeted in space combat
			get { return Enumerations.WeaponTargets.Invalid; }
		}

		/// <summary>
		/// Mines are invisible to everyone except their owner.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public override Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			return Visibility.Unknown;
		}

		public override void Place(ISpaceObject target)
		{
			Utility.Extensions.CommonExtensions.Place(this, target);
		}

		public override bool FillsCombatTile => false;
	}
}