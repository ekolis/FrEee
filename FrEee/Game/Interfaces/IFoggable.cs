﻿using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something that can be obscured by fog of war.
    /// </summary>
    public interface IFoggable : IReferrable
    {
        #region Public Properties

        /// <summary>
        /// Is this object just a memory, or a real object?
        /// </summary>
        bool IsMemory { get; set; }

        /// <summary>
        /// The time at which this object was last seen.
        /// E.g. 2.5 would be halfway through the second turn.
        /// </summary>
        double Timestamp { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// The visibility of this object to an empire.
        /// </summary>
        Visibility CheckVisibility(Empire emp);

        /// <summary>
        /// Is this object an obsolete memory?
        /// Memories become obsolete when the object's last known location is visible,
        /// but the object has not been seen for at least 1 full turn.
        /// </summary>
        bool IsObsoleteMemory(Empire emp);

        /// <summary>
        /// Removes any data from this object that the specified empire cannot see.
        /// </summary>
        void Redact(Empire emp);

        #endregion Public Methods
    }
}
