using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// Places a stellar object on a "true" circle at a specified distance from the center.
	/// </summary>
	public class CircleRadiusStellarObjectLocation : IStellarObjectLocation
	{
		public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

		public int Radius { get; set; }

		public Point? LastResult { get; private set; }

		public Point Resolve(StarSystem sys)
		{
			if (Radius < 0)
				throw new Exception("Invalid location \"Circle Radius " + Radius + "\" specified.");
			var pts = new List<Point>();
			for (int x = -Math.Min(Radius, sys.Radius); x <= Math.Min(Radius, sys.Radius); x++)
			{
				for (int y = -Math.Min(Radius, sys.Radius); y <= Math.Min(Radius, sys.Radius); y++)
				{
					// Don't let stellar objects overlap
					if (Math.Round(Math.Sqrt(x * x + y * y)) == Radius && sys.GetSector(x, y).SpaceObjects.Count == 0)
						pts.Add(new Point(x, y));
				}
			}
			if (!pts.Any())
				throw new Exception("Cannot place stellar object - no empty sectors are available at radius " + Radius + ".");
			LastResult = pts.PickRandom();
			return LastResult.Value;
		}
	}
}
