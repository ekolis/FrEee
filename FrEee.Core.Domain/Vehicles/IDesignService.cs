using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

/// <summary>
/// Manages vehicle designs.
/// </summary>
public interface IDesignService
{
	/// <summary>
	/// Creates a design with a specific vehicle type.
	/// </summary>
	/// <param name="vehicleType">The vehicle type to build.</param>
	/// <returns>The new design, with no hull or components.</returns>
	IDesign CreateDesign(VehicleTypes vehicleType);

	/// <summary>
	/// Creates a design with a specific hull.
	/// </summary>
	/// <param name="hull">The hull to use on the design.</param>
	/// <returns>The new design, with a hull but no components.</returns>
	IDesign CreateDesign(IHull hull);

	/// <summary>
	/// Imports designs from the user's library into the current game.
	/// </summary>
	IEnumerable<IDesign> ImportDesignsFromLibrary();

	/// <summary>
	/// The standard militia unit design for use in defending colonies.
	/// </summary>
	IDesign<IUnit> MilitiaDesign { get; }
}
