using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
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

		/// <summary>
		/// Does this order cost a movement point to execute?
		/// </summary>
		bool ConsumesMovement { get; }
	}

	/// <summary>
	/// An order issued by a player to an object to do something.
	/// TODO - just have the executor be a property of the order; must have done it this way to avoid circular references at some point...
	/// </summary>
	public interface IOrder<in T> : IOrder
		where T : IOrderable
	{
		/// <summary>
		/// Executes the order.
		/// </summary>
		void Execute(T executor);

		/// <summary>
		/// Validation errors.
		/// </summary>
		IEnumerable<LogMessage> GetErrors(T executor);

		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool CheckCompletion(T executor);
	}
}
