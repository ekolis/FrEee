using System.Collections.Generic;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
    public static class GalaxyViewModes
    {
        #region Private Fields

        private static IGalaxyViewMode[] all = new IGalaxyViewMode[]
        {
            new PresenceMode(),
            new ForcesMode(),
            new ColoniesMode(),
            new ResourcesMode(),
            new ResearchIntelMode(),
            new UtilityMode(),
            new ConstructionMode(),
            new SpaceYardMode(),
            new RepairMode(),
            new WarpPointsMode(),
        };

        #endregion Private Fields

        #region Public Properties

        public static IEnumerable<IGalaxyViewMode> All
        {
            get
            {
                return all;
            }
        }

        #endregion Public Properties
    }
}
