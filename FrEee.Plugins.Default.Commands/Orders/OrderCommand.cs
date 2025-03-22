using System;
using System.Collections.Generic;
using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Plugins.Default.Commands.Orders;

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
	public virtual IOrder Order
	{
		get
		{
			return order.Value;
		}
		set
		{
			order = value.ReferViaGalaxy();
		}
	}

	private GameReference<IOrder> order { get; set; }

	public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			base.ReplaceClientIDs(idmap, done);
			order.ReplaceClientIDs(idmap, done);
		}
	}
}