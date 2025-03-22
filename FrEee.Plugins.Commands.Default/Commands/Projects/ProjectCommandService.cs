using FrEee.Gameplay.Commands.Projects;

namespace FrEee.Plugins.Commands.Default.Commands.Projects;

public class ProjectCommandService
	: IProjectCommandService
{
	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
