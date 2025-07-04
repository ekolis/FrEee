using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using FrEee.Objects.GameState;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.Objects.Civilization.CargoStorage;

/// <summary>
/// A change in cargo.
/// </summary>
public class CargoDelta : IPromotable
{
    public CargoDelta()
    {
        RacePopulation = new GameReferenceKeyedDictionary<Race, long?>();
        AllPopulation = false;
        AnyPopulation = 0L;
        Units = new GameReferenceSet<IUnit>();
        UnitDesignTonnage = new GameReferenceKeyedDictionary<IDesign<IUnit>, int?>();
        UnitRoleTonnage = new SafeDictionary<string, int?>();
        UnitTypeTonnage = new SafeDictionary<VehicleTypes, int?>();
    }

    /// <summary>
    /// Should we transfer as much population as possible, regardless of race?
    /// </summary>
    public bool AllPopulation { get; set; }

    /// <summary>
    /// Amount of population to transfer where the race of the population is irrelevant.
    /// </summary>
    public long AnyPopulation { get; set; }

    /// <summary>
    /// Estimated tonnage of the cargo delta.
    /// Will return null if there is any "All" quantity specified.
    /// </summary>
    public int? EstimatedTonnage
    {
        get
        {
            int? tonnage = 0;
            foreach (var kvp in RacePopulation)
            {
                if (kvp.Value == null)
                    return null;
                tonnage += (int)Math.Ceiling(kvp.Value.Value * Mod.Current.Settings.PopulationSize);
            }
            if (AllPopulation)
                return null;
            tonnage += (int)Math.Ceiling(AnyPopulation * Mod.Current.Settings.PopulationSize);
            foreach (var u in Units)
                tonnage += u.Design.Hull.Size;
            foreach (var d in UnitDesignTonnage)
                tonnage += d.Value;
            foreach (var r in UnitRoleTonnage)
                tonnage += r.Value;
            foreach (var t in UnitTypeTonnage)
                tonnage += t.Value;
            return tonnage;
        }
    }

    public GameReferenceKeyedDictionary<Race, long?> RacePopulation { get; private set; }
    public GameReferenceKeyedDictionary<IDesign<IUnit>, int?> UnitDesignTonnage { get; private set; }
    public SafeDictionary<string, int?> UnitRoleTonnage { get; private set; }
    public GameReferenceSet<IUnit> Units { get; private set; }
    public SafeDictionary<VehicleTypes, int?> UnitTypeTonnage { get; private set; }

    public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
        if (done == null)
            done = new HashSet<IPromotable>();
        if (!done.Contains(this))
        {
            done.Add(this);
            RacePopulation.ReplaceClientIDs(idmap, done);
            UnitDesignTonnage.ReplaceClientIDs(idmap, done);
            Units.ReplaceClientIDs(idmap, done);
        }
		return this;
	}

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
        if (AllPopulation)
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
}
