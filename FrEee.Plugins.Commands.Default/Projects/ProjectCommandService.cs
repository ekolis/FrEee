using System;
using FrEee.Gameplay.Commands.Projects;

namespace FrEee.Plugins.Commands.Default.Projects;

public class ProjectCommandService
	: IProjectCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "ProjectCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
