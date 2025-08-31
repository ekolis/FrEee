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
	public IOrder Order { get; set; }

	protected virtual GameReference<IOrder> order
	{
		get => Order?.ReferViaGalaxy();
		set => Order = value?.Value;
	}

	public override IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			base.ReplaceClientIDs(idmap, done);
			order = order.ReplaceClientIDs(idmap, done);
		}
		return this;
	}
}