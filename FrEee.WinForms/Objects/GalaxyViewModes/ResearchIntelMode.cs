﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
    /// <summary>
    /// Displays research/intel.
    /// </summary>
    public class ResearchIntelMode : ArgbMode
    {
        #region Public Properties

        public override string Name
        {
            get { return "Research/Intel"; }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override Color GetColor(StarSystem sys)
        {
            var max = Galaxy.Current.StarSystemLocations.MaxOfAllResources(l => GetResources(l.Item));
            foreach (var r in Resource.All)
                if (max[r] == 0)
                    max[r] = int.MaxValue; // prevent divide by zero
            var amt = GetResources(sys);
            var blue = 255 * amt[Resource.Research] / max[Resource.Research];
            var red = 255 * amt[Resource.Intelligence] / max[Resource.Intelligence];
            return Color.FromArgb(red, 0, blue);
        }

        #endregion Protected Methods

        #region Private Methods

        private ResourceQuantity GetResources(StarSystem sys)
        {
            return sys.SpaceObjects.OwnedBy(Empire.Current).OfType<IIncomeProducer>().Sum(sobj => sobj.GrossIncome());
        }

        #endregion Private Methods
    }
}
