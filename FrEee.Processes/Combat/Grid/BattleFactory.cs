using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// Implementation of <see cref="IBattleFactory"/> for the grid-based combat engine.
/// </summary>
public class BattleFactory
	: IBattleFactory
{
	public IBattle BuildGroundBattle(Planet location)
	{
		return new GroundBattle(location);
	}

	public IBattle BuildSpaceBattle(Sector location)
	{
		return new SpaceBattle(location);
	}
}
