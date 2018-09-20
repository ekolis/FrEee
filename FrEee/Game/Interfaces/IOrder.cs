using FrEee.Game.Objects.LogMessages;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order issued by a player to an object to do something.
	/// Orders are distinguished from commands by being queued, rather than instantaneous.
	/// </summary>
	public interface IOrder : IReferrable, IPromotable
	{
		/// <summary>
		/// Does this order cost a movement point to execute?
		/// </summary>
		bool ConsumesMovement { get; }

		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool IsComplete { get; set; }
	}

	/// <summary>
	/// An order issued by a player to an object to do something.
	/// TODO - just have the executor be a property of the order; must have done it this way to avoid circular references at some point...
	/// </summary>
	public interface IOrder<in T> : IOrder
		where T : IOrderable
	{
		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool CheckCompletion(T executor);

		/// <summary>
		/// Executes the order.
		/// </summary>
		void Execute(T executor);

		/// <summary>
		/// Validation errors.
		/// </summary>
		IEnumerable<LogMessage> GetErrors(T executor);
	}
}