using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order issued by a player to an object to do something.
	/// </summary>
	public interface IOrder<in T, out TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// Executes the order.
		/// </summary>
		/// <param name="target">The object which is executing the order.</param>
		void Execute(T target);

		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool IsComplete { get; }
	}
}
