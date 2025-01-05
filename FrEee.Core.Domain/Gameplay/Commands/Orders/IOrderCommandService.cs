using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands.Orders;

/// <summary>
/// Creates various types of commands used for managing <see cref="IOrder">s.
/// </summary>
public interface IOrderCommandService
{
    IAddOrderCommand AddOrder(IOrderable target, IOrder order);

    IOrderCommand RearrangeOrders<T>(T target, IOrder order, int deltaPosition)
        where T : IOrderable;

    IRemoveOrderCommand RemoveOrder(IOrderable target, IOrder order);

    IToggleOrdersOnHoldCommand ToggleOrdersOnHold(IOrderable target, bool areOrdersOnHold);

    IToggleRepeatOrdersCommand ToggleRepeatOrders(IOrderable target, bool areRepeatOrdersEnabled);
}
