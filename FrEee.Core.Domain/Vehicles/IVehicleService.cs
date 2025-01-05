using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Creates vehicles.
/// </summary>
public interface IVehicleService
{
	/// <summary>
	/// Creates a vehicle of a specific type.
	/// </summary>
	/// <param name="vehicleType">The type of vehicle to create.</param>
	/// <returns>The new vehicle.</returns>
	IVehicle CreateVehicle(VehicleTypes vehicleType);
}
