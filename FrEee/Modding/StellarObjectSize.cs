using FrEee.Enumerations;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;

namespace FrEee.Modding;

/// <summary>
/// A size of stellar objects from PlanetSize.txt.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class StellarObjectSize : IModObject, IComparable<StellarObjectSize>, IComparable
{
	/// <summary>
	/// ID value for the ability on the component used to construct this world around a star.
	/// </summary>
	public string ConstructionAbilityID { get; set; }

	/// <summary>
	/// Is this a constructed world?
	/// Constructed worlds should not appear in galaxy generation.
	/// </summary>
	public bool IsConstructed { get; set; }

	public bool IsDisposed
	{
		get; private set;
	}

	/// <summary>
	/// Maximum kT cargo for a colony.
	/// </summary>
	public int MaxCargo { get; set; }

	/// <summary>
	/// Maximum kT cargo for a domed colony.
	/// </summary>
	public int MaxCargoDomed { get; set; }

	/// <summary>
	/// Maximum facilities for a colony.
	/// </summary>
	public int MaxFacilities { get; set; }

	/// <summary>
	/// Maximum facilities for a domed colony.
	/// </summary>
	public int MaxFacilitiesDomed { get; set; }

	/// <summary>
	/// Maximum population for a colony.
	/// </summary>
	public long MaxPopulation { get; set; }

	/// <summary>
	/// Maximum population in millions for a domed colony.
	/// </summary>
	public long MaxPopulationDomed { get; set; }

	public string ModID
	{
		get;
		set;
	}

	/// <summary>
	/// The name of this size.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Should be "Planet" or "Asteroids".
	/// </summary>
	public string StellarObjectType { get; set; }

	/// <summary>
	/// Stellar size of this size.
	/// </summary>
	public StellarSize StellarSize { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public int CompareTo(object obj)
	{
		if (obj is StellarSize)
			return StellarSize.CompareTo(obj);
		else if (obj is StellarObjectSize)
			return this.CompareTo((StellarObjectSize)obj);
		return StellarSize.CompareTo(obj);
	}

	public int CompareTo(StellarObjectSize obj)
	{
		return StellarSize.CompareTo(obj.StellarSize);
	}

	public void Dispose()
	{
		Mod.Current.StellarObjectSizes.Remove(this);
		IsDisposed = true;
	}

	public override string ToString()
	{
		return Name;
	}

	public static bool operator ==(StellarObjectSize s1, StellarObjectSize s2)
	{
		if (s1 is null && s2 is null)
			return true;
		if (s1 is null || s2 is null)
			return false;
		// TODO - compare template parameters somehow?
		return s1.IsConstructed == s2.IsConstructed && s1.IsDisposed == s2.IsDisposed && s1.MaxCargo == s2.MaxCargo && s1.MaxCargoDomed == s2.MaxCargoDomed && s1.MaxFacilities == s2.MaxFacilities && s1.MaxFacilitiesDomed == s2.MaxFacilitiesDomed && s1.MaxPopulation == s2.MaxPopulation && s1.MaxPopulationDomed == s2.MaxPopulationDomed && s1.ModID == s2.ModID && s1.Name == s2.Name && s1.StellarObjectType == s2.StellarObjectType && s1.StellarSize == s2.StellarSize;
	}

	public static bool operator !=(StellarObjectSize s1, StellarObjectSize s2)
	{
		return !(s1 == s2);
	}
}