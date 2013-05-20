using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can accept orders from an empire.
	/// </summary>
	public interface IOrderable<T> where T : IOrderable<T>
	{
		/// <summary>
		/// A unique ID by which the player orders file can reference this orderable.
		/// </summary>
		int ID { get; set; }

		/// <summary>
		/// The owner of this object. Only the owner can issue orders.
		/// </summary>
		Empire Owner { get; }

		/// <summary>
		/// The queued orders.
		/// </summary>
		IList<IOrder<T>> Orders { get; }
	}
}
