using FrEee.Objects.Space;
using FrEee.Utility;
using System;
using System.Drawing;
using FrEee.Objects.GameState;

namespace FrEee.Modding.StellarObjectLocations;

/// <summary>
/// Chooses a specific sector of a system.
/// </summary>
[Serializable]
public class CoordStellarObjectLocation : IStellarObjectLocation
{
	public Point Coordinates { get; set; }
	public Point? LastResult { get; private set; }
	public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

	/// <summary>
	/// If true, (0,0) is the center of the system.
	/// Otherwise, (6,6) is the center of the system (as in SE4).
	/// </summary>
	public bool UseCenteredCoordinates { get; set; }

	public Point Resolve(StarSystem sys, PRNG dice)
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