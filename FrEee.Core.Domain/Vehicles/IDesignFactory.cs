using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Build vehicle designs.
/// </summary>
public interface IDesignFactory
{
	/// <summary>
	/// Builds a design with a specific vehicle type.
	/// </summary>
	/// <param name="vehicleType">The vehicle type to build.</param>
	/// <returns>The built design, with no hull or components.</returns>
	IDesign Build(VehicleTypes vehicleType);

	/// <summary>
	/// Builds a design with a specific hull.
	/// </summary>
	/// <param name="hull">The hull to use on the design.</param>
	/// <returns>The built design, with a hull but no components.</returns>
	IDesign Build(IHull hull);

	/// <summary>
	/// The standard militia unit design for use in defending colonies.
	/// </summary>
	IDesign<IUnit> Militia { get; }
}
