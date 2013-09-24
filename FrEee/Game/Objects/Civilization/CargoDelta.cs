using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A change in cargo.
	/// </summary>
	public class CargoDelta : IPromotable
	{
		public CargoDelta()
		{
			RacePopulation = new ReferenceKeyedDictionary<Race, long?>();
			AnyPopulation = 0L;
			Units = new ReferenceSet<Unit>();
			UnitDesignTonnage = new ReferenceKeyedDictionary<IDesign<Unit>, int?>();
			UnitRoleTonnage = new SafeDictionary<string, int?>();
			UnitTypeTonnage = new SafeDictionary<VehicleTypes, int?>();
		}

		public ReferenceKeyedDictionary<Race, long?> RacePopulation { get; private set; }
		public long? AnyPopulation { get; set; }
		public ReferenceSet<Unit> Units { get; private set; }
		public ReferenceKeyedDictionary<IDesign<Unit>, int?> UnitDesignTonnage { get; private set; }
		public SafeDictionary<string, int?> UnitRoleTonnage { get; private set; }
		public SafeDictionary<VehicleTypes, int?> UnitTypeTonnage { get; private set; }

		public override string ToString()
		{
			// TODO - describe cargo
			return "Cargo";
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			RacePopulation.ReplaceClientIDs(idmap);
			UnitDesignTonnage.ReplaceClientIDs(idmap);
		}
	}
}
