using FrEee.Game.Enumerations;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads stellar object sizes from PlanetSize.txt.
	/// </summary>
	public class StellarObjectSizeLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
			{
				var sos = new StellarObjectSize();
				mod.StellarObjectSizes.Add(sos);

				int index = -1;

				sos.Name = rec.GetString("Name", ref index, true, 0, true);
				sos.StellarObjectType = rec.GetString("Physical Type", ref index, true, 0, true);
				sos.StellarSize = rec.GetEnum<StellarSize>("Stellar Size", ref index, true, 0, true);
				sos.MaxFacilities = rec.GetInt("Max Facilities", ref index, true, 0, true);
				sos.MaxPopulation = rec.GetInt("Max Population", ref index, true, 0, true);
				sos.MaxCargo = rec.GetInt("Max Cargo Spaces", ref index, true, 0, true);
				sos.MaxFacilitiesDomed = rec.GetInt("Max Facilities Domed", ref index, true, 0, true);
				sos.MaxPopulationDomed = rec.GetInt("Max Population Domed", ref index, true, 0, true);
				sos.MaxCargoDomed = rec.GetInt("Max Cargo Spaces Domed", ref index, true, 0, true);
				sos.IsConstructed = rec.GetBool("Constructed", ref index, true, 0, true);
				sos.ConstructionAbilityID = rec.GetString("Special Ability ID", ref index, true, 0, true);
			}
		}
	}
}