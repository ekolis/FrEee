using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// An autonomous space vehicle which does not operate in groups.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class AutonomousSpaceVehicle : Vehicle, ISpaceObject
	{
		public AutonomousSpaceVehicle()
		{
			IntrinsicAbilities = new List<Ability>();
		}

		public override bool RequiresSpaceYardQueue
		{
			get { return true; }
		}

		public override void Place(ISpaceObject target)
		{
			var search = Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj == target);
			if (!search.Any() || !search.First().Any() || !search.First().First().Any())
				throw new Exception("Can't place newly constructed vehicle near " + target + " because the target is not in any known sector.");
			var sys = search.First().Key.Item;
			var coords = search.First().First().First().Key;
			var sector = sys.GetSector(coords);
			sector.SpaceObjects.Add(this);
		}


		public IList<Ability> IntrinsicAbilities
		{
			get;
			private set;
		}

		public Visibility CheckVisibility(Galaxy galaxy, StarSystem starSystem)
		{
			if (galaxy.CurrentEmpire == null)
				return Visibility.Scanned; // host can see everything

			if (galaxy.CurrentEmpire == Owner)
				return Visibility.Scanned;

			// TODO - check for cloaking vs. sensors

			// TODO - check for long range scanners

			if (starSystem.FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == galaxy.CurrentEmpire).SelectMany(g => g).Any())
				return Visibility.Visible; // player can see vehicles in systems he owns stuff in

			// no fog of war for space vehicles...
			
			return Visibility.Unknown;
		}

		public void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			if (visibility == Visibility.Unknown)
				throw new ArgumentException("If a space vehicle is not visible at all, it should be removed from the player's savegame rather than redacted.", "visibility");

			// Can't see the ship's components if it's not scanned
			if (visibility < Visibility.Scanned)
			{
				// create fake design
				var d = new Design<AutonomousSpaceVehicle>();
				d.Hull = (IHull<AutonomousSpaceVehicle>)Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
			}
		}

		public IEnumerable<Ability> Abilities
		{
			get { return IntrinsicAbilities.Concat(Design.Abilities).Stack(); }
		}

		/// <summary>
		/// Amount of movement remaining for this turn.
		/// </summary>
		public int MovementRemaining { get; set; }

		/// <summary>
		/// The amount of supply present on this vehicle.
		/// </summary>
		public int SupplyRemaining { get; set; }
	}
}
