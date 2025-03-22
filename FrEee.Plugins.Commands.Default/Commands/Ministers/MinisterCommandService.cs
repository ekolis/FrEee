namespace FrEee.Gameplay.Commands.Ministers;
public class MinisterCommandService
	: IMinisterCommandService
{
	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
