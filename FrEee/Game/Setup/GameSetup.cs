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
		public GalaxyTemplate GalaxyTemplate { get; set; }

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

		public int MinPlanetValue {get; set;}

		public int MinSpawnedPlanetValue {get; set;}

		public int HomeworldValue {get; set;}

		public int MaxSpawnedPlanetValue {get; set;}

		public int MaxPlanetValue {get; set;}

		public int MinAsteroidValue {get; set;}

		public int MinSpawnedAsteroidValue {get; set;}

		public int MaxSpawnedAsteroidValue {get; set;}

		public int StartingResources { get; set; }

		public int ResourceStorage { get; set; }

		public int StartingResearch { get; set; }

		public int HomeworldsPerEmpire { get; set; }

		public StellarObjectSize HomeworldSize { get; set; }

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
				if (GalaxyTemplate == null)
					yield return "You must specify a galaxy type.";
				if (StarSystemCount > GalaxySize.Width * GalaxySize.Height)
					yield return "The galaxy is too small to contain " + StarSystemCount + " star systems.";
				if (EmpirePlacement != EmpirePlacement.CanStartInSameSystem && EmpireTemplates.Count + RandomAIs + MinorEmpires > StarSystemCount)
					yield return "There are not enough star systems to give " + (EmpireTemplates.Count + RandomAIs + MinorEmpires > StarSystemCount) + " empires and minor races each their own home system.";
				if (HomeworldSize == null)
					yield return "You must specify a homeworld size.";
				if (!EmpireTemplates.Any() && RandomAIs == 0)
					yield return "You must add at least one empire.";
				foreach (var et in EmpireTemplates)
				{
					if (et.PointsSpent > EmpirePoints)
						yield return "The " + et + " has spent too many empire points.";
				}
			}
		}

		public void PopulateGalaxy(Galaxy gal)
		{
			gal.Name = GameName;

			// remove forbidden techs
			foreach (var tname in ForbiddenTechnologyNames)
				gal.Unregister(gal.Referrables.OfType<Technology>().Single(t => t.Name == tname));

			// set omniscient view flag
			gal.OmniscientView = OmniscientView;

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

			// place player empires
			foreach (var et in EmpireTemplates)
				PlaceEmpire(gal, et);

			// place random AI empires
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
						Color = Color.FromArgb(RandomHelper.Next(256), RandomHelper.Next(256), RandomHelper.Next(256)),
						NativeAtmosphere = atmosphere,
						NativeSurface = surface,
					},
					IsPlayerEmpire = false,
				};
				PlaceEmpire(gal, et);
			}

			// place minor empires
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
						Color = Color.FromArgb(RandomHelper.Next(256), RandomHelper.Next(256), RandomHelper.Next(256)),
						NativeAtmosphere = atmosphere,
						NativeSurface = surface,
					},
					IsPlayerEmpire = false,
					IsMinorEmpire = true,
				};
				PlaceEmpire(gal, et);
			}

			// remove ruins if they're not allowed
			if (!GenerateRandomRuins)
			{
				foreach (var p in gal.FindSpaceObjects<Planet>().Flatten().Flatten())
				{
					foreach (var abil in p.IntrinsicAbilities.ToArray())
					{
						if (abil.Name == "Ancient Ruins")
							p.IntrinsicAbilities.Remove(abil);
					}
				}
			}
			if (!GenerateUniqueRuins)
			{
				foreach (var p in gal.FindSpaceObjects<Planet>().Flatten().Flatten())
				{
					foreach (var abil in p.IntrinsicAbilities.ToArray())
					{
						if (abil.Name == "Ancient Ruins Unique")
							p.IntrinsicAbilities.Remove(abil);
					}
				}
			}
		}

		private void PlaceEmpire(Galaxy gal, EmpireTemplate et)
		{
			var emp = et.Instantiate();
			gal.Empires.Add(emp);
			gal.Register(emp);

			if (AllSystemsExplored)
			{
				// set all systems explored
				foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
					sys.ExploredByEmpires.Add(emp);
			}

			// give empire starting techs
			foreach (var tech in emp.Referrables.OfType<Technology>())
			{
				switch (StartingTechnologyLevel)
				{
					case StartingTechnologyLevel.Low:
						emp.ResearchedTechnologies[tech] = tech.StartLevel;
						break;
					case StartingTechnologyLevel.Medium:
						emp.ResearchedTechnologies[tech] = tech.RaiseLevel;
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
			// TODO - don't crash if facilities not found in mod
			var facils = emp.UnlockedItems.OfType<FacilityTemplate>();
			var sy = facils.WithMax(facil => facil.GetAbilityValue("Space Yard", 2).ToInt()).Last();
			var sp = facils.Last(facil => facil.HasAbility("Spaceport"));
			var rd = facils.Last(facil => facil.HasAbility("Supply Generation"));
			var min = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Minerals").ToInt()).Last();
			var org = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Organics").ToInt()).Last();
			var rad = facils.WithMax(facil => facil.GetAbilityValue("Resource Generation - Radioactives").ToInt()).Last();
			var res = facils.WithMax(facil => facil.GetAbilityValue("Point Generation - Research").ToInt()).Last();
			// TODO - game setup option for intel facilities on homeworlds? HomeworldStartingFacilities.txt ala se5?

			// SY rate, for colonies
			var rate = new Resources();
			// TODO - define mappings between SY ability numbers and resource names in a mod file
			rate.Add(Resource.Minerals, sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "1").ToInt());
			rate.Add(Resource.Organics, sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "2").ToInt());
			rate.Add(Resource.Radioactives, sy.GetAbilityValue("Space Yard", 2, a => a.Value1 == "3").ToInt());

			for (int i = 0; i < HomeworldsPerEmpire; i++)
			{
				// TODO - respect Empire Placement and Max Homeworld Dispersion settings
				var planets = gal.StarSystemLocations.SelectMany(ssl => ssl.Item.FindSpaceObjects<Planet>(p => p.Owner == null).SelectMany(g => g));
				if (!planets.Any())
					throw new Exception("Not enough planets to place homeworlds for all players!");
				var hw = planets.PickRandom();
				if (hw.Surface != emp.PrimaryRace.NativeSurface || hw.Atmosphere != emp.PrimaryRace.NativeAtmosphere || hw.Size != HomeworldSize)
				{
					var replacementHomeworld = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p =>
						p.Surface == emp.PrimaryRace.NativeSurface &&
						p.Atmosphere == emp.PrimaryRace.NativeAtmosphere &&
						p.Size == HomeworldSize).PickRandom();
					if (replacementHomeworld == null)
						throw new Exception("No planets found in SectType.txt with surface " + emp.PrimaryRace.NativeSurface + ", atmosphere " + emp.PrimaryRace.NativeAtmosphere + ", and size " + HomeworldSize + ". Such a planet is required for creating the " + emp + " homeworld.");
					replacementHomeworld.Name = hw.Name;
					replacementHomeworld.CopyTo(hw);
				}
				hw.ResourceValue[Resource.Minerals] = hw.ResourceValue[Resource.Organics] = hw.ResourceValue[Resource.Radioactives] = HomeworldValue;
				hw.Colony = new Colony
				{
					Owner = emp,
					ConstructionQueue = new ConstructionQueue(hw),
				};
				hw.Colony.Population.Add(emp.PrimaryRace, hw.Size.MaxPopulation);
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

					// no research facilities needed at max tech!
					if (StartingTechnologyLevel != StartingTechnologyLevel.High)
					{
						if (hw.Colony.Facilities.Count < hw.MaxFacilities)
							hw.Colony.Facilities.Add(res.Instantiate());
					}
				}
			}

			// mark home systems explored
			foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
			{
				if (!sys.ExploredByEmpires.Contains(emp) && sys.FindSpaceObjects<Planet>().SelectMany(g => g).Any(planet => planet.Owner == emp))
					sys.ExploredByEmpires.Add(emp);
			}
		}

		public static GameSetup Load(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open);
			var gsu = Serializer.Deserialize<GameSetup>(fs);
			fs.Close();
			return gsu;
		}

		public void Save(string filename)
		{
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close();
		}
	}
}
