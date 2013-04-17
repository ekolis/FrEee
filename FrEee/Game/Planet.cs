using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A planet. Planets can be colonized or mined.
	/// </summary>
	public class Planet : ISpaceObject
	{
		/// <summary>
		/// The name of this planet.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Planets cannot be obscured by fog of war.
		/// </summary>
		public bool CanBeFogged { get { return false; } }

		/// <summary>
		/// The size of this planet.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this planet.
		/// </summary>
		public string Surface { get; set; }

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this planet.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// Description of this planet. (For flavor)
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Index of the picture to use to represent this planet.
		/// </summary>
		public int PictureNumber { get; set; }

		/// <summary>
		/// A picture used to represent this planet on the map.
		/// </summary>
		public Image Icon
		{
			get
			{
				return Pictures.GetStellarObjectIcon(PictureNumber);
			}
		}

		/// <summary>
		/// A picture used to represent this planet in reports.
		/// </summary>
		public Image Portrait
		{
			get
			{
				return Pictures.GetStellarObjectPortrait(PictureNumber);
			}
		}
	}
}
