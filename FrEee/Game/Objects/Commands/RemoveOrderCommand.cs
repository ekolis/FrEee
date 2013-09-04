using FrEee.Game.Interfaces;
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
		public RemoveOrderCommand(Empire issuer, T target, IOrder<T> order)
			: base(issuer, target, order)
		{
		}

		public override void Execute()
		{
			try
			{
				if (Order.IsNew())
				{
					Issuer.Log.Add(new GenericLogMessage("The server attempted to remove an order from " + Target + " that was not actually on the server; it was just added and removed this past turn. This is probably a game bug."));
				}
				else if (Issuer == Target.Owner)
				{
					Target.RemoveOrder(Order);
					Galaxy.Current.Unregister(Order);
				}
				else
				{
					Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Target + " belonging to " + Target.Owner + "!", Galaxy.Current.TurnNumber));
				}
			}
			catch (InvalidCastException ex)
			{
				Issuer.Log.Add(new GenericLogMessage("The server attempted to remove an order from " + Target + " that was not actually on the server; it was just added and removed this past turn. This is probably a game bug."));
			}
		}
	}
}
