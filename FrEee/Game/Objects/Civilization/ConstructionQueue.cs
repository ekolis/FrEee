using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	[Serializable]
	public class ConstructionQueue : IOrderable<ConstructionQueue, IConstructionOrder>
	{
		public ConstructionQueue()
		{
			Orders = new List<IConstructionOrder>();
			Galaxy.Current.Register(this);
		}

		/// <summary>
		/// Is this a space yard queue?
		/// </summary>
		public bool IsSpaceYardQueue { get; set; }

		/// <summary>
		/// Is this a colony queue?
		/// </summary>
		public bool IsColonyQueue { get; set; }

		/// <summary>
		/// Can this queue construct something?
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool CanConstruct(IConstructable item)
		{
			return (IsSpaceYardQueue || !item.RequiresSpaceYardQueue) && (IsColonyQueue || !item.RequiresColonyQueue);
		}

		/// <summary>
		/// The rate at which this queue can construct.
		/// </summary>
		public Resources Rate { get; set; }

		/// <summary>
		/// Unspent build rate for this turn.
		/// </summary>
		public Resources UnspentRate { get; set; }

		public IList<IConstructionOrder> Orders
		{
			get;
			private set;
		}

		public int ID
		{
			get;
			set;
		}

		public ISpaceObject SpaceObject { get; set; }

		public Image Icon
		{
			get
			{
				return SpaceObject.Icon;
			}
		}

		public Empire Owner
		{
			get { return SpaceObject.Owner; }
		}

		public string Name
		{
			get { return SpaceObject.Name; }
		}

		/// <summary>
		/// Executes orders for a turn.
		/// </summary>
		public void ExecuteOrders()
		{
			UnspentRate = Rate;
			var empty = new Resources();
			while (UnspentRate > empty && Orders.Any())
			{
				var numOrders = Orders.Count;

				var order = Orders[0];
				order.Execute(this);
				if (order.IsComplete)
					Orders.Remove(order);

				if (Orders.Count == numOrders)
					break; // couldn't accomplish any orders
			}
		}
	}
}
