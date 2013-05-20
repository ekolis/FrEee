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
	public interface ICommand<T> where T : IOrderable<T>
	{
		/// <summary>
		/// The empire issuing the command.
		/// </summary>
		Empire Issuer { get; set; }

		/// <summary>
		/// The object whose queue is being manipulated.
		/// </summary>
		T Target { get; set; }

		/// <summary>
		/// The specific order being manipulated.
		/// </summary>
		IOrder<T> Order { get; set; }

		/// <summary>
		/// Executes the command.
		/// </summary>
		void Execute();
	}
}
