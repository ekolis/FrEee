using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Pages;

public class ScoresViewModel : PageViewModelBase
{
	public IEnumerable<Empire> Empires => Game.Current?.Empires ?? Enumerable.Empty<Empire>();
	public LineGraphViewModel GraphVM { get; set; } = new() { Title = "Empire Scores" };
}
