namespace FrEee.Gameplay.Commands.Ministers;
public class MinisterCommandFactory
	: IMinisterCommandFactory
{
	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
