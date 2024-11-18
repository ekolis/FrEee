using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;

namespace FrEee.Vehicles.Types;

/// <summary>
/// A vehicle which can be contained in cargo.
/// </summary>
public interface IUnit : IVehicle, IContainable<ICargoContainer>
{
	/// <summary>
	/// Can this unit fire into space from planetary cargo?
	/// i.e. in stock is it a weapon platform?
	/// </summary>
	/// <remarks>
	/// Troops could do this in SE3
	/// </remarks>
	// TODO: make this an ability on hulls and/or weapons
	bool CanFireIntoSpaceFromPlanetaryCargo { get; }

	/// <summary>
	/// Can this unit invade enemy colonies and police friendly colonies?
	/// </summary>
	// TODO: make thi an ability on hulls and/or components
	bool CanInvadeAndPoliceColonies { get; }
}