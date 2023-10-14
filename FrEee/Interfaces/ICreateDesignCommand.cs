using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;

namespace FrEee.Interfaces
{
	/// <summary>
	/// A command for an empire to create a design.
	/// </summary>
	public interface ICreateDesignCommand<out T>
		: ICommand<Empire>
		where T : IVehicle
	{
		IDesign<T> Design { get; }
	}
}
