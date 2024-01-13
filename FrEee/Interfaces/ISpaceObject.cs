using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Interfaces
{
	public interface ISpaceObject : IOwnableAbilityObject, IPictorial, IReferrable, IFoggable, INamed, ILocated
	{
		/// <summary>
		/// Can this space object be placed in a fleet?
		/// </summary>
		bool CanBeInFleet { get; }

		/// <summary>
		/// Is this space object affeced by sight obscuration abilities?
		/// This does not affect cloaking abilities, only sector/system obscuration.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance can be obscured; otherwise, <c>false</c>.
		/// </value>
		bool CanBeObscured { get; }

		/// <summary>
		/// Can this space object traverse warp points?
		/// </summary>
		bool CanWarp { get; }

		/// <summary>
		/// Does this space object have infinite supplies?
		/// TODO - make supply a resource?
		/// </summary>
		bool HasInfiniteSupplies { get; }

		/// <summary>
		/// Is this space object idle?
		/// Space objects are idle if they have no orders (but they have speed greater than zero, and are not in a fleet) or their construction queue has a fractional ETA less than 1.
		/// </summary>
		bool IsIdle { get; }

		/// <summary>
		/// The name of this space object.
		/// </summary>
		new string Name { get; set; }

		/// <summary>
		/// Resources stored on this space object.
		/// For a ship, this could include supplies.
		/// </summary>
		ResourceQuantity StoredResources { get; }

		/// <summary>
		/// Supply storage capacity.
		/// </summary>
		int SupplyStorage { get; }
	}
}