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
	/// Moves an order to another location in the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class RearrangeOrdersCommand<T> : OrderCommand<T>
		where T : IOrderable
	{
		public RearrangeOrdersCommand(T target, IOrder<T> order, int deltaPosition)
			: base(target, order)
		 {
			 DeltaPosition = deltaPosition;
		 }

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
				Target.RearrangeOrder(Order, DeltaPosition);
			}
			else
			{
				Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!", Galaxy.Current.TurnNumber));
			}
		}
	}
}
