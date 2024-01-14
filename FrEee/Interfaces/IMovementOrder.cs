using FrEee.Objects.Space;
using FrEee.Utility; using FrEee.Serialization;
using System.Collections.Generic;

namespace FrEee.Interfaces;

/// <summary>
/// An order which moves a space object.
/// </summary>
public interface IMovementOrder : IOrder
{
	/// <summary>
	/// The sector we are moving to.
	/// </summary>
	Sector Destination { get; }

	/// <summary>
	/// Did we already log a pathfinding error this turn?
	/// </summary>
	bool LoggedPathfindingError { get; }

	/// <summary>
	/// Creates a Dijkstra map for this order's movement.
	/// </summary>
	/// <param name="me"></param>
	/// <param name="start"></param>
	/// <returns></returns>
	IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start);

	/// <summary>
	/// Finds the path for executing this order.
	/// </summary>
	/// <param name="sobj">The space object executing the order.</param>
	/// <param name="start">The starting sector.</param>
	/// <returns></returns>
	IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start);
}