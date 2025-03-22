using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Vehicles;

namespace FrEee.Plugins.Commands.Default.Designs;

[Export(typeof(IPlugin))]
public class DesignCommandService
	: Plugin<IDesignCommandService>, IDesignCommandService
{
	public override string Name { get; } = "DesignCommandService";

	public override IDesignCommandService Implementation => this;

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
