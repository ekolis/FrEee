using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Civilization;

/// <summary>
/// A waypoint which is locked to the location of a space object, and thus may move about if the space object is mobile.
/// </summary>
public class SpaceObjectWaypoint : Waypoint
{
	public SpaceObjectWaypoint(ISpaceObject sobj)
	{
		SpaceObject = sobj;
	}

	public override string Name
	{
		get { return "Waypoint at " + SpaceObject.Name; }
	}

	[DoNotSerialize(false)]
	public override Sector Sector
	{
		get
		{
			return SpaceObject?.Sector;
		}
		set
		{
			throw new InvalidOperationException("Cannot set the sector of a space object waypoint.");
		}
	}

	[DoNotSerialize]
	public ISpaceObject SpaceObject { get; set; }

	public override StarSystem StarSystem
	{
		get
		{
			return SpaceObject.StarSystem;
		}
	}

	private GameReference<ISpaceObject> spaceObject
	{
		get => SpaceObject.ReferViaGalaxy();
		set => SpaceObject = value.Value;
	}
}