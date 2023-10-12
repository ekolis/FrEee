using System;
using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;


namespace FrEee.Game.Objects.Combat2
{
    public class CombatSeeker : CombatObject, ITargetable
    {
        public CombatSeeker(CombatObject attacker, CombatWeapon launcher, int ID)
            : base(null, new PointXd(attacker.cmbt_loc), new PointXd(attacker.cmbt_vel), ID, "SKR")
        {
			WorkingObject = this;
            SeekingWeaponInfo skrinfo = (SeekingWeaponInfo)launcher.weapon.Template.ComponentTemplate.WeaponInfo;
			Hitpoints = MaxHitpoints = skrinfo.SeekerDurability;
            cmbt_mass = (Fix16)Hitpoints * 0.1;//(Fix16)s.MaxHitpoints; // sure why not?
            
            
            maxfowardThrust = calcFowardThrust(skrinfo);
            maxStrafeThrust = calcStrafeThrust(skrinfo);
            maxRotate = calcRotate(skrinfo);
            

            cmbt_thrust = new PointXd(0, 0, 0);
            cmbt_accel = new PointXd(0, 0, 0);

            newDice(ID);
#if DEBUG
            Console.WriteLine("MaxAccel = " + maxfowardThrust / cmbt_mass);
#endif
            this.launcher = launcher;
        }

        public static Fix16 calcFowardThrust(SeekingWeaponInfo skrinfo)
        {
            int wpnskrspd = skrinfo.SeekerSpeed;
            int hitpoints = skrinfo.SeekerDurability;
            Fix16 mass = hitpoints * 0.1;
            return (Fix16)wpnskrspd * mass * (Fix16)10.0;
        }
        public static Fix16 calcStrafeThrust(SeekingWeaponInfo skrinfo)
        {
            int wpnskrspd = skrinfo.SeekerSpeed;
            int hitpoints = skrinfo.SeekerDurability;
            Fix16 mass = hitpoints * 0.1;
            int wpnskrEvade = Mod.Current.Settings.SeekerEvasion;
            return (Fix16)wpnskrspd * mass * (Fix16)5.0 / ((Fix16)4 - (Fix16)wpnskrEvade * (Fix16)0.01);
        }
        public static Compass calcRotate(SeekingWeaponInfo skrinfo)
        {
            int wpnskrspd = skrinfo.SeekerSpeed;
            int hitpoints = skrinfo.SeekerDurability;
            Fix16 mass = hitpoints * 0.1;
            int wpnskrEvade = Mod.Current.Settings.SeekerEvasion;
            return new Compass((Fix16)wpnskrspd * mass * (Fix16)10 / ((Fix16)2.5 - (Fix16)wpnskrEvade * (Fix16)0.01), false);
        }

        public static Fix16 seekerTimeToTarget(CombatObject attacker, CombatObject target, SeekingWeaponInfo skrinfo)
        {
            //Fix16 distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
            PointXd px = new PointXd(attacker.cmbt_loc - target.cmbt_loc);
            Fix16 distance_toTarget = px.Length;
            //SeekingWeaponInfo seekerinfo = (SeekingWeaponInfo)weapon.Template.ComponentTemplate.WeaponInfo;
            int hitpoints = skrinfo.SeekerDurability;
            Fix16 mass = hitpoints * 0.1;
            int wpnskrspd = skrinfo.SeekerSpeed;
            Fix16 Thrust = calcFowardThrust(skrinfo);
            Fix16 acceleration = Thrust / mass * -1; //*-1 because we should be accelerating towards the target, not away. 
            Fix16 startV = seekerClosingSpeed_base(attacker, target);
            //Fix16 endV = ???
            Fix16 baseTimetoTarget = distance_toTarget / startV;

            //Fix16 deltaV = baseTimetoTarget
            //Fix16[] ttt = NMath.quadratic(acceleration, startV, distance_toTarget);
            Fix64[] ttt2 = NMath.quadratic64(acceleration, startV, distance_toTarget);
            
            Fix16 TimetoTarget;
            if (ttt2[2] == 1)
            {
                TimetoTarget = Fix16.Max((Fix16)ttt2[0], (Fix16)ttt2[1]);
            }
            else
                TimetoTarget = (Fix16)ttt2[0];
#if DEBUG
            Console.WriteLine("SeekerTimeToTarget = " + TimetoTarget);
#endif
            return TimetoTarget;
        }

        public static Fix16 seekerClosingSpeed_base(CombatObject attacker, CombatObject target)
        {
            Fix16 shotspeed = 0;// boltSpeed; //speed of bullet when ship is at standstill (0 for seekers)
            Fix16 shotspeed_actual = shotspeed + NMath.closingRate(attacker.cmbt_loc, attacker.cmbt_vel, target.cmbt_loc, target.cmbt_vel);
            return shotspeed_actual;
        }

        #region fields & properties

        /// <summary>
        /// seekeer stores this so it can flip it to being a hit or not. 
        /// </summary>
        public CombatTakeFireEvent seekertargethit { get; set; }

