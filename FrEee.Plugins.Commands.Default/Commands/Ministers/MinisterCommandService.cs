using FrEee.Gameplay.Commands.Ministers;

namespace FrEee.Plugins.Commands.Default.Commands.Ministers;
public class MinisterCommandService
	: IMinisterCommandService
{
	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
