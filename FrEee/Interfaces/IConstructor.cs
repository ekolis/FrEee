using FrEee.Objects.Civilization;

namespace FrEee.Interfaces;

/// <summary>
/// Something which has a construction queue.
/// </summary>
public interface IConstructor : ISpaceObject, IOwnable
{
	/// <summary>
	/// This object's construction queue, if any.
	/// </summary>
	ConstructionQueue ConstructionQueue { get; }
}
