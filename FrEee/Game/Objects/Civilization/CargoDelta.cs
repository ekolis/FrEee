using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;
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
			var items = new List<string>();
			foreach (var kvp in RacePopulation)
			{
				if (kvp.Value == null)
					items.Add("All " + kvp.Key + " Population");
				else
					items.Add(kvp.Value.ToUnitString() + " " + kvp.Key + " Population");
			}
			if (AnyPopulation == null)
				items.Add("All Population");
			else if (AnyPopulation != 0)
				items.Add(AnyPopulation.ToUnitString() + " Population of Any Race");
			foreach (var unit in Units)
				items.Add(unit.ToString());
			foreach (var kvp in UnitDesignTonnage)
			{
				if (kvp.Value == null)
					items.Add("All \"" + kvp.Key + "\" " + kvp.Key.VehicleTypeName + "s");
				else
					items.Add(kvp.Value.Kilotons() + " of " + kvp.Key + "\" " + kvp.Key.VehicleTypeName + "s");
			}
			foreach (var kvp in UnitRoleTonnage)
			{
				if (kvp.Value == null)
					items.Add("All " + kvp.Key + " Units");
				else
					items.Add(kvp.Value.Kilotons() + " of " + kvp.Key + " Units");
			}
			foreach (var kvp in UnitTypeTonnage)
			{
				if (kvp.Value == null)
					items.Add("All " + kvp.Key + "s");
				else
					items.Add(kvp.Value.Kilotons() + " of " + kvp.Key + "s");
			}
			return string.Join(", ", items.ToArray());
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			RacePopulation.ReplaceClientIDs(idmap);
			UnitDesignTonnage.ReplaceClientIDs(idmap);
		}
	}
}
