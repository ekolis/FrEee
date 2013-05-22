using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// The index of the empire issuing the command (the first empire is number 1).
		/// </summary>
		int IssuerID { get; set; }

		/// <summary>
		/// The empire issuing the command.
		/// </summary>
		Empire Issuer { get; }

		/// <summary>
		/// Executes the command.
		/// </summary>
		void Execute();
	}

	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	public interface ICommand<T, TOrder> : ICommand where T : IOrderable<T, TOrder> where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// The ID of the target.
		/// </summary>
		int TargetID { get; set; }

		/// <summary>
		/// The object whose queue is being manipulated.
		/// </summary>
		T Target { get; set; }

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
