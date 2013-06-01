using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
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
	[Serializable]
	public class RemoveOrderCommand<T, TOrder> : Command<T, TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		public RemoveOrderCommand(Empire issuer, T target, TOrder order)
			: base(issuer, target, order)
		 {
		 }

		public override void Execute()
		{
			if (Issuer == Target.Owner)
				Target.Orders.Remove(Order);
			else
			{
				Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!", Galaxy.Current.TurnNumber));
			}
		}
	}
}
