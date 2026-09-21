using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Extensions;

namespace FrEee.UI.Blazor.Components.Screens;

public class StatusViewModel : ScreenViewModelBase
{
	public Empire? Empire => Empire.Current;
}
