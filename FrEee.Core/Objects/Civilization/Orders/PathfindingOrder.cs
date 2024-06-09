using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Extensions;

namespace FrEee.Objects.Civilization.Orders;

/// <summary>
/// An order to pathfind relative to a target.
/// </summary>
/// <typeparam name="IMobileSpaceObject"></typeparam>
public abstract class PathfindingOrder
    : IPathfindingOrder
{
    protected PathfindingOrder(ISpaceObject target, bool avoidEnemies)
    {
        Owner = Empire.Current;
        Target = target;
        AvoidEnemies = avoidEnemies;
        // TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
    }

    /// <summary>
    /// Alternate target. This should be the largest ship in a fleet when a fleet is being pursued.
    /// </summary>
    [DoNotSerialize]
    public ISpaceObject AlternateTarget
    {
        get;
        private set;
    }

    /// <summary>
    /// Should pathfinding avoid enemies?
    /// </summary>
    public bool AvoidEnemies { get; set; }

    public bool ConsumesMovement
    {
        get { return true; }
    }

    public Sector Destination
    {
        get { return KnownTarget?.Sector; }
    }

    public long ID { get; set; }

    public bool IsComplete
    {
        get;
        set;
    }

    public bool IsDisposed { get; set; }

    /// <summary>
    /// Either the target itself, or the memory of the target, if it's not visible.
    /// </summary>
    public ISpaceObject KnownTarget
    {
        get
        {
            if (Target == null)
                return null;
            if (Target.CheckVisibility(Owner) >= Visibility.Visible)
                return Target;
            return Owner?.Recall(Target);
        }
    }

    /// <summary>
    /// Did we already log a pathfinding error this turn?
    /// </summary>
    [DoNotSerialize]
    public bool LoggedPathfindingError { get; private set; }

    /// <summary>
    /// The empire which issued the order.
    /// </summary>
    [DoNotSerialize]
    public Empire Owner { get { return owner; } set { owner = value; } }

    /// <summary>
    /// Any pathfinding error that we might have found.
    /// </summary>
    [DoNotSerialize]
    public LogMessage PathfindingError { get; private set; }

    /// <summary>
    /// The target we are pursuing.
    /// </summary>
    [DoNotSerialize]
    public ISpaceObject Target { get { return target?.Value; } set { target = value.ReferViaGalaxy(); } }

    /// <summary>
    /// A verb used to describe this order.
    /// </summary>
    public abstract string Verb { get; }

    private GalaxyReference<Empire> owner { get; set; }
    private GalaxyReference<ISpaceObject> target { get; set; }

    public bool CheckCompletion(IOrderable v)
    {
        return IsComplete;
    }

    /// <summary>
    /// Orders are visible only to their owners.
    /// </summary>
    /// <param name="emp"></param>
    /// <returns></returns>
    public Visibility CheckVisibility(Empire emp)
    {
        if (emp == Owner)
            return Visibility.Visible;
        return Visibility.Unknown;
    }

    public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
    {
        return Pathfinder.CreateDijkstraMap(me, start, Destination, AvoidEnemies, true);
    }

    public void Dispose()
    {
        if (IsDisposed)
            return;
        foreach (var v in Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>())
            v.RemoveOrder(this);
        Galaxy.Current.UnassignID(this);
    }

    public void Execute(IOrderable ord)
    {
        if (ord is IMobileSpaceObject sobj)
        {
            // TODO - movement logs
            if (KnownTarget == null)
                IsComplete = true; // target is known to be dead
            else if (AreWeThereYet(sobj))
                IsComplete = true; // we've arrived at the target
            else
            {
                var gotoSector = Pathfind(sobj, sobj.Sector).FirstOrDefault();
                if (gotoSector != null)
                {
                    // move
                    if (gotoSector == null)
                    {
                        // try to warp through an unexplored warp point
                        var wps = sobj.Sector.SpaceObjects.OfType<WarpPoint>().Where(w => !w.TargetStarSystemLocation.Item.ExploredByEmpires.Contains(sobj.Owner));
                        var wp = wps.PickRandom();
                        if (wp != null)
                        {
                            // warp through the unexplored warp point
                            sobj.Sector = wp.Target;
                        }
                        else if (!LoggedPathfindingError)
                        {
                            // no warp points to explore and we haven'IMobileSpaceObject told the player yet
                            PathfindingError = sobj.CreateLogMessage("{0} found no unexplored warp points at {1} to enter.".F(sobj, sobj.Sector), LogMessageType.Warning);
                            sobj.Owner.Log.Add(PathfindingError);
                            LoggedPathfindingError = true;
                        }
                    }
                    else
                    {
                        sobj.Sector = gotoSector;
                        sobj.RefreshDijkstraMap();

                        // consume supplies
                        sobj.BurnMovementSupplies();

                        // are we there yet, Dad?
                        if (AreWeThereYet(sobj))
                            IsComplete = true; // we've arrived at the target

                        // resupply space vehicles
                        // either this vehicle from other space objects, or other vehicles from this one
                        // TODO - this should really be done AFTER battles...
                        if (gotoSector.HasAbility("Supply Generation", sobj.Owner))
                        {
                            foreach (var v in gotoSector.SpaceObjects.OfType<IMobileSpaceObject>().Where(v => v.Owner == sobj.Owner))
                                v.SupplyRemaining = v.SupplyStorage;
                        }
                        if (gotoSector.StarSystem.HasAbility("Supply Generation - System", sobj.Owner) || gotoSector.StarSystem.HasAbility("Supply Generation - System"))
                        {
                            foreach (var v in gotoSector.StarSystem.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == sobj.Owner))
                                v.SupplyRemaining = v.SupplyStorage;
                        }
                    }
                }
                else if (!LoggedPathfindingError)
                {
                    // log pathfinding error
                    string reason;
                    if (sobj.StrategicSpeed <= 0)
                        reason = sobj + " is immobile";
                    else
                        reason = "there is no available path leading toward " + Destination;
                    PathfindingError = sobj.CreateLogMessage(sobj + " could not " + Verb + " " + KnownTarget + " because " + reason + ".", LogMessageType.Warning);
                    sobj.Owner.Log.Add(PathfindingError);
                    LoggedPathfindingError = true;
                }
            }

            // spend time
            sobj.SpendTime(sobj.TimePerMove);
        }
        else
            ord.Owner.RecordLog(ord, $"{ord} cannot pathfind because it is not a mobile space object.", LogMessageType.Error);
    }

    public IEnumerable<LogMessage> GetErrors(IOrderable v)
    {
        if (PathfindingError != null)
            yield return PathfindingError;
    }

    /// <summary>
    /// Finds the path for executing this order.
    /// </summary>
    /// <param name="sobj">The space object executing the order.</param>
    /// <param name="start">The start location (need not be the current location, in case there are prior orders queued).</param>
    /// <returns></returns>
    public abstract IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start);

    public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
    {
        // This type does not use client objects, so nothing to do here.
    }

    public override string ToString()
    {
        if (KnownTarget == null)
            return "Unknown " + Verb + " order";
        return Verb.Capitalize() + " " + KnownTarget;
    }

    /// <summary>
    /// Call this when calling UpdateMemory on the target.
    /// Sets the alternate target to the largest ship in a fleet, if the target is a fleet.
    /// If the fleet is destroyed, sets the target to the alternate target.
    /// If the target is a ship, etc., and it is destroyed, sets the target to the memory of the target, or deletes the order if there is no memory.
    /// If the target is a memory, and the original object is sighted again, sets the target to the original object.
    /// Otherwise sets the alternate target to the target.
    /// </summary>
    public void UpdateAlternateTarget()
    {
        if (Target is Fleet)
        {
            var f = (Fleet)Target;
            if (!f.IsDestroyed)
                AlternateTarget = f.LeafVehicles.Largest();
            else
                Target = AlternateTarget;
        }
        else if (Target is IMobileSpaceObject)
        {
            var sobj = (IMobileSpaceObject)Target;
            if (sobj.IsMemory && sobj.FindOriginalObject(Owner) != null)
                Target = (ISpaceObject)sobj.FindOriginalObject(Owner);
            if (!sobj.IsDestroyed)
                AlternateTarget = Target;
            else if (Owner.Memory[Target.ID] != null)
                Target = (ISpaceObject)Owner.Memory[Target.ID];
            else
                Dispose();
        }
        else
            AlternateTarget = Target;
    }

    protected abstract bool AreWeThereYet(IMobileSpaceObject me);
}