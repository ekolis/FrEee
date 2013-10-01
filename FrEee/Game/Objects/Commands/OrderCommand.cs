using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility; using FrEee.Utility.Serialization;
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

		private Reference<IOrder<T>> order { get; set; }

		[DoNotSerialize]
		public virtual IOrder<T> Order
		{
			get
			{
				return order.Value;
			}
			set
			{
				order = value.Reference();
			}
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			base.ReplaceClientIDs(idmap);
			order.ReplaceClientIDs(idmap);
		}
	}
}
