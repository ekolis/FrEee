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
	public class Storm : ISpaceObject, ITemplate<Storm>
	{
		/// <summary>
		/// The name of this storm.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Storms cannot be obscured by fog of war.
		/// </summary>
		public bool CanBeFogged { get { return false; } }

		/// <summary>
		/// The size of this storm.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// Index of the picture to use to represent this storm.
		/// </summary>
		public int PictureNumber { get; set; }

		/// <summary>
		/// A picture used to represent this storm on the map.
		/// </summary>
		public Image Icon
		{
			get
			{
				return Pictures.GetStellarObjectIcon(PictureNumber);
			}
		}

		/// <summary>
		/// A picture used to represent this storm in reports.
		/// </summary>
		public Image Portrait
		{
			get
			{
				return Pictures.GetStellarObjectPortrait(PictureNumber);
			}
		}

		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string CombatTile { get; set; }

		/// <summary>
		/// Just copy the storm's data.
		/// </summary>
		/// <returns>A copy of the storm.</returns>
		public Storm Instantiate()
		{
			return this.Clone();
		}
	}
}
