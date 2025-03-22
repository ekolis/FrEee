using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Objects.Civilization.Diplomacy.Messages;

namespace FrEee.Plugins.Commands.Default.Messages;

[Export(typeof(IPlugin))]
public class MessageCommandService
	: Plugin<IMessageCommandService>, IMessageCommandService
{
	public override string Name { get; } = "MessageCommandService";

	public override IMessageCommandService Implementation => this;

	public ISendMessageCommand SendMessage(IMessage message)
	{
		return new SendMessageCommand(message);
	}

	public IDeleteMessageCommand DeleteMessage(IMessage message)
	{
		return new DeleteMessageCommand(message);
	}
}
