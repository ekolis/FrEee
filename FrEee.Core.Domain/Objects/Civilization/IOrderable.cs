using System.Collections.Generic;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Civilization;

/// <summary>
/// Something which can accept orders from an empire and queue them for execution over time.
/// </summary>
public interface IOrderable : IReferrable
{
    bool IsOnHold { get; set; }

    bool IsOnRepeat { get; set; }

    /// <summary>
    /// The queued orders.
    /// </summary>
    IEnumerable<IOrder> Orders { get; }

    void AddOrder(IOrder order);

    /// <summary>
    /// Executes orders for an appropriate amount of time.
    /// Some objects execute orders for an entire turn at once; others only for smaller ticks.
    /// </summary>
    /// <returns>true if there was anything to do this turn</returns>
    bool ExecuteOrders();

    void RearrangeOrder(IOrder order, int delta);

    void RemoveOrder(IOrder order);
}
