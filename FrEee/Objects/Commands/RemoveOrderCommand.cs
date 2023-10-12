using FrEee.Interfaces;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// Removes an order from the queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class RemoveOrderCommand : OrderCommand
	{
		public RemoveOrderCommand(IOrderable target, IOrder order)
			: base(target, order)
		{
		}

		public override void Execute()
		{
			if (Order == null)
				Issuer.Log.Add(new GenericLogMessage("The server attempted to remove a null order from " + Executor + ". Perhaps this order was not actually on the server yet; it was just added and removed this past turn. This is probably a game bug."));
			else if (Order.IsNew())
				Issuer.Log.Add(new GenericLogMessage("The server attempted to remove an order from " + Executor + " that was not actually on the server; it was just added and removed this past turn. This is probably a game bug."));
			else if (Executor == null)
				Issuer.Log.Add(new GenericLogMessage("On your previous turn, you attempted to remove an order from an object with ID #" + executor.ID + ", but no such object exists. This is probably a game bug."));
			else if (Issuer == Executor.Owner)
			{
				Executor.RemoveOrder(Order);
				The.ReferrableRepository.UnassignID(Order);
			}
			else
				Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Executor + " belonging to " + Executor.Owner + "!", The.Game.TurnNumber));
		}
	}
}