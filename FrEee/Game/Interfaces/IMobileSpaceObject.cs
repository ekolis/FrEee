using FrEee.Game.Objects.Space;
using FrEee.Utility; using FrEee.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IMobileSpaceObject : ICombatSpaceObject, IOrderable, IContainable<Fleet>
	{
		double TimeToNextMove { get; set; }
		double TimePerMove { get; }
		int MovementRemaining { get; set; }
		int Speed { get; }
		int SupplyRemaining { get; set; }
		void SpendTime(double timeElapsed);

		/// <summary>
		/// The Dijkstra map used for pathfinding.
		/// </summary>
		IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap { get; set; }
	}

	/// <summary>
	/// A space object which can be ordered to move.
	/// </summary>
	/// <typeparam name="T">The type of mobile space object.</typeparam>
	public interface IMobileSpaceObject<out T> : IMobileSpaceObject, IOrderable
		where T : IMobileSpaceObject<T>
	{
		
	}
}
