using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class SearchBoxResultsViewModel : ScreenViewModelBase
{
	public string Query { get; set; } = string.Empty;
	public IEnumerable<ISpaceObject> Results { get; set; } = Enumerable.Empty<ISpaceObject>();
	public ISpaceObject? SelectedObject { get; set; }

	public void Select()
	{
		Close();
	}
}
