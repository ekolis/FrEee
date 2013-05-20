using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Moves an order to another location in the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RearrangeOrdersCommand<T> : ICommand<T> where T : IOrderable<T>
	{
		public Empire Issuer
		{
			get;
			set;
		}

		public T Target
		{
			get;
			set;
		}

		public IOrder<T> Order
		{
			get;
			set;
		}

		/// <summary>
		/// How many spaces up (if negative) or down (if positive) to move the order.
		/// </summary>
		public int DeltaPosition
		{
			get;
			set;
		}

		public void Execute()
		{
			if (Issuer == Target.Owner)
			{
				int i = Target.Orders.IndexOf(Order);
				Target.Orders.Remove(Order);
				Target.Orders.Insert(i + DeltaPosition, Order);
			}
			else
			{
				// TODO - log message in empire's log?
				Console.WriteLine(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!");
			}
		}
	}
}
