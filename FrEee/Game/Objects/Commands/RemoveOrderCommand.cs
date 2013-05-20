using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Removes an order from the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RemoveOrderCommand<T> : ICommand<T> where T : IOrderable<T>
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

		public void Execute()
		{
			if (Issuer == Target.Owner)
				Target.Orders.Remove(Order);
			else
			{
				// TODO - log message in empire's log?
				Console.WriteLine(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!");
			}
		}
	}
}
