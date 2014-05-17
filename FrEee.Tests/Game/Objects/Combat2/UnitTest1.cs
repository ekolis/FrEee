using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;

using NewtMath.f16;
namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class CombatObjectTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public class CombatTestObject //: CombatObject
    {
        public CombatTestObject()        
        {
            //this.comObj = comObj;
            //Vehicles.SpaceVehicle ship = (Vehicles.SpaceVehicle)comObj;
            int speed = 6;
            int evasion = 40;
            this.cmbt_mass = 150;
            this.maxfowardThrust = speed / this.cmbt_mass;
            this.maxStrafeThrust = (speed / this.cmbt_mass) / (4 - evasion * 0.01);
            this.maxRotate = (speed / this.cmbt_mass) / (12 - evasion * 0.1);

            //this.waypointTarget = new combatWaypoint();
            //this.weaponTarget = new List<CombatObject>(1);//eventualy this should be something with the multiplex tracking component.

            this.cmbt_thrust = new PointXd(0, 0, 0);
            this.cmbt_accel = new PointXd(0, 0, 0);
        }
        /// <summary>
        /// location within the sector
        /// </summary>
        public PointXd cmbt_loc { get; set; }

        /// <summary>
        /// between phys tic locations. 
        /// </summary>
        public PointXd rndr_loc { get; set; }

        /// <summary>
        /// facing towards this point
        /// </summary>
        //public Point3d cmbt_face { get; set; }


        /// <summary>
        /// ship heading 
        /// </summary>
        public Compass cmbt_head { get; set; }

        /// <summary>
        /// ship attitude, ie angle from level plain (0/360) pointing straight up (90)
        /// </summary>
        public Compass cmbt_att { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public PointXd cmbt_vel { get; set; }

        public PointXd cmbt_accel { get; set; }

        public PointXd cmbt_thrust { get; set; }

        public double cmbt_mass { get; set; }

        public double maxfowardThrust { get; set; }
        public double maxStrafeThrust { get; set; }
        public double maxRotate { get; set; }
    }
}
