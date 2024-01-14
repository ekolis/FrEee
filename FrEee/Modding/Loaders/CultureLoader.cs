using FrEee.Objects.Civilization;
using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads empire cultures from Cultures.txt.
/// </summary>
public class CultureLoader : DataFileLoader
{
	public CultureLoader(string modPath)
		: base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "Cultures.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var c = new Culture();
			c.TemplateParameters = rec.Parameters;
			mod.Cultures.Add(c);

			c.ModID = rec.Get<string>("ID", c);
			c.Name = rec.Get<string>("Name", c);
			c.Description = rec.Get<string>("Description", c);
			c.Production = rec.Get<int>("Production", c) ?? 0;
			c.Research = rec.Get<int>("Research", c) ?? 0;
			c.Intelligence = rec.Get<int>("Intelligence", c) ?? 0;
			c.Trade = rec.Get<int>("Trade", c) ?? 0;
			c.SpaceCombat = rec.Get<int>("Space Combat", c) ?? 0;
			c.GroundCombat = rec.Get<int>("Ground Combat", c) ?? 0;
			c.Happiness = rec.Get<int>("Happiness", c) ?? 0;
			c.MaintenanceReduction = rec.Get<int>("Maintenance", c) ?? 0;
			c.Construction = rec.Get<int>("SY Rate", c) ?? 0;
			c.Repair = rec.Get<int>("Repair", c) ?? 0;

			yield return c;
		}
	}
}