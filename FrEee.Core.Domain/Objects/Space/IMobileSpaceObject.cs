using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Objects.Space;

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

// HACK: find a way to get rid of this generic interface, it's stupid...
public interface IMobileSpaceObject<T> : IMobileSpaceObject where T : IMobileSpaceObject<T>
{
	//new IList<IOrder> Orders { get; }
}