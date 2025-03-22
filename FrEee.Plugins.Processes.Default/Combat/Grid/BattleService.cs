using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;

namespace FrEee.Plugins.Processes.Default.Combat.Grid;

/// <summary>
/// Implementation of <see cref="IBattleService"/> for the grid-based combat engine.
/// </summary>
public class BattleService
	: IBattleService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "BattleService";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public IBattle CreateGroundBattle(Planet location)
	{
		return new GroundBattle(location);
	}

	public IBattle CreateSpaceBattle(Sector location)
	{
		return new SpaceBattle(location);
	}
}
