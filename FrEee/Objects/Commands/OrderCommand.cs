using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// A command to manipulate an object's order queue.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TOrder"></typeparam>
	[Serializable]
	public abstract class OrderCommand : Command<IOrderable>, IOrderCommand
	{
		protected OrderCommand(IOrderable target, IOrder order)
			: base(target)
		{
			Order = order;
		}

		[GameReference]
		public virtual IOrder Order {  get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				base.ReplaceClientIDs(idmap, done);
				Order.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
