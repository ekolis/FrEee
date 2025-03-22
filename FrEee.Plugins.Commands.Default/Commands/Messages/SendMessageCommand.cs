using System.Collections.Generic;
using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;

namespace FrEee.Plugins.Commands.Default.Commands.Messages;

/// <summary>
/// A command to send a diplomatic message.
/// </summary>
public class SendMessageCommand
	: Command<Empire>, ISendMessageCommand
{
	public SendMessageCommand(IMessage message)
		: base(Empire.Current)
	{
		Message = message;
	}

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

	public override void Execute()
	{
		Message.Owner.SentMessages.Add(Message);
		// TODO - comms interference intel projects
		Message.Recipient.IncomingMessages.Add(Message);
		Message.Recipient.Log.Add(Message.CreateLogMessage("We have received a diplomatic message from the " + Message.Owner + ": " + Message.Text, LogMessageType.Diplomacy));
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
}