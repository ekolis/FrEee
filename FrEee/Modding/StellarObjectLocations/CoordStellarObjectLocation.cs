﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Interfaces;
using System;
using System.Drawing;

namespace FrEee.Modding.StellarObjectLocations
{
    /// <summary>
    /// Chooses a specific sector of a system.
    /// </summary>
    [Serializable]
    public class CoordStellarObjectLocation : IStellarObjectLocation
    {
        #region Public Properties

        public Point Coordinates { get; set; }
        public Point? LastResult { get; private set; }
        public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

        /// <summary>
        /// If true, (0,0) is the center of the system.
        /// Otherwise, (6,6) is the center of the system (as in SE4).
        /// </summary>
        public bool UseCenteredCoordinates { get; set; }

        #endregion Public Properties

        #region Public Methods

        public Point Resolve(StarSystem sys)
        {
            int realx = Coordinates.X - (UseCenteredCoordinates ? 0 : 6);
            int realy = Coordinates.Y - (UseCenteredCoordinates ? 0 : 6);
            if (UseCenteredCoordinates)
            {
                if (realx < -sys.Radius || realx > sys.Radius || realy < -sys.Radius || realy > sys.Radius)
                    throw new Exception("Invalid location \"Centered Coord " + Coordinates.X + ", " + Coordinates.Y + "\" specified for system of radius " + sys.Radius + ".");
            }
            else
            {
                if (realx < -sys.Radius || realx > sys.Radius || realy < -sys.Radius || realy > sys.Radius)
                    throw new Exception("Invalid location \"Coord " + Coordinates.X + ", " + Coordinates.Y + "\" specified for system of radius " + sys.Radius + ".");
            }
            LastResult = new Point(realx, realy);
            return LastResult.Value;
        }

        #endregion Public Methods
    }
}
