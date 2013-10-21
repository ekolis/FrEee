using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Civilization.Diplomacy;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to send a diplomatic message.
	/// </summary>
	public class SendMessageCommand : Command<Empire>
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

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			base.ReplaceClientIDs(idmap);
			Message.ReplaceClientIDs(idmap);
		}

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
	}
}
