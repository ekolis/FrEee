﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System.Drawing;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
    /// <summary>
    /// A display mode for a galaxy map.
    /// </summary>
    public interface IGalaxyViewMode : INamed
    {
        #region Public Methods

        /// <summary>
        /// Draws a star system.
        /// </summary>
        /// <param name="sys">The system to draw.</param>
        /// <param name="g">What to draw on.</param>
        /// <param name="pos">The location on the graphics object to draw it (centered on this point).</param>
        /// <param name="size">The size to draw the system (e.g. diameter of circle, side of square).</param>
        void Draw(StarSystem sys, Graphics g, PointF pos, float size);

        #endregion Public Methods
    }
}
