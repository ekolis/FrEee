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

        public void setupbattle()
        {                        
            Sector location = new Sector(sys, new System.Drawing.Point());
            Empire emp = new Empire();

            MockCombatant combatant = new MockCombatant(emp);
       
            testComObj = new CombatObject(combatant, new PointXd(), new PointXd(), 42, "SHP");
       
            spinrate = new Compass(5, false);
            testComObj.maxRotate = spinrate;
            testComObj.cmbt_accel = new PointXd(0, 0, 0);
            testComObj.maxStrafeThrust = (Fix16)0;
            ICombatant[] combatants = new ICombatant[] { combatant };

            foreach (ISpaceObject ispobj in (combatants))
            {
                location.Place(ispobj);
            }

            battle = new Battle_Space(location);

            battle.StartNodes.Add(testComObj);

        }

        [TestMethod]
        public void turnship0()
        {
            setupbattle();

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
        public void turnship1()
        {
            setupbattle();

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
        public void turnship2()
        {
            setupbattle();

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
        public void turnship3()
        {
            setupbattle();

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
        public void turnship4()
        {
            setupbattle();

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
        public void turnship5()
        {
            setupbattle();

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
        public void turnship6()
        {
            setupbattle();

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
    }
}
