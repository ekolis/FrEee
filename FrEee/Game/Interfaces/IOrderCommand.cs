using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	public interface IOrderCommand<T, TOrder> : ICommand<T>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// The ID of the order being manipulated (if it already exists).
		/// </summary>
		int OrderID { get; set; }

		/// <summary>
		/// The specific order being manipulated.
		/// </summary>
		TOrder Order { get; set; }
	}
}
