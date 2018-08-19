﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Game.Objects.Civilization
{
    /// <summary>
    /// A waypoint which is locked to the location of a space object, and thus may move about if the space object is mobile.
    /// </summary>
    public class SpaceObjectWaypoint : Waypoint
    {
        #region Public Constructors

        public SpaceObjectWaypoint(ISpaceObject sobj)
        {
            SpaceObject = sobj;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Name
        {
            get { return "Waypoint at " + SpaceObject.Name; }
        }

        [DoNotSerialize(false)]
        public override Sector Sector
        {
            get
            {
                return SpaceObject?.Sector;
            }
            set
            {
                throw new InvalidOperationException("Cannot set the sector of a space object waypoint.");
            }
        }

        [DoNotSerialize]
        public ISpaceObject SpaceObject { get { return spaceObject.Value; } set { spaceObject = value.ReferViaGalaxy(); } }

        public override StarSystem StarSystem
        {
            get
            {
                return SpaceObject.StarSystem;
            }
        }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<ISpaceObject> spaceObject { get; set; }

        #endregion Private Properties
    }
}
