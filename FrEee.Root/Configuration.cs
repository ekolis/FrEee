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
using FrEee.Processes.Construction;
using FrEee.Utility;
using FrEee.Vehicles;

namespace FrEee.Root;

/// <summary>
/// Configures FrEee.
/// </summary>
public static class Configuration
{
	/// <summary>
	/// Sets up any dependencies which need to be injected.
	/// </summary>
	public static void ConfigureDI(Action additionlConfig = null)
	{
		// TODO: load dependencies from configuration file in mod data so we can really modularize this thing!

		// reset in case DI was already running (e.g. unit test suite)
		DI.Reset();

		// processes
		DI.RegisterSingleton<ITurnProcessor, TurnProcessor>();
		DI.RegisterSingleton<IBattleService, BattleService>();
		DI.RegisterSingleton<IConstructionQueueService, ConstructionQueueService>();

		// gameplay
		DI.RegisterSingleton<IDesignCommandService, DesignCommandService>();
		DI.RegisterSingleton<IFleetCommandService, FleetCommandService>();
		DI.RegisterSingleton<IMessageCommandService, MessageCommandService>();
		DI.RegisterSingleton<IMinisterCommandService, MinisterCommandService>();
		DI.RegisterSingleton<INoteCommandService, NoteCommandService>();
		DI.RegisterSingleton<IOrderCommandService, OrderCommandService>();
		DI.RegisterSingleton<IProjectCommandService, ProjectCommandService>();
		DI.RegisterSingleton<IWaypointCommandService, WaypointCommandService>();

		// vehicles
		DI.RegisterSingleton<IHullService, HullService>();
		DI.RegisterSingleton<IDesignService, DesignService>();
		DI.RegisterSingleton<IVehicleService, VehicleService>();

		// addtional configuration for the GUI or whatever
		additionlConfig?.Invoke();

		// run this in the background, without awaiting it
		DI.Run();
	}
}