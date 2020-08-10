using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Commands
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

		[DoNotSerialize]
		public virtual IOrder? Order
		{
			get => order?.Value;
			set => order = value.ReferViaGalaxy();
		}

		private GalaxyReference<IOrder?>? order { get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				base.ReplaceClientIDs(idmap, done);
				order?.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
