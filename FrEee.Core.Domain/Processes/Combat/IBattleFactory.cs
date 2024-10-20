using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Processes.Combat;

/// <summary>
/// Builds battles.
/// </summary>
public interface IBattleFactory
{
	/// <summary>
	/// Builds a space battle.
	/// </summary>
	/// <param name="location"></param>
	/// <returns></returns>
	IBattle BuildSpaceBattle(Sector location);

	/// <summary>
	/// Builds a ground battle.
	/// </summary>
	/// <param name="location"></param>
	/// <returns></returns>
	IBattle BuildGroundBattle(Planet location);
}
