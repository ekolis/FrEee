using FrEee.Utility;
namespace FrEee.Interfaces;

/// <summary>
/// Something which can be recycled (scrapped, etc.)
/// </summary>
public interface IRecyclable : IReferrable, IPictorial
{
	/// <summary>
	/// The object that's the "container" object of this object for recycling purposes.
	/// </summary>
	IMobileSpaceObject RecycleContainer { get; }

	/// <summary>
	/// Amount of resources gained by scrapping this object.
	/// </summary>
	ResourceQuantity ScrapValue { get; }

	/// <summary>
	/// Performs the recycling action.
	/// </summary>
	/// <param name="behavior">The action to perform.</param>
	/// <param name="didExecute">Did the action already execute? If so, just do the object type specific logic.</param>
	void Recycle(IRecycleBehavior behavior, bool didExecute = false);
}