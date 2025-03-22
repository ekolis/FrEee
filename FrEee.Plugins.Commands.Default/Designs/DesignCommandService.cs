using System;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Vehicles;

namespace FrEee.Plugins.Commands.Default.Designs;
public class DesignCommandService
	: IDesignCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "DesignCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public ICreateDesignCommand CreateDesign<T>(IDesign<T> design)
		where T : IVehicle
	{
		return new CreateDesignCommand<T>(design);
	}

	public ISetObsoleteFlagCommand SetObsoleteFlag(IDesign design, bool isObsolete)
	{
		return new SetObsoleteFlagCommand(design, isObsolete);
	}
}
