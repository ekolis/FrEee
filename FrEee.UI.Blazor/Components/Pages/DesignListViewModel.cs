using FrEee.Objects.Civilization;
using FrEee.Vehicles;

namespace FrEee.UI.Blazor.Components.Pages;

public class DesignListViewModel : PageViewModelBase
{
	public IEnumerable<IDesign> Designs => Empire.Current?.KnownDesigns ?? Enumerable.Empty<IDesign>();
	public IDesign? SelectedDesign { get; set; }
}
