using System;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Waypoints;
using FrEee.Objects.Civilization;

namespace FrEee.Plugins.Commands.Default.Waypoints;
public class WaypointCommandService
	: IWaypointCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "WaypointCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

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
