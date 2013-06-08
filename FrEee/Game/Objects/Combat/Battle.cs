using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	public class Battle : INamed
	{
		public Battle(Sector location)
		{
			Location = location;
			Log = new List<LogMessage>();
		}

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector Location { get; private set; }

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		public string Name
		{
			get
			{
				if (Location.SpaceObjects.OfType<StellarObject>().Any())
					return "Battle at " + Location.SpaceObjects.OfType<StellarObject>().Largest();
				var coords = Location.Coordinates;
				return "Battle at " + Location.FindStarSystem() + " sector (" + coords.X + ", " + coords.Y + ")";
			}
		}

		/// <summary>
		/// Resolves the battle.
		/// </summary>
		public void Resolve()
		{
			// fight for 100 rounds, just because
			for (int i = 0; i < 100; i++)
			{
				LogRound(i + 1);
				// TODO - real 2D combat mechanics
				foreach (var attacker in Location.SpaceObjects.Where(sobj => sobj.Weapons.Any()))
				{
					var defenders = Location.SpaceObjects.Where(sobj => attacker.CanTarget(sobj));
					if (!defenders.Any())
						continue; // no one to shoot at
					var defender = defenders.PickRandom();

					foreach (var weapon in attacker.Weapons.Where(w => w.CanTarget(defender)))
						weapon.Attack(defender, this); // TODO - range and such
				}
			}
		}

		public IList<LogMessage> Log { get; private set; }

		public void LogRound(int i)
		{
			Log.Add(new GenericLogMessage("Begin round " + i + "!"));
		}

		public void LogSalvo(ISpaceObject attacker, ISpaceObject defender)
		{
			Log.Add(new PictorialLogMessage<ISpaceObject>(attacker + " attacks " + defender + "!", attacker));
		}

		public void LogShot(Component weapon, DamageType damageType, int damage)
		{
			Log.Add(new PictorialLogMessage<Component>("Fires " + weapon + " for " + damage + " " + damageType.Name.ToLower() + " damage!", weapon));
		}
	}
}
