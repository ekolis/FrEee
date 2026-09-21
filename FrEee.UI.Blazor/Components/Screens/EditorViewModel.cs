using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class EditorViewModel : ScreenViewModelBase
{
	public StarSystem? SelectedStarSystem => Game.Current?.Galaxy?.StarSystemLocations.FirstOrDefault()?.Item;
	public Sector? SelectedSector { get; set; }
}
