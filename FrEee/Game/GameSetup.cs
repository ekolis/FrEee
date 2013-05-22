using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game
{
	/// <summary>
	/// Setup parameters for a game.
	/// </summary>
	 [Serializable] public class GameSetup
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

		public void PopulateGalaxy(Galaxy gal)
		{
			// find facilities to place on homeworlds
			// TODO - if facility not found, don't place it, but don't crash
			var sy = Mod.Current.Facilities.Last(facil => facil.HasAbility("Space Yard"));
			var sp = Mod.Current.Facilities.Last(facil => facil.HasAbility("Spaceport"));
			var rd = Mod.Current.Facilities.Last(facil => facil.HasAbility("Supply Generation"));
			var min = Mod.Current.Facilities.Last(facil => facil.HasAbility("Resource Generation - Minerals"));
			var org = Mod.Current.Facilities.Last(facil => facil.HasAbility("Resource Generation - Organics"));
			var rad = Mod.Current.Facilities.Last(facil => facil.HasAbility("Resource Generation - Radioactives"));
			var res = Mod.Current.Facilities.Last(facil => facil.HasAbility("Point Generation - Research"));

			// SY rate, for colonies
			var rate = new Resources();
			// TODO - define mappings between SY ability numbers and resource names in a mod file
			rate.Add("Minerals", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "1").ToInt());
			rate.Add("Organics", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "2").ToInt());
			rate.Add("Radioactives", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "3").ToInt());

			// add players and place homeworlds
			foreach (var emp in Empires)
			{
				gal.Empires.Add(emp);
				// TODO - place homeworlds fairly
				var planets = gal.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner == null).SelectMany(g => g));
				if (!planets.Any())
					throw new Exception("Not enough planets to place homeworlds for all players!");
				var hw = planets.PickRandom();
				hw.Colony = new Colony
				{
					Owner = emp,
					ConstructionQueue = new ConstructionQueue
					{
						IsSpaceYardQueue = true,
						SpaceObject = hw,
						Rate = rate,
					}
				};
				hw.Colony.Facilities.Add(sy);
				hw.Colony.Facilities.Add(sp); // TODO - don't add spaceport for Natural Merchants
				hw.Colony.Facilities.Add(rd);
				// TODO - respect planet facility space once we have PlanetSize.txt loaded
				for (int i = 0; i < 5; i++)
				{
					hw.Colony.Facilities.Add(min);
					hw.Colony.Facilities.Add(org);
					hw.Colony.Facilities.Add(rad);
					hw.Colony.Facilities.Add(res);
				}

				// mark home systems explored
				foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
				{
					if (!sys.ExploredByEmpires.Contains(emp) && sys.FindSpaceObjects<Planet>().SelectMany(g => g).Any(planet => planet == hw))
						sys.ExploredByEmpires.Add(emp);
				}
			}
		}
	}
}
