using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders
{
    /// <summary>
    /// Loads design roles from DefaultDesignTypes.txt
    /// </summary>
    [Serializable]
    public class DesignRoleLoader : ILoader
    {
        #region Public Fields

        public const string Filename = "DefaultDesignTypes.txt";

        #endregion Public Fields

        #region Public Constructors

        public DesignRoleLoader(string modPath)
        {
            ModPath = modPath;
            FileName = Filename;
            DataFile = DataFile.Load(modPath, Filename);
        }

        #endregion Public Constructors

        #region Public Properties

        public DataFile DataFile { get; set; }

        public string FileName { get; set; }

        public string ModPath { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IEnumerable<IModObject> Load(Mod mod)
        {
            foreach (var rec in DataFile.Records)
            {
                mod.DesignRoles.Add(rec.Get<string>("Name"));
            }
            yield break; // no actual mod objects to load
        }

        #endregion Public Methods
    }
}
