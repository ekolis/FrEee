using FrEee.Game.Interfaces;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// A general message with no parameters. Just text.
    /// </summary>
    public class GeneralMessage : Message
    {
        #region Public Constructors

        public GeneralMessage(Empire recipient)
            : base(recipient)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override IEnumerable<string> IconPaths
        {
            get
            {
                return Owner.IconPaths;
            }
        }

        public override IEnumerable<string> PortraitPaths
        {
            get
            {
                return Owner.PortraitPaths;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
        {
            // nothing to do
        }

        #endregion Public Methods
    }
}
