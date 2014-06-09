using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FrEee.Game;
using System.Drawing;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Loaders;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.AI;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding
{
	/// <summary>
	/// A set of data files containing templates for game objects.
	/// </summary>
	[Serializable]
	public class Mod
	{
		/// <summary>
		/// The currently loaded mod.
		/// </summary>
		public static Mod Current { get; set; }

		/// <summary>
		/// The file name being loaded. (For error reporting)
		/// </summary>
		public static string CurrentFileName { get; private set; }

		/// <summary>
		/// Loads a mod.
		/// </summary>
		/// <param name="path">The mod root path, relative to the Mods folder. Or null to load the stock mod.</param>
		/// <param name="setCurrent">Set the current mod to the new mod?</param>
		/// <param name="status">A status object to report status back to the GUI.</param>
		/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done loading the mod? 1.0 means all done with everything that needs to be done.</param>
		public static Mod Load(string path, bool setCurrent = true, Status status = null, double desiredProgress = 1.0)
		{
			var mod = new Mod();
			mod.RootPath = path;

			if (setCurrent)
				Current = mod;

			var loaders = new ILoader[]
			{
				new ModInfoLoader(path),
				new TextLoader(path, "SystemNames.txt", m => m.StarSystemNames),
				new ScriptLoader(path),
				new AbilityRuleLoader(path),
				new ModSettingsLoader(path),
				new StellarObjectSizeLoader(path),
				new StellarObjectLoader(path),
				new TraitLoader(path),
				new TechnologyLoader(path),
				new FacilityLoader(path),
				new HullLoader(path),
				new DamageTypeLoader(path),
				new ComponentLoader(path),
				new MountLoader(path),
				new StellarAbilityLoader(path),
				new StarSystemLoader(path),
				new GalaxyLoader(path),
				new HappinessModelLoader(path),
				new CultureLoader(path),
				new EmpireAILoader(path),
			};

			var progressPerFile = (desiredProgress - (status == null ? 0 : status.Progress)) / loaders.Length;

			foreach (var loader in loaders)
			{
				if (status != null)
					status.Message = "Loading " + loader.FileName;
				CurrentFileName = loader.FileName;
				loader.Load(mod);
				if (status != null)
					status.Progress += progressPerFile;
			}
			

			CurrentFileName = null;

			return mod;
		}

		public Mod()
		{
			Errors = new List<DataParsingException>();

			Info = new ModInfo();
			Settings = new ModSettings();
			AbilityRules = new List<AbilityRule>();
			StarSystemNames = new List<string>();
			Traits = new List<Trait>();
			Technologies = new List<Technology>();
			FacilityTemplates = new List<FacilityTemplate>();
			Hulls = new List<IHull<IVehicle>>();
			DamageTypes = new List<DamageType>();
			ComponentTemplates = new List<ComponentTemplate>();
			Mounts = new List<Mount>();
			StellarObjectSizes = new List<StellarObjectSize>();
			StarSystemTemplates = new List<StarSystemTemplate>();
			StellarAbilityTemplates = new List<RandomAbilityTemplate>();
			GalaxyTemplates = new List<GalaxyTemplate>();
			StellarObjectTemplates = new List<StellarObject>();
			HappinessModels = new List<HappinessModel>();
			Cultures = new List<Culture>();
			EmpireAIs = new List<AI<Empire, Galaxy>>();
		}

		/// <summary>
		/// General info about the mod.
		/// </summary>
		public ModInfo Info { get; set; }

		/// <summary>
		/// General mod settings.
		/// </summary>
		public ModSettings Settings { get; set; }

		/// <summary>
		/// The path to the mod's root folder, relative to the Mods folder.
		/// </summary>
		public string RootPath { get; set; }

		/// <summary>
		/// Rules for grouping and stacking abilities.
		/// </summary>
		public ICollection<AbilityRule> AbilityRules { get; private set; }

		/// <summary>
		/// Names to use for star systems.
		/// </summary>
		public ICollection<string> StarSystemNames { get; private set; }

		/// <summary>
		/// Planet and asteroid field sizes.
		/// </summary>
		public ICollection<StellarObjectSize> StellarObjectSizes { get; private set; }

		/// <summary>
		/// Templates for star systems.
		/// </summary>
		public ICollection<StarSystemTemplate> StarSystemTemplates { get; private set; }

		/// <summary>
		/// The facilities in the mod.
		/// </summary>
		public ICollection<FacilityTemplate> FacilityTemplates { get; private set; }

		/// <summary>
		/// The vehicle hulls in the mod.
		/// </summary>
		public ICollection<IHull<IVehicle>> Hulls { get; private set; }

		/// <summary>
		/// The damage types in the mod.
		/// </summary>
		public ICollection<DamageType> DamageTypes { get; private set; }

		/// <summary>
		/// The components in the mod.
		/// </summary>
		public ICollection<ComponentTemplate> ComponentTemplates { get; private set; }

		/// <summary>
		/// The component mounts in the mod.
		/// </summary>
		public ICollection<Mount> Mounts { get; private set; }

		/// <summary>
		/// Templates for stellar abilities.
		/// </summary>
		public ICollection<RandomAbilityTemplate> StellarAbilityTemplates { get; private set; }

		/// <summary>
		/// Templates for galaxies.
		/// </summary>
		public ICollection<GalaxyTemplate> GalaxyTemplates { get; private set; }

		/// <summary>
		/// Templates for stellar objects.
		/// </summary>
		public ICollection<StellarObject> StellarObjectTemplates { get; private set; }

		/// <summary>
		/// The technologies in the game.
		/// </summary>
		public ICollection<Technology> Technologies { get; private set; }

		/// <summary>
		/// The happiness models in the game.
		/// </summary>
		public ICollection<HappinessModel> HappinessModels { get; private set; }

		/// <summary>
		/// The race/empire traits in the game.
		/// </summary>
		public ICollection<Trait> Traits { get; private set; }

		/// <summary>
		/// The empire cultures in the game.
		/// </summary>
		public ICollection<Culture> Cultures { get; private set; }

		/// <summary>
		/// The global Python script module which is available to all scripts in the mod.
		/// </summary>
		public Script GlobalScript { get; set; }

		/// <summary>
		/// The script which runs on game initialization, prior to the first turn.
		/// </summary>
		public Script GameInitScript { get; set; }

		/// <summary>
		/// The script which runs after each turn.
		/// </summary>
		public Script EndTurnScript { get; set; }

		/// <summary>
		/// The empire AIs in the game.
		/// </summary>
		public ICollection<AI<Empire, Galaxy>> EmpireAIs { get; private set; }

		/// <summary>
		/// Errors encountered when loading the mod.
		/// </summary>
		public static IList<DataParsingException> Errors { get; private set; }

		/// <summary>
		/// Do these ability names refer to the same ability, using aliases?
		/// </summary>
		/// <param name="n1"></param>
		/// <param name="n2"></param>
		/// <returns></returns>
		public bool DoAbilityNamesMatch(string n1, string n2)
		{
			if (n1 == n2)
				return true;
			var r1 = FindAbilityRule(n1);
			var r2 = FindAbilityRule(n2);
			return r1 != null && r2 != null && r1 == r2;
		}

		/// <summary>
		/// Finds an ability rule by name or alias.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public AbilityRule FindAbilityRule(string name)
		{
			return AbilityRules.SingleOrDefault(r => r.Matches(name));
		}

		/// <summary>
		/// Patches a mod with data from a new mod.
		/// </summary>
		/// <param name="newMod">The new mod.</param>
		public void Patch(Mod newMod)
		{
			newMod.Info.CopyTo(Info);
			newMod.GlobalScript.CopyTo(GlobalScript);
			StarSystemNames.Clear();
			foreach (var n in newMod.StarSystemNames)
				StarSystemNames.Add(n);
			AbilityRules.Patch(newMod.AbilityRules);
			newMod.Settings.CopyTo(Settings);
			StellarObjectSizes.Patch(newMod.StellarObjectSizes);
			StellarObjectTemplates.Patch(newMod.StellarObjectTemplates);
			Traits.Patch(newMod.Traits);
			Technologies.Patch(newMod.Technologies);
			FacilityTemplates.Patch(newMod.FacilityTemplates);
			Hulls.Patch(newMod.Hulls);
			ComponentTemplates.Patch(newMod.ComponentTemplates);
			Mounts.Patch(newMod.Mounts);
			StellarAbilityTemplates.Patch(newMod.StellarAbilityTemplates);
			StarSystemTemplates.Patch(newMod.StarSystemTemplates);
			GalaxyTemplates.Patch(newMod.GalaxyTemplates);
			HappinessModels.Patch(newMod.HappinessModels);
			Cultures.Patch(newMod.Cultures);
			newMod.GameInitScript.CopyTo(GameInitScript);
			newMod.EndTurnScript.CopyTo(EndTurnScript);
			EmpireAIs.Patch(newMod.EmpireAIs);
		}
	}
}
