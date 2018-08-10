using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Enumerations;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads design roles from DefaultDesignTypes.txt
	/// </summary>
	[Serializable]
	public class DesignRoleLoader : ILoader
	{
		public const string Filename = "DefaultDesignTypes.txt";

		public DesignRoleLoader(string modPath)
		{
			ModPath = modPath;
			FileName = Filename;
			DataFile = DataFile.Load(modPath, Filename);
		}

		public IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				mod.DesignRoles.Add(rec.Get<string>("Name"));
			}
			yield break; // no actual mod objects to load
		}

		public string ModPath { get; set; }

		public string FileName { get; set; }

		public DataFile DataFile { get; set; }
	}
}
