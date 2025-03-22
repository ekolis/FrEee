using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;
using FrEee.Plugins;

namespace FrEee.Processes.Combat;

/// <summary>
/// Manages battles.
/// </summary>
public interface IBattleService
	: IPlugin
{
	/// <summary>
	/// Creates a space battle.
	/// </summary>
	/// <param name="location"></param>
	/// <returns></returns>
	IBattle CreateSpaceBattle(Sector location);

	/// <summary>
	/// Creates a ground battle.
	/// </summary>
	/// <param name="location"></param>
	/// <returns></returns>
	IBattle CreateGroundBattle(Planet location);
}
