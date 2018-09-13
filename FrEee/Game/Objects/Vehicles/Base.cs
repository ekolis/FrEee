using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Linq;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Base : SpaceVehicle
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Base; }
		}

		public override bool CanWarp
		{
			get { return false; }
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

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}

		public override IMobileSpaceObject RecycleContainer
		{
			get { return this; }
		}

		public override bool RequiresSpaceYardQueue
		{
			get { return true; }
		}

		public override WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Ship; }
		}

		public override void Place(ISpaceObject target)
		{
			var search = Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj == target);
			if (!search.Any())
				throw new Exception("Can't place newly constructed vehicle near " + target + " because the target is not in any known sector.");
			var sys = search.First().StarSystem;
			var coords = search.First().Sector.Coordinates;
			sys.SpaceObjectLocations.Add(new ObjectLocation<ISpaceObject>(this, coords));
		}
	}
}