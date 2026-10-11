using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Pages;

public class EditorViewModel : PageViewModelBase
{
	public StarSystem? SelectedStarSystem => Game.Current?.Galaxy?.StarSystemLocations.FirstOrDefault()?.Item;
	public Sector? SelectedSector { get; set; }
}
