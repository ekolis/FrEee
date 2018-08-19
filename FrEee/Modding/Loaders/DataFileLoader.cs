using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders
{
    /// <summary>
    /// Loads data from a data file.
    /// </summary>
    public abstract class DataFileLoader : ILoader
    {
        #region Public Constructors

        public DataFileLoader(string modPath, string filename, DataFile df)
        {
            ModPath = modPath;
            FileName = filename;
            DataFile = df;
        }

        #endregion Public Constructors

        #region Public Properties

        public DataFile DataFile { get; set; }

        public string FileName { get; set; }

        public string ModPath { get; set; }

        #endregion Public Properties

        #region Public Methods

        public abstract IEnumerable<IModObject> Load(Mod mod);

        #endregion Public Methods
    }
}
