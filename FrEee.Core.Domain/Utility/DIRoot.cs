using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Processes;
using FrEee.Processes.Combat;

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
	/// Allows players to manage notes.
	/// </summary>
	public static INoteCommandFactory NoteCommands => DI.Get<INoteCommandFactory>();
	
	/// <summary>
	/// Allows players to issue orders.
	/// </summary>
	public static IOrderCommandFactory OrderCommands => DI.Get<IOrderCommandFactory>();
}
