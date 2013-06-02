using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A vehicle which operates in groups in space.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class SpaceUnit<T> : Unit<T>, ISpaceObject where T : SpaceUnit<T>, new()
	{
		public SpaceUnit()
		{
			IntrinsicAbilities = new List<Ability>();
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


		public IList<Abilities.Ability> IntrinsicAbilities
		{
			get;
			private set;
		}

		public void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			if (visibility == Visibility.Unknown)
				throw new ArgumentException("If a space vehicle is not visible at all, it should be removed from the player's savegame rather than redacted.", "visibility");

			// Can't see the ship's components if it's not scanned
			if (visibility < Visibility.Scanned)
			{
				// create fake design
				var d = new Design<T>();
				d.Hull = Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
			}
		}

		public IEnumerable<Ability> Abilities
		{
			get { return IntrinsicAbilities.Concat(Design.Abilities).Stack(); }
		}
	}
}
