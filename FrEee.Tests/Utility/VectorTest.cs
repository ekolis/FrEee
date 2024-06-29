using NUnit.Framework;

namespace FrEee.Utility;

public class VectorTest
{
	[Test]
	public void AddLinearGradientEightWay()
	{
		var map = new HeatMap();
		map.AddLinearGradientEightWay(new Vector2<int>(0, 0), 10, 10, -1);
		map.AddLinearGradientEightWay(new Vector2<int>(1, 1), 2, 2, -1);
		Assert.AreEqual(11, map[0, 0]);
		Assert.AreEqual(11, map[1, 1]);
		Assert.AreEqual(0, map[99, 99]);
	}

	[Test]
	public void InterpolationEightWay()
	{
		var v1 = new Vector2<int>(-1, 3);
		var v2 = new Vector2<int>(6, 4);
		var interp = Vector2<int>.InterpolateEightWay(v1, v2, 3);
		var trip = v2 - v1;
		var traveled = interp - v1;
		var togo = v2 - interp;
		Assert.AreEqual(3, traveled.LengthEightWay);
		Assert.AreEqual(trip.LengthEightWay - 3, togo.LengthEightWay);
	}
}
