using FrEee.Modding;
using FrEee.Vehicles;

namespace FrEee.UI.Blazor.Components.Pages;

public class HullPickerViewModel : PageViewModelBase
{
	public IEnumerable<IHull> Hulls => Mod.Current?.Hulls ?? Enumerable.Empty<IHull>();
	public IHull? SelectedHull { get; set; }

	public void Select()
	{
		Close();
	}
}
