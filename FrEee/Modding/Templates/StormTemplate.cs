﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Linq;

namespace FrEee.Modding.Templates
{
    /// <summary>
    /// A template for generating storms.
    /// </summary>
    [Serializable]
    public class StormTemplate : ITemplate<Storm>, IModObject
    {
        #region Public Properties

        /// <summary>
        /// Abilities to assign to the storm.
        /// </summary>
        public RandomAbilityTemplate Abilities { get; set; }

        public bool IsDisposed
        {
            get; private set;
        }

        public string ModID
        {
            get;
            set;
        }

        public string Name { get; set; }

        /// <summary>
        /// The size of the storm, or null to choose a size randomly.
        /// </summary>
        public StellarSize? Size { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            // TODO - remove it from somewhere?
            IsDisposed = true;
        }

        public Storm Instantiate()
        {
            var candidates = Mod.Current.StellarObjectTemplates.OfType<Storm>();
            if (Size != null)
                candidates = candidates.Where(p => p.StellarSize == Size.Value);
            if (!candidates.Any())
                throw new Exception("No storms in SectType.txt of stellar size " + Size + "!");

            var storm = candidates.PickRandom().Instantiate();

            var abil = Abilities.Instantiate();
            if (abil != null)
                storm.IntrinsicAbilities.Add(abil);

            return storm;
        }

        #endregion Public Methods
    }
}
