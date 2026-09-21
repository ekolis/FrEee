using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Screens;

public class GameOverViewModel : ScreenViewModelBase
{
	public Empire? Winner { get; set; }
	public string Reason { get; set; } = "Victory condition met.";
}
