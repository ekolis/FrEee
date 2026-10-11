using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Extensions;

namespace FrEee.UI.Blazor.Components.Pages;

public class StatusViewModel : PageViewModelBase
{
	public Empire? Empire => Empire.Current;
}
