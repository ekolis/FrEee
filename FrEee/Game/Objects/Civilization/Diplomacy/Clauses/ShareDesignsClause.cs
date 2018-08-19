using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
    /// <summary>
    /// A treaty clause which shares knowledge of vehicle designs between empires.
    /// </summary>
    public class ShareDesignsClause : Clause
    {
        #region Public Constructors

        public ShareDesignsClause(Empire giver, Empire receiver)
            : base(giver, receiver)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string BriefDescription
        {
            get { return "Share Designs"; }
        }

        public override string FullDescription
        {
            get
            {
                return Giver.WeOrName() + " will share all known vehicle designs with " + Receiver.UsOrName();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void PerformAction()
        {
            foreach (var d in Giver.KnownDesigns)
                Receiver.KnownDesigns.Add(d);
        }

        #endregion Public Methods
    }
}
