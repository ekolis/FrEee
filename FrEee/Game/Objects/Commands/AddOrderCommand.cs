using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Adds an order to the end of the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class AddOrderCommand<T, TOrder> : Command<T, TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		public AddOrderCommand(Empire issuer, T target, TOrder order)
			: base(issuer, target, order)
		{
		}

		public override void Execute()
		{
			if (Issuer == Target.Owner)
				Target.Orders.Add(Order);
			else
			{
				// TODO - log message in empire's log?
				Console.WriteLine(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!");
			}
		}

		public override TOrder Order
		{
			get;
			set;
		}
	}
}
