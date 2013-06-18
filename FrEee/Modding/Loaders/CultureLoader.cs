using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads empire cultures from Cultures.txt.
	/// </summary>
	public class CultureLoader : DataFileLoader
	{
		public const string Filename = "Cultures.txt";

		public CultureLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var c = new Culture();
				mod.Cultures.Add(c);

				int index = -1;

				c.Name = rec.GetString("Name", ref index);
				c.Description = rec.GetNullString("Description", ref index);
				c.Production = rec.GetNullInt("Production", ref index) ?? 0;
				c.Research = rec.GetNullInt("Research", ref index) ?? 0;
				c.Intelligence = rec.GetNullInt("Intelligence", ref index) ?? 0;
				c.Trade = rec.GetNullInt("Trade", ref index) ?? 0;
				c.SpaceCombat = rec.GetNullInt("Space Combat", ref index) ?? 0;
				c.GroundCombat = rec.GetNullInt("Ground Combat", ref index) ?? 0;
				c.Happiness = rec.GetNullInt("Happiness", ref index) ?? 0;
				c.MaintenanceReduction = rec.GetNullInt("Maintenance", ref index) ?? 0;
				c.Construction = rec.GetNullInt("SY Rate", ref index) ?? 0;
				c.Repair = rec.GetNullInt("Repair", ref index) ?? 0;
			}
		}
	}
}
