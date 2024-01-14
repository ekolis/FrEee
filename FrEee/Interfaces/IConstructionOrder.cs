using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Interfaces;

public interface IConstructionOrder : IOrder, INamed
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