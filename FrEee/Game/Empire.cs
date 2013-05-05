using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	public class Empire : INamed
	{
		public Empire()
		{
			ExploredStarSystems = new HashSet<StarSystem>();
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The title of the emperor.
		/// </summary>
		public string EmperorTitle { get; set; }

		/// <summary>
		/// The name of the emperor.
		/// </summary>
		public string EmperorName { get; set; }

		/// <summary>
		/// The folder (under Pictures/Races) where the empire's shipset is located.
		/// </summary>
		public string ShipsetFolder { get; set; }

		/// <summary>
		/// The empire's flag.
		/// </summary>
		public Image Flag
		{
			get
			{
				// TODO - implement empire flag
				return null;
			}
		}

		/// <summary>
		/// The color used to represent this empire's star systems on the galaxy map.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Star systems which have been explored by this empire.
		/// </summary>
		public ICollection<StarSystem> ExploredStarSystems { get; private set; }
	}
}
