using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
    /// <summary>
    /// A treaty clause which shares vision between empires.
    /// </summary>
    public class ShareVisionClause : Clause
    {
        #region Public Constructors

        public ShareVisionClause(Empire giver, Empire receiver)
            : base(giver, receiver)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string BriefDescription
        {
            get { return "Share Vision"; }
        }

        public override string FullDescription
        {
            get
            {
                return Giver.WeOrName() + " will share all vision memory with " + Receiver.UsOrName();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void PerformAction()
        {
            foreach (var kvp in Giver.Memory)
            {
                var existing = Receiver.Memory[kvp.Key];
                if (existing == null || existing.Timestamp < kvp.Value.Timestamp)
                {
                    // copy memory
                    var copy = kvp.Value.CopyAndAssignNewID();
                    Receiver.Memory[kvp.Key] = copy;
                }
            }
        }

        #endregion Public Methods
    }
}
