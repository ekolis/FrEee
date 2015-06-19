using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads data from a data file.
	/// </summary>
	public abstract class DataFileLoader : ILoader
	{
		public DataFileLoader(string modPath, string filename, DataFile df)
		{
			ModPath = modPath;
			FileName = filename;
			DataFile = df;
		}

		public abstract IEnumerable<IModObject> Load(Mod mod);

		public string ModPath { get; set; }

		public string FileName { get; set; }

		public DataFile DataFile { get; set; }
	}
}
