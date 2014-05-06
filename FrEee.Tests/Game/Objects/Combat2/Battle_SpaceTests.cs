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

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(90, false);
            Compass endHeading = new Compass(startHeading.Radians + spinrate.Radians);
            // test 0 ship heading 0 waypoint at 90
            // ship should rotate positive by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Radians);
            
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

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(180, false);
            Compass endHeading = new Compass(startHeading.Radians - spinrate.Radians);
            // test 1 ship heading 0 waypoint at 180
            // ship should rotate either direction by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Radians);

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
            Compass endHeading = new Compass(startHeading.Radians - spinrate.Radians);
            //test 2 ship heading 0 waypoint at 270
            // ship should rotate negative by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Radians);

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
            Compass endHeading = new Compass(startHeading.Radians - spinrate.Radians);
            // test 3 ship heading 180 waypoint at 90
            // ship should rotate negative by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Radians);

            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
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
            Compass endHeading = new Compass(startHeading.Radians + spinrate.Radians);
            // test 4 ship heading 180 waypoint at 0
            // ship should rotate either direction by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Radians);
    
            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            //Assert.AreEqual(
            //    spinrate.Degrees,
            //    Fix16.Abs(testComObj.cmbt_head - (Fix16)180));
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
            Compass endHeading = new Compass(startHeading.Degrees + spinrate.Degrees);
            // test 5 ship heading 180 waypoint at 270
            // ship should rotate positive by turn rate but not past waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(180, false);
            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);
        }

        [TestMethod]
        public void Combat_turnship6()
        {
            Combat_setupbattle();

            Console.WriteLine("doing battle.start from test");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;

            Compass startHeading = new Compass(0, false);
            Compass wpCompass = new Compass(spinrate.Degrees / 2, false);
            Compass endHeading = new Compass(wpCompass.Degrees, false);
            // test 6 ship heading 0 waypoint at half turn rate
            // ship should rotate to face waypoint
            testComObj.cmbt_loc = new PointXd();
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
            battle.ProcessTick(ref tick, ref cmdFreqCounter);
            Assert.AreEqual(
                endHeading.Degrees,
                testComObj.cmbt_head.Degrees);

            battle.End(tick);
        }


        [TestMethod]
        public void Combat_ThrustShip0()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 0");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
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
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

            battle.End(tick);
        }

        [TestMethod]
        public void Combat_ThrustShip1()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 1");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
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
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

            battle.End(tick);
        }

        [TestMethod]
        public void Combat_ThrustShip2()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 2");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(45, false);
            PointXd expectedResult = new PointXd(0, 70.70159912109375, 0);
            bool toWaypoint = true;
            // Ship heading 0 Angle to turn 45
            // ship should thrust 70.70159912109375%
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);

            testComObj.testThrustShip(angletoTurn, toWaypoint);
            Assert.AreEqual(expectedResult, testComObj.cmbt_thrust);

            battle.End(tick);
        }

        [TestMethod]
        public void Combat_Helm0()
        {
            Combat_setupbattle();

            Console.WriteLine("Helm test 0");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
            Compass startHeading = new Compass(0, false);     
            
            PointXd waypntloc = new PointXd(0, 1000, 0);
            PointXd waypndVel = new PointXd(0, 1000, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            PointXd expectedThrustResult = new PointXd(0, 100, 0);
            Compass expectedHeading = new Compass(startHeading.Degrees + spinrate.Degrees, false);

            // Ship heading 0 Angle to turn 0
            // ship should thrust 100%
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            //battle.ProcessTick(ref tick, ref cmdFreqCounter);

            testComObj.helm();
            Assert.AreEqual(expectedThrustResult, testComObj.cmbt_thrust);
            Assert.AreEqual(
                expectedHeading.Degrees,
                testComObj.cmbt_head.Degrees);
            battle.End(tick);
        }

        [TestMethod]
        public void Combat_physicsTest0()
        {
            Combat_setupbattle();

            Console.WriteLine("Physics test 0: thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s ");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            PointXd expectedResult = new PointXd(0, 1, 0);


            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.cmbt_thrust = new PointXd(0, 100, 0); 

            //thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s 


            //battle.ProcessTick(ref tick, ref cmdFreqCounter);

            battle.SimNewtonianPhysics(testComObj);

            Assert.AreEqual(expectedResult, testComObj.cmbt_accel);

            battle.End(tick);
        }

        [TestMethod]
        public void Combat_physicsTest1()
        {
            Combat_setupbattle();

            Console.WriteLine("Thrust test 1");

            battle.Start();

            int tick = 0, cmdFreqCounter = 0;
            Compass startHeading = new Compass(0, false);
            Compass angletoTurn = new Compass(0, false);
            PointXd expectedResult = new PointXd(0, 50, 0); //0.5 * 1 * (100 / 10)^2 = 50


            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.cmbt_thrust = new PointXd(0, 100, 0);

            //thrust is 100N mass is 100N = (f / m = a) 100/100 = 1m/s 


            //battle.ProcessTick(ref tick, ref cmdFreqCounter);
            for (int i = 0; i < 100; i++)
            {
                battle.SimNewtonianPhysics(testComObj); //run 100 times to get 10 simseconds. 
            }           
            Assert.AreEqual(expectedResult, testComObj.cmbt_loc);

            battle.End(tick);
        }

    }
}
