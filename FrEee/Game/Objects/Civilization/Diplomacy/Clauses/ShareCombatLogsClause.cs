using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Utility.Extensions;
using System.Linq;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
    /// <summary>
    /// A treaty clause which shares combat logs between empires.
    /// </summary>
    public class ShareCombatLogsClause : Clause
    {
        #region Public Constructors

        public ShareCombatLogsClause(Empire giver, Empire receiver)
            : base(giver, receiver)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string BriefDescription
        {
            get { return "Share Combat Logs"; }
        }

        public override string FullDescription
        {
            get
            {
                return Giver.WeOrName() + " will share all combat logs with " + Receiver.UsOrName();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void PerformAction()
        {
            foreach (var msg in Giver.Log.OfType<PictorialLogMessage<IBattle>>())
                Receiver.Log.Add(msg.Copy());
        }

        #endregion Public Methods
    }
}
