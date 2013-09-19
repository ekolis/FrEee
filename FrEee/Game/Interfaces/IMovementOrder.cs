using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order which consumes movement points.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	interface IMovementOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>
	{
		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		Sector Destination { get; }

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <param name="start">The starting sector.</param>
		/// <returns></returns>
		IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start);

		/// <summary>
		/// Creates a Dijkstra map for this order's movement.
		/// </summary>
		/// <param name="me"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start);

		/// <summary>
		/// Did we already log a pathfinding error this turn?
		/// </summary>
		bool LoggedPathfindingError { get; }
	}
}
