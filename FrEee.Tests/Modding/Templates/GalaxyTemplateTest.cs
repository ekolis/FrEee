using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.Tests.Modding
{
	/// <summary>
	/// Tests galaxy templates.
	/// </summary>
	[TestClass]
	public class GalaxyTemplateTest
	{
		[TestMethod]
		public void IntersectionsAtEndpointsDoNotCount()
		{
			var p1 = new Point(0, 0);
			var p2 = new Point(10, 0);
			var q1 = new Point(10, 0);
			var q2 = new Point(11, 1);
			Assert.IsFalse(GalaxyTemplate.IntersectsExceptAtEnds(p1, p2, q1, q2));
		}

		[TestMethod]
		public void IntersectionsAtEndpointAndMiddleDoCount()
		{
			var p1 = new Point(0, 0);
			var p2 = new Point(10, 0);
			var q1 = new Point(9, -1);
			var q2 = new Point(11, 1);
			Assert.IsTrue(GalaxyTemplate.IntersectsExceptAtEnds(p1, p2, q1, q2));
		}

		[TestMethod]
		public void CrossoverIntersectionsCount()
		{
			var p1 = new Point(-1, -1);
			var p2 = new Point(1, 1);
			var q1 = new Point(1, -1);
			var q2 = new Point(-1, 1);
			Assert.IsTrue(GalaxyTemplate.IntersectsExceptAtEnds(p1, p2, q1, q2));
		}

		[TestMethod]
		public void VerticalAndHorizontalIntersectionsCount()
		{
			var p1 = new Point(-1, 0);
			var p2 = new Point(1, 0);
			var q1 = new Point(0, -1);
			var q2 = new Point(0, 1);
			Assert.IsTrue(GalaxyTemplate.IntersectsExceptAtEnds(p1, p2, q1, q2));
		}

		[TestMethod]
		public void CollinearIntersectionsCount()
		{
			var p1 = new Point(0, 0);
			var p2 = new Point(0, 10);
			var q1 = new Point(0, 2);
			var q2 = new Point(0, 11);
			Assert.IsTrue(GalaxyTemplate.IntersectsExceptAtEnds(p1, p2, q1, q2));
		}

		[TestMethod]
		public void WarpPointAngularDistance()
		{
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(15, 0, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(-15, 0, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(345, 0, 60));

			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(90, 0, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(-90, 0, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(270, 0, 60));

			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(165, 180, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(-195, 180, 60));

			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(90, 180, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(-90, 180, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(270, 180, 60));

			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(0, 30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(15, 30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(-15, 30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(345, 30, 60));

			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(90, 30, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(-30, 30, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(330, 30, 60));

			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(0, -30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(15, -30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(-15, -30, 60));
			Assert.IsTrue(GalaxyTemplate.AngleIsInRangeExclusive(345, -30, 60));

			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(90, -30, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(-90, -30, 60));
			Assert.IsFalse(GalaxyTemplate.AngleIsInRangeExclusive(270, -30, 60));
		}
	}
}