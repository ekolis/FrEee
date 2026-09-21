using FrEee.Processes.Combat;

namespace FrEee.UI.Blazor.Components.Screens;

public class BattleReplayViewModel : ScreenViewModelBase
{
	public IBattle? Battle { get; set; }
	public BattleViewModel BattleVM { get; set; } = new();
}
