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

                maxRange = (Fix16)wpMaxR * (Fix16)1000;
                minRange = (Fix16)wpMinR * (Fix16)1000;
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.ProjectileWeaponDisplayEffect))
            {
                weaponType = "Bolt";

                boltSpeed = (Fix16)wpMaxR * (Fix16)1000 * (Fix16)(Battle_Space.TickLength); // convert from kilometers per second to meters per tick
                maxRange = (Fix16)1; // (maxTime for bolts) untill modfiles can handle this, bolt weapons range is the distance it can go in 1 sec.
                minRange = ((Fix16)wpMinR / boltSpeed); //(minTime for bolts) distance / speed = time                  
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.SeekerWeaponDisplayEffect))
                weaponType = "Seeker";
            else
                weaponType = "Unknown";
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
        public Fix16 maxRange { get; private set; }

        /// <summary>
        /// if a bolt (or seeker?), this is time, else it's distance 
        /// </summary>
        public Fix16 minRange { get; private set; }

        public bool CanTarget(ICombatant target)
        {
            return weapon.CanTarget(target);
        }
       
    }


    public class CombatNode
    {
        /// <summary>
        /// for creating bullets. & other non AI or acelerating things. used by the renderer to move and display.
        /// </summary>
        /// <param name="position"> ship position this is fiired from </param>
        /// <param name="vector">direction this is going</param>
        public CombatNode(Point3d position, Point3d vector, long ID)
        {
            this.cmbt_loc = position;
            this.cmbt_vel = vector;
            this.cmbt_head = new Compass((Fix16)0);
            this.ID = ID;
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

        public long ID { get; private set; }
    }

	public class CombatObject : CombatNode
	{
		private ICombatant icomObj;

		private PRNG shipDice;

		public CombatObject(SpaceVehicle start_v, SpaceVehicle working_v, int battleseed)
            : this((ICombatant)start_v, (ICombatant)working_v, battleseed)
		{
            this.cmbt_mass = (Fix16)working_v.Size;
            this.maxfowardThrust = (Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1;
            this.maxStrafeThrust = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)4 - (Fix16)working_v.Evasion * (Fix16)0.01);
            this.maxRotate = ((Fix16)working_v.Speed * this.cmbt_mass * (Fix16)0.1) / ((Fix16)12000 - (Fix16)working_v.Evasion * (Fix16)0.1);
		}

		public CombatObject(Seeker s, int battleseed)
			: this(null, (ICombatant)s, battleseed)
		{
			this.cmbt_mass = (Fix16)s.MaxHitpoints; // sure why not?
            int wpnskrspd = s.WeaponInfo.SeekerSpeed;
            this.maxfowardThrust = (Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001;
            this.maxStrafeThrust = ((Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001) / ((Fix16)4 - (Fix16)s.Evasion * (Fix16)0.01);
            this.maxRotate = ((Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001) / ((Fix16)12 - (Fix16)s.Evasion * (Fix16)0.1);
		}



        public CombatObject(ICombatant startObj, ICombatant workingObj, int battleseed)
            : base(new Point3d(0, 0, 0), new Point3d(0, 0, 0), startObj.ID)
		{
            this.icomobj_WorkingCopy = workingObj;

            this.icomobj_StartCopy = startObj;   

			this.waypointTarget = new combatWaypoint();
			this.weaponTarget = new List<CombatObject>(1);//eventualy this should be something with the multiplex tracking component.

			this.cmbt_thrust = new Point3d(0, 0, 0);
			this.cmbt_accel = new Point3d(0, 0, 0);

            this.weaponList = new List<CombatWeapon>();
            foreach (Component weapon in icomobj_WorkingCopy.Weapons)
            {
                CombatWeapon wpn = new CombatWeapon(weapon);
                this.weaponList.Add(wpn);
            }
			newDice(battleseed);
		}



		/// <summary>
		/// between phys tic locations. 
		/// </summary>
		//public Point3d rndr_loc { get; set; }

		/// <summary>
		/// facing towards this point
		/// </summary>
		//public Point3d cmbt_face { get; set; }




		/// <summary>
		/// ship attitude, ie angle from level plain (0/360) pointing straight up (90)
		/// </summary>
		public Compass cmbt_att { get; set; }

		public Point3d cmbt_thrust { get; set; }

        public Point3d cmbt_accel { get; set; }

		public Fix16 cmbt_mass { get; set; }

		//public Point3d cmbt_maxThrust { get; set; }
		//public Point3d cmbt_minThrust { get; set; }

		public ICombatant icomobj_WorkingCopy
		{

			get { return this.icomObj; }
			set { this.icomObj = value; }
		}

        public ICombatant icomobj_StartCopy
        {
            get;
            set;
        }

        public void renewtoStart()
        {
            var ship = icomobj_StartCopy.Copy();
            ship.IsMemory = true;
            if (ship.Owner != icomobj_StartCopy.Owner)
                ship.Owner.Dispose(); // don't need extra empires!

            // copy over the components individually so they can take damage without affecting the starting state
            // TODO - deal with planets in combat
            ((SpaceVehicle)ship).Components.Clear();
            foreach (var comp in ((SpaceVehicle)icomobj_StartCopy).Components)
                ((SpaceVehicle)ship).Components.Add(comp.Copy());

            this.icomObj = ship;

            this.cmbt_loc = new Point3d(0, 0, 0);
            this.cmbt_vel = new Point3d(0, 0, 0);
            this.cmbt_head = new Compass(0);
            this.cmbt_att = new Compass(0);
            this.cmbt_thrust = new Point3d(0, 0, 0);
            this.cmbt_accel = new Point3d(0, 0, 0);
        }

		public combatWaypoint waypointTarget;
		public Point3d lastVectortoWaypoint { get; set; }
		//public double lastDistancetoWaypoint { get; set; }

		public List<CombatObject> weaponTarget { get; set; }

        public List<CombatWeapon> weaponList { get; set; }

		public Fix16 maxfowardThrust { get; set; }
        public Fix16 maxStrafeThrust { get; set; }
        public Fix16 maxRotate { get; set; } // TODO - make maxRotate a compass so we don't get confused between radians and degrees

		public PRNG getDice()
		{
			return shipDice;
		}
		public void newDice(int battleseed)
		{
			int seed = (int)(this.icomobj_WorkingCopy.ID % 100000) + battleseed;
			shipDice = new PRNG(seed);
		}


        public string debuginfo = "";
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
