using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// Setup parameters for a game.
	/// </summary>
	public class GameSetup
	{
		public GameSetup()
		{
			Empires = new List<Empire>();
		}

		/// <summary>
		/// The size of the galaxy.
		/// </summary>
		public System.Drawing.Size GalaxySize { get; set; }

		/// <summary>
		/// How many star systems will be in the galaxy?
		/// </summary>
		public int StarSystemCount { get; set; }

		public IList<Empire> Empires { get; private set; }

		public GalaxyTemplate GalaxyTemplate { get; set; }

		public Galaxy CreateGalaxy()
		{
			// create galaxy
			GalaxyTemplate.GameSetup = this;
			var galaxy = GalaxyTemplate.Instantiate();

			// add players and place homeworlds
			foreach (var emp in Empires)
			{
				galaxy.Empires.Add(emp);
				// TODO - place homeworlds fairly
				var planets = galaxy.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner == null).SelectMany(g => g));
				if (!planets.Any())
					throw new Exception("Not enough planets to place homeworlds for all players!");
				var hw = planets.PickRandom();
				hw.Colony = new Colony { Owner = emp };

				// mark home systems explored
				foreach (var sys in galaxy.StarSystemLocations.Select(ssl => ssl.Item))
				{
					if (!emp.ExploredStarSystems.Contains(sys) && sys.FindSpaceObjects<Planet>().SelectMany(g => g).Any(planet => planet == hw))
						emp.ExploredStarSystems.Add(sys);
				}
			}

			// done
			return galaxy;
		}
	}
}
