using FrEee.Objects.LogMessages;
using System.Collections.Generic;

namespace FrEee.Interfaces
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

		/// <summary>
		/// Is this order done executing?
		/// </summary>
		bool CheckCompletion(IOrderable executor);

		/// <summary>
		/// Executes the order.
		/// </summary>
		void Execute(IOrderable executor);

		/// <summary>
		/// Validation errors.
		/// </summary>
		IEnumerable<LogMessage> GetErrors(IOrderable executor);
	}
}