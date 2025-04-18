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
using FrEee.Persistence;
using FrEee.Plugins;
using FrEee.Processes;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;
using FrEee.Vehicles;

namespace FrEee.Utility;

/// <summary>
/// Wrapper for <see cref="DI"/> that exposes various services used by FrEee.
/// </summary>
/// <remarks>
/// TODO: Really this is just a service locator, maybe call it Services?
/// </remarks>
public static class DIRoot
{
	/// <summary>
	/// Processes turns.
	/// </summary>
	public static ITurnProcessor TurnProcessor => DI.Get<ITurnProcessor>();

	/// <summary>
	/// Manages battles.
	/// </summary>
	public static IBattleService Battles => DI.Get<IBattleService>();

	/// <summary>
	/// Allows players to manage designs.
	/// </summary>
	public static IDesignCommandService DesignCommands => DI.Get<IDesignCommandService>();

	/// <summary>
	/// Allows players to manage fleets.
	/// </summary>
	public static IFleetCommandService FleetCommands => DI.Get<IFleetCommandService>();

	/// <summary>
	/// Allows players to manage diplomatic messages.
	/// </summary>
	public static IMessageCommandService MessageCommands => DI.Get<IMessageCommandService>();

	/// <summary>
	/// Allows players to manage AI ministers.
	/// </summary>
	public static IMinisterCommandService MinisterCommands => DI.Get<IMinisterCommandService>();

	/// <summary>
	/// Allows players to manage notes.
	/// </summary>
	public static INoteCommandService NoteCommands => DI.Get<INoteCommandService>();
	
	/// <summary>
	/// Allows players to issue orders.
	/// </summary>
	public static IOrderCommandService OrderCommands => DI.Get<IOrderCommandService>();

	/// <summary>
	/// Allows players to manage empire wide projects such as research and espionage.
	/// </summary>
	public static IProjectCommandService ProjectCommands => DI.Get<IProjectCommandService>();

	/// <summary>
	/// Allows players to manage waypoints.
	/// </summary>
	public static IWaypointCommandService WaypointCommands => DI.Get<IWaypointCommandService>();

	/// <summary>
	/// Creates vehicle hulls.
	/// </summary>
	public static IHullService Hulls => DI.Get<IHullService>();

	/// <summary>
	/// Creates vehicle designs.
	/// </summary>
	public static IDesignService Designs => DI.Get<IDesignService>();

	/// <summary>
	/// Creates vehicles.
	/// </summary>
	public static IVehicleService Vehicles => DI.Get<IVehicleService>();

	/// <summary>
	/// Creates construction queues.
	/// </summary>
	public static IConstructionQueueService ConstructionQueues => DI.Get<IConstructionQueueService>();

	/// <summary>
	/// Saves and loads game state.
	/// </summary>
	public static IGamePersister GamePersister => DI.Get<IGamePersister>();

	/// <summary>
	/// Saves and loads player commands.
	/// </summary>
	public static ICommandPersister CommandPersister => DI.Get<ICommandPersister>();

	/// <summary>
	/// Saves and loads game setups.
	/// </summary>
	public static IGameSetupPersister GameSetupPersister => DI.Get<IGameSetupPersister>();

	/// <summary>
	/// Saves and loads empire templates
	/// </summary>
	public static IEmpireTemplatePersister EmpireTemplatePersister => DI.Get<IEmpireTemplatePersister>();

	/// <summary>
	/// Manages the GUI. Only available when there is a GUI.
	/// </summary>
	public static IGuiController Gui => DI.Get<IGuiController>();

	/// <summary>
	/// Stores vehicle designs for the player across multiple games.
	/// </summary>
	public static ILibrary<IDesign> DesignLibrary => DI.Get<ILibrary<IDesign>>();
}
