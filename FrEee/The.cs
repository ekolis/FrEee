using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Processes;
using FrEee.Setup;
using FrEee.Utility;

namespace FrEee;

/// <summary>
/// Das Uber-Singleton!!!
/// </summary>
public static class The
{
	/// <summary>
	/// The filename of the mod currently being played.
	/// </summary>
	public static string ModFileName { get; set; }

	/// <summary>
	/// The mod currently being played.
	/// </summary>
	public static Mod Mod { get; set; }

	/// <summary>
	/// The game currently being played.
	/// </summary>
	public static Game Game { get; set; }

	public static GameSetup Setup => Game.Setup;

	public static ReferrableRepository ReferrableRepository => Game.ReferrableRepository;

	public static Galaxy Galaxy => Game.Galaxy;

	public static Empire? Empire { get => Game.CurrentEmpire; set => Game.CurrentEmpire = value; }

	public static TurnProcessor TurnProcessor => Game.TurnProcessor;

	public static double Timestamp => TurnProcessor.Timestamp;
}
