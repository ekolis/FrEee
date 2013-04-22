using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace FrEee.Modding
{
	/// <summary>
	/// A set of data files containing templates for game objects.
	/// </summary>
	public class Mod
	{
		/// <summary>
		/// The currently loaded mod.
		/// </summary>
		public static Mod Current { get; private set; }

		/// <summary>
		/// Loads a mod.
		/// </summary>
		/// <param name="path">The mod root path, relative to the Mods folder.</param>
		/// <param name="setCurrent">Set the current mod to the new mod?</param>
		public static Mod Load(string path, bool setCurrent = true)
		{
			var mod = new Mod();

			var settings = new JsonSerializerSettings();
			settings.MissingMemberHandling = MissingMemberHandling.Error;
			mod.StarSystemTemplates = JsonConvert.DeserializeObject<IDictionary<string, StarSystemTemplate>>(File.ReadAllText(Path.Combine("Mods", path, "StarSystems.json")), settings);

			if (setCurrent)
				Current = mod;
			return mod;
		}

		public Mod()
		{
			StarSystemTemplates = new Dictionary<string, StarSystemTemplate>();
		}

		/// <summary>
		/// Templates for star systems.
		/// </summary>
		public IDictionary<string, StarSystemTemplate> StarSystemTemplates { get; private set; }
	}
}
