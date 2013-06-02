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
	public class ConstructionOrder<T, TTemplate> : IOrder<ConstructionQueue, IConstructionOrder>, IConstructionOrder
		where T : IConstructable
		where TTemplate : ITemplate<T>, IReferrable<object>, IConstructionTemplate
	{
		/// <summary>
		/// The construction template.
		/// </summary>
		public TTemplate Template { get { return template; } set { template = value; } }

		IConstructionTemplate IConstructionOrder.Template { get { return template.Value; } }

		private Reference<TTemplate> template;

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
			{
				Item = Template.Instantiate();
				Item.Owner = target.Owner;
			}

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
