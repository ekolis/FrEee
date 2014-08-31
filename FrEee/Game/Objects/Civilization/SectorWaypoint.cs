using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint which is fixed at a specific location in space.
	/// </summary>
	public class SectorWaypoint : Waypoint
	{
		public SectorWaypoint(Sector sector)
			: base()
		{
			Sector = sector;
		}

		public override Sector Sector
		{
			get;
			protected set;
		}

		public override StarSystem StarSystem
		{
			get { return Sector.StarSystem; }
		}

		public override string Name
		{
			get { return "Waypoint at " + Sector.Name; }
		}
	}
}
