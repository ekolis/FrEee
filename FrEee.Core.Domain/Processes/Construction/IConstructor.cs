using FrEee.Objects.Civilization;
using FrEee.Objects.Space;

namespace FrEee.Processes.Construction;

/// <summary>
/// Something which has a construction queue.
/// </summary>
public interface IConstructor : ISpaceObject, IOwnable
{
	/// <summary>
	/// This object's construction queue, if any.
	/// </summary>
	IConstructionQueue ConstructionQueue { get; }
}
