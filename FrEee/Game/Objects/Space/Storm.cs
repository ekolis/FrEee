using System;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A space storm.
	/// </summary>
	 [Serializable] public class Storm : StellarObject, ITemplate<Storm>
	{
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