        //the component that fired the missile.
        public CombatWeapon launcher { get; private set; }

		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Seeker; }
		}

		public int Hitpoints
		{
			get;
			set;
		}

		public int NormalShields
		{
			get;
			set;
		}

		public int PhasedShields
		{
			get;
			set;
		}

		public int MaxHitpoints
		{
			get;
			private set;
		}

		public int MaxNormalShields
		{
			get { return 0; }
		}

		public int MaxPhasedShields
		{
			get { return 0; }
		}

		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		public int ArmorHitpoints
		{
			get { return 0; }
		}

		public int HullHitpoints
		{
			get { return Hitpoints; }
		}

		public int MaxShieldHitpoints
		{
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHullHitpoints
		{
			get { return MaxHitpoints; }
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

		public int HitChance
		{
			get { return 1; }
		}

		[DoNotSerialize(false)]
		public Empire Owner
		{
			// seeker owner is irrelevant outside of combat, and we have CombatEmpire for that
			get { return null; }
			set { throw new NotSupportedException("Cannot set the ownership of a combat seeker."); }
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public int Evasion
		{
			get
			{
				// TODO - per-seeker evasion settings
				return Mod.Current.Settings.SeekerEvasion;
			}
		}

        #endregion

        #region methods & functions

        public override void renewtoStart()
        {
            //do nothing. this should not ever happen here.
        }

        public override void helm()
        {
            
            Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 

            Tuple<Compass, bool?> nav = Nav(angletoWaypoint);
            Compass angletoturn = nav.Item1;
            bool? thrustToWaypoint = nav.Item2;

            turnship(angletoturn, angletoWaypoint);

            thrustship(angletoturn, true);            
        }

        protected override Tuple<Compass, bool?> Nav(Compass angletoWaypoint)
        {          
            Compass angletoturn = new Compass();
            bool? thrustTowards = true;
     
            CombatWaypoint wpt = this.waypointTarget;
            angletoturn = new Compass(angletoWaypoint.Degrees - this.cmbt_head.Degrees, false);
            PointXd vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;

            //this stuff doesn't actualy do anything yet:
            Fix16 acceleration = maxfowardThrust / cmbt_mass;
            Fix16 startSpeed = NMath.distance(cmbt_vel, wpt.cmbt_vel);
            Fix16 distance = vectortowaypoint.Length;

            Fix16 closingSpeed = NMath.closingRate(this.cmbt_loc, this.cmbt_vel, this.waypointTarget.cmbt_loc, this.waypointTarget.cmbt_vel);
            Fix16 timetowpt = distance / closingSpeed;


            Fix64[] ttt2 = NMath.quadratic64(acceleration, startSpeed, distance);

            Fix16 TimetoTarget;
            if (ttt2[2] == 1)
            {
                TimetoTarget = Fix16.Max((Fix16)ttt2[0], (Fix16)ttt2[1]);
            }
            else
                TimetoTarget = (Fix16)ttt2[0];
            Fix16 endV = startSpeed + acceleration * TimetoTarget;
            //above doesn't actualy do anything yet. 
#if DEBUG
            Console.WriteLine("seeker ttt: " + TimetoTarget);
            Console.WriteLine("timetowpt: " + timetowpt);
            Console.WriteLine("seeker distance: " + distance);
            Console.WriteLine("seeker startV: " + startSpeed);
            Console.WriteLine("seeker endV: " + endV);
#endif

            return new Tuple<Compass, bool?>(angletoturn, thrustTowards);
        }

		public void ReplenishShields(int? amount = null)
		{
			// seekers don't have shields
		}

		public int? Repair(int? amount = null)
		{
			if (amount == null)
			{
				Hitpoints = MaxHitpoints;
				return null;
			}
			if (amount + Hitpoints > MaxHitpoints)
			{
				amount -= (MaxHitpoints - Hitpoints);
				Hitpoints = MaxHitpoints;
				return amount;
			}
			Hitpoints += amount.Value;
			return 0;
		}

		public void Dispose()
		{
			Hitpoints = 0;
			IsDisposed = true;
		}

		public override int handleShieldDamage(int damage)
		{
			// seekers don't have shields, just leak the damage
			return damage;
		}

        /*/// <summary>
        /// was missilefirecontrol in battlespace.
        /// </summary>
        /// <param name="battletick"></param>
        /// <param name="comSek"></param>

        public override void firecontrol(int battletick)
        {
            Fix16 locdistance = Trig.distance(comSek.cmbt_loc, comSek.weaponTarget[0].cmbt_loc);
            if (locdistance <= comSek.cmbt_vel.Length)//erm, I think? (if we're as close as we're going to get in one tick) could screw up at high velocities.
            {
                if (!IsReplay)
                {
                    CombatTakeFireEvent evnt = comSek.seekertargethit;
                    evnt.IsHit = true;
                    evnt.Tick = battletick;
                }
                Component launcher = comSek.launcher.weapon;
                CombatObject target = comSek.weaponTarget[0];
                if (target is ControlledCombatObject)
                {
                    ControlledCombatObject ccTarget = (ControlledCombatObject)target;
                    var target_icomobj = ccTarget.WorkingObject;
                    var shot = new Combat.Shot(launcher, target_icomobj, 0);
                    //defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
                    int damage = shot.Damage;
                    combatDamage(battletick, target, comSek.launcher, damage, comSek.getDice());
                    if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
                        target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
                    if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
                        target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
                }

                DeadNodes.Add(comSek);
                CombatNodes.Remove(comSek);
            }
            else if (battletick > comSek.deathTick)
            {
                DeadNodes.Add(comSek);
                CombatNodes.Remove(comSek);
            }
        }
         */

		public override void TakeSpecialDamage(Battle_Space battle, Hit hit, PRNG dice)
		{
			// find out who hit us
			var atkr = battle.FindCombatObject(hit.Shot.Attacker);

			// find out how too
			var dmgType = hit.Shot.DamageType;

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

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			// TODO - damage types
			var skpct = hit.Shot.DamageType.SeekerDamage.Evaluate(hit);
			var damage = skpct.PercentOfRounded(hit.NominalDamage);
			if (damage > Hitpoints)
			{
				damage -= Hitpoints;
				Hitpoints = 0;
				return (int)Math.Floor(damage / skpct.Percent());
			}
			Hitpoints -= damage;
			return 0;
		}

        #endregion
	}
}
