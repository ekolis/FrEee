using FrEee.Game.Objects.Civilization;
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
	public interface IOrder : IReferrable, IPromotable
	{
		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool IsComplete { get; }
	}

	/// <summary>
	/// An order issued by a player to an object to do something.
	/// </summary>
	public interface IOrder<in T> : IOrder
		where T : IOrderable
	{
		/// <summary>
		/// Executes the order.
		/// </summary>
		void Execute(T executor);
	}
}
