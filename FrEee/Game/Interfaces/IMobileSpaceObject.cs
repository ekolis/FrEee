using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IMobileSpaceObject : ICombatSpaceObject, IOrderable
	{
		double TimeToNextMove { get; set; }
		double TimePerMove { get; }
		void ExecuteOrders();
		void RefillMovement();
		bool CanWarp { get; }
		int Speed { get; }

		/// <summary>
		/// The path that this space object is ordered to follow.
		/// </summary>
		IEnumerable<Sector> Path { get; }

		/// <summary>
		/// The Dijkstra map used for pathfinding.
		/// </summary>
		IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap { get; }

		/// <summary>
		/// Refreshes the Dijkstra map according to this object's movement orders.
		/// </summary>
		void RefreshDijkstraMap();
	}

	/// <summary>
	/// A space object which can be ordered to move.
	/// </summary>
	/// <typeparam name="T">The type of mobile space object.</typeparam>
	public interface IMobileSpaceObject<T> : IMobileSpaceObject, IOrderable
		where T : IMobileSpaceObject<T>
	{
		
	}
}
