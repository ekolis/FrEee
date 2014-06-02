using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects;

using FrEee.Modding;
using FrEee.Utility.Extensions;

using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;

using NewtMath.f16;

using FixMath.NET;

namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Battle_SpaceTests
    {
        //private HashSet<SimulatedEmpire> Empires { get; set; }

        //private SimulatedEmpire CurrentEmpire { get; set; }

        //private SimulatedSpaceObject CurrentSpaceObject { get; set; }

        //Galaxy gal = new Galaxy(); // always need one of these...
        //StarSystem sys = new StarSystem(1);
        //Sector sector; 
        //Empire emp = new Empire();
        //MockCombatant combatant;

//        [TestMethod]
        //public void simulateBattle()
        //{
        //    sector = new Sector(sys, new System.Drawing.Point());
        //    combatant = new MockCombatant(emp);
        //    //Sector Sector = new Sector(new StarSystem(0), new System.Drawing.Point());
        //    Empires = new HashSet<SimulatedEmpire>(Galaxy.Current.Empires.Except((Empire)null).Select(e => new SimulatedEmpire(e)));
        //    //var battle = new Battle_Space(Empires.SelectMany(se => se.SpaceObjects.Select(ss => ss.SpaceObject)));
            
        //    //var battle = new Battle_Space(
        //}
        Galaxy gal = new Galaxy(); // always need one of these...
        StarSystem sys = new StarSystem(1);
        CombatObject testComObj;
        Battle_Space battle;
        Compass spinrate;

        public void Combat_setupbattle()
        {                        
            Sector location = new Sector(sys, new System.Drawing.Point());
            Empire emp = new Empire();

            MockCombatant combatant = new MockCombatant(emp);
       
            testComObj = new CombatObject(combatant, new PointXd(), new PointXd(), 42, "SHP");
       
            spinrate = new Compass(5, false);
            testComObj.maxRotate = spinrate;
            testComObj.cmbt_accel = new PointXd(0, 0, 0);
            testComObj.maxfowardThrust = 100;
            testComObj.maxStrafeThrust = (Fix16)0;
            testComObj.cmbt_mass = 100;
            ICombatant[] combatants = new ICombatant[] { combatant };

            foreach (ISpaceObject ispobj in (combatants))
            {
                location.Place(ispobj);
            }

            battle = new Battle_Space(location);

            battle.StartNodes.Add(testComObj);

        }

        [TestMethod]
        public void Combat_turnship0()
        {
            Combat_setupbattle();

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(90, false);
            Compass endHeading = new Compass(startHeading.Degrees + (spinrate.Degrees / Battle_Space.TicksPerSecond), false);
            // test 0 ship heading 0 waypoint at 90
            // ship should rotate positive by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;
            
            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }
        [TestMethod]
        public void Combat_turnship1()
        {
            Combat_setupbattle();

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(180, false);
            Compass endHeading = new Compass(startHeading.Degrees - (spinrate.Degrees/Battle_Space.TicksPerSecond), false);
            // test 1 ship heading 0 waypoint at 180
            // ship should rotate either direction by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship2()
        {
            Combat_setupbattle();

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(270, false);
            Compass endHeading = new Compass(startHeading.Degrees - (spinrate.Degrees / Battle_Space.TicksPerSecond), false);
            //test 2 ship heading 0 waypoint at 270
            // ship should rotate negative by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            //Assert.AreEqual(
            //    Compass.NormalizeDegrees(Fix16.Max((Fix16)360 - spinrate.Degrees, (Fix16)270)),
            //    testComObj.cmbt_head.Degrees);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship3()
        {
            Combat_setupbattle();

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(180, false);
            Compass wpCompass = new Compass(90, false);
            Compass endHeading = new Compass(startHeading.Degrees - (spinrate.Degrees / Battle_Space.TicksPerSecond), false);
            // test 3 ship heading 180 waypoint at 90
            // ship should rotate negative by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            battle.End(tick);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship4()
        {
            Combat_setupbattle();

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(180, false);
            Compass wpCompass = new Compass(0, false);
            Compass endHeading = new Compass(startHeading.Degrees + (spinrate.Degrees / Battle_Space.TicksPerSecond), false);
            // test 4 ship heading 180 waypoint at 0
            // ship should rotate either direction by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;
    
            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            //Assert.AreEqual(
            //    spinrate.Degrees,
            //    Fix16.Abs(testComObj.cmbt_head - (Fix16)180));
            battle.End(tick);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship5()
        {
            Combat_setupbattle();

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(180, false);
            Compass wpCompass = new Compass(270, false);
            Compass endHeading = new Compass(startHeading.Degrees + (spinrate.Degrees / Battle_Space.TicksPerSecond), false);
            // test 5 ship heading 180 waypoint at 270
            // ship should rotate positive by turn rate but not more than the spinrate.
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            battle.End(tick);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship6() //I think this is a floating point error from when it gets a point from a bearing. 
        {
            Combat_setupbattle();

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(spinrate.Degrees / 2, false);
            Compass endHeading = new Compass(wpCompass.Degrees, false);
            // test 6 ship heading 0 waypoint at half turn rate
            // ship should rotate to face waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = startHeading;

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)5));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            battle.End(tick);
			AssertAngleWithinTolerance(endHeading, testComObj.cmbt_head, 0.01);

            battle.End(tick);
        }


        [TestMethod]
        public void Combat_ThrustShip0()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 0");

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            PointXd expectedResult = new PointXd(0, 100, 0);
            bool toWaypoint = true;
            // Ship heading 0 Angle to turn 0
            // ship should thrust 100%
            testComObj.cmbt_loc = new PointXd(0,0,0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            //battle.ProcessTick(ref tick, ref cmdFreqCounter);
            
            testComObj.testThrustShip(angletoTurn, toWaypoint);

            battle.End(1);
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

        }

        [TestMethod]
        public void Combat_ThrustShip1()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 1");

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(180, false);
            PointXd expectedResult = new PointXd(0, 0, 0);
            bool toWaypoint = false;
            // Ship heading 0 Angle to turn 180
            // ship should thrust 0%
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            //battle.ProcessTick(ref tick, ref cmdFreqCounter);

            testComObj.testThrustShip(angletoTurn, toWaypoint);
            battle.End(1);
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

        }

        [TestMethod]
        public void Combat_ThrustShip2()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 2");

            battle.Start();


            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(45, false);
            PointXd expectedResult = new PointXd(0, 70.70159912109375, 0);
            bool toWaypoint = true;
            // Ship heading 0 Angle to turn 45
            // ship should thrust 70.70159912109375%
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);

            testComObj.testThrustShip(angletoTurn, toWaypoint);
            battle.End(1);
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

        }

        [TestMethod]
        public void Combat_Helm0()
        {
            Combat_setupbattle();

            Console.WriteLine("Helm test 0");

            battle.Start();

            Compass startHeading = new Compass(0, false);     
            
            PointXd waypntloc = new PointXd(0, 1000, 0);
            PointXd waypndVel = new PointXd(0, 1000, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            PointXd expectedThrustResult = new PointXd(0, 100, 0);
            Compass expectedHeading = new Compass(startHeading.Degrees);// + (spinrate.Degrees / Battle_Space.TicksPerSecond), false);

            // Ship heading 0 Angle to turn 0
            // ship should thrust 100%
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
			testComObj.waypointTarget = waypoint;
            //battle.ProcessTick(ref tick, ref cmdFreqCounter);

            testComObj.helm();
            battle.End(1);
            AssertPointWithinTolerance(expectedThrustResult, testComObj.cmbt_thrust, 1);
			AssertAngleWithinTolerance(expectedHeading, testComObj.cmbt_head, 0.01);
        }

        [TestMethod]
        public void Combat_Nav0()
        {
            Combat_setupbattle();

            Console.WriteLine("Nav test 0");

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(0, false);
            PointXd waypntloc = new PointXd(0, 1000, 0);
            PointXd waypndVel = new PointXd(0, 1000, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedToWaypoint = true;
            Compass expectedHeading = new Compass(0);
            Tuple<Compass, bool> expectednav = new Tuple<Compass, bool>(expectedHeading, expectedToWaypoint);

            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            Tuple<Compass, bool?> nav = testComObj.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_Nav1()
        {
            Combat_setupbattle();

            Console.WriteLine("Nav test 1");

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(180, false);
            //PointXd waypntloc = new PointXd(0, -1000, 0);
            //PointXd waypndVel = new PointXd(0, 0, 0);
            //combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedToWaypoint = true;
            Compass expectedHeading = new Compass(180, false);
            Tuple<Compass, bool?> expectednav = new Tuple<Compass, bool?>(expectedHeading, expectedToWaypoint);

            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            Tuple<Compass, bool?> nav = testComObj.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_physicsTest0()
        {
            Combat_setupbattle();

            Console.WriteLine("Physics test 0: thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s/s ");

            battle.Start();

  
            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            PointXd expectedResult = new PointXd(0, 1, 0);


            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.cmbt_thrust = new PointXd(0, 100, 0); 

            //thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s 


            //battle.ProcessTick(ref tick, ref cmdFreqCounter);

            battle.SimNewtonianPhysics(testComObj);
            battle.End(1);
            Assert.AreEqual(expectedResult, testComObj.cmbt_accel);

        }

        [TestMethod]
        public void Combat_physicsTest1()
        {
            Combat_setupbattle();


            battle.Start();


            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            Fix16 t = (Fix16)1 / Battle_Space.TicksPerSecond;
            PointXd expectedResult = new PointXd(0, 0.5 * 1 * t * t, 0);
            // (1/2 * a * t^2) = (0.5 * a * t * t) = 0.5 * 1 * t * t)

            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.cmbt_thrust = new PointXd(0, 100, 0);

            //thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s/s 

            battle.SimNewtonianPhysics(testComObj); 
            battle.End(1);
            AssertPointWithinTolerance(expectedResult, testComObj.cmbt_loc, 1);
        }

        [TestMethod]
        public void Combat_physicsTest2()
        {
            Combat_setupbattle();

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            PointXd expectedResult = new PointXd(0, 0.5, 0); 
            // (1/2 * a * t^2) = (0.5 * a * t * t) = 0.5 * 1 * 1 * 1)

            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.cmbt_thrust = new PointXd(0, 100, 0);

            //thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s/s 

            for (int i = 0; i < Battle_Space.TicksPerSecond; i++)
            {
                battle.SimNewtonianPhysics(testComObj); //run * TicksPerSecond to get 1 simsecond. 
            }
            battle.End(1);
            AssertPointWithinTolerance(expectedResult, testComObj.cmbt_loc, 1);
        }

		/// <summary>
		/// Asserts that two points are "close enough" within a certain tolerance.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		private void AssertPointWithinTolerance(PointXd expected, PointXd actual, Fix16 tolerance)
		{
			var dist = (actual - expected).Length;
			Assert.IsTrue(dist <= tolerance, "distance between points must not be greater than " + tolerance + "; was " + dist);
		}

		/// <summary>
		/// Asserts that two points are "close enough" within a certain tolerance.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		private void AssertPointWithinTolerance(Compass expectedAngle, Fix16 expectedRadius, Compass actualAngle, Fix16 actualRadius, Fix16 tolerance)
		{
			AssertPointWithinTolerance(expectedAngle.Point(expectedRadius), actualAngle.Point(actualRadius), tolerance);
		}

		/// <summary>
		/// Asserts that two angles are "close enough" that points spaced at radius 1 are going to be within a certain tolerance.
		/// </summary>
		/// <param name="expectedAngle"></param>
		/// <param name="actualAngle"></param>
		/// <param name="tolerancePerRadius"></param>
		private void AssertAngleWithinTolerance(Compass expectedAngle, Compass actualAngle, Fix16 tolerancePerRadius)
		{
			AssertPointWithinTolerance(expectedAngle, 1, actualAngle, 1, tolerancePerRadius);
		}
    }
}
