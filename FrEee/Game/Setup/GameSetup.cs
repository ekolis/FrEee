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
using FrEee.Game.Objects.Technology;
using FrEee.Game.Setup.WarpPointPlacementStrategies;

namespace FrEee.Game.Setup
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
		/// The name of the game. Used in save file names.
		/// </summary>
		public string GameName { get; set; }

		/// <summary>
		/// The size of the galaxy.
		/// </summary>
		public System.Drawing.Size GalaxySize { get; set; }

		/// <summary>
		/// How many star systems will be in the galaxy?
		/// </summary>
		public int StarSystemCount { get; set; }

		/// <summary>
		/// Strategy for placing warp points within systems.
		/// </summary>
		public WarpPointPlacementStrategy WarpPointPlacementStrategy { get; set; }

		/// <summary>
		/// Are we setting up a single player game?
		/// </summary>
		public bool IsSinglePlayer { get; set; }

		public IList<Empire> Empires { get; private set; }

		public GalaxyTemplate GalaxyTemplate { get; set; }

		/// <summary>
		/// Problems with this game setup.
		/// </summary>
		public IEnumerable<string> Warnings
		{
			get
			{
				if (string.IsNullOrWhiteSpace(GameName))
					yield return "You must specify a name for the game.";
				if (GalaxyTemplate == null)
					yield return "You must specify a galaxy type.";
				if (StarSystemCount > GalaxySize.Width * GalaxySize.Height)
					yield return "The galaxy is too small to contain " + StarSystemCount + " star systems.";
				if (WarpPointPlacementStrategy == null)
					yield return "You must choose an option for warp point placement.";
				if (!Empires.Any())
					yield return "You must add at least one empire.";
			}
		}

		public void PopulateGalaxy(Galaxy gal)
		{
			gal.Name = GameName;

			// add players and place homeworlds
			foreach (var emp in Empires)
			{
				gal.Empires.Add(emp);
				gal.Register(emp);

				// TODO - let game host and/or players configure starting techs
				foreach (var tech in Galaxy.Current.Referrables.OfType<Technology>())
					emp.ResearchedTechnologies[tech] = tech.StartLevel;

				// find facilities to place on homeworlds
				// TODO - don't crash if facilities not found in mod
				var facils = emp.UnlockedItems.OfType<FacilityTemplate>();
				var sy = facils.WithMax(facil => facil.GetAbilityValue("Space Yard", 2).ToInt()).Last();
				var sp = facils.Last(facil => facil.HasAbility("Spaceport"));
				var rd = facils.Last(facil => facil.HasAbility("Supply Generation"));
				var min = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Minerals").ToInt()).Last();
				var org = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Organics").ToInt()).Last();
				var rad = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Radioactives").ToInt()).Last();
				var res = facils.WithMax(facil => facil.GetAbilityValue("Point Generation - Research").ToInt()).Last();

				// SY rate, for colonies
				var rate = new Resources();
				// TODO - define mappings between SY ability numbers and resource names in a mod file
				rate.Add("Minerals", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "1").ToInt());
				rate.Add("Organics", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "2").ToInt());
				rate.Add("Radioactives", sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "3").ToInt());

				// TODO - place homeworlds fairly
				var planets = gal.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner == null).SelectMany(g => g));
				if (!planets.Any())
					throw new Exception("Not enough planets to place homeworlds for all players!");
				var hw = planets.PickRandom();
				hw.Colony = new Colony
				{
					Owner = emp,
					ConstructionQueue = new ConstructionQueue(hw)
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
