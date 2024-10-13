using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Messages;

namespace FrEee.Gameplay.Commands.Messages;

public interface IMessageCommand
	: ICommand<Empire>
{
	IMessage Message { get; }
}
