using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Plugins;

namespace FrEee.Gameplay.Commands.Waypoints;

/// <summary>
/// Creates commands to manage waypoints.
/// </summary>
public interface IWaypointCommandService
	: IPlugin<IWaypointCommandService>
{
	ICommand<Empire> CreateWaypoint(Waypoint waypoint);

	ICommand<Waypoint> DeleteWaypoint(Waypoint waypoint);

	ICommand<Waypoint> HotkeyWaypoint(Waypoint waypoint, int hotkey, bool redirect);
}