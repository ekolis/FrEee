using FrEee.Modding;
using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Screens;

public class CultureComparisonViewModel : ScreenViewModelBase
{
	public IEnumerable<Culture> Cultures => Mod.Current?.Cultures ?? Enumerable.Empty<Culture>();
	public Culture? SelectedCulture { get; set; }
}
