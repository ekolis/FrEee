using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Utility;
namespace FrEee.Processes.Construction;

// TODO: IOrder itself should implement IFoggable
public interface IConstructionOrder : IOrder, INamed, IFoggable
{
	/// <summary>
	/// The cost of the construction.
	/// </summary>
	ResourceQuantity Cost { get; }

	/// <summary>
	/// The item being constructed.
	/// </summary>
	IConstructable Item { get; }

	/// <summary>
	/// The template.
	/// </summary>
	IConstructionTemplate Template { get; }

	/// <summary>
	/// Resets this order so it can be repeated.
	/// </summary>
	void Reset();
}