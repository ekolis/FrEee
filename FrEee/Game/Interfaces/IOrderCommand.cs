using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	public interface IOrderCommand<T> : ICommand<T>
		where T : IOrderable
	{
		/// <summary>
		/// The specific order being manipulated.
		/// </summary>
		IOrder<T> Order { get; set; }
	}
}
