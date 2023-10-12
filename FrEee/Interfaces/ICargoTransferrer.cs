namespace FrEee.Interfaces
{
	/// <summary>
	/// A space object which can contain cargo and receive cargo transfer orders.
	/// </summary>
	public interface ICargoTransferrer : ICargoContainer, ISpaceObject, IOrderable
	{
	}
}