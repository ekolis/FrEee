using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using System;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Moves an order to another location in the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class RearrangeOrdersCommand<T> : OrderCommand
		where T : IOrderable
	{
		public RearrangeOrdersCommand(T target, IOrder order, int deltaPosition)
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
			if (Issuer == Executor.Owner)
			{
				Executor.RearrangeOrder(Order, DeltaPosition);
			}
			else
			{
				Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Executor + " belonging to " + Executor.Owner + "!", Galaxy.Current.TurnNumber));
			}
		}
	}
}