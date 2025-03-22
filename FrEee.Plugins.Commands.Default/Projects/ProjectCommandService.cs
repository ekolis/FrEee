using FrEee.Gameplay.Commands.Projects;

namespace FrEee.Plugins.Commands.Default.Projects;

public class ProjectCommandService
	: IProjectCommandService
{
	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
