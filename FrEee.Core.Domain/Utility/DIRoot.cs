using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Gameplay.Commands.Ministers;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Gameplay.Commands.Projects;
using FrEee.Gameplay.Commands.Waypoints;
using FrEee.Processes;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;
using FrEee.Vehicles;

namespace FrEee.Utility;

/// <summary>
/// Wrapper for <see cref="DI"/> that exposes various services used by FrEee.
/// </summary>
public static class DIRoot
{
	/// <summary>
	/// Processes turns.
	/// </summary>
	public static ITurnProcessor TurnProcessor => DI.Get<ITurnProcessor>();

	/// <summary>
	/// Manages battles.
	/// </summary>
	public static IBattleFactory Battles => DI.Get<IBattleFactory>();

	/// <summary>
	/// Allows players to manage designs.
	/// </summary>
	public static IDesignCommandFactory DesignCommands => DI.Get<IDesignCommandFactory>();

	/// <summary>
	/// Allows players to manage fleets.
	/// </summary>
	public static IFleetCommandFactory FleetCommands => DI.Get<IFleetCommandFactory>();

	/// <summary>
	/// Allows players to manage diplomatic messages.
	/// </summary>
	public static IMessageCommandFactory MessageCommands => DI.Get<IMessageCommandFactory>();

	/// <summary>
	/// Allows players to manage AI ministers.
	/// </summary>
	public static IMinisterCommandFactory MinisterCommands => DI.Get<IMinisterCommandFactory>();

	/// <summary>
	/// Allows players to manage notes.
	/// </summary>
	public static INoteCommandFactory NoteCommands => DI.Get<INoteCommandFactory>();
	
	/// <summary>
	/// Allows players to issue orders.
	/// </summary>
	public static IOrderCommandFactory OrderCommands => DI.Get<IOrderCommandFactory>();

	/// <summary>
	/// Allows players to manage empire wide projects such as research and espionage.
	/// </summary>
	public static IProjectCommandFactory ProjectCommands => DI.Get<IProjectCommandFactory>();

	/// <summary>
	/// Allows players to manage waypoints.
	/// </summary>
	public static IWaypointCommandFactory WaypointCommands => DI.Get<IWaypointCommandFactory>();

	/// <summary>
	/// Creates vehicle hulls.
	/// </summary>
	public static IHullFactory Hulls => DI.Get<IHullFactory>();

	/// <summary>
	/// Creates vehicle designs.
	/// </summary>
	public static IDesignFactory Designs => DI.Get<IDesignFactory>();

	/// <summary>
	/// Creates vehicles.
	/// </summary>
	public static IVehicleFactory Vehicles => DI.Get<IVehicleFactory>();

	/// <summary>
	/// Creates construction queues.
	/// </summary>
	public static IConstructionQueueFactory ConstructionQueues => DI.Get<IConstructionQueueFactory>();

	/// <summary>
	/// Manages the GUI. Only available when there is a GUI.
	/// </summary>
	public static IGuiController Gui => DI.Get<IGuiController>();
}
