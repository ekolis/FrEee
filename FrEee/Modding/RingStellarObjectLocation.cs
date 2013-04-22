using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee;
using FrEee.Game;

namespace FrEee.Modding
{
	/// <summary>
	/// Chooses a sector at a particular distance from the center of the system.
	/// Ring 1 is the center, ring 2 is one sector from the center, etc.
	/// </summary>
	public class RingStellarObjectLocation : IStellarObjectLocation
	{
		public ITemplate<ISpaceObject> StellarObjectTemplate { get; set; }

		public int Ring { get; set; }

		public Point Resolve(int radius)
		{
			if (Ring < 1 || Ring > radius + 1)
				throw new Exception("Invalid location \"Ring " + Ring + "\" specified for system of radius " + radius + ".");
			var pts = new List<Point>();
			var dist = Ring - 1;
			for (int x = -dist; x <= dist; x++)
			{
				for (int y = -dist; y <= dist; y++)
				{
					if (Math.Abs(x) == dist || Math.Abs(y) == dist)
						pts.Add(new Point(x, y));
				}
			}
			return pts.PickRandom();
		}
	}
}
