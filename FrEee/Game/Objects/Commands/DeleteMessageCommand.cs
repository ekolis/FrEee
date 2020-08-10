using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

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
			=> Message = msg;

		public IMessage? Message { get => message?.Value; set => message = value.ReferViaGalaxy(); }

		private GalaxyReference<IMessage?>? message { get; set; }

		public override void Execute()
		{
			Executor?.IncomingMessages.Remove(Message);
			Executor?.SentMessages.Remove(Message);
		}
	}
}
