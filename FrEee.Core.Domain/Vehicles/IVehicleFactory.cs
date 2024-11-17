using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Build vehicles.
/// </summary>
public interface IVehicleFactory
{
	/// <summary>
	/// Builds a vehicle of a specific type.
	/// </summary>
	/// <param name="vehicleType">The type of vehicle to build.</param>
	/// <returns>The built vehicle.</returns>
	public IVehicle Build(VehicleTypes vehicleType);
}
