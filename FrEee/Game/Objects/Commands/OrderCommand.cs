using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TOrder"></typeparam>
	[Serializable]
	public abstract class Command<T, TOrder> : Command<T>, IOrderCommand<T, TOrder>
		where T : IOrderable<T, TOrder>
		where TOrder : IOrder<T, TOrder>
	{
		protected Command(Empire issuer, T target, TOrder order)
			: base(issuer, target)
		{
			Order = order;
		}

		public int OrderID
		{
			get;
			set;
		}

		[DoNotSerialize]
		public virtual TOrder Order
		{
			get
			{
				return Target.Orders[OrderID - 1];
			}
			set
			{
				OrderID = Target.Orders.IndexOf(value) + 1;
			}
		}
	}
}
