using System.Drawing;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Game.Objects.Space;
using Newtonsoft.Json;

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

		/// <summary>
		/// The empire's resource income.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <returns></returns>
		[JsonIgnore]
		public Resources Income
		{
			get
			{
				// TODO - take into account maintenance costs
				return ColonizedPlanets.Select(p => p.Income).Aggregate((r1, r2) => r1 + r2);
			}
		}

		/// <summary>
		/// Finds star systems explored by the empire.
		/// </summary>
		[JsonIgnore]
		public IEnumerable<StarSystem> ExploredStarSystems
		{
			get { return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).Where(sys => sys.ExploredByEmpires.Contains(this)); }
		}

		/// <summary>
		/// Planets colonized by the empire.
		/// </summary>
		[JsonIgnore]
		public IEnumerable<Planet> ColonizedPlanets
		{
			get
			{
				return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).SelectMany(ss => ss.FindSpaceObjects<Planet>(p => p.Owner == this).Flatten());
			}
		}
	}
}
