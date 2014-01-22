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
            this.cmbt_loc = new Point3d(0, 0, 0);
            this.cmbt_vel = new Point3d(0, 0, 0);
            this.cmbt_head = new Compass(0);
            this.cmbt_att = new Compass(0);
            this.cmbt_thrust = new Point3d(0, 0, 0);
            this.cmbt_accel = new Point3d(0, 0, 0);
        }

        public string debuginfo = "";


        public virtual void helm()
        {
            combatWaypoint wpt = this.waypointTarget;
            Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 
            Compass angletoturn = new Compass(angletoWaypoint.Radians - this.cmbt_head.Radians);
            Point3d vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;

            Fix16 acceleration = maxfowardThrust * cmbt_mass;
            Fix16 startV = Trig.distance(cmbt_vel, wpt.cmbt_vel);
            Fix16 distance = Trig.distance(cmbt_loc, wpt.cmbt_loc);
            Fix16[] ttt = GravMath.quadratic(acceleration, startV, distance);


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

        #endregion
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
