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
	public class CombatVehicle : CombatControlledObject
	{
		/// <summary>
		/// use this constructor for creating an 'Start' combat Object.
		/// </summary>
		/// <param name="start_v"></param>
		/// <param name="working_v"></param>
		/// <param name="battleseed"></param>
		/// <param name="OrigionalID">this should be the FrEee ID for the origional Vehicle</param>
		/// <param name="IDPrefix"></param>
		public CombatVehicle(Vehicle start_v, Vehicle working_v, int battleseed, long OrigionalID, string IDPrefix = "SHP")
			: base(start_v, working_v, new PointXd(0, 0, 0), new PointXd(0, 0, 0), battleseed, IDPrefix)
		{
			this.ID = OrigionalID;
			// TODO - don't some mods have vehicles >32MT?
			cmbt_mass = (Fix16)working_v.Size;
			maxfowardThrust = (Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.5;
			maxStrafeThrust = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.5) / ((Fix16)4 - (Fix16)working_v.Evasion * (Fix16)0.01);
			maxRotate.Degrees = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)2.5 - (Fix16)working_v.Evasion * (Fix16)0.01);
			if (start_v.Design.Strategy == null)
				strategy = new StragegyObject_Default();
			else
				strategy = start_v.Design.Strategy.Copy();
			SpaceVehicle sv = (SpaceVehicle)start_v;
			//combatfleet = sv.Container.Name;
#if DEBUG
			Console.WriteLine("Created new CombatVehicle with ID " + ID);
			Console.WriteLine("MaxAccel = " + maxfowardThrust / cmbt_mass);
			//Console.WriteLine("Strategy: " + strategy
#endif
		}

//        public CombatVehicle(Vehicle start_v, Vehicle working_v, int battleseed, string IDPrefix = "SHP")
//            : base(start_v, working_v, new PointXd(0, 0, 0), new PointXd(0, 0, 0), battleseed, IDPrefix)
//        {
//            // TODO - don't some mods have vehicles >32MT?
//            cmbt_mass = (Fix16)working_v.Size;
//            maxfowardThrust = (Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1;
//            maxStrafeThrust = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)4 - (Fix16)working_v.Evasion * (Fix16)0.01);
//            maxRotate.Radians = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)12000 - (Fix16)working_v.Evasion * (Fix16)0.1);
//            if (start_v.Design.Strategy == null)
//                strategy = new StragegyObject_Default();
//            else
//                strategy = start_v.Design.Strategy;
//#if DEBUG
//            Console.WriteLine("MaxAccel = " + maxfowardThrust / cmbt_mass);
//#endif
//        }



		#region fields & properties

		
		/// <summary>
		/// The vehicle's state at the start of combat.
		/// </summary>
		[DoNotAssignID]
		[DoNotSerialize]
		public Vehicle StartVehicle { get { return (Vehicle)StartCombatant; } private set { StartCombatant = value; } }

		/// <summary>
		/// The current state of the vehicle.
		/// </summary>
		[DoNotSerialize]
		public Vehicle WorkingVehicle { get { return (Vehicle)WorkingObject; } private set { WorkingObject = value; } }


		#endregion

		#region methods and functions
		public override void renewtoStart()
		{

#if DEBUG
			Console.WriteLine("renewtoStart for CombatVehcile");
			Console.WriteLine(this.strID);
			Console.WriteLine(StartVehicle.Name);
#endif
			Vehicle ship = StartVehicle.Copy();
			ship.IsMemory = true;
			if (ship.Owner != StartVehicle.Owner && ship.Owner != null)
				ship.Owner.Dispose(); // don't need extra empires!
			ship.Owner = StartVehicle.Owner;
#if DEBUG
			Console.WriteLine(ship.Name);
#endif
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
				Console.WriteLine(ccopy.Name);
				Console.WriteLine("Container is " + ccopy.Container);
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

			SpaceVehicle start_v = (SpaceVehicle)this.StartCombatant;
			if (start_v.Design.Strategy == null)
				strategy = new StragegyObject_Default();
			else
				strategy = start_v.Design.Strategy.Copy();
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
				Console.Write("Weapn Conatiner " + wpn.weapon.Container);
#endif
			}
			Weapons = weapons;
#if DEBUG
			Console.WriteLine("Done");
#endif
		}

		public override void TakeSpecialDamage(Battle_Space battle, Hit hit, PRNG dice)
		{
			// find out who hit us
			var atkr = battle.FindCombatObject(hit.Shot.Attacker);

			// find out how too
			var dmgType = hit.Shot.DamageType;

			// disrupt reload
			{
				var disrupt = dmgType.DisruptReload.Value * hit.Shot.DamageLeft / 100;
				foreach (var w in Weapons)
				{
					w.nextReload += disrupt;
					if (w.nextReload - battle.CurrentTick > w.reloadRate)
						w.nextReload = battle.CurrentTick + w.reloadRate; // this damage type can't increase past normal reload time
				}
			}

			// increase reload (doesn't work againts master computers, even disabled ones)
			if (!WorkingVehicle.Components.Any(c => c.HasAbility("Master Computer")))
			{
				var inc = dmgType.IncreaseReload.Value * hit.Shot.DamageLeft / 100;
				foreach (var w in Weapons)
					w.nextReload += inc; // this damage type can increase past normal reload time
			}

			// ship capture
			{
				var cap = dmgType.ShipCapture.Value * hit.Shot.DamageLeft;
				if (cap > WorkingVehicle.GetAbilityValue("Boarding Attack").ToInt() + WorkingVehicle.GetAbilityValue("Boarding Defense").ToInt())
				{
					// destroy all boarding parties and security stations on the target
					foreach (var c in WorkingVehicle.Components.Where(c => c.HasAbility("Boarding Attack") || c.HasAbility("Boarding Defense")))
						c.Hitpoints = 0;

					// see if that killed the target
					if (battle.CheckForDeath(battle.CurrentTick, this))
						return;

					// transfer ownership
					this.WorkingCombatant.Owner = atkr.WorkingObject.Owner;
				}
			}

			// push/pull effects
			if (atkr.CanPushOrPull(this))
			{
				var deltaV = dmgType.TargetPush.Value * hit.Shot.DamageLeft / 100;
				var vector = atkr.cmbt_loc - this.cmbt_loc;
				if (vector.Length == 0)
				{
					// pick a random direction to push/pull
					vector = new Compass(dice.Next(360), false).Point(1);
				}
				vector /= vector.Length; // normalize to unit vector
				vector *= Battle_Space.KilometersPerSquare / Battle_Space.TicksPerSecond; // scale to combat map
				vector *= deltaV; // scale to push/pull acceleration factor
				this.cmbt_vel += deltaV; // apply force
			}

			// teleport effects
			{
				var deltaPos = dmgType.TargetTeleport.Value * hit.Shot.DamageLeft / 100;
				var vector = new Compass(dice.Next(360), false).Point(deltaPos);
				this.cmbt_loc += deltaPos; // apply teleport
			}
		}
		#endregion
	}
}
