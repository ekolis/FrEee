using FrEee.Objects.Civilization;

namespace FrEee.Gameplay.Commands.Waypoints;
public class WaypointCommandService
	: IWaypointCommandService
{
	public ICommand<Empire> CreateWaypoint(Waypoint Waypoint)
	{
		return new CreateWaypointCommand(Waypoint);
	}

	public ICommand<Waypoint> DeleteWaypoint(Waypoint waypoint)
	{
		return new DeleteWaypointCommand(waypoint);
	}

	public ICommand<Waypoint> HotkeyWaypoint(Waypoint waypoint, int hotkey, bool redirect)
	{
		return new HotkeyWaypointCommand(waypoint, hotkey, redirect);
	}
}
