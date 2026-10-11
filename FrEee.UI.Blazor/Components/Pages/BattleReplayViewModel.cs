using FrEee.Processes.Combat;

namespace FrEee.UI.Blazor.Components.Pages;

public class BattleReplayViewModel : PageViewModelBase
{
	public IBattle? Battle { get; set; }
	public BattleViewModel BattleVM { get; set; } = new();
}
