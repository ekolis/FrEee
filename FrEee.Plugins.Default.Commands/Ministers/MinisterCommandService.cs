using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Ministers;

namespace FrEee.Plugins.Default.Commands.Ministers;

[Export(typeof(IPlugin))]
public class MinisterCommandService
	: Plugin<IMinisterCommandService>, IMinisterCommandService
{
	public override string Name { get; } = "MinisterCommandService";

	public override IMinisterCommandService Implementation => this;

	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
