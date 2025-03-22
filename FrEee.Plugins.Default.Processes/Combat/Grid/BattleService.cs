using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;
using FrEee.Persistence;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;

namespace FrEee.Plugins.Default.Processes.Combat.Grid;

/// <summary>
/// Implementation of <see cref="IBattleService"/> for the grid-based combat engine.
/// </summary>
[Export(typeof(IPlugin))]
public class BattleService
	: Plugin<IBattleService>, IBattleService
{
	public override string Name { get; } = "BattleService";

	public override IBattleService Implementation => this;

	public IBattle CreateGroundBattle(Planet location)
	{
		return new GroundBattle(location);
	}

	public IBattle CreateSpaceBattle(Sector location)
	{
		return new SpaceBattle(location);
	}
}
