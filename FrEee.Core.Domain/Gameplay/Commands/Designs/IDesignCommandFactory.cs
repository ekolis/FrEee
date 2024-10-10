using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Orders;

/// <summary>
/// Builds various types of commands used for managing vehicle designs.
public interface IDesignCommandFactory
{
	ICreateDesignCommand CreateDesign<T>(IDesign<T> design)
		where T : IVehicle;

	ISetObsoleteFlagCommand SetObsoleteFlag(IDesign design, bool isObsolete);
}
