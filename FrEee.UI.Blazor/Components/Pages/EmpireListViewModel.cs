using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Pages;

public class EmpireListViewModel : PageViewModelBase
{
	public IEnumerable<Empire> Empires => Game.Current?.Empires ?? Enumerable.Empty<Empire>();
	public Empire? SelectedEmpire { get; set; }
}
