using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Drawing;
using FrEee.Game.Interfaces;

using FixMath.NET;

namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Battle_SpaceTests
    {
        
        //[TestMethod]
        //public void turnship()
        //{
        //    var gal = new Galaxy(); // always need one of these...
        //    var sys = new StarSystem(1);
        //    var emp = new Empire();
        //    var combatant = new MockCombatant(emp);
        //    //var vCombatant = MockCombatant;
        //    //var testComObj = new CombatObject(combatant, new Point3d(), new Point3d(), 42);
        //    var testComObj = new CombatVehicle(combatant, combatant, 1);
        //    var spinrate = new Compass(5, false);
        //    testComObj.maxRotate = spinrate.Radians;
        //    testComObj.cmbt_accel = new Point3d(0, 0, 0);
        //    testComObj.maxStrafeThrust = (Fix16)0;
        //    var combatants = new ICombatant[] { combatant };
        //    var battle = new Battle_Space(combatants);
        //    //battle.WorkingCombatants.Add(combatant);
        //    battle.CombatNodes.Add(testComObj);

        //    battle.Start();

        //    int tick = 1, cmdFreqCounter = 0;

        //    Compass wpCompass = new Compass();

        //    // test 0 ship heading 0 waypoint at 90
        //    // ship should rotate positive by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(0, false);
        //    wpCompass.Degrees = (Fix16)90;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        Fix16.Min(spinrate.Degrees, (Fix16)90),
        //        testComObj.cmbt_head.Degrees);
			
        //    // test 1 ship heading 0 waypoint at 180
        //    // ship should rotate either direction by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(0, false);
        //    wpCompass.Degrees = (Fix16)180;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        Fix16.Min(spinrate.Degrees, (Fix16)180),
        //        Fix16.Abs(testComObj.cmbt_head.Degrees));

        //    //test 2 ship heading 0 waypoint at 270
        //    // ship should rotate negative by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(0, false);
        //    wpCompass.Degrees = (Fix16)270;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        Compass.NormalizeDegrees(Fix16.Max((Fix16)360 - spinrate.Degrees, (Fix16)270)),
        //        testComObj.cmbt_head.Degrees);

        //    // test 3 ship heading 180 waypoint at 90
        //    // ship should rotate negative by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(180, false);
        //    wpCompass.Degrees = (Fix16)90;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        Compass.NormalizeDegrees(Fix16.Max((Fix16)180 - spinrate.Degrees, (Fix16)90)),
        //        testComObj.cmbt_head.Degrees);

        //    // test 4 ship heading 180 waypoint at 0
        //    // ship should rotate either direction by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(180, false);
        //    wpCompass.Degrees = (Fix16)0;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        spinrate.Degrees,
        //        Fix16.Abs(testComObj.cmbt_head - (Fix16)180));

        //    // test 5 ship heading 180 waypoint at 270
        //    // ship should rotate positive by turn rate but not past waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(180, false);
        //    wpCompass.Degrees = (Fix16)270;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        Compass.NormalizeDegrees(Fix16.Min((Fix16)180 + spinrate.Degrees, (Fix16)270)),
        //        testComObj.cmbt_head.Degrees);

        //    // test 6 ship heading 0 waypoint at half turn rate
        //    // ship should rotate to face waypoint
        //    testComObj.cmbt_loc = new Point3d();
        //    testComObj.cmbt_head = new Compass(0, false);
        //    wpCompass.Degrees = testComObj.maxRotate / (Fix16)2;
        //    testComObj.waypointTarget = new combatWaypoint(wpCompass.Point((Fix16)1));
        //    battle.ProcessTick(ref tick, ref cmdFreqCounter);
        //    Assert.AreEqual(
        //        wpCompass.Degrees,
        //        testComObj.cmbt_head.Degrees);

        //    battle.End();
        //}
    }
}
