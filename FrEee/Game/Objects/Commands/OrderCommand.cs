using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
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
	public abstract class OrderCommand<T> : Command<T>, IOrderCommand<T>
		where T : IOrderable
	{
		protected OrderCommand(Empire issuer, T target, IOrder<T> order)
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
		public virtual IOrder<T> Order
		{
			get
			{
				return (IOrder<T>)Target.Orders.ElementAt(OrderID - 1);
			}
			set
			{
				OrderID = Target.Orders.Cast<IOrder<T>>().IndexOf(value) + 1;
			}
		}
	}
}
