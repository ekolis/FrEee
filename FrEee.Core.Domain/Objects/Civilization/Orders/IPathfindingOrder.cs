using FrEee.Objects.Space;

namespace FrEee.Objects.Civilization.Orders;

/// <summary>
/// A movement order whose movement is relative to some space object.
/// </summary>
public interface IPathfindingOrder : IMovementOrder, IOwnable
{
    /// <summary>
    /// Alternate target. This should be the largest ship in a fleet when a fleet is being pursued.
    /// </summary>
    ISpaceObject AlternateTarget { get; }

    /// <summary>
    /// Should pathfinding avoid enemies?
    /// </summary>
    bool AvoidEnemies { get; set; }

    new bool IsComplete { get; set; }

    /// <summary>
    /// Either the target itself, or the memory of the target, if it's not visible.
    /// </summary>
    ISpaceObject KnownTarget { get; }

    /// <summary>
    /// The empire which issued the order.
    /// </summary>
    new Empire Owner { get; set; }

    /// <summary>
    /// The target space object to pursue, evade, etc.
    /// </summary>
    ISpaceObject Target { get; set; }

    /// <summary>
    /// A verb used to describe this order.
    /// </summary>
    string Verb { get; }

    /// <summary>
    /// Call this when calling UpdateMemory on the target.
    /// Sets the alternate target to the largest ship in a fleet, if the target is a fleet.
    /// If the fleet is destroyed, sets the target to the alternate target.
    /// If the target is a ship, etc., and it is destroyed, sets the target to the memory of the target, or deletes the order if there is no memory.
    /// If the target is a memory, and the original object is sighted again, sets the target to the original object.
    /// Otherwise sets the alternate target to the target.
    /// </summary>
    void UpdateAlternateTarget();
}