using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
    /// <summary>
    /// Displays the presence of empires in star systems using pies with equal slices of each present empire's color, similar to SE3.
    /// </summary>
    public class PresenceMode : PieMode
    {
        #region Public Properties

        public override string Name
        {
            get { return "Presence"; }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override int GetAlpha(StarSystem sys)
        {
            // draw all systems at full opacity
            return 255;
        }

        protected override IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys)
        {
            // count each empire equally
            var owners = FindOwners(sys);
            foreach (var owner in owners)
                yield return Tuple.Create(owner.Color, 1f);
        }

        #endregion Protected Methods

        #region Private Methods

        private IEnumerable<Empire> FindOwners(StarSystem sys)
        {
            return sys.FindSpaceObjects<ISpaceObject>().Select(x => x.Owner).Distinct().Where(o => o != null);
        }

        #endregion Private Methods
    }
}
