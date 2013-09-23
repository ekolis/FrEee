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
	public class CargoDelta
	{
		public CargoDelta()
		{
			RacePopulation = new ReferenceKeyedDictionary<Race, long?>();
			Units = new HashSet<Unit>();
			UnitDesignTonnage = new ReferenceKeyedDictionary<IDesign<Unit>, int?>();
			UnitRoleTonnage = new SafeDictionary<string, int?>();
			UnitTypeTonnage = new SafeDictionary<VehicleTypes, int?>();
		}

		public ReferenceKeyedDictionary<Race, long?> RacePopulation { get; private set; }
		public long? AnyPopulation { get; set; }
		public ISet<Unit> Units { get; private set; }
		public ReferenceKeyedDictionary<IDesign<Unit>, int?> UnitDesignTonnage { get; private set; }
		public SafeDictionary<string, int?> UnitRoleTonnage { get; private set; }
		public SafeDictionary<VehicleTypes, int?> UnitTypeTonnage { get; private set; }
	}
}
