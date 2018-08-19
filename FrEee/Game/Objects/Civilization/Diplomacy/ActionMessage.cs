using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// A message specifying some unilateral action.
    /// </summary>
    public class ActionMessage : Message
    {
        #region Public Constructors

        public ActionMessage(Empire recipient)
            : base(recipient)
        {
            if (Recipient == Owner)
                throw new Exception("You can't perform a diplomatic action on yourself!");
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The action in question.
        /// </summary>
        public Action Action { get; set; }

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

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            Action.ReplaceClientIDs(idmap);
        }

        #endregion Public Methods
    }
}
