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
	public class CombatEmpire
	{
		public List<CombatObject> ownships = new List<CombatObject>();
		public List<CombatObject> friendly = new List<CombatObject>();
		public List<CombatObject> neutral = new List<CombatObject>(); //not currently used.
		public List<CombatObject> hostile = new List<CombatObject>();
		public CombatEmpire()
		{ }
	}

    public class CombatWeapon
    {        
        
        
        public CombatWeapon(Component weapon)
        {
            this.weapon = weapon;
            var wpninfo = weapon.Template.ComponentTemplate.WeaponInfo;
            int wpMaxR = wpninfo.MaxRange;
            int wpMinR = wpninfo.MinRange;
            if (wpninfo.DisplayEffect.GetType() == typeof(Combat.BeamWeaponDisplayEffect))
            {
                weaponType = "Beam";

                maxRange_distance = (Fix16)wpMaxR * (Fix16)1000;
                minRange = (Fix16)wpMinR * (Fix16)1000;
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.ProjectileWeaponDisplayEffect))
            {
                weaponType = "Bolt";

                boltSpeed = (Fix16)wpMaxR * (Fix16)1000 * (Fix16)(Battle_Space.TickLength); // convert from kilometers per second to meters per tick
                maxRange_time = (Fix16)1; // (maxTime for bolts) untill mod files can handle this, bolt weapons range is the distance it can go in 1 sec.
                minRange = ((Fix16)wpMinR / boltSpeed); //(minTime for bolts) distance / speed = time                  
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.SeekerWeaponDisplayEffect))
            {
                SeekingWeaponInfo seekerinfo = (SeekingWeaponInfo)weapon.Template.ComponentTemplate.WeaponInfo;
                weaponType = "Seeker";
                boltSpeed = 0; //seekers get launched at 0 speed. 
                int mass = seekerinfo.SeekerDurability; // sure why not?
                int wpnskrspd = seekerinfo.SeekerSpeed;
                Fix16 Thrust = (Fix16)wpnskrspd * mass * (Fix16)0.001;

                //boltSpeed = (Fix16)wpMaxR * (Fix16)1000 * (Fix16)(Battle_Space.TickLength); // convert from kilometers per second to meters per tick
                maxRange_time = (Fix16)wpMaxR; // (maxTime for Missiles) untill mod files can handle this, bolt weapons range is the distance it can go in 1 sec.
            }
            else //treat it like a beam I guess. 
            {
                weaponType = "Unknown";
                maxRange_distance = (Fix16)wpMaxR * (Fix16)1000;
                minRange = (Fix16)wpMinR * (Fix16)1000;
            }
            double wpiReloadRate = wpninfo.ReloadRate;
            reloadRate = (Fix16)wpiReloadRate;
            nextReload = 1;

            
        }

        /// <summary>
        /// "Beam", "Bolt", "Seeker".
        /// </summary>
        public string weaponType { get; set; }

        /// <summary>
        /// the actual component.
        /// </summary>
        public Component weapon { get; private set; }

        /// <summary>
        /// nextReload tick (when battletick >= this, fire again, then reset this to current tick + reload rate)
        /// </summary>
        public int nextReload { get; set; }

        /// <summary>
        /// the rate the weapon can reload in seconds.
        /// </summary>
        public Fix16 reloadRate { get; private set; }

        /// <summary>
        /// if a bolt, what is it's speed if fired at rest?
        /// </summary>
        public Fix16 boltSpeed { get; private set; }

        /// <summary>
        /// if a bolt (or seeker?), this is time, else it's distance 
        /// </summary>
        public Fix16 maxRange {
            get
            {
                Fix16 retnum;
                if (weaponType == "Beam")
                {
                    retnum = maxRange_distance;
                }
                else
                {
                    retnum = maxRange_time;
                }
                return retnum;
            }
        }

        /// <summary>
        /// for seekers and bolts.
        /// </summary>
        public Fix16 maxRange_time { get; private set; }

        /// <summary>
        /// for beams.
        /// </summary>
        public Fix16 maxRange_distance { get; private set; }

        /// <summary>
        /// if a bolt (or seeker?), this is time, else it's distance 
        /// </summary>
        public Fix16 minRange { get; private set; }

        public bool CanTarget(ITargetable target)
        {
            return weapon.CanTarget(target);
        }

        public bool isinRange(CombatObject attacker, CombatObject target)
        {
            bool inrange = false;
            var wpninfo = weapon.Template.ComponentTemplate.WeaponInfo;
            Fix16 distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);

            string weaponRangeinfo = "RangeInfo:\r\n ";


            if (weaponType == "Beam")          //beam
            {
                if (distance_toTarget <= maxRange && distance_toTarget >= minRange)
                {
                    inrange = true;
                    weaponRangeinfo += "Range for Beam is good \r\n";
                }
            }
            else if (weaponType == "Bolt") //projectile
            {
                inrange = bolt_isinRange(attacker, target);
            }
            else if (weaponType == "Seeker")       //seeker
            {
                inrange = seeker_isinRange(attacker, target);
            }

            attacker.debuginfo += weaponRangeinfo;
            return inrange;
        }

        private bool seeker_isinRange(CombatObject attacker, CombatObject target)
        {
            bool isinRange = false;
            Fix16 TickLength = Battle_Space.TickLength;
            if (seekerTimeToTarget(attacker, target) < maxRange_time)
                isinRange = true;
            return isinRange;
        }

        public Fix16 seekerTimeToTarget(CombatObject attacker, CombatObject target)
        {
            Fix16 distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
            SeekingWeaponInfo seekerinfo = (SeekingWeaponInfo)weapon.Template.ComponentTemplate.WeaponInfo;
            int mass = seekerinfo.SeekerDurability; // sure why not?
            int wpnskrspd = seekerinfo.SeekerSpeed;
            Fix16 Thrust = (Fix16)wpnskrspd * mass * (Fix16)0.001;
            Fix16 acceleration = Thrust * mass;
            Fix16 startV = seekerClosingSpeed_base(attacker, target);
            //Fix16 endV = ???
            //Fix16 baseTimetoTarget = distance_toTarget / startV;
            
            //Fix16 deltaV = baseTimetoTarget
            Fix16[] ttt = GravMath.quadratic(acceleration, startV, distance_toTarget);
            Fix16 TimetoTarget;
            if (ttt[2] == 1)
            {
                TimetoTarget = Fix16.Min(ttt[0], ttt[1]);
            }
            else
                TimetoTarget = ttt[0];
            return TimetoTarget;
        }

        public Fix16 seekerClosingSpeed_base(CombatObject attacker, CombatObject target)
        {
            Fix16 shotspeed = boltSpeed; //speed of bullet when ship is at standstill
            Fix16 shotspeed_actual = shotspeed + GravMath.closingrate(attacker.cmbt_loc, attacker.cmbt_vel, target.cmbt_loc, target.cmbt_vel);
            return shotspeed_actual * Battle_Space.TickLength;
        }

        private bool bolt_isinRange(CombatObject attacker, CombatObject target)
        {
            bool isinRange = false;
            Fix16 TickLength = Battle_Space.TickLength;
            Fix16 boltTTT = boltTimeToTarget(attacker, target);
            //remember, maxRange is bolt lifetime in seconds 
            if (boltTTT <= maxRange / TickLength && boltTTT >= minRange / TickLength)
            {
                isinRange = true;
                //weaponRangeinfo += "Range for Projectile is good \r\n";
            }
            return isinRange;
        }

        public Fix16 boltClosingSpeed(CombatObject attacker, CombatObject target)
        {
            Fix16 shotspeed = boltSpeed; //speed of bullet when ship is at standstill
            Fix16 shotspeed_actual = shotspeed + GravMath.closingrate(attacker.cmbt_loc, attacker.cmbt_vel, target.cmbt_loc, target.cmbt_vel);
            return shotspeed_actual * Battle_Space.TickLength;
        }

        public Fix16 boltTimeToTarget(CombatObject attacker, CombatObject target)
        {
            Fix16 distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
            Fix16 boltTimetoTarget = distance_toTarget / boltClosingSpeed(attacker, target);
            return boltTimetoTarget;
        }


    }


    public class CombatNode
    {
        /// <summary>
        /// for creating bullets. & other non AI or acelerating things. used by the renderer to move and display.
        /// </summary>
        /// <param name="position"> ship position this is fiired from </param>
        /// <param name="vector">direction this is going</param>
        public CombatNode(Point3d position, Point3d vector, long ID, string IDPrefix)
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
        public Point3d cmbt_loc { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public Point3d cmbt_vel { get; set; }


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

        public CombatObject(ITargetable workingObject, Point3d position, Point3d vector, long ID, string IDprefix)
            : base(position, vector, ID, IDprefix)
        {
			WorkingObject = workingObject;
            this.waypointTarget = new combatWaypoint();
            weaponTarget = new List<CombatObject>(1); //eventualy this should be something with the multiplex tracking component.
            this.cmbt_thrust = new Point3d(0, 0, 0);
            this.cmbt_accel = new Point3d(0, 0, 0);
		}

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

		public Point3d cmbt_thrust { get; set; }

        public Point3d cmbt_accel { get; set; }

		public Fix16 cmbt_mass { get; set; }

		//public Point3d cmbt_maxThrust { get; set; }
		//public Point3d cmbt_minThrust { get; set; }


		public combatWaypoint waypointTarget;
		public Point3d lastVectortoWaypoint { get; set; }
		//public double lastDistancetoWaypoint { get; set; }

		public List<CombatObject> weaponTarget { get; set; }



		public Fix16 maxfowardThrust { get; set; }
        public Fix16 maxStrafeThrust { get; set; }
        public Fix16 maxRotate { get; set; } // TODO - make maxRotate a compass so we don't get confused between radians and degrees

		public PRNG getDice()
		{
			return shipDice;
		}
		public void newDice(int battleseed)
		{
			int seed = (int)(this.ID % 100000) + battleseed;
			shipDice = new PRNG(seed);
		}


        public string debuginfo = "";


        public virtual void helm()
        {
            combatWaypoint wpt = this.waypointTarget;
            Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 
            Compass angletoturn = new Compass(angletoWaypoint.Radians - this.cmbt_head.Radians);
            Point3d vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;

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

                //Point3d fowardthrust = new Point3d(comObj.cmbt_face + thrustby);
                Point3d fowardthrust = new Point3d(Trig.sides_ab(thrustby, this.cmbt_head.Radians));
                this.cmbt_thrust += fowardthrust;
            }
            else
            {
                //match velocity with waypoint
                Point3d wayptvel = this.waypointTarget.cmbt_vel;
                Point3d ourvel = this.cmbt_vel;

                thrustby = (Fix16)this.maxfowardThrust / (Fix16.Max((Fix16)1, angletoturn.Degrees / (Fix16)0.9));

                Point3d fowardthrust = new Point3d(Trig.intermediatePoint(ourvel, wayptvel, thrustby));

            }

        }

		public virtual int handleShieldDamage(int damage) { return damage; }
		public virtual int handleComponentDamage(int damage, DamageType damageType, PRNG attackersdice) { return damage; }

	}

	public class combatWaypoint
	{
		public combatWaypoint()
		{
			this.cmbt_loc = new Point3d(0, 0, 0);
			this.cmbt_vel = new Point3d(0, 0, 0);
		}
		public combatWaypoint(Point3d cmbt_loc)
		{
			this.cmbt_loc = cmbt_loc;
			this.cmbt_vel = new Point3d(0, 0, 0);
		}
		public combatWaypoint(Point3d cmbt_loc, Point3d cmbt_vel)
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
		public Point3d cmbt_loc { get; set; }

		/// <summary>
		/// combat velocity
		/// </summary>
		public Point3d cmbt_vel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public CombatObject comObj { get; set; }

	}
}
