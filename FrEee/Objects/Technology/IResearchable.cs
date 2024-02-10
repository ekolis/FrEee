using FrEee.Objects.GameState;

namespace FrEee.Objects.Technology;

/// <summary>
/// Something that can be unlocked in-game, as opposed to only in empire setup.
/// </summary>
public interface IResearchable : IUnlockable, IFoggable
{
}