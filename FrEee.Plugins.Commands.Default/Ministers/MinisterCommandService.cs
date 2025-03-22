using FrEee.Gameplay.Commands.Ministers;

namespace FrEee.Plugins.Commands.Default.Ministers;
public class MinisterCommandService
	: IMinisterCommandService
{
	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
