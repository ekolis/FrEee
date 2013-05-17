using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// Chooses a specific sector of a system.
	/// </summary>
	public class CoordStellarObjectLocation : IStellarObjectLocation
	{
		public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

		public Point Coordinates { get; set; }

		/// <summary>
		/// If true, (0,0) is the center of the system.
		/// Otherwise, (6,6) is the center of the system (as in SE4).
		/// </summary>
		public bool UseCenteredCoordinates { get; set; }

		public Point? LastResult { get; private set; }

		public Point Resolve(StarSystem sys)
		{
			int realx = Coordinates.X - (UseCenteredCoordinates ? 0 : 6);
			int realy = Coordinates.Y - (UseCenteredCoordinates ? 0 : 6);
			if (UseCenteredCoordinates)
			{
				if (realx < -sys.Radius || realx > sys.Radius || realy < -sys.Radius || realy > sys.Radius)
					throw new Exception("Invalid location \"Centered Coord " + Coordinates.X + ", " + Coordinates.Y + "\" specified for system of radius " + sys.Radius + ".");
			}
			else
			{
				if (realx < -sys.Radius || realx > sys.Radius || realy < -sys.Radius || realy > sys.Radius)
					throw new Exception("Invalid location \"Coord " + Coordinates.X + ", " + Coordinates.Y + "\" specified for system of radius " + sys.Radius + ".");
			}
			LastResult = new Point(realx, realy);
			return LastResult.Value;
		}
	}
}
