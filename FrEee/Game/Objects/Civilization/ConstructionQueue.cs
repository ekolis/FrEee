using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	public class ConstructionQueue : IOrderable<ConstructionQueue>
	{
		public ConstructionQueue()
		{
			Orders = new List<IOrder<ConstructionQueue>>();
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

		public IList<IOrder<ConstructionQueue>> Orders
		{
			get;
			private set;
		}

		public int ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get;
			set;
		}
	}
}
