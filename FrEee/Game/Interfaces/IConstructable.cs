using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can be constructed at a construction queue.
	/// </summary>
	public interface IConstructable : INamed, IPictorial
	{
		/// <summary>
		/// The empire which owns this item.
		/// </summary>
		Empire Owner { get; set; }

		/// <summary>
		/// The resource cost to build this item.
		/// </summary>
		Resources Cost { get; }

		/// <summary>
		/// The progress toward constructing this item.
		/// </summary>
		Resources ConstructionProgress { get; set; }

		/// <summary>
		/// An icon used to represent this item in the construction queue.
		/// </summary>
		Image Icon { get; }

		/// <summary>
		/// Places the newly constructed item at a location.
		/// </summary>
		/// <param name="target">The space object which the item should be placed on or near.</param>
		void Place(ISpaceObject target);
	}
}
