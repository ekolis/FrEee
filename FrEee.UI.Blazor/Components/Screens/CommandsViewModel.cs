using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.UI.Blazor.Components.Screens;

public class CommandsViewModel : ScreenViewModelBase
{
	public IEnumerable<ICommand> Commands => Empire.Current?.Commands ?? Enumerable.Empty<ICommand>();
}
