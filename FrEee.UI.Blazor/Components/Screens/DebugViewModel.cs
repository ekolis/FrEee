using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Screens;

public class DebugViewModel : ScreenViewModelBase
{
	public string DebugLog { get; set; } = "Game initialized.";
	public string CommandInput { get; set; } = string.Empty;

	public void ExecuteCommand()
	{
		if (!string.IsNullOrWhiteSpace(CommandInput))
		{
			DebugLog += $"\n> {CommandInput}\nExecuted.";
			CommandInput = string.Empty;
		}
	}
}
