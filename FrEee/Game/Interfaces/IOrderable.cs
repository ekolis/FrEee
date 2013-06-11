using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can accept orders from an empire and queue them for execution over time.
	/// </summary>
	public interface IOrderable : ICommandable
	{
		/// <summary>
		/// The queued orders.
		/// </summary>
		IEnumerable<IOrder> Orders { get; }

		void AddOrder(IOrder order);

		void RemoveOrder(IOrder order);

		void RearrangeOrder(IOrder order, int delta);

		/// <summary>
		/// Executes orders for an appropriate amount of time.
		/// Some objects execute orders for an entire turn at once; others only for smaller ticks.
		/// </summary>
		void ExecuteOrders();
	}
}
