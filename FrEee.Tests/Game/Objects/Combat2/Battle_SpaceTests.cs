using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Drawing;
using FrEee.Game.Interfaces;

namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Battle_SpaceTests
    {
        
        [TestMethod]
        public void turnship()
        {
			var gal = new Galaxy(); // always need one of these...
			var sys = new StarSystem(1);
			var emp = new Empire();
			var combatant = new MockCombatant(emp);
            var testComObj = new CombatObject(combatant, 42);
			testComObj.maxRotate = 5;
			testComObj.cmbt_accel = new Point3d(0, 0);
			testComObj.maxStrafeThrust = 0;
			var combatants = new ICombatant[] { combatant };
			var battle = new Battle_Space(combatants);
			battle.Combatants.Add(combatant);
			battle.CombatObjects.Add(testComObj);

			battle.Start();

			int tick = 1, cmdFreqCounter = 0;

			Compass wpCompass = new Compass();

            // test 0 ship heading 0 waypoint at 90
			// ship should rotate positive by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(0, false);
			wpCompass.Degrees = 90;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				Math.Min(testComObj.maxRotate, 90),
				testComObj.cmbt_head.Degrees);
			
            // test 1 ship heading 0 waypoint at 180
			// ship should rotate either direction by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(0, false);
			wpCompass.Degrees = 180;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				Math.Min(testComObj.maxRotate, 180),
				Math.Abs(testComObj.cmbt_head.Degrees));

            //test 2 ship heading 0 waypoint at 270
			// ship should rotate negative by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(0, false);
			wpCompass.Degrees = 270;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				Compass.NormalizeDegrees(Math.Max(360 - testComObj.maxRotate, 270)),
				testComObj.cmbt_head.Degrees);

            // test 3 ship heading 180 waypoint at 90
			// ship should rotate negative by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(180, false);
			wpCompass.Degrees = 90;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				Compass.NormalizeDegrees(Math.Max(180 - testComObj.maxRotate, 90)),
				testComObj.cmbt_head.Degrees);

            // test 4 ship heading 180 waypoint at 0
			// ship should rotate either direction by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(180, false);
			wpCompass.Degrees = 0;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				testComObj.maxRotate,
				Math.Abs(testComObj.cmbt_head - 180));

            // test 5 ship heading 180 waypoint at 270
			// ship should rotate positive by turn rate but not past waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(180, false);
			wpCompass.Degrees = 270;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				Compass.NormalizeDegrees(Math.Min(180 + testComObj.maxRotate, 270)),
				testComObj.cmbt_head.Degrees);

			// test 6 ship heading 0 waypoint at half turn rate
			// ship should rotate to face waypoint
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(0, false);
			wpCompass.Degrees = testComObj.maxRotate / 2;
			testComObj.waypointTarget = new combatWaypoint(wpCompass.Point(1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(
				wpCompass.Degrees,
				testComObj.cmbt_head.Degrees);

			battle.End();
        }
    }
}
