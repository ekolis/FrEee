using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to delete a message from an empire's inbox or sentbox.
    /// To delete from the outbox, simply delete the SendMessageCommand from the empire's command queue.
    /// </summary>
    public class DeleteMessageCommand : Command<Empire>
    {
        #region Public Constructors

        public DeleteMessageCommand(IMessage msg)
            : base(Empire.Current)
        {
            Message = msg;
        }

        #endregion Public Constructors

        #region Public Properties

        public IMessage Message { get { return message.Value; } set { message = value.ReferViaGalaxy(); } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<IMessage> message { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            Executor.IncomingMessages.Remove(Message);
            Executor.SentMessages.Remove(Message);
        }

        #endregion Public Methods
    }
}
