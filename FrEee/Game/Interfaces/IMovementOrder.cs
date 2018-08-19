﻿using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// An order which moves a space object.
    /// </summary>
    public interface IMovementOrder : IOrder<IMobileSpaceObject>
    {
        #region Public Properties

        /// <summary>
        /// The sector we are moving to.
        /// </summary>
        Sector Destination { get; }

        /// <summary>
        /// Did we already log a pathfinding error this turn?
        /// </summary>
        bool LoggedPathfindingError { get; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}
