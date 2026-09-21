using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Screens;

public class ScoresViewModel : ScreenViewModelBase
{
	public IEnumerable<Empire> Empires => Game.Current?.Empires ?? Enumerable.Empty<Empire>();
	public LineGraphViewModel GraphVM { get; set; } = new() { Title = "Empire Scores" };
}
