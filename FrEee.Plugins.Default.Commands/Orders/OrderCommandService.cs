﻿using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Plugins.Default.Commands.Orders;

[Export(typeof(IPlugin))]
public class OrderCommandService
	: Plugin<IOrderCommandService>, IOrderCommandService
{
	public override string Name { get; } = "OrderCommandService";

	public override IOrderCommandService Implementation => this;

	public IAddOrderCommand AddOrder(IOrderable target, IOrder order)
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
