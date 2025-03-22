using FrEee.Objects.Civilization.Diplomacy.Messages;

namespace FrEee.Gameplay.Commands.Messages;
public class MessageCommandService
	: IMessageCommandService
{
	public ISendMessageCommand SendMessage(IMessage message)
	{
		return new SendMessageCommand(message);
	}

	public IDeleteMessageCommand DeleteMessage(IMessage message)
	{
		return new DeleteMessageCommand(message);
	}
}
