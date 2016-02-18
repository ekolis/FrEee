﻿using FrEee.Game.Objects.Civilization;
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
using FrEee.Game.Enumerations;
using System.Drawing;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.VictoryConditions;
using FrEee.Game.Objects.Abilities;
using System.IO;

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
			EmpireTemplates = new List<EmpireTemplate>();
			ForbiddenTechnologyNames = new List<string>();
			VictoryConditions = new List<IVictoryCondition>();
			VictoryConditions.Add(new TotalEliminationVictoryCondition());
		}

		/// <summary>
		/// The name of the game. Used in save file names.
		/// </summary>
		public string GameName { get; set; }

		/// <summary>
		/// The galaxy template to use.
		/// </summary>
		[DoNotSerialize]
		public GalaxyTemplate GalaxyTemplate
		{
			get { return Mod.Current.GalaxyTemplates.FindByName(GalaxyTemplateName); }
			set { GalaxyTemplateName = value.Name; }
		}

		public string GalaxyTemplateName
		{
			get;
			private set;
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
		/// Number of groups of connected star systems to generate.
		/// 1 = all warp points connected
		/// 2 = 2 distinct groups
		/// ...
		/// StarSystemCount = no warp points
		/// </summary>
		public int StarSystemGroups { get; set; }

		/// <summary>
		/// Strategy for placing warp points within systems.
		/// </summary>
		public WarpPointPlacementStrategy WarpPointPlacementStrategy { get; set; }

		/// <summary>
		/// Should all systems start explored for all players?
		/// </summary>
		public bool AllSystemsExplored { get; set; }

		/// <summary>
		/// Should players have an omniscient view of all explored systems?
		/// Does not prevent cloaking from working; this is just basic sight.
		/// Also does not give battle reports for other empires' battles.
		/// </summary>
		public bool OmniscientView { get; set; }

		/// <summary>
		/// Model to use for standard planetary mining.
		/// </summary>
		public MiningModel StandardMiningModel { get; set; }

		/// <summary>
		/// Model to use for remote mining.
		/// </summary>
		public MiningModel RemoteMiningModel { get; set; }

		public int MinPlanetValue { get; set; }

		public int MinSpawnedPlanetValue { get; set; }

		public int HomeworldValue { get; set; }

		public int MaxSpawnedPlanetValue { get; set; }

		public int MaxPlanetValue { get; set; }

		public int MinAsteroidValue { get; set; }

		public int MinSpawnedAsteroidValue { get; set; }

		public int MaxSpawnedAsteroidValue { get; set; }

		public int StartingResources { get; set; }

		public int ResourceStorage { get; set; }

		public int StartingResearch { get; set; }

		public int HomeworldsPerEmpire { get; set; }

		public StellarSize HomeworldSize { get; set; }

		public EmpirePlacement EmpirePlacement { get; set; }

		public int MaxHomeworldDispersion { get; set; }

		public ScoreDisplay ScoreDisplay { get; set; }

		public int EmpirePoints { get; set; }

		public int RandomAIs { get; set; }

		public int MinorEmpires { get; set; }

		/// <summary>
		/// Are we setting up a single player game?
		/// </summary>
		public bool IsSinglePlayer { get { return EmpireTemplates.Where(et => et.IsPlayerEmpire).Count() == 1; } }

		/// <summary>
		/// Empire templates in this game setup.
		/// </summary>
		public IList<EmpireTemplate> EmpireTemplates { get; private set; }

		/// <summary>
		/// The starting technology level for empires.
		/// TODO - have a separate starting tech level setting for neutrals?
		/// </summary>
		public StartingTechnologyLevel StartingTechnologyLevel { get; set; }

		/// <summary>
		/// Technology research cost formula.
		/// Low = Level * BaseCost
		/// Medium = BaseCost for level 1, Level ^ 2 * BaseCost / 2 otherwise
		/// Hight = Level ^ 2 * BaseCost
		/// </summary>
		public TechnologyCost TechnologyCost { get; set; }

		/// <summary>
		/// Technologies that are locked at level zero.
		/// </summary>
		public IList<string> ForbiddenTechnologyNames { get; private set; }

		/// <summary>
		/// Game victory conditions.
		/// </summary>
		public IList<IVictoryCondition> VictoryConditions { get; private set; }

		/// <summary>
		/// Delay in turns before victory conditions take effect.
		/// </summary>
		public int VictoryDelay { get; set; }

		/// <summary>
		/// Is this a "humans vs. AI" game?
		/// </summary>
		public bool IsHumansVsAI { get; set; }

		/// <summary>
		/// Allowed trades in this game.
		/// </summary>
		public AllowedTrades AllowedTrades { get; set; }

		public bool IsSurrenderAllowed { get; set; }

		public bool IsIntelligenceAllowed { get; set; }

		public bool IsAnalysisAllowed { get; set; }

		public bool GenerateRandomRuins { get; set; }

		public bool GenerateUniqueRuins { get; set; }

		public bool CanColonizeOnlyBreathable { get; set; }

		public bool CanColonizeOnlyHomeworldSurface { get; set; }

		/// <summary>
		/// Problems with this game setup.
		/// </summary>
		public IEnumerable<string> Warnings
		{
			get
			{
				if (string.IsNullOrWhiteSpace(GameName))
					yield return "You must specify a name for the game.";
				if (GalaxyTemplate == null) // TODO - default to first galaxy template?
					yield return "You must specify a galaxy type.";
				if (StarSystemCount > GalaxySize.Width * GalaxySize.Height)
					yield return "The galaxy is too small to contain " + StarSystemCount + " star systems.";
				if (EmpirePlacement != EmpirePlacement.CanStartInSameSystem && EmpireTemplates.Count + RandomAIs + MinorEmpires > StarSystemCount)
					yield return "There are not enough star systems to give " + (EmpireTemplates.Count + RandomAIs + MinorEmpires) + " empires and minor races each their own home system.";
				if (!EmpireTemplates.Any() && RandomAIs == 0)
					yield return "You must add at least one empire.";
				foreach (var et in EmpireTemplates)
				{
					if (et.PointsSpent > EmpirePoints)
						yield return "The " + et + " has spent too many empire points.";
				}
			}
		}

		/// <summary>
		/// Generates a random number used to pick a color from a limited palette of 63 colors.
		/// </summary>
		/// <returns></returns>
		private int RandomRGB()
		{
			return RandomHelper.Range(0, 3) * 85;
		}

		/// <summary>
		/// Picks a random color from a limited palette of 63 colors.
		/// </summary>
		/// <returns></returns>
		private Color RandomColor()
		{
			int r = 0, g = 0, b = 0;
			while (r == 0 && g == 0 && b == 0)
			{
				r = RandomRGB();
				g = RandomRGB();
				b = RandomRGB();
			}
			return Color.FromArgb(r, g, b);
		}

		// TODO - status messages for the GUI
		public void PopulateGalaxy(Galaxy gal)
		{
			gal.Name = GameName;

			gal.AssignIDs();

			// remove forbidden techs
			foreach (var tname in ForbiddenTechnologyNames)
				gal.Referrables.OfType<Technology>().Single(t => t.Name == tname).Dispose();

			// set omniscient view and all systems seen flags
			gal.OmniscientView = OmniscientView;
			gal.AllSystemsExploredFromStart = AllSystemsExplored;

			// set up mining models and resource stuff
			gal.StandardMiningModel = StandardMiningModel;
			gal.RemoteMiningModel = RemoteMiningModel;
			gal.MinPlanetValue = MinPlanetValue;
			gal.MinSpawnedPlanetValue = MinSpawnedPlanetValue;
			gal.MaxSpawnedPlanetValue = MaxSpawnedPlanetValue;
			gal.MaxPlanetValue = MaxPlanetValue;
			gal.MinAsteroidValue = MinAsteroidValue;
			gal.MinSpawnedAsteroidValue = MinSpawnedAsteroidValue;
			gal.MaxSpawnedAsteroidValue = MaxSpawnedAsteroidValue;

			// set score display setting
			gal.ScoreDisplay = ScoreDisplay;

			// set up victory conditions
			foreach (var vc in VictoryConditions)
				gal.VictoryConditions.Add(vc);
			gal.VictoryDelay = VictoryDelay;

			// set up misc. game options
			gal.TechnologyCost = TechnologyCost;
			gal.IsHumansVsAI = IsHumansVsAI;
			gal.AllowedTrades = AllowedTrades;
			gal.IsSurrenderAllowed = IsSurrenderAllowed;
			gal.IsIntelligenceAllowed = IsIntelligenceAllowed;
			gal.CanColonizeOnlyBreathable = CanColonizeOnlyBreathable;
			gal.CanColonizeOnlyHomeworldSurface = CanColonizeOnlyHomeworldSurface;
			gal.WarpPointPlacementStrategy = WarpPointPlacementStrategy;

			// create player empires
			foreach (var et in EmpireTemplates)
			{
				var emp = et.Instantiate();
				gal.Empires.Add(emp);
			}

			// TODO - make sure empires don't reuse colors unless we really have to?

			// create random AI empires
			for (int i = 1; i <= RandomAIs; i++)
			{
				// TODO - load saved EMP files for random AI empires
				var surface = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed).Select(p => p.Surface).Distinct().PickRandom();
				var atmosphere = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed && p.Surface == surface).Select(p => p.Atmosphere).Distinct().PickRandom();
				var et = new EmpireTemplate
				{
					Name = "Random Empire #" + i,
					LeaderName = "Random Leader #" + i,
					PrimaryRace = new Race
					{
						Name = "Random Race #" + i,
						NativeAtmosphere = atmosphere,
						NativeSurface = surface,
					},
					IsPlayerEmpire = false,
					Color = RandomColor(),
					Culture = Mod.Current.Cultures.PickRandom(),
					AIName = "AI_Default",
				};
				foreach (var apt in Aptitude.All)
					et.PrimaryRace.Aptitudes[apt.Name] = 100;
				var emp = et.Instantiate();
				gal.Empires.Add(emp);
			}

			// create minor empires
			for (int i = 1; i <= MinorEmpires; i++)
			{
				// TODO - load saved EMP files for minor empires
				var surface = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed).Select(p => p.Surface).Distinct().PickRandom();
				var atmosphere = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed && p.Surface == surface).Select(p => p.Atmosphere).Distinct().PickRandom();
				var et = new EmpireTemplate
				{
					Name = "Minor Empire #" + i,
					LeaderName = "Minor Leader #" + i,
					PrimaryRace = new Race
					{
						Name = "Minor Race #" + i,
						NativeAtmosphere = atmosphere,
						NativeSurface = surface,
					},
					IsPlayerEmpire = false,
					IsMinorEmpire = true,
					Color = RandomColor(),
					Culture = Mod.Current.Cultures.PickRandom(),
					AIName = "AI_Default",
				};
				foreach (var apt in Aptitude.All)
					et.PrimaryRace.Aptitudes[apt.Name] = 100;
				var emp = et.Instantiate();
				gal.Empires.Add(emp);
			}

			// place empires
			// don't do them in any particular order, so P1 and P2 don't always wind up on opposite sides of the galaxy when using equidistant placement
			foreach (var emp in gal.Empires.Shuffle())
				PlaceEmpire(gal, emp);

			// remove ruins if they're not allowed
			if (!GenerateRandomRuins)
			{
				foreach (var p in gal.FindSpaceObjects<Planet>())
				{
					foreach (var abil in p.IntrinsicAbilities.ToArray())
					{
						if (abil.Rule.Matches("Ancient Ruins"))
							p.IntrinsicAbilities.Remove(abil);
					}
				}
			}
			if (!GenerateUniqueRuins)
			{
				foreach (var p in gal.FindSpaceObjects<Planet>())
				{
					foreach (var abil in p.IntrinsicAbilities.ToArray())
					{
						if (abil.Rule.Matches("Ancient Ruins Unique"))
							p.IntrinsicAbilities.Remove(abil);
					}
				}
			}

			// also remove ruins from homeworlds, that's just silly :P
			foreach (var p in gal.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
			{
				foreach (var abil in p.IntrinsicAbilities.ToArray())
				{
					if (abil.Rule.Matches("Ancient Ruins") || abil.Rule.Matches("Ancient Ruins Unique"))
						p.IntrinsicAbilities.Remove(abil);
				}
			}

			// set up omniscient view
			if (OmniscientView)
			{
				foreach (var emp in gal.Empires)
				{
					foreach (var sys in gal.StarSystemLocations.Select(l => l.Item))
						sys.ExploredByEmpires.Add(emp);
				}
			}
		}

		// TODO - status messages for the GUI
		private void PlaceEmpire(Galaxy gal, Empire emp)
		{
			if (AllSystemsExplored)
			{
				// set all systems explored
				foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
					sys.ExploredByEmpires.Add(emp);
			}

			// give empire starting techs
			Galaxy.Current.AssignIDs(); // need to know what the techs in the game are!
			foreach (var tech in Galaxy.Current.Referrables.OfType<Technology>())
			{
				switch (StartingTechnologyLevel)
				{
					case StartingTechnologyLevel.Low:
						emp.ResearchedTechnologies[tech] = tech.StartLevel;
						break;
					case StartingTechnologyLevel.Medium:
						emp.ResearchedTechnologies[tech] = Math.Max(tech.StartLevel, tech.RaiseLevel);
						break;
					case StartingTechnologyLevel.High:
						emp.ResearchedTechnologies[tech] = tech.MaximumLevel;
						break;
				}
			}

			// give empire starting resources and storage capacity
			foreach (var r in Resource.All.Where(r => r.IsGlobal))
			{
				emp.StoredResources.Add(r, StartingResources);
				emp.IntrinsicResourceStorage.Add(r, ResourceStorage);
			}

			// give empire starting research
			emp.BonusResearch = StartingResearch;

			// TODO - moddable colony techs?
			string colonyTechName = null;
			if ((emp.PrimaryRace.NativeSurface) == "Rock")
				colonyTechName = "Rock Planet Colonization";
			else if ((emp.PrimaryRace.NativeSurface) == "Ice")
				colonyTechName = "Ice Planet Colonization";
			else if ((emp.PrimaryRace.NativeSurface) == "Gas Giant")
				colonyTechName = "Gas Giant Colonization";
			var colonyTech = Mod.Current.Technologies.SingleOrDefault(t => t.Name == colonyTechName);
			if (colonyTech != null && emp.ResearchedTechnologies[colonyTech] < 1)
				emp.ResearchedTechnologies[colonyTech] = 1;

			// find facilities to place on homeworlds
			var facils = emp.UnlockedItems.OfType<FacilityTemplate>();
			var sy = facils.WithMax(facil => facil.GetAbilityValue("Space Yard", 2).ToInt()).LastOrDefault();
			var sp = facils.LastOrDefault(facil => facil.HasAbility("Spaceport"));
			var rd = facils.LastOrDefault(facil => facil.HasAbility("Supply Generation"));
			var min = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Minerals").ToInt()).LastOrDefault();
			var org = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Organics").ToInt()).LastOrDefault();
			var rad = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Radioactives").ToInt()).LastOrDefault();
			var res = facils.WithMax(facil => facil.GetAbilityValue("Point Generation - Research").ToInt()).LastOrDefault();
			// TODO - game setup option for intel facilities on homeworlds? HomeworldStartingFacilities.txt ala se5?

			// SY rate, for colonies
			var rate = new ResourceQuantity();
			if (sy != null)
			{
				// TODO - define mappings between SY ability numbers and resource names in a mod file
				rate.Add(Resource.Minerals, sy.GetAbilityValue("Space Yard", 2, true, true, a => a.Value1 == "1").ToInt());
				rate.Add(Resource.Organics, sy.GetAbilityValue("Space Yard", 2, true, true, a => a.Value1 == "2").ToInt());
				rate.Add(Resource.Radioactives, sy.GetAbilityValue("Space Yard", 2, true, true, a => a.Value1 == "3").ToInt());
			}

			// build connectivity graph for computing warp distance
			var graph = new ConnectivityGraph<StarSystem>();
			foreach (var s in Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item))
				graph.Add(s);
			foreach (var s in Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item))
			{
				foreach (var wp in s.FindSpaceObjects<WarpPoint>())
					graph.Connect(s, wp.TargetStarSystemLocation.Item, true);
			}

			for (int i = 0; i < HomeworldsPerEmpire; i++)
			{
				// TODO - respect Empire Placement and Max Homeworld Dispersion settings
				var planets = gal.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner == null && p.MoonOf == null));
				var okSystems = gal.StarSystemLocations.Select(ssl => ssl.Item).Where(sys => sys.EmpiresCanStartIn);
				if (i > 0)
				{
					// make sure subsequent homeworlds are placed within a limited number of warps from the first homeworld
					okSystems = okSystems.Where(sys => graph.ComputeDistance(sys, emp.OwnedSpaceObjects.OfType<Planet>().First().FindStarSystem()) <= MaxHomeworldDispersion);
				}
				switch (EmpirePlacement)
				{
					case EmpirePlacement.CanStartInSameSystem:
						// no further filtering
						break;
					case EmpirePlacement.DifferentSystems:
						// filter to systems containing no other empires' homeworlds
						okSystems = okSystems.Where(sys => !sys.FindSpaceObjects<Planet>(p => p.Owner != null && p.Owner != emp).Any());
						break;
					case EmpirePlacement.Equidistant:
						// filter to systems containing no other empires' homeworlds
						okSystems = okSystems.Where(sys => !sys.FindSpaceObjects<Planet>(p => p.Owner != null && p.Owner != emp).Any());
						// filter to systems that are the maximum distance away from any other empire's homeworlds
						var otherEmpireHomeSystems = gal.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner != null && p.Owner != emp).Select(p => p.FindStarSystem()).Distinct()).ToArray();
						okSystems = okSystems.WithMax(sys => otherEmpireHomeSystems.Min(o => graph.ComputeDistance(sys, o)));
						break;
				}
				okSystems = okSystems.ToArray();
				Planet hw;
				planets = planets.Where(p => okSystems.Contains(p.FindStarSystem()));
				if (!planets.Any())
				{
					// make sure we're placing the homeworld in a system with at least one empty sector
					okSystems = okSystems.Where(sys2 => sys2.Sectors.Any(sec => !sec.SpaceObjects.Any()));

					if (!okSystems.Any())
						throw new Exception("No suitable system found to place " + emp + "'s homeworld #" + (i + 1) + ". (Try regenerating the map or increasing the number of star systems.)");

					// make brand new planet in an OK system
					var sys = okSystems.PickRandom();
					var nextNum = sys.FindSpaceObjects<Planet>(p => p.MoonOf == null).Count() + 1;
					hw = MakeHomeworld(emp, sys.Name + " " + nextNum.ToRomanNumeral());
					var okSectors = sys.Sectors.Where(sector => !sector.SpaceObjects.Any());
					okSectors.PickRandom().Place(hw);
				}
				else
					hw = planets.PickRandom();
				if (hw.Surface != emp.PrimaryRace.NativeSurface || hw.Atmosphere != emp.PrimaryRace.NativeAtmosphere || hw.StellarSize != HomeworldSize)
				{
					var replacementHomeworld = MakeHomeworld(emp, hw.Name);
					replacementHomeworld.CopyTo(hw);
				}
				hw.ResourceValue[Resource.Minerals] = hw.ResourceValue[Resource.Organics] = hw.ResourceValue[Resource.Radioactives] = HomeworldValue;
				hw.Colony = new Colony
				{
					Owner = emp,
					ConstructionQueue = new ConstructionQueue(hw),
				};
				hw.Colony.Population.Add(emp.PrimaryRace, hw.Size.MaxPopulation);
				if (sy != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
					hw.Colony.Facilities.Add(sy.Instantiate());
				if (sp != null && hw.Colony.Facilities.Count < hw.MaxFacilities && (!emp.HasAbility("No Spaceports") || sp.Abilities.Count > 1))
					// natural merchants get spaceports only if spaceports have more than one ability
					// of course, if the other abilities are *penalties*... oh well, they can scrap them!
					hw.Colony.Facilities.Add(sp.Instantiate());
				if (rd != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
					hw.Colony.Facilities.Add(rd.Instantiate());
				var lastCount = 0;
				while (hw.Colony.Facilities.Count < hw.MaxFacilities && hw.Colony.Facilities.Count > lastCount)
				{
					lastCount = hw.Colony.Facilities.Count;

					if (min != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(min.Instantiate());
					if (org != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(org.Instantiate());
					if (rad != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
						hw.Colony.Facilities.Add(rad.Instantiate());

					// no research facilities needed at max tech!
					if (StartingTechnologyLevel != StartingTechnologyLevel.High)
					{
						if (res != null && hw.Colony.Facilities.Count < hw.MaxFacilities)
							hw.Colony.Facilities.Add(res.Instantiate());
					}
				}
			}

			// mark home systems explored
			foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
			{
				if (!sys.ExploredByEmpires.Contains(emp) && sys.FindSpaceObjects<Planet>().Any(planet => planet.Owner == emp))
					sys.ExploredByEmpires.Add(emp);
			}
		}

		/// <summary>
		/// Makes a suitable homeworld for an empire.
		/// </summary>
		/// <param name="emp"></param>
		private Planet MakeHomeworld(Empire emp, string hwName)
		{
			var hw = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p =>
						p.Surface == emp.PrimaryRace.NativeSurface &&
						p.Atmosphere == emp.PrimaryRace.NativeAtmosphere &&
						p.StellarSize == HomeworldSize &&
						!p.Size.IsConstructed).PickRandom();
			if (hw == null)
				throw new Exception("No planets found in SectType.txt with surface " + emp.PrimaryRace.NativeSurface + ", atmosphere " + emp.PrimaryRace.NativeAtmosphere + ", and size " + HomeworldSize + ". Such a planet is required for creating the " + emp + " homeworld.");
			hw = hw.Instantiate();
			hw.Name = hwName;
			hw.Size = Mod.Current.StellarObjectSizes.Where(s =>
				s.StellarSize == HomeworldSize &&
				s.StellarObjectType == "Planet" &&
				!s.IsConstructed).PickRandom();
			return hw;
		}

		public static GameSetup Load(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open);
			var gsu = Serializer.Deserialize<GameSetup>(fs);
			fs.Close(); fs.Dispose();
			return gsu;
		}

		public void Save(string filename)
		{
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close(); fs.Dispose();
		}
	}
}
