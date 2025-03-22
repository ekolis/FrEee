using FrEee.Vehicles;

namespace FrEee.Gameplay.Commands.Designs;
public class DesignCommandService
	: IDesignCommandService
{
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
