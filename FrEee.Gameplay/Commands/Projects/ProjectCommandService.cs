namespace FrEee.Gameplay.Commands.Projects;

public class ProjectCommandService
	: IProjectCommandService
{
	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
