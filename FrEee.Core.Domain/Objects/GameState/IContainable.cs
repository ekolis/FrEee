using FrEee.Utility;

namespace FrEee.Objects.GameState;

/// <summary>
/// Something which can be contained by another object.
/// </summary>
public interface IContainable<out TContainer>
{
    /// <summary>
    /// The container of this object.
    /// </summary>
    [DoNotCopy]
    TContainer Container { get; }
}
