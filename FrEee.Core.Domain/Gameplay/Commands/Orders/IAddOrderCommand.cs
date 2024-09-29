using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands.Orders;

/// <summary>
/// A command which adds an <see cref="IOrder"/> to an <see cref="IOrderable"/>
/// </summary>
public interface IAddOrderCommand
	: IOrderCommand
{
}
