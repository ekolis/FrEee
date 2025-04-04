﻿using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.GameState;

namespace FrEee.Plugins.Default.Commands.Messages;

/// <summary>
/// A command to delete a message from an empire's inbox or sentbox.
/// To delete from the outbox, simply delete the SendMessageCommand from the empire's command queue.
/// </summary>
public class DeleteMessageCommand
	: Command<Empire>, IDeleteMessageCommand
{
	public DeleteMessageCommand(IMessage msg)
		: base(Empire.Current)
	{
		Message = msg;
	}

	public IMessage Message { get { return message.Value; } set { message = value.ReferViaGalaxy(); } }

	private GameReference<IMessage> message { get; set; }

	public override void Execute()
	{
		Executor.IncomingMessages.Remove(Message);
		Executor.SentMessages.Remove(Message);
	}
}