using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands.Orders;

/// <summary>
/// A command which sets the <see cref="PlayerInfo"/> of an <see cref="Empire"/>.
/// </summary>
public interface ISetPlayerInfoCommand
	: ICommand<Empire>
{
	PlayerInfo PlayerInfo { get; set; }
}
