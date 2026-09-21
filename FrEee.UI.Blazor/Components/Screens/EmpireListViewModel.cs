using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Screens;

public class EmpireListViewModel : ScreenViewModelBase
{
	public IEnumerable<Empire> Empires => Game.Current?.Empires ?? Enumerable.Empty<Empire>();
	public Empire? SelectedEmpire { get; set; }
}
