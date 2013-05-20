using System.Drawing;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	public class Empire : INamed
	{
		public Empire()
		{
			StoredResources = new Resources();

			// TODO - make starting resources moddable
			StoredResources.Add("Minerals", 50000);
			StoredResources.Add("Organics", 50000);
			StoredResources.Add("Radioactives", 50000);

			Commands = new List<ICommand>();
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

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// The resources stored by the empire.
		/// </summary>
		public Resources StoredResources { get; set; }

		/// <summary>
		/// Commands issued by the player this turn.
		/// </summary>
		public IList<ICommand> Commands { get; private set; }
	}
}
