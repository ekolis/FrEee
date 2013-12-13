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
            

            //test 0 ship heading 0 waypoint at 90
			testComObj.cmbt_loc = new Point3d();
			testComObj.cmbt_head = new Compass(0, false);
			testComObj.waypointTarget = new combatWaypoint(new Point3d(0, 1));
			battle.ProcessTick(ref tick, ref cmdFreqCounter);
			Assert.AreEqual(testComObj.maxRotate, testComObj.cmbt_head.Degrees);


            /*//test 1 ship heading 0 waypoint at 180
            Compass angletoturn_t1 = new Compass(180, false);
            Compass angletotarget_t1 = new Compass(180, false);
            Compass shipHeading_t1 = new Compass(0);
            Compass angleExpected_t1 = new Compass(5, false);

            //test 2 ship heading 0 waypoint at 270
            Compass angletoturn_t2 = new Compass(270, false);
            Compass angletotarget_t2 = new Compass(270, false);
            Compass shipHeading_t2 = new Compass(0);
            Compass angleExpected_t2 = new Compass(-5, false);

            //test 3 ship heading 180 waypoint at 90
            Compass angletoturn_t3 = new Compass(90, false);
            Compass angletotarget_t3 = new Compass(270,false);
            Compass shipHeading_t3 = new Compass(180);
            Compass angleExpected_t3 = new Compass(175, false);

            //test 4 ship heading 180 waypoint at 0
            Compass angletoturn_t4 = new Compass(180, false);
            Compass angletotarget_t4 = new Compass(0, false);
            Compass shipHeading_t4 = new Compass(180);
            Compass angleExpected_t4 = new Compass(185, false);

            //test 5 ship heading 180 waypoint at 270
            Compass angletoturn_t5 = new Compass(90, false);
            Compass angletotarget_t5 = new Compass(270, false);
            Compass shipHeading_t5 = new Compass(180);
            Compass angleExpected_t5 = new Compass(185, false);

            Assert.AreEqual(testComObj.cmbt_head, angleExpected_t0);*/

			battle.End();
        }
    }
}
