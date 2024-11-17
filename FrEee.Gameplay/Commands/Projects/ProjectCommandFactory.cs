namespace FrEee.Gameplay.Commands.Projects;

public class ProjectCommandFactory
	: IProjectCommandFactory
{
	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
