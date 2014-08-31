using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint which is locked to the location of a space object, and thus may move about if the space object is mobile.
	/// </summary>
	public class SpaceObjectWaypoint : Waypoint
	{
		public SpaceObjectWaypoint(ISpaceObject sobj)
		{
			SpaceObject = sobj;
		}

		public ISpaceObject SpaceObject { get; private set; }

		public override Sector Sector
		{
			get
			{
				return SpaceObject.Sector;
			}
			protected set
			{
				throw new InvalidOperationException("Cannot set the sector of a space object waypoint.");
			}
		}

		public override StarSystem StarSystem
		{
			get
			{
				return SpaceObject.StarSystem;
			}
		}
	}
}
