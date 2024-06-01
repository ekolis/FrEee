using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;

namespace FrEee.Modding.StellarObjectLocations;

/// <summary>
/// Chooses a sector at a particular distance from the center of the system.
/// Ring 1 is the center, ring 2 is one sector from the center, etc.
/// </summary>
[Serializable]
public class RingStellarObjectLocation : IStellarObjectLocation
{
	public Point? LastResult { get; private set; }
	public int Ring { get; set; }
	public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

	public Point Resolve(StarSystem sys, PRNG dice)
	{
		if (Ring < 1 || Ring > sys.Radius + 1)
			throw new Exception("Invalid location \"Ring " + Ring + "\" specified for system of radius " + sys.Radius + ".");
		var pts = new List<Point>();
		var dist = Ring - 1;
		for (int x = -dist; x <= dist; x++)
		{
			for (int y = -dist; y <= dist; y++)
			{
				// Don't let stellar objects overlap
				if ((Math.Abs(x) == dist || Math.Abs(y) == dist) && !sys.GetSector(x, y).SpaceObjects.Any())
					pts.Add(new Point(x, y));
			}
		}
		if (!pts.Any())
			throw new Exception("Cannot place stellar object - no empty sectors are available in ring " + Ring + ".");
		LastResult = pts.PickRandom(dice);
		return LastResult.Value;
	}
}