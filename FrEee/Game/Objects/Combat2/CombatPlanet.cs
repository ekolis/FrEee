using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatPlanet : ControlledCombatObject
	{
		public CombatPlanet(Planet start, Planet working, int battleseed, string IDPrefix = "PLN")
			: base(start, working, new PointXd(0, 0, 0), new PointXd(0, 0, 0), battleseed, IDPrefix)
		{
			// TODO - planets that can move in combat?
			cmbt_mass = Fix16.MaxValue;
			maxfowardThrust = 0;
			maxStrafeThrust = 0;
			maxRotate = 0;
		}

		#region fields & properties

		/// <summary>
		/// The planet's state at the start of combat.
		/// </summary>
		public Planet StartPlanet { get { return (Planet)StartCombatant; } private set { StartCombatant = value; } }

		/// <summary>
		/// The current state of the planet.
		/// </summary>
		public Planet WorkingPlanet { get { return (Planet)WorkingObject; } private set { WorkingObject = value; } }

		#endregion

		#region methods and functions
		public override void renewtoStart()
		{

#if DEBUG
            Console.WriteLine("renewtoStart for CombatPlanet");
#endif
			var planet = StartPlanet.Copy();
			planet.IsMemory = true;
			if (planet.Owner != StartPlanet.Owner)
				planet.Owner.Dispose(); // don't need extra empires!
			if (planet.Colony != null)
			{
				planet.Colony.Owner = StartPlanet.Owner;

				// copy over the facilities individually so they can take damage without affecting the starting state
				planet.Colony.Facilities.Clear();
#if DEBUG
				Console.WriteLine("copying facilities");
#endif
				foreach (var f in (StartPlanet.Colony.Facilities))
				{
					var fcopy = f.Copy();
					planet.Colony.Facilities.Add(fcopy);
#if DEBUG
					Console.Write(".");
#endif
				}
#if DEBUG
				Console.WriteLine("Done");
#endif
			}

			WorkingPlanet = planet;
			RefreshWeapons();

			foreach (var w in Weapons)
				w.nextReload = 1;

            base.renewtoStart();
#if DEBUG
            Console.WriteLine("Done");
#endif
		}

		protected override void RefreshWeapons()
		{
			var weapons = new List<CombatWeapon>();
			weapons = new List<CombatWeapon>();
#if DEBUG
            Console.WriteLine("RefreshingWeapons");
#endif
			foreach (Component weapon in WorkingPlanet.Weapons)
			{
				CombatWeapon wpn = new CombatWeapon(weapon);
				weapons.Add(wpn);
#if DEBUG
                Console.Write(".");
#endif
			}
			Weapons = weapons;
#if DEBUG
            Console.WriteLine("Done");
#endif
		}

		#endregion
	}
}
