using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle that can exist in space.
	/// </summary>
	public interface ISpaceVehicle : ICombatSpaceObject, IOrderable, IContainable<Fleet>
	{
		double TimeToNextMove { get; set; }
		double TimePerMove { get; }
		int MovementRemaining { get; set; }
		int Speed { get; }
		int SupplyRemaining { get; set; }
		void SpendTime(double timeElapsed);
		Sector Sector { get; set; }

		/// <summary>
		/// The Dijkstra map used for pathfinding.
		/// </summary>
		IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap { get; set; }
	}

	/// <summary>
	/// A vehicle that can exist in space.
	/// </summary>
	/// <typeparam name="T">The type of mobile space object.</typeparam>
	public interface ISpaceVehicle<out T> : ISpaceVehicle, IOrderable
		where T : ISpaceVehicle<T>
	{
		
	}
}
