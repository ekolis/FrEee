using FrEee.Enumerations;
using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads stellar object sizes from PlanetSize.txt.
/// </summary>
public class StellarObjectSizeLoader : DataFileLoader
{
	public StellarObjectSizeLoader(string modPath)
		 : base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "PlanetSize.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var sos = new StellarObjectSize();
			sos.TemplateParameters = rec.Parameters;
			mod.StellarObjectSizes.Add(sos);

			sos.ModID = rec.Get<string>("ID", sos);
			sos.Name = rec.Get<string>("Name", sos);
			sos.StellarObjectType = rec.Get<string>("Physical Type", sos);
			sos.StellarSize = rec.Get<StellarSize>("Stellar Size", sos);
			sos.MaxFacilities = rec.Get<int>("Max Facilities", sos);
			sos.MaxPopulation = rec.Get<long>("Max Population", sos).Value * mod.Settings.PopulationFactor;
			sos.MaxCargo = rec.Get<int>("Max Cargo Spaces", sos);
			sos.MaxFacilitiesDomed = rec.Get<int>("Max Facilities Domed", sos);
			sos.MaxPopulationDomed = rec.Get<long>("Max Population Domed", sos).Value * mod.Settings.PopulationFactor;
			sos.MaxCargoDomed = rec.Get<int>("Max Cargo Spaces Domed", sos);
			sos.IsConstructed = rec.Get<bool>("Constructed", sos);
			sos.ConstructionAbilityID = rec.Get<string>("Special Ability ID", sos);

			yield return sos;
		}
	}
}