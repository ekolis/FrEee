using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Utility;

namespace FrEee;

/// <summary>
/// Das Uber-Singleton!!!
/// </summary>
public static class The
{
	/// <summary>
	/// The mod currently being played.
	/// </summary>
	public static Mod Mod { get; set; }
	
	/// <summary>
	/// The game currently being played.
	/// </summary>
	public static Game Game { get; set; }

	public static ReferrableRepository ReferrableRepository => Game.ReferrableRepository;

	public static Galaxy Galaxy => Game.Galaxy;

	public static Empire? Empire => Game.CurrentEmpire;
}
