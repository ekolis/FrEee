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
        public void closingVector()
        {
            Point3d p1 = new Point3d(-10, 0, 0);
            Point3d v1 = new Point3d(5, 5, 0);

            Point3d p2 = new Point3d(5, 15, 0);
            Point3d v2 = new Point3d(-3, -3, 0);

            Point3d result = new Point3d(8, 8, 0);
            
            //Assert.AreEqual(result, GravMath.closingVector(p1, v1, p2, v2));       
        }
    }
}
