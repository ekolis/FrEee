using System;
using FrEee.Gameplay.Commands.Ministers;

namespace FrEee.Plugins.Commands.Default.Ministers;
public class MinisterCommandService
	: IMinisterCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "MinisterCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public IToggleMinistersCommand ToggleMinisters()
	{
		return new ToggleMinistersCommand();
	}
}
