﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Orders
{
    /// <summary>
    /// An order to move a mobile space object toward another space object.
    /// </summary>
    [Serializable]
    public class PursueOrder : PathfindingOrder
    {
        #region Public Constructors

        public PursueOrder(ISpaceObject target, bool avoidEnemies)
            : base(target, avoidEnemies)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Verb
        {
            get
            {
                if (KnownTarget == null)
                    return "pursue";
                else if (AvoidEnemies && KnownTarget.Owner != null && (!(KnownTarget is ICombatant) || !(KnownTarget as ICombatant).IsHostileTo(Owner)))
                    return "escort";
                else
                    return "pursue";
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Finds the path for executing this order.
        /// </summary>
        /// <param name="sobj">The space object executing the order.</param>
        /// <returns></returns>
        public override IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
        {
            return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies, true, me.DijkstraMap);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override bool AreWeThereYet(IMobileSpaceObject me)
        {
            return me.Sector == Destination;
        }

        #endregion Protected Methods
    }
}
