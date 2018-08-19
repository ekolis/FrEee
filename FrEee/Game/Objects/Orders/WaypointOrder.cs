﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Orders
{
    /// <summary>
    /// An order to move to a waypoint.
    /// </summary>
    /// <typeparam name="IMobileSpaceObject"></typeparam>
    public class WaypointOrder : IMovementOrder
    {
        #region Public Constructors

        public WaypointOrder(Waypoint target, bool avoidEnemies)
        {
            Owner = Empire.Current;
            Target = target;
            AvoidEnemies = avoidEnemies;
            // TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
        }

        #endregion Public Constructors

        #region Public Properties

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
            get { return Target.Sector; }
        }

        public long ID { get; set; }

        public bool IsComplete
        {
            get;
            set;
        }

        public bool IsDisposed { get; set; }

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
        public Waypoint Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

        /// <summary>
        /// A verb used to describe this order.
        /// </summary>
        public string Verb
        {
            get
            {
                if (AvoidEnemies)
                    return "navigate to";
                else
                    return "patrol";
            }
        }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<Empire> owner { get; set; }
        private GalaxyReference<Waypoint> target { get; set; }

        #endregion Private Properties

        #region Public Methods

        public bool CheckCompletion(IMobileSpaceObject v)
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

        public void Execute(IMobileSpaceObject sobj)
        {
            // TODO - movement logs
            if (Target == null)
                IsComplete = true; // target waypoint doesn't exist anymore
            else if (sobj.Sector == Target.Sector)
                IsComplete = true; // we've arrived at the target
            else
            {
                var gotoSector = Pathfind(sobj, sobj.Sector).FirstOrDefault();
                if (gotoSector != null)
                {
                    // move
                    sobj.Sector = gotoSector;
                    sobj.RefreshDijkstraMap();

                    // consume supplies
                    sobj.BurnMovementSupplies();

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
                else if (!LoggedPathfindingError)
                {
                    // log pathfinding error
                    string reason;
                    if (sobj.Speed <= 0)
                        reason = sobj + " is immobile";
                    else
                        reason = "there is no available path leading toward " + Destination;
                    PathfindingError = sobj.CreateLogMessage(sobj + " could not " + Verb + " " + Target + " because " + reason + ".");
                    sobj.Owner.Log.Add(PathfindingError);
                    LoggedPathfindingError = true;
                }
            }

            // spend time
            sobj.SpendTime(sobj.TimePerMove);
        }

        public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject v)
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
        public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
        {
            return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies, true, me.DijkstraMap);
        }

        public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
        {
            target.ReplaceClientIDs(idmap, done);
        }

        public override string ToString()
        {
            if (Target == null)
                return "Unknown " + Verb + " order";
            return Verb.Capitalize() + " " + Target;
        }

        #endregion Public Methods
    }
}
