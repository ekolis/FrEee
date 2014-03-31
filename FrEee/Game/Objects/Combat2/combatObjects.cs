﻿using System;
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
using FrEee.WinForms;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatEmpire
	{
		public List<CombatObject> ownships = new List<CombatObject>();
		public List<CombatObject> friendly = new List<CombatObject>();
		public List<CombatObject> neutral = new List<CombatObject>(); //not currently used.
		public List<CombatObject> hostile = new List<CombatObject>();
		public CombatEmpire()
		{ }
	}


    public class CombatNode
    {
        /// <summary>
        /// for creating bullets. & other non AI or acelerating things. used by the renderer to move and display.
        /// </summary>
        /// <param name="position"> ship position this is fiired from </param>
        /// <param name="vector">direction this is going</param>
        public CombatNode(PointXd position, PointXd vector, long ID, string IDPrefix)
        {
            this.cmbt_loc = position;
            this.cmbt_vel = vector;
            this.cmbt_head = new Compass((Fix16)0);
            this.ID = ID;
            this.IDPrefix = IDPrefix;
        }
        /// <summary>
        /// location within the sector
        /// </summary>
        public PointXd cmbt_loc { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public PointXd cmbt_vel { get; set; }


        /// <summary>
        /// ship heading 
        /// </summary>
        public Compass cmbt_head { get; set; }

        /// <summary>
        /// the ID of the origional icomatant if a ship.
        /// </summary>
        public long ID { get; set; }

        public string IDPrefix { get; private set; }

        public string strID
        {
            get
            {
                return IDPrefix + ID.ToString();
            }
        }

        public int deathTick { get; set; }
    }


	public class CombatObject : CombatNode
	{

        public CombatObject(ITargetable workingObject, PointXd position, PointXd vector, long ID, string IDprefix)
            : base(position, vector, ID, IDprefix)
        {
			WorkingObject = workingObject;
            this.waypointTarget = new combatWaypoint();
            weaponTarget = new List<CombatObject>(1); //eventualy this should be something with the multiplex tracking component.
            this.cmbt_thrust = new PointXd(0, 0, 0);
            this.cmbt_accel = new PointXd(0, 0, 0);
		}

        #region fields & properties
        /// <summary>
		/// The object's current state.
		/// </summary>
		public ITargetable WorkingObject
		{
			get;
			protected set;
		}

		private PRNG shipDice;

        
		/// <summary>
		/// ship attitude, ie angle from level plain (0/360) pointing straight up (90)
		/// </summary>
		public Compass cmbt_att { get; set; }

		public PointXd cmbt_thrust { get; set; }

        public PointXd cmbt_accel { get; set; }

		public Fix16 cmbt_mass { get; set; }

		//public PointXd cmbt_maxThrust { get; set; }
		//public PointXd cmbt_minThrust { get; set; }


		public combatWaypoint waypointTarget;
		public PointXd lastVectortoWaypoint { get; set; }
		//public double lastDistancetoWaypoint { get; set; }

		public List<CombatObject> weaponTarget { get; set; }



		public Fix16 maxfowardThrust { get; set; }
        public Fix16 maxStrafeThrust { get; set; }
        public Fix16 maxRotate { get; set; } // TODO - make maxRotate a compass so we don't get confused between radians and degrees

        

        #endregion


        #region methods & functions
        public PRNG getDice()
		{
			return shipDice;
		}
		public void newDice(int battleseed)
		{
			int seed = (int)(this.ID % 100000) + battleseed;
			shipDice = new PRNG(seed);
		}

        public virtual void renewtoStart() 
        {
            this.cmbt_loc = new PointXd(0, 0, 0);
            this.cmbt_vel = new PointXd(0, 0, 0);
            this.cmbt_head = new Compass(0);
            this.cmbt_att = new Compass(0);
            this.cmbt_thrust = new PointXd(0, 0, 0);
            this.cmbt_accel = new PointXd(0, 0, 0);
        }

        public string debuginfo = "";


        public virtual void helm()
        {
            combatWaypoint wpt = this.waypointTarget;
            Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 
            Compass angletoturn = new Compass(angletoWaypoint.Radians - this.cmbt_head.Radians);
            PointXd vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;

            Fix16 acceleration = maxfowardThrust * cmbt_mass;
            Fix16 startV = Trig.distance(cmbt_vel, wpt.cmbt_vel);
            Fix16 distance = Trig.distance(cmbt_loc, wpt.cmbt_loc);
            Fix16[] ttt = NMath.quadratic(acceleration, startV, distance);
            

            turnship(angletoturn, angletoWaypoint);

            thrustship(angletoturn, true);
        }


        protected void turnship(Compass angletoturn, Compass angleToTarget)
        {
            if (angletoturn.Degrees <= (Fix16)180) //turn clockwise
            {
                if (angletoturn.Radians > this.maxRotate)
                {
                    //comObj.cmbt_face += comObj.Rotate;
                    this.cmbt_head.Radians += this.maxRotate;
                }
                else
                {
                    //comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                    this.cmbt_head.Degrees += angletoturn.Degrees;
                }
            }
            else //turn counterclockwise
            {
                if (((Fix16)360 - angletoturn.Radians) > this.maxRotate)
                {
                    //comObj.cmbt_face -= comObj.maxRotate;
                    this.cmbt_head.Radians -= this.maxRotate;
                }
                else
                {
                    //comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                    // subtract 360 minus the angle
                    this.cmbt_head.Degrees += angletoturn.Degrees;
                }
            }
        }

 

        protected void thrustship(Compass angletoturn, bool? thrustToWaypoint)
        {
            this.cmbt_thrust.ZEROIZE();
            strafeship(thrustToWaypoint);
            //main foward thrust - still needs some work, ie it doesnt know when to turn it off when close to a waypoint.
            Fix16 thrustby = (Fix16)0;
            if (thrustToWaypoint != null)
            {
                if (angletoturn.Degrees >= (Fix16)0 && angletoturn.Degrees < (Fix16)90)
                {

                    thrustby = (Fix16)this.maxfowardThrust / (Fix16.Max((Fix16)1, angletoturn.Degrees / (Fix16)0.9));
                }
                else if (angletoturn.Degrees > (Fix16)270 && angletoturn.Degrees < (Fix16)360)
                {
                    Compass angle = new Compass((Fix16)360 - angletoturn.Degrees);
                    angle.normalize();
                    thrustby = (Fix16)this.maxfowardThrust / (Fix16.Max((Fix16)1, angle.Degrees / (Fix16)0.9));
                }

                //PointXd fowardthrust = new PointXd(comObj.cmbt_face + thrustby);
                PointXd fowardthrust = new PointXd(Trig.sides_ab(thrustby, this.cmbt_head.Radians));
                this.cmbt_thrust += fowardthrust;
                
            }
            else
            {
                //match velocity with waypoint
                PointXd wayptvel = this.waypointTarget.cmbt_vel;
                PointXd ourvel = this.cmbt_vel;

                thrustby = (Fix16)this.maxfowardThrust / (Fix16.Max((Fix16)1, angletoturn.Degrees / (Fix16)0.9));

                PointXd fowardthrust = new PointXd(Trig.intermediatePoint(ourvel, wayptvel, thrustby));

            }

        }

        protected void strafeship(bool? thrustToWaypoint)
        {
            //thrust ship using strafe
            if (thrustToWaypoint == true) //(if we want to accelerate towards the target, not away from it)
            {
                this.cmbt_thrust = Trig.intermediatePoint(this.cmbt_loc, this.waypointTarget.cmbt_loc, this.maxStrafeThrust);
            }
            else if (thrustToWaypoint == false)
            {
                this.cmbt_thrust = Trig.intermediatePoint(this.cmbt_loc, this.waypointTarget.cmbt_loc, -this.maxStrafeThrust);
            }
            else //if null
            {
                //comObj.cmbt_thrust = Trig.
            }
        }

		public virtual int handleShieldDamage(int damage) { return damage; }
		public virtual int handleComponentDamage(int damage, DamageType damageType, PRNG attackersdice) { return damage; }

        public virtual void firecontrol(int tic_countr)
        {
        }

        /// <summary>
        /// attempt at adding this to the combatObject. it's not working too well though.
        /// </summary>
        /// <param name="battletick"></param>
        /// <param name="attacker"></param>
        /// <param name="weapon"></param>
        /// <param name="Sector"></param>
        /// <param name="IsReplay"></param>
        /// <returns></returns>
        /*
        public CombatTakeFireEvent FireWeapon(int battletick, CombatObject attacker, CombatWeapon weapon, Space.Sector Sector, bool IsReplay)
        {
            var wpninfo = weapon.weapon.Template.ComponentTemplate.WeaponInfo;
            Fix16 rangeForDamageCalcs = (Fix16)0;
            Fix16 rangetotarget = Trig.distance(attacker.cmbt_loc, this.cmbt_loc);
            int targettic = battletick;

            //reset the weapon nextReload.
            weapon.nextReload = battletick + (int)(weapon.reloadRate * Battle_Space.TicksPerSecond); // TODO - round up, so weapons that fire more than 10 times per second don't fire at infinite rate

            var target_icomobj = this.WorkingObject;
            //Vehicle defenderV = (Vehicle)target_icomobj;

            if (!weapon.CanTarget(target_icomobj))
                return null;

            // TODO - check range too
            var tohit =
                Mod.Current.Settings.WeaponAccuracyPointBlank // default weapon accuracy at point blank range
                + weapon.weapon.Template.WeaponAccuracy // weapon's intrinsic accuracy modifier
                + weapon.weapon.Container.Accuracy // firing ship's accuracy modifier
                - target_icomobj.Evasion // target's evasion modifier
                - Sector.GetAbilityValue(this.WorkingObject.Owner, "Sector - Sensor Interference").ToInt() // sector evasion modifier
                + Sector.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - Sector").ToInt() // generic combat bonuses
                - Sector.GetAbilityValue(this.WorkingObject.Owner, "Combat Modifier - Sector").ToInt()
                + Sector.StarSystem.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - System").ToInt()
                - Sector.StarSystem.GetAbilityValue(this.WorkingObject.Owner, "Combat Modifier - System").ToInt()
                + attacker.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt()
                - this.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
            // TODO - moddable min/max hit chances with per-weapon overrides
            if (tohit > 99)
                tohit = 99;
            if (tohit < 1)
                tohit = 1;
            if (weapon.weapon.Container.HasAbility("Weapons Always Hit"))
                tohit = 100;

            //bool hit = RandomHelper.Range(0, 99) < tohit;
            PRNG dice = attacker.getDice();
            bool hit = dice.Range(0, 99) < tohit;

            CombatTakeFireEvent target_event = null;

            if (weapon.weaponType == "Seeker")
            {

                //create seeker and node.
                CombatSeeker seeker = new CombatSeeker(attacker, weapon, -tempObjCounter);
                seeker.waypointTarget = new combatWaypoint(this);
                seeker.weaponTarget = new List<CombatObject>() { this };
                seeker.deathTick = battletick + weapon.maxRange_time;
                seeker.cmbt_head = attacker.cmbt_head;
                seeker.cmbt_att = attacker.cmbt_att;
                FreshNodes.Add(seeker);

                foreach (var emp in Empires.Values)
                {
                    if (emp.ownships.Contains(attacker))
                        emp.ownships.Add(seeker);
                    if (emp.friendly.Contains(attacker))
                        emp.friendly.Add(seeker);
                    if (emp.neutral.Contains(attacker))
                        emp.neutral.Add(seeker);
                    if (emp.hostile.Contains(attacker))
                        emp.hostile.Add(seeker);
                }

                if (IsReplay)
                {
                    //read the event
                    target_event = ReplayLog.EventsForObjectAtTick(this, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];
                    target_event.BulletNode = seeker;
                }
                else
                {
                    //*write* the event
                    target_event = new CombatTakeFireEvent(battletick, this, this.cmbt_loc, false);
                    target_event.BulletNode = seeker;
                    seeker.seekertargethit = target_event;
                }
            }
            //for bolt calc, need again for adding to list.
            else if (weapon.weaponType == "Bolt")
            {

                rangeForDamageCalcs = rangeForDamageCalcs_bolt(attacker, weapon, this);
                Fix16 boltTTT = weapon.boltTimeToTarget(attacker, target);
                //set target tick for the future.
                targettic += (int)boltTTT;



                if (IsReplay)
                {
                    //read the event
                    target_event = ReplayLog.EventsForObjectAtTick(this, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];

                    //because bullets don't need to be created during processing
                    Fix16 rThis_distance = (target_event.Location - target_event.fireOnEvent.Location).Length;
                    PointXd bulletVector = Trig.intermediatePoint(attacker.cmbt_loc, target_event.Location, rThis_distance);
                    if (!target_event.IsHit) //jitter it!
                    {
                        // TODO - take into account firing ship's accuracy and target's evasion
                        int accuracy = target_event.fireOnEvent.Weapon.weapon.Template.WeaponAccuracy;
                        int jitterAmount = 0;
                        if (accuracy < 50)
                            jitterAmount = (int)System.Math.Pow(50 - accuracy, 2) / 50;
                        if (jitterAmount < 5)
                            jitterAmount = 5;
                        if (jitterAmount > 30)
                            jitterAmount = 30;
                        //do *NOT* use ship prng here!!!! (since this is not done during normal processing, it'll cause differences, use any rand)
                        Compass jitter = new Compass(RandomHelper.Range(-jitterAmount, jitterAmount), false);
                        Compass bulletCompass = bulletVector.Compass;
                        Compass offsetCompass = bulletCompass + jitter;
                        bulletVector = offsetCompass.Point(bulletVector.Length);
                    }
                    CombatNode bullet = new CombatNode(attacker.cmbt_loc, bulletVector, -tempObjCounter, "BLT");
                    target_event.BulletNode = bullet;
                    FreshNodes.Add(bullet);
                    if (target_event.IsHit)
                    {
                        bullet.deathTick = target_event.Tick;
                    }
                    else
                    {
                        bullet.deathTick = battletick + target_event.fireOnEvent.Weapon.maxRange;
                    }
                }
                else
                {
                    //*write* the event
                    target_event = new CombatTakeFireEvent(targettic, this, this.cmbt_loc, hit);
                    int nothing = tempObjCounter; //increase it just so processing has the same number of tempObjects created as replay will. 
                }

            }
            else //not bolt
            {
                if (IsReplay)
                { //read the replay... nothing to do if a beam. 
                }
                else
                { //write the event.
                    rangeForDamageCalcs = rangetotarget / (Fix16)1000;
                    target_event = new CombatTakeFireEvent(targettic, this, this.cmbt_loc, hit);
                }
            }

            rangeForDamageCalcs = Fix16.Max((Fix16)1, rangeForDamageCalcs); //don't be less than 1.

            if (hit && !target_icomobj.IsDestroyed)
            {
                var shot = new Combat.Shot(weapon.weapon, target_icomobj, (int)rangeForDamageCalcs);
                //defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
                int damage = shot.Damage;
                combatDamage(battletick, this, weapon, damage, attacker.getDice());
                if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
                    target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
                if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
                    target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
                //if (defender.IsDestroyed)
                //battle.LogTargetDeath(defender);
            }
            return target_event;
        }
        */
        #endregion
    }

	public class combatWaypoint
	{
		public combatWaypoint()
		{
			this.cmbt_loc = new PointXd(0, 0, 0);
			this.cmbt_vel = new PointXd(0, 0, 0);
		}
		public combatWaypoint(PointXd cmbt_loc)
		{
			this.cmbt_loc = cmbt_loc;
			this.cmbt_vel = new PointXd(0, 0, 0);
		}
		public combatWaypoint(PointXd cmbt_loc, PointXd cmbt_vel)
		{
			this.cmbt_loc = cmbt_loc;
			this.cmbt_vel = cmbt_vel;
		}
		public combatWaypoint(CombatObject tgtcomObj)
		{
			this.comObj = tgtcomObj;
			this.cmbt_loc = tgtcomObj.cmbt_loc;
			this.cmbt_vel = tgtcomObj.cmbt_vel;
		}

		/// <summary>
		/// location within the sector
		/// </summary>
		public PointXd cmbt_loc { get; set; }

		/// <summary>
		/// combat velocity
		/// </summary>
		public PointXd cmbt_vel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public CombatObject comObj { get; set; }

	}
}
