using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Combat2;

namespace FrEee.Tests.Game.Objects.Combat2
{
    [TestClass]
    public class Point3dTests
    {
        [TestMethod]
        public void pythagC()
        {
            double side_at0 = 3;
            double side_bt0 = 4;
            Assert.AreEqual(5, Trig.pythagC(side_at0, side_bt0));

            double side_at1 = 5;
            double side_bt1 = 12;
            Assert.AreEqual(13,Trig.pythagC(side_at1, side_bt1));
        }
    }
}
