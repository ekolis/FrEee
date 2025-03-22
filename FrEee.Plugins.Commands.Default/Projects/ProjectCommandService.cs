using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Projects;

namespace FrEee.Plugins.Commands.Default.Projects;

[Export(typeof(IPlugin))]
public class ProjectCommandService
	: Plugin<IProjectCommandService>, IProjectCommandService
{
	public override string Name { get; } = "ProjectCommandService";

	public override IProjectCommandService Implementation => this;

	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
