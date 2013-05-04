using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A space storm.
	/// </summary>
	public class Storm : StellarObject, ITemplate<Storm>
	{
		/// <summary>
		/// A description of this storm.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The size of this storm.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string CombatTile { get; set; }

		/// <summary>
		/// Just copy the storm's data.
		/// </summary>
		/// <returns>A copy of the storm.</returns>
		public new Storm Instantiate()
		{
			return this.Clone();
		}
	}
}
