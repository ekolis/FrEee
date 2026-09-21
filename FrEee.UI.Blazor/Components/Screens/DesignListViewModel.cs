using FrEee.Objects.Civilization;
using FrEee.Vehicles;

namespace FrEee.UI.Blazor.Components.Screens;

public class DesignListViewModel : ScreenViewModelBase
{
	public IEnumerable<IDesign> Designs => Empire.Current?.KnownDesigns ?? Enumerable.Empty<IDesign>();
	public IDesign? SelectedDesign { get; set; }
}
