using FrEee.Processes.Combat;
using FrEee.Utility;

namespace FrEee.UI.Blazor.Components;

public class BattleViewModel : ViewModelBase
{
	public IBattle? Battle { get; set; }
	public int Round { get; set; } = 0;
	public bool AutoZoom { get; set; } = true;
	public bool ShowGrid { get; set; } = true;
	public bool IsPaused { get; set; } = true;
	public ICombatant? SelectedCombatant { get; set; }

	public void PrevRound()
	{
		if (Battle is not null && Round > 0)
			Round--;
	}

	public void NextRound()
	{
		if (Battle is not null && Round < Battle.Duration - 1)
			Round++;
	}

	public void TogglePause()
	{
		IsPaused = !IsPaused;
	}
}
