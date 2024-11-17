using System.Collections.Generic;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Space;
using FrEee.Processes.Combat;

namespace FrEee.Vehicles;

/// <summary>
/// A space vehicle which can contain cargo and build things.
/// Should not be a unit unless you want Matryosha ships...
/// </summary>
public interface IMajorSpaceVehicle
	: ISpaceVehicle, ICargoTransferrer, IConstructor
{
}