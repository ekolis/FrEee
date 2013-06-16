using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads data from a data file.
	/// </summary>
	public abstract class DataFileLoader : ILoader
	{
		public DataFileLoader(DataFile df)
		{
			DataFile = df;
		}

		public abstract void Load(Mod mod);

		public DataFile DataFile { get; set; }
	}
}
