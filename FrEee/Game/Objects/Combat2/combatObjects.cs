using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Combat2
{
    public class EmpireinCombat
    {
        public List<CombatObj> ownships;
        public List<CombatObj> friendly;
        public List<CombatObj> nutral; //not actualy used.
        public List<CombatObj> hostile;
        public EmpireinCombat()
        { }
    }

    public class CombatObj
    {
        private ICombatObject comObj;

        public CombatObj(ICombatObject comObj)
        {
            this.comObj = comObj;
            Vehicles.SpaceVehicle ship = (Vehicles.SpaceVehicle)comObj;
            this.Accel = ship.Speed;
            this.Strafe = ship.Speed / 4;
            this.Rotate = ship.Speed / 12;
        }

        /// <summary>
        /// location within the sector
        /// </summary>
        public Point3d cmbt_loc { get; set; }


        /// <summary>
        /// between phys tic locations. 
        /// </summary>
        public Point3d rndr_loc { get; set; }

        /// <summary>
        /// facing towards this point
        /// </summary>
        public Point3d cmbt_face { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public Point3d cmbt_vel { get; set; }

        public ICombatObject icomobj
        {

            get { return this.comObj; }
            set { this.comObj = value; }
        }

        public combatWaypoint waypointTarget { get; set; }
        public Point3d lastVectortoWaypoint { get; set; }
        //public double lastDistancetoWaypoint { get; set; }

        public List<CombatObj> weaponTarget { get; set; }

        public int Accel { get; set; }
        public int Strafe { get; set; }
        public int Rotate { get; set; }

    }

    public class combatReplayLog
    {
        Dictionary<double, List<combatEvent>> log;
        public combatReplayLog()
        {
            combatEvent comevnt = new combatEvent(0, "startlog", null, null);
            log = new Dictionary<double, List<combatEvent>>();
            List<combatEvent> evntlist = new List<combatEvent>();
            evntlist.Add(comevnt);
            log.Add(0, new List<combatEvent>(evntlist));
        }

        public void addEvent(double tic, combatEvent comevnt)
        {
            if (log.ContainsKey(tic))
            {
                log[tic].Add(comevnt);
            }
            else
            {
                List<combatEvent> evntlist = new List<combatEvent>();
                evntlist.Add(comevnt);
                log.Add(tic, new List<combatEvent>(evntlist));
            }
        }
    }

    public class combatEvent
    {
        double evnttic;
        string evnttype;
        CombatObj comObj;
        //something component/weapon type
        CombatObj target;
        bool hit;
        double endpoint_tic;
        Point3d endpoint;
        public combatEvent(double tic, string type, CombatObj comObj, CombatObj target)
        {
            this.evnttic = tic;
            this.evnttype = type;
            this.comObj = comObj;
            this.target = target;
        }
        public void hitTarget(bool hit)
        {
            this.hit = hit;
        }
        public void endevent(double endtic, Point3d endpoint)
        {
            this.endpoint_tic = endtic;
            this.endpoint = endpoint;
        }
    }

    public class combatWaypoint
    {
        public combatWaypoint()
        { }
        public combatWaypoint(Point3d cmbt_loc)
        {
            this.cmbt_loc = cmbt_loc;
        }
        public combatWaypoint(CombatObj comObj)
        {
            this.comObj = comObj;
            this.cmbt_loc = comObj.cmbt_loc;
            this.cmbt_vel = comObj.cmbt_vel;
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
        public CombatObj comObj { get; set; }

    }
}
