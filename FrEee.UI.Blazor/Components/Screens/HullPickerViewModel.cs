using FrEee.Modding;
using FrEee.Vehicles;

namespace FrEee.UI.Blazor.Components.Screens;

public class HullPickerViewModel : ScreenViewModelBase
{
	public IEnumerable<IHull> Hulls => Mod.Current?.Hulls ?? Enumerable.Empty<IHull>();
	public IHull? SelectedHull { get; set; }

	public void Select()
	{
		Close();
	}
}
