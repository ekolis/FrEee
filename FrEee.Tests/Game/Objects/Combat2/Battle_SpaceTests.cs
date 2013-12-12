using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Vehicles;

namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Battle_SpaceTests
    {
        
        [TestMethod]
        public void turnship()
        {
            CombatObject testComObj = null;
            int degreesPerTic = 5;
            

            //test 0 ship heading 0 waypoint at 90
            Compass angletoturn_t0 = new Compass(90, false);
            Compass angletotarget_t0 = new Compass(90, false);
            Compass shipHeading_t0 = new Compass(0);
            Compass angleExpected_t0 = new Compass(5, false);

            //test 1 ship heading 0 waypoint at 180
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

            Assert.AreEqual(testComObj.cmbt_head, angleExpected_t0);
        }
    }
}
