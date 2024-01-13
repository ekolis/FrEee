using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Interfaces
{
	/// <summary>
	/// Something which can be constructed at a construction queue.
	/// </summary>
	public interface IConstructable : INamed, IPictorial, ITransferrable
	{
		/// <summary>
		/// The progress toward constructing this item.
		/// </summary>
		ResourceQuantity ConstructionProgress { get; set; }

		/// <summary>
		/// The resource cost to build this item.
		/// </summary>
		ResourceQuantity Cost { get; }

		/// <summary>
		/// The empire which owns this item.
		/// </summary>
		new Empire Owner { get; set; }

		/// <summary>
		/// The construction template used to build this item.
		/// </summary>
		IConstructionTemplate Template { get; }

		/// <summary>
		/// Places the newly constructed item at a location.
		/// </summary>
		/// <param name="target">The space object which the item should be placed on or near.</param>
		void Place(ISpaceObject target);
	}
}