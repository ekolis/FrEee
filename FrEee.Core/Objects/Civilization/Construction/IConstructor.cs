using FrEee.Objects.Space;

namespace FrEee.Objects.Civilization.Construction;

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
