using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands.Orders;
public class OrderCommandFactory
	: IOrderCommandFactory
{
	public ISetPlayerInfoCommnad AddOrder(IOrderable target, IOrder order)
	{
		return new AddOrderCommand(target, order);
	}

	public IOrderCommand RearrangeOrders<T>(T target, IOrder order, int deltaPosition) where T : IOrderable
	{
		return new RearrangeOrdersCommand<T>(target, order, deltaPosition);
	}

	public IRemoveOrderCommand RemoveOrder(IOrderable target, IOrder order)
	{
		return new RemoveOrderCommand(target, order);
	}

	public IToggleOrdersOnHoldCommand ToggleOrdersOnHold(IOrderable target, bool areOrdersOnHold)
	{
		return new ToggleOrdersOnHoldCommand(target, areOrdersOnHold);
	}

	public IToggleRepeatOrdersCommand ToggleRepeatOrders(IOrderable target, bool areRepeatOrdersEnabled)
	{
		return new ToggleRepeatOrdersCommand(target, areRepeatOrdersEnabled);
	}
}
