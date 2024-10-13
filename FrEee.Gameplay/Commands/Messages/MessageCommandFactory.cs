using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Messages;
public class MessageCommandFactory
	: IMessageCommandFactory
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
