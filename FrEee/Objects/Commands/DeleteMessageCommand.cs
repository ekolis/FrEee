using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization; using FrEee.Serialization.Attributes;

namespace FrEee.Objects.Commands
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

		[GameReference]
		public IMessage Message { get; set; }

		public override void Execute()
		{
			Executor.IncomingMessages.Remove(Message);
			Executor.SentMessages.Remove(Message);
		}
	}
}
