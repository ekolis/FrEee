﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee;
using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.StellarObjectLocations
{
	/// <summary>
	/// Chooses a sector at a particular distance from the center of the system.
	/// Ring 1 is the center, ring 2 is one sector from the center, etc.
	/// </summary>
	 [Serializable] public class RingStellarObjectLocation : IStellarObjectLocation
	{
		public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

		public int Ring { get; set; }

		public Point? LastResult { get; private set; }

		public Point Resolve(StarSystem sys)
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
					if (Math.Abs(x) == dist || Math.Abs(y) == dist && !sys.GetSector(x, y).SpaceObjects.Any())
						pts.Add(new Point(x, y));
				}
			}
			if (!pts.Any())
				throw new Exception("Cannot place stellar object - no empty sectors are available in ring " + Ring + ".");
			LastResult = pts.PickRandom();
			return LastResult.Value;
		}
	}
}
