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

namespace FrEee.Game.Objects.Combat2
{
	public class CombatVehicle : ControlledCombatObject
	{
		public CombatVehicle(Vehicle start_v, Vehicle working_v, int battleseed, string IDPrefix = "SHP")
			: base(start_v, working_v, new PointXd(0, 0, 0), new PointXd(0, 0, 0), battleseed, IDPrefix)
		{
			// TODO - don't some mods have vehicles >32MT?
			cmbt_mass = (Fix16)working_v.Size;
			maxfowardThrust = (Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1;
			maxStrafeThrust = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)4 - (Fix16)working_v.Evasion * (Fix16)0.01);
			maxRotate.Radians = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)12000 - (Fix16)working_v.Evasion * (Fix16)0.1);
            strategy = start_v.Design.Strategy;
#if DEBUG
            Console.WriteLine("MaxAccel = " + maxfowardThrust / cmbt_mass);
#endif
		}

		#region fields & properties

		/// <summary>
		/// The vehicle's state at the start of combat.
		/// </summary>
		public Vehicle StartVehicle { get { return (Vehicle)StartCombatant; } private set { StartCombatant = value; } }

		/// <summary>
		/// The current state of the vehicle.
		/// </summary>
		public Vehicle WorkingVehicle { get { return (Vehicle)WorkingObject; } private set { WorkingObject = value; } }


		#endregion

		#region methods and functions
		public override void renewtoStart()
		{

#if DEBUG
            Console.WriteLine("renewtoStart for CombatVehcile");
#endif
			var ship = StartVehicle.Copy();
			ship.IsMemory = true;
			if (ship.Owner != StartVehicle.Owner)
				ship.Owner.Dispose(); // don't need extra empires!
			ship.Owner = StartVehicle.Owner;

			// copy over the components individually so they can take damage without affecting the starting state
			ship.Components.Clear();
#if DEBUG
            Console.WriteLine("copying components");
#endif
			foreach (var comp in (StartVehicle.Components))
			{
				var ccopy = comp.Copy();
				ship.Components.Add(ccopy);
				ccopy.Container = ship;
#if DEBUG
                Console.Write(".");
#endif
			}
#if DEBUG
            Console.WriteLine("Done");
#endif

			WorkingVehicle = ship;
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
#if DEBUG
            Console.WriteLine("RefreshingWeapons");
#endif
			foreach (Component weapon in WorkingVehicle.Weapons)
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
