using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order for a construction queue to build something.
	/// </summary>
	[Serializable]
	public class ConstructionOrder<T> : IOrder<ConstructionQueue, IConstructionOrder>, IConstructionOrder where T : IConstructable
	{
		/// <summary>
		/// The construction template.
		/// </summary>
		public ITemplate<T> Template { get; set; }

		/// <summary>
		/// The item being built.
		/// </summary>
		public T Item { get; set; }

		/// <summary>
		/// Does 1 turn's worth of building.
		/// </summary>
		/// <param name="target"></param>
		public void Execute(ConstructionQueue target)
		{
			// create item if needed
			if (Item == null)
				Item = Template.Instantiate();

			// apply build rate
			var costLeft = Item.Cost - Item.ConstructionProgress;
			var spending = Resources.Min(costLeft, target.UnspentRate);
			spending = Resources.Min(spending, target.Owner.StoredResources);
			target.Owner.StoredResources -= spending;
			target.UnspentRate -= spending;
			Item.ConstructionProgress += spending;
		}


		public bool IsComplete
		{
			get { return Item.ConstructionProgress >= Item.Cost; }
		}

		IConstructable IConstructionOrder.Item
		{
			get { return Item; }
		}
	}
}
