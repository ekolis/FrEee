using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FrEee.Game;
using System.Drawing;
using FrEee.Modding.Loaders;

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
		/// The file name being loaded. (For error reporting)
		/// </summary>
		public static string CurrentFileName { get; private set; }

		/// <summary>
		/// Loads a mod.
		/// </summary>
		/// <param name="path">The mod root path, relative to the Mods folder.</param>
		/// <param name="setCurrent">Set the current mod to the new mod?</param>
		public static Mod Load(string path, bool setCurrent = true)
		{
			var mod = new Mod();

			CurrentFileName = Path.Combine("Mods", path, "Data", "SystemTypes.txt");
			new StarSystemLoader().Load(new DataFile(File.ReadAllText(CurrentFileName)), mod);

			CurrentFileName = null;

			if (setCurrent)
				Current = mod;

			// TODO - display errors to user
			foreach (var err in mod.Errors)
				Console.WriteLine(err.Message);

			return mod;
		}

		public Mod()
		{
			Errors = new List<DataParsingException>();
			StarSystemTemplates = new Dictionary<string, StarSystemTemplate>();
		}

		/// <summary>
		/// Templates for star systems.
		/// </summary>
		public IDictionary<string, StarSystemTemplate> StarSystemTemplates { get; private set; }

		/// <summary>
		/// Errors encountered when loading the mod.
		/// </summary>
		public IList<DataParsingException> Errors { get; private set; }
	}
}
