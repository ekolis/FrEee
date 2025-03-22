using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Plugins;
using FrEee.Vehicles;

namespace FrEee.Gameplay.Commands.Designs;

/// <summary>
/// Creates various types of commands used for managing vehicle designs.
/// </summary>
public interface IDesignCommandService
	: IPlugin<IDesignCommandService>
{
	ICreateDesignCommand CreateDesign<T>(IDesign<T> design)
		where T : IVehicle;

	ISetObsoleteFlagCommand SetObsoleteFlag(IDesign design, bool isObsolete);
}
