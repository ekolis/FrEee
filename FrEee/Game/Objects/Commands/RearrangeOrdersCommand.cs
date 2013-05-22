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
	[Serializable]
	public class RearrangeOrdersCommand<T, TOrder> : Command<T, TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		/// <summary>
		/// How many spaces up (if negative) or down (if positive) to move the order.
		/// </summary>
		public int DeltaPosition
		{
			get;
			set;
		}

		public override void Execute()
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
