using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads mod files.
/// </summary>
public class ModLoader
{
	/// <summary>
	/// Loads a mod.
	/// </summary>
	/// <param name="path">The mod root path, relative to the Mods folder. Or null to load the stock mod.</param>
	/// <param name="setCurrent">Set the current mod to the new mod?</param>
	/// <param name="status">A status object to report status back to the GUI.</param>
	/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done loading the mod? 1.0 means all done with everything that needs to be done.</param>
	public Mod Load(string? path, bool setCurrent = true, Status? status = null, double desiredProgress = 1.0)
	{
		var mod = new Mod();
		mod.RootPath = path;
		if (setCurrent)
			Mod.Current = mod;

		if (path != null && !Directory.Exists(Path.Combine("Mods", path)))
			throw new DirectoryNotFoundException($"Could not find mod {path} in the Mods folder.");

		var loaders = new Dictionary<ILoader, int>
		{
			{ new ModInfoLoader(path), 0 },
			{ new TextLoader(path, "SystemNames.txt", m => m.StarSystemNames), 0 },
			{ new DesignRoleLoader(path), 0 },
			{ new ScriptLoader(path), 0 },
			{ new AbilityRuleLoader(path), 0 },
			{ new ModSettingsLoader(path), 0 },
			{ new StellarObjectSizeLoader(path), 1 },
			{ new StellarAbilityLoader(path), 2 },
			{ new StellarObjectLoader(path), 3 },
			{ new TraitLoader(path), 1 },
			{ new TechnologyLoader(path), 2 },
			{ new FacilityLoader(path), 3 },
			{ new HullLoader(path), 3 },
			{ new DamageTypeLoader(path), 0 },
			{ new ComponentLoader(path), 3 },
			{ new MountLoader(path), 3 },
			{ new StarSystemLoader(path), 3 },
			{ new GalaxyLoader(path), 4 },
			{ new HappinessModelLoader(path), 0 },
			{ new CultureLoader(path), 0 },
			{ new EmpireAILoader(path) , 0 },
			{ new EventTypeLoader(path), 0 },
			{ new EventLoader(path), 1 },
		};
		var progressPerFile = (desiredProgress - (status == null ? 0 : status.Progress)) / loaders.Count;
		var used = new HashSet<string>();

		var minPriority = loaders.Values.Min();
		var maxPriority = loaders.Values.Max();

		for (var p = minPriority; p <= maxPriority; p++)
		{
			var files = loaders.Where(q => q.Value == p).Select(q => q.Key.FileName);
			Mod.CurrentFileName = string.Join(" / ", files);
			if (status != null)
				status.Message = "Loading " + Mod.CurrentFileName;

			loaders.Where(q => q.Value == p).ParallelSafeForeach(loader =>
			{
				foreach (var mo in loader.Key.Load(mod).ToArray())
				{
					mod.AssignID(mo, used);
				}
				if (status != null)
					status.Progress += progressPerFile;
			});
		}

		Mod.CurrentFileName = null;

		var dupes = mod.Objects.GroupBy(o => o.ModID).Where(g => g.Count() > 1);
		if (dupes.Any())
			throw new Exception("Multiple objects with mod ID {0} found ({1} total IDs with duplicates)".F(dupes.First().Key, dupes.Count()));

		return mod;
	}
}
