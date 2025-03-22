using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Creates vehicle hulls.
/// </summary>
public interface IHullService
	: IPlugin<IHullService>
{
	/// <summary>
	/// Creates a hull of a specific type.
	/// </summary>
	/// <param name="vehicleType">The type of hull to create.</param>
	/// <returns>The new hull.</returns>
	IHull CreateHull(VehicleTypes vehicleType);
}
