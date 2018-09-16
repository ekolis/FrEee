using FrEee.Game.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
	public interface IConstructionOrder : IOrder<ConstructionQueue>, INamed
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
}