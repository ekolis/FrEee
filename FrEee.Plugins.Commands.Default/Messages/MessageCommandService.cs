using System;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Objects.Civilization.Diplomacy.Messages;

namespace FrEee.Plugins.Commands.Default.Messages;
public class MessageCommandService
	: IMessageCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "MessageCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public ISendMessageCommand SendMessage(IMessage message)
	{
		return new SendMessageCommand(message);
	}

	public IDeleteMessageCommand DeleteMessage(IMessage message)
	{
		return new DeleteMessageCommand(message);
	}
}
