using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order issued by a player to an object to do something.
	/// </summary>
	public interface IOrder<in T> where T : IOrderable<T>
	{
		/// <summary>
		/// The ID of this order, for referencing in command files.
		/// </summary>
		int ID { get; set; }

		/// <summary>
		/// Executes the order.
		/// </summary>
		/// <param name="target">The object which is executing the order.</param>
		void Execute(T target);
	}
}
