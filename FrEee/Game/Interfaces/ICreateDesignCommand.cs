using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command for an empire to create a design.
	/// </summary>
	public interface ICreateDesignCommand : ICommand<Empire>
	{
		IDesign Design { get; }
	}
}