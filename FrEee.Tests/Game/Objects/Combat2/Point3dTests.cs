using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;
using FixMath.NET;
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
            Assert.AreEqual(5, Trig.pythagC(side_at0, side_bt0));

            Fix16 side_at1 = (Fix16)5;
            Fix16 side_bt1 = (Fix16)12;
            Assert.AreEqual(13,Trig.pythagC(side_at1, side_bt1));
        }

        [TestMethod]
        public void closingRate()
        {
            Point3d p1 = new Point3d(-10, 0, 0);
            Point3d v1 = new Point3d(5, 0, 0);

            Point3d p2 = new Point3d(10, 0, 0);
            Point3d v2 = new Point3d(0, 0, 0);

            Fix16 result = 5;

            Assert.AreEqual(result, GravMath.closingRate(p1, v1, p2, v2));       
        }

        //[TestMethod]
        //public void distance()
        //{
        //    Point3d p1 = new Point3d(
        //}
    }
}
