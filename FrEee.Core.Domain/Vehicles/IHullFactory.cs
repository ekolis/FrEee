using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Build vehicle hulls.
/// </summary>
public interface IHullFactory
{
	/// <summary>
	/// Builds a hull of a specific type.
	/// </summary>
	/// <param name="vehicleType">The type of hull to build.</param>
	/// <returns>The built hull.</returns>
	IHull Build(VehicleTypes vehicleType);
}
