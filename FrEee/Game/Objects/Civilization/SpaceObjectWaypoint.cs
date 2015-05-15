using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
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

		[DoNotSerialize]
		public ISpaceObject SpaceObject { get { return spaceObject.Value; } set { spaceObject = value.Reference(); } }

		private Reference<ISpaceObject> spaceObject { get; set; }

		[DoNotSerialize(false)]
		public override Sector Sector
		{
			get
			{
				return SpaceObject.Sector;
			}
			set
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

		public override string Name
		{
			get { return "Waypoint at " + SpaceObject.Name; }
		}
	}
}
