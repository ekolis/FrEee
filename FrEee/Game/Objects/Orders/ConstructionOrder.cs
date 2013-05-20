using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order for a construction queue to build something.
	/// </summary>
	public class ConstructionOrder<T> : IOrder<ConstructionQueue>
	{
		/// <summary>
		/// The construction item.
		/// </summary>
		public IConstructableTemplate<T> Item { get; set; }

		/// <summary>
		/// Does 1 turn's worth of building.
		/// </summary>
		/// <param name="target"></param>
		public void Execute(ConstructionQueue target)
		{
			
		}
	}
}
