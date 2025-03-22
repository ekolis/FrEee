using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Waypoints;
using FrEee.Objects.Civilization;

namespace FrEee.Plugins.Default.Commands.Waypoints;

[Export(typeof(IPlugin))]
public class WaypointCommandService
	: Plugin<IWaypointCommandService>, IWaypointCommandService
{
	public override string Name { get; } = "WaypointCommandService";

	public override IWaypointCommandService Implementation => this;

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
