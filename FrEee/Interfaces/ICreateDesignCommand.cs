using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;
using FrEee.Serialization;

namespace FrEee.Interfaces
{
	/// <summary>
	/// A command for an empire to create a design.
	/// </summary>
	public interface ICreateDesignCommand
		: ICommand<Empire>, IAfterDeserialize
	{
		IDesign Design { get; }
	}
}
