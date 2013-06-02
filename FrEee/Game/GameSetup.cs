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
	[Serializable]
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

		/// <summary>
		/// Are we setting up a single player game?
		/// </summary>
		public bool IsSinglePlayer { get; set; }

		public IList<Empire> Empires { get; private set; }

		public GalaxyTemplate GalaxyTemplate { get; set; }

		public void PopulateGalaxy(Galaxy gal)
		{
			// set single player flag
			gal.IsSinglePlayer = IsSinglePlayer;

			// find facilities to place on homeworlds
			// TODO - if facility not found, don't place it, but don't crash
			var sy = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Space Yard"));
			var sp = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Spaceport"));
			var rd = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Supply Generation"));
			var min = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Resource Generation - Minerals"));
			var org = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Resource Generation - Organics"));
			var rad = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Resource Generation - Radioactives"));
			var res = Mod.Current.FacilityTemplates.Last(facil => facil.HasAbility("Point Generation - Research"));

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
				gal.Register(emp);
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
				if (hw.Colony.Facilities.Count < hw.MaxFacilities)
					hw.Colony.Facilities.Add(sy.Instantiate());
				if (hw.Colony.Facilities.Count < hw.MaxFacilities)
					hw.Colony.Facilities.Add(sp.Instantiate()); // TODO - don't add spaceport for Natural Merchants
				if (hw.Colony.Facilities.Count < hw.MaxFacilities)
					hw.Colony.Facilities.Add(rd.Instantiate());
				while (hw.Colony.Facilities.Count < hw.MaxFacilities)
				{
					if (hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(min.Instantiate());
					if (hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(org.Instantiate());
					if (hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(rad.Instantiate());
					if (hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(res.Instantiate());
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
