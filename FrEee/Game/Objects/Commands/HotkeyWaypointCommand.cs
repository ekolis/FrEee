﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to set a waypoint to a hotkey.
    /// </summary>
    public class HotkeyWaypointCommand : Command<Waypoint>
    {
        #region Public Constructors

        public HotkeyWaypointCommand(Waypoint waypoint, int hotkey, bool redirect)
            : base(waypoint)
        {
            if (hotkey < 0 || hotkey > 9)
                throw new ArgumentOutOfRangeException("Hotkey for waypoint must be between zero and nine if not null.", "hotkey");
            Hotkey = hotkey;
            Redirect = redirect;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// A hotkey from zero to nine used to access this waypoint.
        /// </summary>
        public int Hotkey { get; private set; }

        /// <summary>
        /// If there is a previous waypoint on this hotkey, should ships ordered to go to it be redirected to the new waypoint?
        /// </summary>
        public bool Redirect { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            // sanity check
            var emp = Executor.Owner;
            if (emp != Issuer)
            {
                Issuer.Log.Add(Issuer.CreateLogMessage("We cannot issue a command to hotkey another empire's waypoints!"));
                return;
            }

            // replace old waypoint
            var oldWaypoint = emp.NumberedWaypoints[Hotkey];
            emp.NumberedWaypoints[Hotkey] = Executor;

            if (Redirect)
            {
                int count = 0;
                foreach (var sobj in Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>())
                {
                    bool found = false;
                    // check if space object has orders to move to this waypoint
                    // if so, change that order's waypoint to this one
                    foreach (var order in sobj.Orders)
                    {
                        if (order is WaypointOrder)
                        {
                            var wo = order as WaypointOrder;
                            if (wo.Target == oldWaypoint)
                            {
                                wo.Target = Executor;
                                found = true;
                            }
                        }
                    }
                    if (found)
                        count++;
                }

                if (count > 0)
                    emp.Log.Add(Issuer.CreateLogMessage(count + " vehicles were redirected from " + oldWaypoint + " to " + Executor + "."));
            }
        }

        #endregion Public Methods
    }
}
