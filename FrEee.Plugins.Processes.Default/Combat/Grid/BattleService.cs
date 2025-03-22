using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// Implementation of <see cref="IBattleService"/> for the grid-based combat engine.
/// </summary>
public class BattleService
	: IBattleService
{
	public IBattle CreateGroundBattle(Planet location)
	{
		return new GroundBattle(location);
	}

	public IBattle CreateSpaceBattle(Sector location)
	{
		return new SpaceBattle(location);
	}
}
