using FrEee.Objects.Civilization;

namespace FrEee.Ecs;

/// <summary>
/// Something which can have abilities and be owned.
/// </summary>
public interface IOwnableAbilityObject : IOwnable, IAbilityObject
{
}