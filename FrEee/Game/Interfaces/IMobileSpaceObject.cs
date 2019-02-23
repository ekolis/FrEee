using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object that can move about in space and/or receive orders.
	/// </summary>
	public interface IMobileSpaceObject : ICombatSpaceObject, IOrderable, IContainable<Fleet>, IDamageableReferrable
	{
		new Fleet Container { get; set; }

		/// <summary>
		/// The Dijkstra map used for pathfinding.
		/// </summary>
		IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap { get; set; }

		ResourceQuantity MaintenanceCost { get; }
		int MovementRemaining { get; set; }
		new Sector Sector { get; set; }

		/// <summary>
		/// The tonnage of the object, in kT, or null if it's a planet.
		/// </summary>
		int? Size { get; }

		int StrategicSpeed { get; }

		int SupplyRemaining { get; set; }
		double TimePerMove { get; }
		double TimeToNextMove { get; set; }

		/// <summary>
		/// Burns the supplies necessary to move one sector.
		/// </summary>
		void BurnMovementSupplies();

		void SpendTime(double timeElapsed);
	}

	public interface IMobileSpaceObject<T> : IMobileSpaceObject where T : IMobileSpaceObject<T>
	{
		new IList<IOrder<T>> Orders { get; }
	}
}