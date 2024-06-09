using FrEee.Objects.Space;

namespace FrEee.Objects.Civilization;

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

	public override string Name
	{
		get { return "Waypoint at " + Sector.Name; }
	}

	public override Sector Sector
	{
		get;
		set;
	}

	public override StarSystem StarSystem
	{
		get { return Sector.StarSystem; }
	}
}