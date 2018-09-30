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
	}
}