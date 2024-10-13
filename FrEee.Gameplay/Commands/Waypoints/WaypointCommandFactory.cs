using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Waypoints;
public class WaypointCommandFactory
	: IWaypointCommandFactory
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
