using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Vehicles;

/// <summary>
/// A vehicle which can be contained in cargo.
/// </summary>
public interface IUnit : IVehicle, IContainable<ICargoContainer>
{
}