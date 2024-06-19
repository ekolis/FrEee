using FrEee.Objects.Civilization;

namespace FrEee.Ecs.Abilities.Utility;

/// <summary>
/// Something which can have abilities and be owned.
/// </summary>
public interface IOwnableAbilityObject : IOwnable, IAbilityObject
{
}