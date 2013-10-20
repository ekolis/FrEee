using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads data from plain text.
	/// </summary>
	public class TextLoader : ILoader
	{
		public TextLoader(string modPath, string filename, Func<Mod, ICollection<string>> destinationGetter)
		{
			ModPath = modPath;
			FileName = filename;
			DestinationGetter = destinationGetter;
		}

		public void Load(Mod mod)
		{
			var dest = DestinationGetter(mod);
			string filepath;
			if (ModPath == null)
				filepath = Path.Combine("Data", FileName);
			else
				filepath = Path.Combine("Mods", ModPath, "Data", FileName);
			if (File.Exists(filepath))
			{
				foreach (var s in File.ReadAllLines(filepath))
					dest.Add(s);
			}
			else if (ModPath != null)
			{
				filepath = Path.Combine("Data", FileName);
				if (File.Exists(filepath))
				{
					foreach (var s in File.ReadAllLines(filepath))
						dest.Add(s);
				}
				else
					throw new FileNotFoundException("Could not find data file: " + FileName + ".", FileName);
			}
			else
				throw new FileNotFoundException("Could not find data file: " + FileName + ".", FileName);
		}

		/// <summary>
		/// The mod path, or null to use stock.
		/// </summary>
		public string ModPath { get; set; }

		/// <summary>
		/// The file name to read from.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// How Load knows where the text goes in the mod.
		/// </summary>
		public Func<Mod, ICollection<string>> DestinationGetter { get; set; }
	}
}
