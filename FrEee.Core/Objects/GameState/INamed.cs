namespace FrEee.Objects.GameState;

/// <summary>
/// Something that has a name.
/// </summary>
public interface INamed
{
    /// <summary>
    /// The name of the object.
    /// </summary>
    string Name { get; }
}