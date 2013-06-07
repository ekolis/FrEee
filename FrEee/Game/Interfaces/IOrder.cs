using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order issued by a player to an object to do something.
	/// </summary>
	[ClientSafe]
	public interface IOrder<T, out TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// The object receiving the order.
		/// </summary>
		T Target { get; set; }

		/// <summary>
		/// Executes the order.
		/// </summary>
		void Execute();

		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool IsComplete { get; }
	}
}
