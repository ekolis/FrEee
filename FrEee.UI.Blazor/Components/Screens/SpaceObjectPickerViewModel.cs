using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class SpaceObjectPickerViewModel : ScreenViewModelBase
{
	public string Title { get; set; } = "Select Target Object";
	public IEnumerable<ISpaceObject> Objects { get; set; } = Game.Current?.Galaxy?.FindSpaceObjects<ISpaceObject>() ?? Enumerable.Empty<ISpaceObject>();
	public ISpaceObject? SelectedObject { get; set; }

	public void Select()
	{
		Close();
	}
}
