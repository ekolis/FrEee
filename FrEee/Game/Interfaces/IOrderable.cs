using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can accept orders from an empire.
	/// </summary>
	public interface IOrderable
	{
		/// <summary>
		/// A unique ID by which the player orders file can reference this orderable.
		/// </summary>
		int ID { get; set; }

		/// <summary>
		/// The owner of this object. Only the owner can issue orders.
		/// </summary>
		Empire Owner { get; }

		/// <summary>
		/// Executes orders for an appropriate amount of time.
		/// Some objects execute orders for an entire turn at once; others only for smaller ticks.
		/// </summary>
		void ExecuteOrders();
	}

	/// <summary>
	/// Something which can accept orders from an empire.
	/// </summary>
	public interface IOrderable<T, TOrder> : IOrderable  where T : IOrderable<T, TOrder> where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// The queued orders.
		/// </summary>
		IList<TOrder> Orders { get; }
	}
}
