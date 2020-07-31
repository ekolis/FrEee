using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint which is locked to the location of a space object, and thus may move about if the space object is mobile.
	/// </summary>
	public class SpaceObjectWaypoint : Waypoint
	{
		public SpaceObjectWaypoint(ISpaceObject sobj) => SpaceObject = sobj;

		public override string Name => "Waypoint at " + SpaceObject?.Name;

		[DoNotSerialize(false)]
		public override Sector? Sector
		{
			get => SpaceObject?.Sector;
			set => throw new InvalidOperationException("Cannot set the sector of a space object waypoint.");
		}

		[DoNotSerialize]
		public ISpaceObject? SpaceObject { get => spaceObject?.Value; set => spaceObject = value?.ReferViaGalaxy(); }

		public override StarSystem? StarSystem => SpaceObject?.StarSystem;

		private GalaxyReference<ISpaceObject>? spaceObject { get; set; }
	}
}
