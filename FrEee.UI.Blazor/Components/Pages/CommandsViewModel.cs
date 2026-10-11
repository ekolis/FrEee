using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Pages;

public class CommandsViewModel : PageViewModelBase
{
	public IEnumerable<ICommand> Commands => Empire.Current?.Commands ?? Enumerable.Empty<ICommand>();
}
