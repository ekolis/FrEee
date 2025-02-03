using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Objects.VictoryConditions;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.Diplomacy;
using FrEee.Processes.Setup.WarpPointPlacementStrategies;
using FrEee.Modding.Abilities;

namespace FrEee.Processes.Setup;

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
    /// Allowed trades in this game.
    /// </summary>
    public AllowedTrades AllowedTrades { get; set; }

    /// <summary>
    /// Should all systems start explored for all players?
    /// </summary>
    public bool AllSystemsExplored { get; set; }

    public bool CanColonizeOnlyBreathable { get; set; }

    public bool CanColonizeOnlyHomeworldSurface { get; set; }

    public EmpirePlacement EmpirePlacement { get; set; }

    public int EmpirePoints { get; set; }

    /// <summary>
    /// Empire templates in this game setup.
    /// </summary>
    public IList<EmpireTemplate> EmpireTemplates { get; private set; }

    /// <summary>
    /// Per mille chance of a random event occurring, per turn, per player.
    /// </summary>
    public double EventFrequency { get; set; }

    /// <summary>
    /// The maximum event severity in this game.
    /// </summary>
    public EventSeverity MaximumEventSeverity { get; set; }

    /// <summary>
    /// Technologies that are locked at level zero.
    /// </summary>
    public IList<string> ForbiddenTechnologyNames { get; private set; }

    /// <summary>
    /// The size of the galaxy.
    /// </summary>
    public Size GalaxySize { get; set; }

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
    /// The name of the game. Used in save file names.
    /// </summary>
    public string GameName { get; set; }

    public bool GenerateRandomRuins { get; set; }

    public bool GenerateUniqueRuins { get; set; }

    public StellarObjectSize HomeworldSize { get; set; }

    public int HomeworldsPerEmpire { get; set; }

    public int HomeworldValue { get; set; }

    public bool IsAnalysisAllowed { get; set; }

    /// <summary>
    /// Is this a "humans vs. AI" game?
    /// </summary>
    public bool IsHumansVsAI { get; set; }

    public bool IsIntelligenceAllowed { get; set; }

    /// <summary>
    /// Are we setting up a single player game?
    /// </summary>
    public bool IsSinglePlayer { get { return EmpireTemplates.Where(et => et.IsPlayerEmpire).Count() == 1; } }

    public bool IsSurrenderAllowed { get; set; }

    public int MaxHomeworldDispersion { get; set; }

    public int MaxPlanetValue { get; set; }

    public int MaxSpawnedAsteroidValue { get; set; }

    public int MaxSpawnedPlanetValue { get; set; }

    public int MinAsteroidValue { get; set; }

    public int MinorEmpires { get; set; }

    public int MinPlanetValue { get; set; }

    public int MinSpawnedAsteroidValue { get; set; }

    public int MinSpawnedPlanetValue { get; set; }

    /// <summary>
    /// Should players have an omniscient view of all explored systems?
    /// Does not prevent cloaking from working; this is just basic sight.
    /// Also does not give battle reports for other empires' battles.
    /// </summary>
    public bool OmniscientView { get; set; }

    public int RandomAIs { get; set; }

    /// <summary>
    /// Model to use for remote mining.
    /// </summary>
    public MiningModel RemoteMiningModel { get; set; }

    /// <summary>
    /// The research points granted to empires per unspent empire point.
    /// </summary>
    public decimal ResearchPointsPerUnspentEmpirePoint { get; set; }

    public int ResourceStorage { get; set; }

    public ScoreDisplay ScoreDisplay { get; set; }

    public int Seed { get; set; }

    /// <summary>
    /// Model to use for standard planetary mining.
    /// </summary>
    public MiningModel StandardMiningModel { get; set; }

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

    public int StartingResearch { get; set; }

    public int StartingResources { get; set; }

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
    /// The technology uniqueness factor.
    /// Tech cost is increased if other players know a tech and this factor is positive, or decreased if it's negative.
    /// </summary>
    public int TechnologyUniqueness { get; set; }

    /// <summary>
    /// Game victory conditions.
    /// </summary>
    public IList<IVictoryCondition> VictoryConditions { get; private set; }

    /// <summary>
    /// Delay in turns before victory conditions take effect.
    /// </summary>
    public int VictoryDelay { get; set; }

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

    /// <summary>
    /// Strategy for placing warp points within systems.
    /// </summary>
    public WarpPointPlacementStrategy WarpPointPlacementStrategy { get; set; }

    // TODO - status messages for the GUI
    public void PopulateGame(Game game, PRNG dice)
    {
        game.Name = GameName;

        game.CleanGameState();

        // remove forbidden techs
        foreach (var tname in ForbiddenTechnologyNames.Distinct())
            Mod.Current.Technologies.Single(t => t.Name == tname).Dispose();

        // create player empires
        foreach (var et in EmpireTemplates)
        {
            var emp = et.Instantiate();
            game.Empires.Add(emp);
        }

        // TODO - make sure empires don't reuse colors unless we really have to?

        // create random AI empires
        for (int i = 1; i <= RandomAIs; i++)
        {
            // TODO - load saved EMP files for random AI empires
            var surface = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed).Select(p => p.Surface).Distinct().PickRandom(dice);
            var atmosphere = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed && p.Surface == surface).Select(p => p.Atmosphere).Distinct().PickRandom(dice);
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
                Color = RandomColor(dice),
                Culture = Mod.Current.Cultures.PickRandom(dice),
                AIName = Mod.Current.EmpireAIs.PickRandom(dice).Name,
            };
            foreach (var apt in Aptitude.All)
                et.PrimaryRace.Aptitudes[apt.Name] = 100;
            var emp = et.Instantiate();
            game.Empires.Add(emp);
        }

        // create minor empires
        for (int i = 1; i <= MinorEmpires; i++)
        {
            // TODO - load saved EMP files for minor empires
            var surface = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed).Select(p => p.Surface).Distinct().PickRandom(dice);
            var atmosphere = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => !p.Size.IsConstructed && p.Surface == surface).Select(p => p.Atmosphere).Distinct().PickRandom(dice);
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
                Color = RandomColor(dice),
                Culture = Mod.Current.Cultures.PickRandom(dice),
                AIName = Mod.Current.EmpireAIs.PickRandom(dice).Name,
            };
            foreach (var apt in Aptitude.All)
                et.PrimaryRace.Aptitudes[apt.Name] = 100;
            var emp = et.Instantiate();
            game.Empires.Add(emp);
        }

        // place empires
        // don't do them in any particular order, so P1 and P2 don't always wind up on opposite sides of the galaxy when using equidistant placement
        foreach (var emp in game.Empires.Shuffle(dice))
            PlaceEmpire(game.Galaxy, emp, dice);


        //Enabled AI ministers, so the AI's actually can do stuff. 
        foreach (var emp in game.Empires.Where(x => !x.IsPlayerEmpire && x.AI != null))
            emp.EnabledMinisters = emp.AI.MinisterNames;

        // remove ruins if they're not allowed
        if (!GenerateRandomRuins)
        {
            foreach (var p in game.Galaxy.FindSpaceObjects<Planet>())
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
            foreach (var p in game.Galaxy.FindSpaceObjects<Planet>())
            {
                foreach (var abil in p.IntrinsicAbilities.ToArray())
                {
                    if (abil.Rule.Matches("Ancient Ruins Unique"))
                        p.IntrinsicAbilities.Remove(abil);
                }
            }
        }

        // also remove ruins from homeworlds, that's just silly :P
        foreach (var p in game.Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
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
            foreach (var emp in game.Empires)
            {
                foreach (var sys in game.Galaxy.StarSystemLocations.Select(l => l.Item))
                    sys.ExploredByEmpires.Add(emp);
            }
        }
    }

    /// <summary>
    /// Makes a suitable homeworld for an empire.
    /// </summary>
    /// <param name="emp"></param>
    private Planet MakeHomeworld(Empire emp, string hwName, PRNG dice)
    {
        var hw = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p =>
                    p.Surface == emp.PrimaryRace.NativeSurface &&
                    p.Atmosphere == emp.PrimaryRace.NativeAtmosphere &&
                    p.Size == HomeworldSize)
                    .PickRandom(dice);
        if (hw == null)
            throw new Exception("No planets found in SectType.txt with surface " + emp.PrimaryRace.NativeSurface + ", atmosphere " + emp.PrimaryRace.NativeAtmosphere + ", and size " + HomeworldSize + ". Such a planet is required for creating the " + emp + " homeworld.");
        hw = hw.Instantiate();
        hw.Name = hwName;
        hw.Size = HomeworldSize;
        hw.ConditionsAmount = Mod.Current.Settings.HomeworldConditions;
        return hw;
    }

    // TODO - status messages for the GUI
    private void PlaceEmpire(Galaxy gal, Empire emp, PRNG dice)
    {
        if (AllSystemsExplored)
        {
            // set all systems explored
            foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
                sys.ExploredByEmpires.Add(emp);
        }

        // give empire starting techs
        Game.Current.CleanGameState(); // need to know what the techs in the game are!
        foreach (var tech in Mod.Current.Technologies.Where(t => !t.IsRacial || emp.Abilities().Any(a => a.Rule.Matches("Tech Area") && a.Value1 == t.RacialTechID)))
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
        emp.BonusResearch = StartingResearch + (int)(ResearchPointsPerUnspentEmpirePoint * (EmpirePoints - emp.PrimaryRace.PointsSpent));

        // TODO - moddable colony techs?
        string colonyTechName = null;
        if (emp.PrimaryRace.NativeSurface == "Rock")
            colonyTechName = "Rock Planet Colonization";
        else if (emp.PrimaryRace.NativeSurface == "Ice")
            colonyTechName = "Ice Planet Colonization";
        else if (emp.PrimaryRace.NativeSurface == "Gas Giant")
            colonyTechName = "Gas Giant Colonization";
        var colonyTech = Mod.Current.Technologies.SingleOrDefault(t => t.Name == colonyTechName);
        if (colonyTech != null && emp.ResearchedTechnologies[colonyTech] < 1)
            emp.ResearchedTechnologies[colonyTech] = 1;

        // find facilities to place on homeworlds
        var facils = emp.UnlockedItems.OfType<FacilityTemplate>();
        var sy = facils.LastOrDefault(facil => facil.HasAbility("Space Yard"));
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
            if (!okSystems.Any())
            {
                // replace an inhospitable system with a hospitable one
                var convertSys = gal.StarSystemLocations.Select(ssl => ssl.Item).Where(sys => !sys.EmpiresCanStartIn).PickRandom(dice);
                if (convertSys == null)
                    throw new Exception("No suitable system found to place " + emp + "'s homeworld #" + (i + 1) + ". (Try increasing the number of star systems.)");
                var newSys = Mod.Current.StarSystemTemplates.Where(q => q.EmpiresCanStartIn).PickRandom(dice).Instantiate();
                var sid = convertSys.ID;
                newSys.CopyTo(convertSys);
                convertSys.ID = sid;
                convertSys.Name = Mod.Current.StarSystemNames.Except(gal.StarSystemLocations.Select(q => q.Item.Name)).PickRandom(dice);
                foreach (var l in Galaxy.Current.StarSystemLocations)
                {
                    foreach (var wp in l.Item.FindSpaceObjects<WarpPoint>().Where(q => q.Target.StarSystem == convertSys).ToArray())
                    {
                        wp.Dispose();
                        WarpPointPlacementStrategy.PlaceWarpPoints(Galaxy.Current.StarSystemLocations.Single(q => q.Item == convertSys), l);
                    }
                }
                GalaxyTemplate.NameStellarObjects(convertSys);
                okSystems = new[] { convertSys };
            }
            Planet hw;
            planets = planets.Where(p => okSystems.Contains(p.FindStarSystem()));
            if (!planets.Any())
            {
                // make sure we're placing the homeworld in a system with at least one empty sector
                okSystems = okSystems.Where(sys2 => sys2.Sectors.Any(sec => !sec.SpaceObjects.Any()));

                if (!okSystems.Any())
                    throw new Exception("No suitable system found to place " + emp + "'s homeworld #" + (i + 1) + ". (Try regenerating the map or increasing the number of star systems.)");

                // make brand new planet in an OK system
                var sys = okSystems.PickRandom(dice);
                var nextNum = sys.FindSpaceObjects<Planet>(p => p.MoonOf == null).Count() + 1;
                hw = MakeHomeworld(emp, sys.Name + " " + nextNum.ToRomanNumeral(), dice);
                var okSectors = sys.Sectors.Where(sector => !sector.SpaceObjects.Any());
                okSectors.PickRandom(dice).Place(hw);
            }
            else
                hw = planets.PickRandom(dice);
            if (hw.Surface != emp.PrimaryRace.NativeSurface || hw.Atmosphere != emp.PrimaryRace.NativeAtmosphere || hw.Size != HomeworldSize)
            {
                var replacementHomeworld = MakeHomeworld(emp, hw.Name, dice);
                replacementHomeworld.CopyTo(hw);
            }
            hw.ResourceValue[Resource.Minerals] = hw.ResourceValue[Resource.Organics] = hw.ResourceValue[Resource.Radioactives] = HomeworldValue;
            hw.Colony = new Colony
            {
                Owner = emp,
                ConstructionQueue = DIRoot.ConstructionQueues.CreateConstructionQueue(hw),
                IsHomeworld = true,
            };
            hw.AddPopulation(emp.PrimaryRace, hw.Size.MaxPopulation);

            // function to create a facility if possible
            void TryCreateFacility(FacilityTemplate? template)
            {
                if (template is not null && hw.Colony.Facilities.Count < hw.MaxFacilities)
                {
                    var facility = template.Instantiate();
                    hw.Colony.Facilities.Add(facility);
                    facility.ConstructionProgress = facility.Cost;
                }
            }

            // create basic facilities, one each
            TryCreateFacility(sy);
            if (!emp.PrimaryRace.HasAbility("No Spaceports"))
            {
                TryCreateFacility(sp);
            }
            TryCreateFacility(rd);
            TryCreateFacility(rad);
            TryCreateFacility(org);

            // fill remaining space with half mineral miners and half research facilities
            var lastCount = 0;
            while (hw.Colony.Facilities.Count < hw.MaxFacilities && hw.Colony.Facilities.Count > lastCount)
            {
                lastCount = hw.Colony.Facilities.Count;

                TryCreateFacility(min);

                // no research facilities needed at max tech!
                if (StartingTechnologyLevel != StartingTechnologyLevel.High)
                {
                    TryCreateFacility(res);
                }
            }
        }

        // mark home systems explored
        foreach (var sys in gal.StarSystemLocations.Select(ssl => ssl.Item))
        {
            if (!sys.ExploredByEmpires.Contains(emp) && sys.FindSpaceObjects<Planet>().Any(planet => planet.Owner == emp))
                sys.ExploredByEmpires.Add(emp);
        }

        // in case two empires started in the same system
        foreach (var x in gal.FindSpaceObjects<ISpaceObject>().Owned().ToArray())
            x.UpdateEmpireMemories();
    }

    /// <summary>
    /// Picks a random color from a limited palette of 63 colors.
    /// </summary>
    /// <returns></returns>
    private Color RandomColor(PRNG dice)
    {
        int r = 0, g = 0, b = 0;
        while (r == 0 && g == 0 && b == 0)
        {
            r = RandomRGB(dice);
            g = RandomRGB(dice);
            b = RandomRGB(dice);
        }
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Generates a random number used to pick a color from a limited palette of 63 colors.
    /// </summary>
    /// <returns></returns>
    private int RandomRGB(PRNG dice)
    {
        return RandomHelper.Range(0, 3, dice) * 85;
    }
}
