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
	public static void ConfigureDI()
	{
		// TODO: load dependencies from configuration file in mod data so we can really modularize this thing!

		// reset in case DI was already running (e.g. unit test suite)
		DI.Reset();

		// processes
		DI.RegisterSingleton<ITurnProcessor, TurnProcessor>();
		DI.RegisterSingleton<IBattleFactory, BattleFactory>();
		DI.RegisterSingleton<IConstructionQueueFactory, ConstructionQueueFactory>();

		// gameplay
		DI.RegisterSingleton<IDesignCommandFactory, DesignCommandFactory>();
		DI.RegisterSingleton<IFleetCommandFactory, FleetCommandFactory>();
		DI.RegisterSingleton<IMessageCommandFactory, MessageCommandFactory>();
		DI.RegisterSingleton<IMinisterCommandFactory, MinisterCommandFactory>();
		DI.RegisterSingleton<INoteCommandFactory, NoteCommandFactory>();
		DI.RegisterSingleton<IOrderCommandFactory, OrderCommandFactory>();
		DI.RegisterSingleton<IProjectCommandFactory, ProjectCommandFactory>();
		DI.RegisterSingleton<IWaypointCommandFactory, WaypointCommandFactory>();

		// vehicles
		DI.RegisterSingleton<IHullFactory, HullFactory>();
		DI.RegisterSingleton<IDesignFactory, DesignFactory>();
		DI.RegisterSingleton<IVehicleFactory, VehicleFactory>();

		// run this in the background, without awaiting it
		DI.Run();
	}
}