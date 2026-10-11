using FrEee.Modding;
using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Pages;

public class CultureComparisonViewModel : PageViewModelBase
{
	public IEnumerable<Culture> Cultures => Mod.Current?.Cultures ?? Enumerable.Empty<Culture>();
	public Culture? SelectedCulture { get; set; }
}
