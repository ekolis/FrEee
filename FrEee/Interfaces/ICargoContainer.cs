using FrEee.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Interfaces;

/// <summary>
/// An object which can contain cargo.
/// </summary>
public interface ICargoContainer : IPictorial, INamed, ILocated
{
	/// <summary>
	/// All population stored by this cargo container, whether in a colony or in cargo.
	/// </summary>
	IDictionary<Race, long> AllPopulation { get; }

	/// <summary>
	/// All units that are in or part of this cargo container.
	/// </summary>
	IEnumerable<IUnit> AllUnits { get; }

	/// <summary>
	/// The cargo contained by this object.
	/// </summary>
	Cargo Cargo { get; }

	/// <summary>
	/// The total amount of cargo storage possessed by this object.
	/// </summary>
	int CargoStorage { get; }

	/// <summary>
	/// The amount of available population storage.
	/// </summary>
	long PopulationStorageFree { get; }

	/// <summary>
	/// Adds population.
	/// </summary>
	/// <param name="race"></param>
	/// <param name="amount"></param>
	/// <returns>The amount of population that could not be added due to overflow.</returns>
	long AddPopulation(Race race, long amount);

	/// <summary>
	/// Adds a unit.
	/// </summary>
	/// <param name="unit"></param>
	/// <returns>true if there was space left to add the unit, otherwise false.</returns>
	bool AddUnit(IUnit unit);

	/// <summary>
	/// Removes population.
	/// </summary>
	/// <param name="race"></param>
	/// <param name="amount">The amount of population that could not be removed due to lack of population.</param>
	long RemovePopulation(Race race, long amount);

	/// <summary>
	/// Removes a unit.
	/// </summary>
	/// <param name="unit"></param>
	bool RemoveUnit(IUnit unit);
}