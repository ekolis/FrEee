using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Combat.Grid;
using FrEee.Objects.Space;

namespace FrEee.WinForms.ViewModels;

public class BattleReplayFormViewModel(Battle battle)
{
	/// <summary>
	/// The battle.
	/// </summary>
	[Obsolete("Battle is only available here for the battle view and minimap until they get their own view models.")]
	public Battle Battle => battle;

	/// <summary>
	/// Gets the name of an empire in the battle.
	/// </summary>
	/// <param name="empire"></param>
	/// <returns></returns>
	public string NameFor(Empire empire) => battle.NameFor(empire);

	/// <summary>
	/// Events in the battle, grouped by the round number in which they occurred.
	/// </summary>
	public IEnumerable<IEnumerable<IBattleEvent>> Events => battle.Events;

	/// <summary>
	/// Gets the result of the battle from the perspective of a particular empire.
	/// </summary>
	/// <param name="empire"></param>
	/// <returns></returns>
	public string ResultFor(Empire empire) => battle.ResultFor(empire);
}
