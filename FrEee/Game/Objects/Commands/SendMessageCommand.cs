using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to send a diplomatic message.
    /// </summary>
    public class SendMessageCommand : Command<Empire>
    {
        #region Public Constructors

        public SendMessageCommand(IMessage message)
            : base(Empire.Current)
        {
            Message = message;
        }

        #endregion Public Constructors

        #region Public Properties

        public IMessage Message { get; set; }

        public override IEnumerable<IReferrable> NewReferrables
        {
            get
            {
                foreach (var r in base.NewReferrables)
                    yield return r;
                yield return Message;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            Message.Owner.SentMessages.Add(Message);
            // TODO - comms interference intel projects
            Message.Recipient.IncomingMessages.Add(Message);
            Message.Recipient.Log.Add(Message.CreateLogMessage("We have received a diplomatic message from the " + Message.Owner + ": " + Message.Text));
            if (Message is ActionMessage)
            {
                // execute unilateral action
                ((ActionMessage)Message).Action.Execute();
            }
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                base.ReplaceClientIDs(idmap, done);
                Message.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
