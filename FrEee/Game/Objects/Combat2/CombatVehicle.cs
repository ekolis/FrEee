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

using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatVehicle : CombatObject
	{
		private ICombatant icomObj;

		public CombatVehicle(Vehicle start_v, Vehicle working_v, int battleseed)
			: base(working_v, new Point3d(0, 0, 0), new Point3d(0, 0, 0), start_v.ID)
		{
			cmbt_mass = (Fix16)working_v.Size;
			maxfowardThrust = (Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1;
			maxStrafeThrust = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)4 - (Fix16)working_v.Evasion * (Fix16)0.01);
			maxRotate = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)12000 - (Fix16)working_v.Evasion * (Fix16)0.1);
			StartVehicle = start_v;
			newDice(battleseed);
			RefreshWeapons();
		}

		#region fields

		/// <summary>
		/// The vehicle's state at the start of combat.
		/// </summary>
		public Vehicle StartVehicle { get; private set; }

		public List<CombatWeapon> weaponList { get; set; }

		/// <summary>
		/// The current state of the vehicle.
		/// </summary>
		public Vehicle WorkingVehicle { get { return (Vehicle)WorkingObject; } }

		#endregion

		#region methods and functions
		public void renewtoStart()
		{
			var ship = StartVehicle.Copy();
			ship.IsMemory = true;
			if (ship.Owner != StartVehicle.Owner)
				ship.Owner.Dispose(); // don't need extra empires!

			// copy over the components individually so they can take damage without affecting the starting state
			// TODO - deal with planets in combat
			ship.Components.Clear();
			foreach (var comp in (StartVehicle.Components))
			{
				var ccopy = comp.Copy();
				ship.Components.Add(ccopy);
				ccopy.Container = ship;
			}

			this.icomObj = ship;

			this.cmbt_loc = new Point3d(0, 0, 0);
			this.cmbt_vel = new Point3d(0, 0, 0);
			this.cmbt_head = new Compass(0);
			this.cmbt_att = new Compass(0);
			this.cmbt_thrust = new Point3d(0, 0, 0);
			this.cmbt_accel = new Point3d(0, 0, 0);

			RefreshWeapons();

			foreach (var w in weaponList)
				w.nextReload = 1;
		}

		private void RefreshWeapons()
		{
			this.weaponList = new List<CombatWeapon>();
			foreach (Component weapon in WorkingVehicle.Weapons)
			{
				CombatWeapon wpn = new CombatWeapon(weapon);
				this.weaponList.Add(wpn);
			}
		}

		/// <summary>
		/// endgoal for helm is for the  ship to get to and match speed with the comObj.targetWaypiont as fast as possible.
		/// the strategic AI script should be responsible for setting where the waypoint is and where thet waypoint is going. 
		/// </summary>
		/// <param name="comObj"></param>
		public override void helm()
		{


			this.debuginfo += "HelmInfo:" + "\r\n";
			var ship = this.WorkingVehicle;
			string name = ship.Name;
			//rotate ship
			Fix16 timetoturn = (Fix16)0;
			//Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.waypointTarget.cmbt_loc));
			combatWaypoint wpt = this.waypointTarget;
			Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 
			Compass angletoturn = new Compass(angletoWaypoint.Radians - this.cmbt_head.Radians);
			Point3d vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;
			//if (comObj.lastVectortoWaypoint != null)
			//    angletoturn.Radians = Trig.angleA(vectortowaypoint - comObj.lastVectortoWaypoint);

			timetoturn = angletoturn.Radians / this.maxRotate;
			Fix16 oneEightytime = Fix16.Pi / this.maxRotate;
			//Point3d offsetVector = comObj.waypointTarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
			//Point3d combinedVelocity = comObj.cmbt_vel - comObj.waypointTarget.cmbt_vel;
			//Point3d distancePnt = comObj.waypointTarget.cmbt_loc - comObj.cmbt_loc;
			//double closingSpeed = Trig.dotProduct(combinedVelocity, distancePnt);
			Fix16 closingSpeed = GravMath.closingrate(this.cmbt_loc, this.cmbt_vel, this.waypointTarget.cmbt_loc, this.waypointTarget.cmbt_vel);

			Fix16 myspeed = Trig.hypotinuse(this.cmbt_vel);

			Fix16 timetokill_ClosingSpeed = closingSpeed / (this.maxfowardThrust / this.cmbt_mass); //t = v / a
			Fix16 strafetimetokill_ClosingSpeed = closingSpeed / (this.maxStrafeThrust / this.cmbt_mass);
			Fix16 timetokill_MySpeed = myspeed / (this.maxfowardThrust / this.cmbt_mass);


			Fix16 distance = Trig.distance(this.waypointTarget.cmbt_loc, this.cmbt_loc);


			Fix16 nominaldistance = this.maxStrafeThrust;
			Fix16 timetowpt = distance / closingSpeed;

			bool? thrustToWaypoint = true;
			string helmdo = "";

			if (closingSpeed > (Fix16)0) //getting closer
			{
				if (distance <= nominaldistance)  //close to the waypoint.
				{
					thrustToWaypoint = null;//should attempt to match speed
				}
				if (timetowpt <= timetokill_ClosingSpeed + oneEightytime)//if/when we're going to overshoot teh waypoint.
				{
					if (timetowpt < strafetimetokill_ClosingSpeed) //if time to waypoint is less than time to kill speed with strafe thrusters
					{

						thrustToWaypoint = false;
					}
					else
					{ //use strafe thrust to get close to the waypoint. 

						helmdo = "null" + "\r\n";
						thrustToWaypoint = null; //else match speed and use thrusters to get closer
					}
				}
			}
			else
			{
			}

			if (thrustToWaypoint == false)
			{
				helmdo = "Initiating Turnaround" + "\r\n"; //turn around and thrust the other way
				angletoturn.Degrees = (angletoWaypoint.Degrees - (Fix16)180) - this.cmbt_head.Degrees; //turn around and thrust the other way
				angletoturn.normalize();
			}
			else if (thrustToWaypoint == null)
			{
				angletoturn.Radians = Trig.angleA(this.waypointTarget.cmbt_vel);
			}

			this.debuginfo += "timetowpt:\t" + timetowpt.ToString() + "\r\n";
			this.debuginfo += "strafetime:\t" + strafetimetokill_ClosingSpeed.ToString() + "\r\n";
			this.debuginfo += "speedkilltime:\t" + timetokill_ClosingSpeed.ToString() + "\r\n";
			this.debuginfo += "180time:\t" + oneEightytime.ToString() + "\r\n";
			this.debuginfo += "ThrustTo:\t" + thrustToWaypoint.ToString() + "\r\n";
			this.debuginfo += helmdo + "\r\n";

			turnship(angletoturn, angletoWaypoint);

			thrustship(angletoturn, thrustToWaypoint);

			this.lastVectortoWaypoint = vectortowaypoint;


		}


		public override int handleShieldDamage(int damage)
		{

			Vehicle thisV = (Vehicle)WorkingVehicle;
			int shieldDmg = 0;
			if (thisV.NormalShields > 0)
			{
				var dmg = Math.Min(damage, thisV.NormalShields);
				thisV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (thisV.PhasedShields > 0)
			{
				var dmg = Math.Min(damage, thisV.PhasedShields);
				thisV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (shieldDmg > 0)// && battle != null)
			{
				//battle.LogShieldDamage(this, shieldDmg);
			}

			return damage;
		}

		public override int handleComponentDamage(int damage, DamageType damageType, PRNG attackersdice)
		{
			Vehicle thisV = (Vehicle)WorkingVehicle;
			while (damage > 0 && !thisV.IsDestroyed)
			{
				var comps = thisV.Components.Where(c => c.Hitpoints > 0);
				var armor = comps.Where(c => c.HasAbility("Armor"));
				var internals = comps.Where(c => !c.HasAbility("Armor"));
				var canBeHit = armor.Any() ? armor : internals;
				var comp = canBeHit.ToDictionary(c => c, c => c.HitChance).PickWeighted(attackersdice);

				damage = comp.TakeDamage(damageType, damage, null);// battle);
			}
			return damage;
		}
		#endregion
	}
}
