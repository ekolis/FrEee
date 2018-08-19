using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders
{
    /// <summary>
    /// Loads mod data.
    /// </summary>
    public interface ILoader
    {
        #region Public Properties

        string FileName { get; set; }

        string ModPath { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Loads mod data.
        /// </summary>
        /// <param name="mod">The mod we are loading data into.</param>
        /// <returns>Any mod objects which need IDs generated.</returns>
        IEnumerable<IModObject> Load(Mod mod);

        #endregion Public Methods
    }
}
