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
	public interface IConstructable : INamed
	{
		/// <summary>
		/// Does this item require a colony to build it?
		/// </summary>
		bool RequiresColonyQueue { get; }

		/// <summary>
		/// Does this item require a space yard to build it?
		/// </summary>
		bool RequiresSpaceYardQueue { get; }

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
	}
}
