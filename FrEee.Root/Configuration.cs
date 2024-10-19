using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Gameplay.Commands.Waypoints;
using FrEee.Processes;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;
using FrEee.Utility;

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
		// processes
		DI.RegisterSingleton<ITurnProcessor, TurnProcessor>();
		DI.RegisterSingleton<IBattleFactory, BattleFactory>();

		// gameplay
		DI.RegisterSingleton<IDesignCommandFactory, DesignCommandFactory>();
		DI.RegisterSingleton<IFleetCommandFactory, FleetCommandFactory>();
		DI.RegisterSingleton<IMessageCommandFactory, MessageCommandFactory>();
		DI.RegisterSingleton<INoteCommandFactory, NoteCommandFactory>();
		DI.RegisterSingleton<IOrderCommandFactory, OrderCommandFactory>();
		DI.RegisterSingleton<IWaypointCommandFactory, WaypointCommandFactory>();

		// run this in the background, without awaiting it
		DI.Run();
	}
}