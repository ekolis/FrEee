using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to delete a message from an empire's inbox or sentbox.
	/// To delete from the outbox, simply delete the SendMessageCommand from the empire's command queue.
	/// </summary>
	public class DeleteMessageCommand : Command<Empire>
	{
		public DeleteMessageCommand(IMessage msg)
			: base(Empire.Current)
		{
			Message = msg;
		}

		private Reference<IMessage> message { get; set; }

		public IMessage Message { get { return message.Value; } set { message = value.Reference(); } }

		public override void Execute()
		{
			Executor.IncomingMessages.Remove(Message);
			Executor.SentMessages.Remove(Message);
		}
	}
}
