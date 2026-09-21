using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Screens;

public class HostConsoleViewModel : ScreenViewModelBase
{
	public string Status { get; set; } = "Turn ready.";
	public int TurnNumber => Game.Current?.TurnNumber ?? 1;

	public void ProcessTurn()
	{
		// Process turn execution
		Status = $"Turn {TurnNumber} processed.";
	}
}
