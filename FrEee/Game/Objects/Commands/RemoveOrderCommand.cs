﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
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
	public class RemoveOrderCommand<T> : OrderCommand<T>
		where T : IOrderable
	{
		public RemoveOrderCommand(T target, IOrder<T> order)
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
				Galaxy.Current.UnassignID(Order);
			}
			else
				Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Executor + " belonging to " + Executor.Owner + "!", Galaxy.Current.TurnNumber));
		}
	}
}
