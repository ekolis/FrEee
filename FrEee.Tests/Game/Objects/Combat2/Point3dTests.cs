using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;
using FixMath.NET;
using NewtMath.f16;
namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Point3dTests
    {
        [TestMethod]
        public void pythagC()
        {
            Fix16 side_at0 = (Fix16)3;
            Fix16 side_bt0 = (Fix16)4;
            Fix16 expectedResult = 5;
            Assert.AreEqual(expectedResult, Trig.pythagC(side_at0, side_bt0));

            Fix16 side_at1 = (Fix16)5;
            Fix16 side_bt1 = (Fix16)12;

            expectedResult = 13;
            Assert.AreEqual(expectedResult, Trig.pythagC(side_at1, side_bt1));
        }

        [TestMethod]
        public void closingRate()
        {
            //easy test
            PointXd p1 = new PointXd(-10, 0, 0);
            PointXd v1 = new PointXd(5, 0, 0);

            PointXd p2 = new PointXd(10, 0, 0);
            PointXd v2 = new PointXd(0, 0, 0);

            Fix16 expectedResult = 5;

            Assert.AreEqual(expectedResult, NMath.closingRate(p1, v1, p2, v2));

            //edge case, parrellel.
            p1 = new PointXd(-0, -5, 0);
            v1 = new PointXd(0, 0, 0);

            p2 = new PointXd(0, 0, 0);
            v2 = new PointXd(1, -1, 0);

            expectedResult = 1;

            Assert.AreEqual(expectedResult, NMath.closingRate(p1, v1, p2, v2));

            //edge case, parrellel.
            p1 = new PointXd(-10, 0, 0);
            v1 = new PointXd(10, 0, 0);

            p2 = new PointXd(10, 0, 0);
            v2 = new PointXd(10, 0, 0);

            expectedResult = 0;

            Assert.AreEqual(expectedResult, NMath.closingRate(p1, v1, p2, v2));

            //moving away from each other.
            p1 = new PointXd(-10, 0, 0);
            v1 = new PointXd(-10, 0, 0);

            p2 = new PointXd(10, 0, 0);
            v2 = new PointXd(0, 0, 0);

            expectedResult = -10;

            Assert.AreEqual(expectedResult, NMath.closingRate(p1, v1, p2, v2));
        }

        [TestMethod]
        public void distance()
        {
            PointXd p1 = new PointXd(0, 0, 0);
            PointXd p2 = new PointXd(0, 10, 0);
            Fix16 expectedResult = 10;

            Assert.AreEqual(expectedResult, NMath.distance(p1, p2));

        }
    }
}
