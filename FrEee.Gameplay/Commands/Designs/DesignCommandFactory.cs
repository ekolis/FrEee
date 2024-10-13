using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Designs;
public class DesignCommandFactory
	: IDesignCommandFactory
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
