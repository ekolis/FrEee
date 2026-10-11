using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Pages;

public class GameOverViewModel : PageViewModelBase
{
	public Empire? Winner { get; set; }
	public string Reason { get; set; } = "Victory condition met.";
}
