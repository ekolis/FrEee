namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle which can be contained in cargo.
	/// </summary>
	public interface IUnit : IVehicle, IContainable<ICargoContainer>
	{
	}
}