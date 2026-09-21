using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class MainGameViewModel : ScreenViewModelBase
{
	public Empire? Empire => Empire.Current;
	public StarSystem? SelectedStarSystem { get; set; } = Game.Current?.Galaxy?.StarSystemLocations.FirstOrDefault()?.Item;
	public Sector? SelectedSector { get; set; }
	public ISpaceObject? SelectedSpaceObject { get; set; }

	public StarSystemViewModel StarSystemVM { get; set; } = new();
	public SearchBoxViewModel SearchBoxVM { get; set; } = new();
	public HistoryLogViewModel HistoryLogVM { get; set; } = new();

	public void EndTurn()
	{
		// End turn logic
	}
}
